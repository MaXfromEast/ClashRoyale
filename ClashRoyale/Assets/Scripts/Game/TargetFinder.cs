using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private bool unitCanAttackFlying;
    [SerializeField] private bool unitCanAttackWalking;
    [SerializeField] private bool unitCanAttackTower;
    private Tower nearestTower;
    private Unit nearestUnit;
    private float distanceToTower;
    private float distanceToUnit;
    private Unit thisUnit;
    private Transform thisUnitTransform;
    private bool targetIsEnemy;
    private void Start()
    {
        thisUnit = GetComponent<Unit>();
        thisUnitTransform = thisUnit.transform;
        targetIsEnemy = thisUnit.IsEnemy == false;
        SetTargetTower();
        SetTargetUnit();
    }

    private Tower SetTargetTower()
    {
        if (unitCanAttackTower)
        {
            nearestTower = MapInfo.Instance.GetNearestTower(thisUnitTransform.position, targetIsEnemy, out float distance);
            distanceToTower = distance;
        }
        else
        {
            nearestTower = null;
        }
        return nearestTower;
    }

    private Unit SetTargetUnit()
    {
        if (unitCanAttackWalking && unitCanAttackFlying)
        {
            nearestUnit = MapInfo.Instance.TryGetNearestAnyUnit(thisUnitTransform.position, targetIsEnemy, out float distance);
            distanceToUnit = distance;
        }
        else if (unitCanAttackWalking)
        {
            nearestUnit = MapInfo.Instance.TryGetNearestWalkingUnit(thisUnitTransform.position, targetIsEnemy, out float distance);
            distanceToUnit = distance;
        }
        else if (unitCanAttackFlying)
        {
            nearestUnit = MapInfo.Instance.TryGetNearestFlyUnit(thisUnitTransform.position, targetIsEnemy, out float distance);
            distanceToUnit = distance;
        }
        else
        {
            nearestUnit = null;
        }
        return nearestUnit;
    }

    public Unit GetTargetUnit()
    {
        return nearestUnit;
    }

    public Tower GetTargetTower()
    {
        return nearestTower;
    }

    private void Update()
    {
        SetTargetTower();
        SetTargetUnit();
    }

    public void TryChangeStateToAttack()
    {
        if (nearestUnit != null)
        {
            if (distanceToUnit - nearestUnit.UnitParameters.ModelRadius <= thisUnit.UnitParameters.StartAttackDistance)
            {
                thisUnit.SetState(UnitStateType.Attack);
                Debug.Log(gameObject.name);
            }

        }
        else if(nearestTower != null)
        {
            if (distanceToTower - nearestTower.TowerRadius <= thisUnit.UnitParameters.StartAttackDistance)
            {
                thisUnit.SetState(UnitStateType.Attack);
                Debug.Log(gameObject.name);
            }
        }
    }
    public void TryChangeStateToChase()
    {
        if (nearestUnit != null)
        {
            if ((distanceToUnit - nearestUnit.UnitParameters.ModelRadius <= thisUnit.UnitParameters.StartChaseDistance) && (distanceToUnit - nearestUnit.UnitParameters.ModelRadius > thisUnit.UnitParameters.StartAttackDistance))
            {
                thisUnit.SetState(UnitStateType.Chase);
                Debug.Log(gameObject.name);
            }
        }
    }

    public void ChangeStateToDefault()
    {
        thisUnit.SetState(UnitStateType.Default);
    }
}

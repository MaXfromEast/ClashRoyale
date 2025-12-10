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
    
    public void Init()
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
            distanceToUnit = float.MaxValue;
        }
        return nearestUnit;
    }

    public Unit GetTargetUnit(out float disToThePointOfAttack)
    {
        if (nearestUnit != null)
        {
            disToThePointOfAttack = this.distanceToUnit - nearestUnit.UnitParameters.ModelRadius;
        }
        else
        {
            disToThePointOfAttack = this.distanceToUnit;
        }
        return nearestUnit;
    }

    public Tower GetTargetTower(out float disToThePointOfAttack)
    {
        disToThePointOfAttack = this.distanceToTower - nearestTower.TowerRadius;
        return nearestTower;
    }

    private void Update()
    {
        SetTargetTower();
        SetTargetUnit();
    }

    public void TryChangeStateToPrepare()
    {
        if (nearestUnit != null)
        {
            if (distanceToUnit - nearestUnit.UnitParameters.ModelRadius <= thisUnit.UnitParameters.StartAttackDistance)
            {
                thisUnit.SetState(UnitStateType.Prepare);
                Debug.Log(gameObject.name);
            }
           
        }
        if(nearestTower != null)
        {
            if (distanceToTower - nearestTower.TowerRadius <= thisUnit.UnitParameters.StartAttackDistance)
            {
                thisUnit.SetState(UnitStateType.Prepare);
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

    public void ChangeStateToAttack()
    {
        thisUnit.SetState(UnitStateType.Attack);
    }
}

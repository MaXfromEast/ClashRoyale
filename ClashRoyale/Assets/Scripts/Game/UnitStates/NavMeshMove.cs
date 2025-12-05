using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NavMeshMove", menuName = "UnitState/NavMeshMover")]
public class NavMeshMove : UnitState
{   
    private NavMeshAgent agent;
    
    private TargetFinder targetFinder;
    private Unit targetUnit;
    private Tower targetTower;

    public override void Constructor(Unit _unit)
    {
        base.Constructor(_unit);
        
        agent = this.unit.GetComponent<NavMeshAgent>();
        targetFinder = this.unit.GetComponent<TargetFinder>();
        if (agent == null) Debug.LogError($"На персонаже {_unit.name} нет компонента NavMeshAgent");
        //agent.stoppingDistance = unit.UnitParameters.StartAttackDistance;
        //agent.speed = unit.UnitParameters.Speed;
        //agent.radius = unit.UnitParameters.ModelRadius;
    }

    public override void Init()
    {
        targetTower = targetFinder.GetTargetTower();
        targetUnit = targetFinder.GetTargetUnit();
        agent.SetDestination(targetTower.transform.position);
    }

    public override void Run()
    {
       targetFinder.TryChangeStateToChase();
       targetFinder.TryChangeStateToAttack();
    }

    //public bool TryAttackTower()
    //{
    //    float distanceToTarget = nearestTower.GetDistance(unit.transform.position);
    //    if (distanceToTarget <= unit.UnitParameters.StartAttackDistance)
    //    {
    //        unit.SetState(UnitStateType.Attack);
    //        return true;
    //    }
    //    return false;
    //}

    //public bool TryAttackUnit()
    //{
    //    bool hasEnemy = MapInfo.Instance.TryGetNearestAnyUnit(unit.transform.position, targetIsEnemy, out Unit enemy, out float distance);
    //    if ((hasEnemy)&&(distance <= unit.UnitParameters.StartChaseDistance))
    //    {
    //        unit.SetState(UnitStateType.Chase);
    //        return true;
    //    }
    //    return false;
    //}

    public override void Finish()
    {
        agent.velocity = Vector3.zero;
        agent.ResetPath();
    }
}

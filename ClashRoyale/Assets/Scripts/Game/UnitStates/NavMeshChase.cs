using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NavMeshChase", menuName = "UnitState/NavMeshChase")]
public class NavMeshChase : UnitState
{
    private NavMeshAgent agent;
    private bool targetIsEnemy;
    private Unit targetUnit;
    private TargetFinder targetFinder;

    public override void Constructor(Unit _unit)
    {
        base.Constructor(_unit);
        unit = _unit;
        targetFinder = this.unit.GetComponent<TargetFinder>();
        agent = this.unit.GetComponent<NavMeshAgent>();
        if (agent == null) Debug.LogError($"На персонаже {_unit.name} нет компонента NavMeshAgent");
    }
    public override void Init()
    {
        targetUnit = targetFinder.GetTargetUnit();
    }

    public override void Run()
    {
        if (targetUnit == null)
        {
            targetFinder.ChangeStateToDefault();
        }
        else
        {
            agent.SetDestination(targetUnit.transform.position);
            targetFinder.TryChangeStateToAttack();
        }
        //{
        //    float distanceToTarget = Vector3.Distance(unit.transform.position, targetUnit.transform.position);
        //    if(distanceToTarget > unit.UnitParameters.StopChaseDistance)
        //    {
        //        unit.SetState(UnitStateType.Default);
        //    }
        //    else if(distanceToTarget <= unit.UnitParameters.StartAttackDistance + targetUnit.UnitParameters.ModelRadius)
        //    {
        //        unit.SetState(UnitStateType.Attack);
        //    }
        //    else
        //    {
        //        agent.SetDestination(targetUnit.transform.position);
        //    }
        //}

    }

    public override void Finish()
    {
        agent.velocity = Vector3.zero;
        agent.ResetPath();
    }

#if UNITY_EDITOR
    public override void DebugDrawDistance(Unit unit)
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.UnitParameters.StartChaseDistance);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.UnitParameters.StopChaseDistance);
    }
#endif
}

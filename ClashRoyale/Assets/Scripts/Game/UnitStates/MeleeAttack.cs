using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_MeleeAttack", menuName = "UnitState/MeleeAttack")]
public class MeleeAttack : UnitState
{
    [SerializeField] private int damage = 1;
    private float delayBetweenAttacks;
    private Health target;
    private float timer;
    private NavMeshAgent agent;
    private TargetFinder targetFinder;
    private Unit targetUnit;
    private Tower targetTower;
    private float distanceToTower;
    private float distanceToUnit;


    public override void Constructor(Unit _unit)
    {
        base.Constructor(_unit);
        agent = this.unit.GetComponent<NavMeshAgent>();
        delayBetweenAttacks = unit.UnitParameters.DelayBetweenAttacks;
        targetFinder = this.unit.GetComponent<TargetFinder>();
       
    }

   

    public override void Init()
    {
        targetUnit = null;
        targetTower = null;
        targetUnit = targetFinder.GetTargetUnit(out float distanceToUnit);
        this.distanceToUnit = distanceToUnit;
        targetTower = targetFinder.GetTargetTower(out float distanceToTower);
        this.distanceToTower = distanceToTower;
        TryFindTarget();
    }

    public override void Run()
    { 
        timer += Time.deltaTime;
        if (timer < delayBetweenAttacks) return;
        timer = timer - delayBetweenAttacks;
        if (target != null)
        {
            target.ApplyDamage(damage);
            TryFindTarget();
        }
        else
        {
            targetFinder.ChangeStateToDefault();
        }
        
    } 
    
    public override void Finish()
    {
        agent.ResetPath();
    }

    private void TryFindTarget()
    {   
        if ((targetUnit != null) || (targetTower != null))
        {
            if (distanceToUnit <= unit.UnitParameters.StartAttackDistance)
            {
                target = targetUnit.Health;
                unit.transform.LookAt(target.transform.position);
            }
            else if (distanceToTower <= unit.UnitParameters.StartAttackDistance)
            {
                target = targetTower.Health;
                unit.transform.LookAt(target.transform.position);
            }
            else
            {
                target = null;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_UsualAttack", menuName = "UnitState/UsualAttack")]
public class UsualAttack : UnitState
{
    [SerializeField] private int damage = 1;
    private float delayBetweenAttacks;
    private Health target;
    private float timer;
    private NavMeshAgent agent;
    private TargetFinder targetFinder;
    private Unit targetUnit;
    private Tower targetTower;

    public override void Constructor(Unit _unit)
    {
        base.Constructor(_unit);
        agent = this.unit.GetComponent<NavMeshAgent>();
        delayBetweenAttacks = unit.UnitParameters.DelayBetweenAttacks;
        targetFinder = this.unit.GetComponent<TargetFinder>();
        timer = delayBetweenAttacks - unit.UnitParameters.DelayFirstHit;
        Debug.Log(unit.gameObject.name + timer);
    }

   

    public override void Init()
    {
        targetUnit = null;
        targetTower = null;
        targetUnit = targetFinder.GetTargetUnit();
        targetTower = targetFinder.GetTargetTower();
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
        
        if (targetUnit != null)
        {
            targetFinder.TryChangeStateToChase();
            target = targetUnit.Health;
            unit.transform.LookAt(target.transform.position);    
        }
        else if (targetTower != null)
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

using UnityEngine;
using UnityEngine.AI;
using KevinIglesias;

[CreateAssetMenu(fileName = "_DistaceAttack", menuName = "UnitState/DistaceAttack")]
public class DistaceAttack : UnitState
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float projectileSpeed = 10;
    [SerializeField] private GameObject arrowToShoot;
    private Transform bowstringAnchorPoint;
    private float delayBetweenAttacks;
    private Health target;
    private float timer;
    private NavMeshAgent agent;
    private TargetFinder targetFinder;
    private Unit targetUnit;
    private Tower targetTower;
    private HumanArcherController humanArcherController;
    private Transform thisUnitTransform;
    private float distanceToUnit;
    private float distanceToTower;
    private ListOfClips listOfClips;

    public override void Constructor(Unit _unit)
    {
        base.Constructor(_unit);
        agent = this.unit.GetComponent<NavMeshAgent>();
        thisUnitTransform = unit.transform;
        delayBetweenAttacks = unit.UnitParameters.DelayBetweenAttacks;
        targetFinder = this.unit.GetComponent<TargetFinder>();
        humanArcherController = unit.transform.GetComponentInChildren<HumanArcherController>();
        if(humanArcherController != null)
        {
            bowstringAnchorPoint = humanArcherController.bowstringAnchorPoint;
        }
        listOfClips = this.unit.GetComponentInChildren<ListOfClips>();
        listOfClips.Init();
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
        if (timer > delayBetweenAttacks)
        {
            timer = timer - delayBetweenAttacks;
            if (target != null)
            {
               
                if(humanArcherController != null)
                {
                   
                    MakeProjectile();
                }
                TryFindTarget();
            }
            else
            {
                targetFinder.ChangeStateToDefault();
            }
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

    private void MakeProjectile()
    {
        GameObject ArrowClone = Instantiate(arrowToShoot, bowstringAnchorPoint.position, bowstringAnchorPoint.rotation);
        if (ArrowClone.GetComponent<HumanArcherArrow>() != null)
        {
            HumanArcherArrow humanArcherArrow = ArrowClone.GetComponent<HumanArcherArrow>();
            humanArcherArrow.ArrowSpeed = projectileSpeed;
            humanArcherArrow.HealthTarget = target;
            humanArcherArrow.DMG = damage;
        }
    }

}

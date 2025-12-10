using UnityEngine;

[CreateAssetMenu(fileName = "_PrepareAttack", menuName = "UnitState/PrepareAttack")]
public class PrepareAttack : UnitState
{
    private float prepareTime;
    private TargetFinder targetFinder;
    private float timer;

    public override void Constructor(Unit _unit)
    {
        base.Constructor(_unit);
        unit = _unit;
        targetFinder = this.unit.GetComponent<TargetFinder>();
    }


    public override void Init()
    {
        prepareTime = unit.UnitParameters.TimeForPrepare;
        timer = 0;
    }

    public override void Run()
    {
        timer += Time.deltaTime;
        if (timer < prepareTime) return;
        SwichState();
    }

    public override void Finish()
    {
        timer = 0;
    }

    private  void SwichState()
    {
        targetFinder.ChangeStateToAttack();
    }
}

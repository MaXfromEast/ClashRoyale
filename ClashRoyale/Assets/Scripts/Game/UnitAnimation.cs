using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    private const string State = "State";
    private const string AttackSpeed = "AttackSpeed";
    [SerializeField] private Animator animator;

    public void Init(Unit unit)
    {
        float attackDelay = unit.UnitParameters.DelayBetweenAttacks;
        animator.SetFloat(AttackSpeed, 1 / attackDelay);
    }
    public void SetState(UnitStateType type)
    {
        animator.SetInteger(State, (int)type);
        Debug.Log("Поменял статус на" + type);
    }
}

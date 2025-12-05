using System;
using UnityEngine;

[RequireComponent(typeof(UnitParameters), typeof(Health))]
public class Unit : MonoBehaviour, IHealth, IDestroyed
{
    [SerializeField] private UnitParameters unitParameters;
    public UnitParameters UnitParameters
    {
        get
        {
            return unitParameters;
        }
    }
    
    [SerializeField] private bool isEnemy;
    public bool IsEnemy
    {
        get
        {
            return isEnemy;
        }
    }

    [SerializeField] private Health health;
    public Health Health
    {
        get
        {
            return health;
        }
    }

    [SerializeField] private UnitState defaultStateSO;
    [SerializeField] private UnitState chaseStateSO;
    [SerializeField] private UnitState attackStateSO;

    private UnitState defaultState;
    private UnitState chaseState;
    private UnitState attackState;

    private UnitState currentState;

    private void Start()
    {
        CreateStates();

        currentState = defaultState;
        currentState.Init();

        health.UpdateHealth += CheckDestroy;
    }

    private void CreateStates()
    {
        defaultState = Instantiate(defaultStateSO);
        defaultState.Constructor(this);

        chaseState = Instantiate(chaseStateSO);
        chaseState.Constructor(this);

        attackState = Instantiate(attackStateSO);
        attackState.Constructor(this);
    }

    private void Update()
    {
       currentState.Run();
    }

    public void SetState(UnitStateType type)
    {
        currentState.Finish();
        switch (type)
        {
            case UnitStateType.Default:
                currentState = defaultState;
                Debug.Log("Состояние Default");
                break;
            case UnitStateType.Chase:
                currentState = chaseState;
                Debug.Log("Состояние Case");
                break;
            case UnitStateType.Attack:
                currentState = attackState;
                Debug.Log("Состояние Attack");
                break;
            default:
                Debug.Log("Нет такого состояния" + type);
                break;

        }
        currentState.Init();
    }

    public event Action destroyed;

    private void CheckDestroy(int currentHP)
    {
        if (currentHP > 0) return;
        health.UpdateHealth -= CheckDestroy;
        Destroy(gameObject);
        destroyed?.Invoke();
    }

#if UNITY_EDITOR
    [SerializeField] private bool isDebug;
    private void OnDrawGizmos()
    {
        if (isDebug)
        {
            if (chaseStateSO != null)
            {
                chaseStateSO.DebugDrawDistance(this);
            }
        }
    }
#endif
}

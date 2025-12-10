using System;
using UnityEngine;

[RequireComponent(typeof(UnitParameters), typeof(Health), typeof(UnitAnimation))]
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

    [SerializeField] private UnitAnimation unitAnimation;

    [SerializeField] private UnitState defaultStateSO;
    [SerializeField] private UnitState chaseStateSO;
    [SerializeField] private UnitState prepareStateSO;
    [SerializeField] private UnitState attackStateSO;
    private TargetFinder targetFinder;
    private UnitState defaultState;
    private UnitState chaseState;
    private UnitState prepareState;
    private UnitState attackState;

    private UnitState currentState;

    private void Start()
    {
        targetFinder = GetComponent<TargetFinder>();
        targetFinder.Init();
        CreateStates();
        unitAnimation.Init(this);
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

        prepareState = Instantiate(prepareStateSO);
        prepareState.Constructor(this);

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
                Debug.Log("Состояние Chase");
                break;
            case UnitStateType.Prepare:
                currentState = prepareState;
                Debug.Log("Состояние Prepare");
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
        unitAnimation.SetState(type);
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

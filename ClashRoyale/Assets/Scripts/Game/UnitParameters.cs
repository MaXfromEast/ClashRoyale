using UnityEngine;

public class UnitParameters : MonoBehaviour
{
    [SerializeField] private float startAttackDistance = 1f;
    public float StartAttackDistance
    {
        get
        {
            return startAttackDistance + modelRadius;
        }
    }

    [SerializeField] private float startChaseDistance = 5f;
    public float StartChaseDistance
    {
        get
        {
            return startChaseDistance;
        }
    }

    [SerializeField] private float stopChaseDistance = 7f;
    public float StopChaseDistance
    {
        get
        {
            return stopChaseDistance;
        }
    }

    [SerializeField] private float speed = 4f;
    public float Speed
    {
        get
        {
            return speed;
        }
    }

    [SerializeField] private float modelRadius = 0.5f;
    public float ModelRadius
    {
        get
        {
            return modelRadius;
        }
    }

    [SerializeField] private bool isFly = false;
    public bool IsFly
    {
        get
        {
            return isFly;
        }
    }

    [SerializeField] private float delayBetweenAttacks = 1;
    public float DelayBetweenAttacks
    {
        get
        {
            return delayBetweenAttacks;
        }
    }

    [SerializeField] private float maxTimeForPrepare = 0.5f;
    public float TimeForPrepare
    {
        get
        {
            return Random.Range(0, maxTimeForPrepare);
        }
    }
}

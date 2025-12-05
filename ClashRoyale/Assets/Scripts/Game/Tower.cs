using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Tower : MonoBehaviour, IHealth, IDestroyed
{
    [SerializeField] private float towerRadius = 2f;
    public float TowerRadius
    {
        get
        {
            return towerRadius;
        }
    }
    [SerializeField] private Health health;

    public event Action destroyed;

    public Health Health
    {
        get
        {
            return health;
        }
    }

    private void Start()
    {
        health.UpdateHealth += CheckDestroy;
    }

    public float GetDistance(Vector3 point)
    {
        return Vector3.Distance(transform.position, point) - towerRadius;
    }

    private void CheckDestroy(int currentHP)
    {
        if (currentHP > 0) return;
        health.UpdateHealth -= CheckDestroy;
        Destroy(gameObject);
        destroyed?.Invoke();
    }
}

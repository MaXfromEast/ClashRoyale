using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int> UpdateHealth;
    [SerializeField] private int max;
    public int Max
    {
        get
        {
            return max;
        }
    }

    private int current;

    private void Start()
    {
        current = Max;
    }

    public void ApplyDamage(int value)
    {
        current = current - value;
        if(current < 0)
        {
            current = 0;
        }
        Debug.Log($"Объект {name} получил damage, осталось HP: " + current);
        UpdateHealth?.Invoke(current);
    }
}
interface IHealth
{
    Health Health { get; }
}
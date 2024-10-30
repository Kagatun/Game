using System;
using UnityEngine;

public class Health
{
    private int _defaultHealth = 1;

    public event Action Hited;
    public event Action Deceased;

    public int Value { get; private set; }
    public int MaxValue { get; private set; }

    public Health()
    {
        MaxValue = _defaultHealth;
        Value = MaxValue;
    }

    public virtual void TakeDamage(int damage)
    {
        Value -= damage;
        Value = Mathf.Clamp(Value, 0, MaxValue);
        Hited?.Invoke();

        if (Value <= 0)
            Deceased?.Invoke();
    }

    public virtual void Heal(int healingPoints)
    {
        Value += healingPoints;
        Value = Mathf.Clamp(Value, 0, MaxValue);
    }

    public void SetLittleHealth()
    {
        MaxValue = 1;
        Value = MaxValue;
    }

    public void SetMediumHealth()
    {
        MaxValue = 2;
        Value = MaxValue;
    }

    public void SetBigHealth()
    {
        MaxValue = 3;
        Value = MaxValue;
    }

    public void SetBossHealth()
    {
        MaxValue = 50;
        Value = MaxValue;
    }

    public int RestartHealth(int startValue) =>
       Value = startValue;
}


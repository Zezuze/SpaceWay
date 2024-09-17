using System;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public static event Action OnDied;
    public static event Action OnHealthDecreased;
    public static event Action OnHealthImproved;

    private int _health = 3;
    private int _maxHealth = 3;

    private void OnEnable()
    {
        ShipCollisionHandler.OnHealthDecreasedByCollision += TakeDamage;
        ShipCollisionHandler.OnHealthIncreasedByCollision += TakeHealth;
    }

    private void OnDisable()
    {
        ShipCollisionHandler.OnHealthDecreasedByCollision -= TakeDamage;
        ShipCollisionHandler.OnHealthIncreasedByCollision -= TakeHealth;
    }

    private void TakeDamage()
    {
        if (_health <= 1)
        {
            _health--;
            Die();
        }
        else
        {
            _health--;
            OnHealthDecreased?.Invoke();
        }
    }

    private void TakeHealth()
    {
        if (_health == _maxHealth) return;

        _health++;
        OnHealthImproved?.Invoke();
    }

    private void Die()
    {
        OnDied?.Invoke();
    }
}

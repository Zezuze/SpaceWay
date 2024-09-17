using System;
using UnityEngine;

public class AsteroidHealth : EnemyHealth
{
    public static event Action<Asteroid> OnAsteroidDied;

    [SerializeField] private EnemyCollisionHandler _enemyCollisionHandler;
    private Asteroid _asteroid;

    private void OnEnable()
    {
        _enemyCollisionHandler.OnDamageTaken += TakeDamage;
    }

    private void OnDisable()
    {
        _enemyCollisionHandler.OnDamageTaken -= TakeDamage;
    }

    protected override void GetMainComponent()
    {
        _asteroid = GetComponent<Asteroid>();
    }


    protected override void TakeDamage()
    {
        _healthCount--;
        Die();
    }

    protected override void Die()
    {
        OnAsteroidDied?.Invoke(_asteroid);
        _healthCount++;
    }
}

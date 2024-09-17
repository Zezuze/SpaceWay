using System;
using UnityEngine;

public class EnemyShipHealth : EnemyHealth
{
    public static event Action<EnemyShip> OnEnemyShipDied;

    [SerializeField] private EnemyCollisionHandler _enemyCollisionHandler;
    private EnemyShip _enemyShip;

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
        _enemyShip = GetComponent<EnemyShip>();
    }


    protected override void TakeDamage()
    {
        _healthCount--;
        Die();
    }

    protected override void Die()
    {
        OnEnemyShipDied?.Invoke(_enemyShip);
        _healthCount++;
    }
}

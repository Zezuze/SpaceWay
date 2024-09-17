using UnityEngine;

public abstract class EnemyHealth : MonoBehaviour
{
    [SerializeField] protected int _healthCount;

    public void SetHealthFromConfig(EnemyConfigSO enemyConfigSO)
    {
        _healthCount = enemyConfigSO.Health;
        GetMainComponent();
    }

    protected abstract void GetMainComponent();
    protected abstract void TakeDamage();
    protected abstract void Die();
}

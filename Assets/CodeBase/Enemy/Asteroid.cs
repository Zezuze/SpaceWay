using UnityEngine;

public abstract class Asteroid : MonoBehaviour
{
    [SerializeField] private EnemyConfigSO _asteroidConfigSO;
    protected EnemyHealth _enemyHealth;
    protected Rigidbody2D _rigidbody2D;

    protected EnemyConfigSO AsteroidConfig => _asteroidConfigSO;

    protected IMovable _movable;

    public void Init()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyHealth.SetHealthFromConfig(AsteroidConfig);

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
        InitBehaviors();
    }

    protected abstract void InitBehaviors();

    private void FixedUpdate() => Move();
    private void Move() => _movable.Move();
}

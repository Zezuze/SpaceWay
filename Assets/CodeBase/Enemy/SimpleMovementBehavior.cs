using UnityEngine;

public class SimpleMovementBehavior : IMovable
{
    private Rigidbody2D _rigidbody2D;
    private float _speed;

    public SimpleMovementBehavior(Rigidbody2D rigidbody2D, EnemyConfigSO shipConfigSO)
    {
        _rigidbody2D = rigidbody2D;
        _speed = shipConfigSO.Speed;
    }

    public void Move()
    {
        _rigidbody2D.velocity = Vector2.down * _speed;
    }
}

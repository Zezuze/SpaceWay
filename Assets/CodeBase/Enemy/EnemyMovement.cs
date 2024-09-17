using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private IMovable _movable;

    public void Init(IMovable movable)
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        // _transform = GetComponent<Transform>();
        _rigidbody2D.gravityScale = 0;
        _movable = movable;
    }

    public void ChangeMoving(IMovable movable) => _movable = movable;

    private void FixedUpdate() => Move();

    public void Move() => _movable.Move();
}

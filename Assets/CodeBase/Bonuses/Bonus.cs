using UnityEngine;

public class Bonus : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float _speed = 5f;

    public void Init()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
    }

    private void FixedUpdate() => Move();
    private void Move() => _rigidbody2D.velocity = Vector2.down * _speed;
}

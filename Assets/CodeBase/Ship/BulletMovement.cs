using UnityEngine;

public abstract class BulletMovement : MonoBehaviour
{
    [SerializeField] protected BulletConfigSO _bulletConfigSO;
    protected Rigidbody2D _rigidbody2D;
    protected float _bulletSpeed;

    public virtual void Init()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
    }

    private void FixedUpdate() =>
        MoveBullet();

    protected abstract void MoveBullet();
}
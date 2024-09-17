using UnityEngine;

public class EnemyBullet : BulletMovement
{
    public override void Init()
    {
        base.Init();
        _bulletSpeed = _bulletConfigSO.BulletSpeed;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    protected override void MoveBullet()
    {
        _rigidbody2D.velocity = Vector2.down * _bulletSpeed;
    }
}
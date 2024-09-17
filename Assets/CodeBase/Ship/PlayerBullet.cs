using UnityEngine;

public class PlayerBullet : BulletMovement
{
    public override void Init()
    {
        base.Init();
        _bulletSpeed = _bulletConfigSO.BulletSpeed;
    }

    protected override void MoveBullet()
    {
        _rigidbody2D.velocity = Vector2.up * _bulletSpeed;
    }
}

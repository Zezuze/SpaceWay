using System;
using UnityEngine;

public class OutOfViewCheckerUp : OutOfViewChecker
{
    public static event Action<BulletMovement> OnPlayerBulletOut;

    public override void Init()
    {
        _boxY = 5;
        base.Init();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<BulletMovement>(out BulletMovement bullet))
        {
            OnPlayerBulletOut?.Invoke(bullet);
        }
    }
}

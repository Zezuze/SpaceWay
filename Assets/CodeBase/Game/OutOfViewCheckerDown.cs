using System;
using UnityEngine;

public class OutOfViewCheckerDown : OutOfViewChecker
{
    public static event Action<Asteroid> OnAsteroidOut;
    public static event Action<EnemyShip> OnShipOut;
    public static event Action<BulletMovement> OnBulletOut;
    public static event Action<Bonus> OnHealthOut;
    public static event Action<Bonus> OnShieldOut;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Asteroid>(out Asteroid asteroid))
        {
            OnAsteroidOut?.Invoke(asteroid);
        }
        else if (other.gameObject.TryGetComponent<EnemyShip>(out EnemyShip ship))
        {
            OnShipOut?.Invoke(ship);
        }
        else if (other.gameObject.TryGetComponent<BulletMovement>(out BulletMovement bullet))
        {
            OnBulletOut?.Invoke(bullet);
        }
        else if (other.gameObject.TryGetComponent<HealthBonus>(out HealthBonus health))
        {
            OnHealthOut?.Invoke(other.gameObject.GetComponent<Bonus>());
        }
        else if (other.gameObject.TryGetComponent<ShieldBonus>(out ShieldBonus shield))
        {
            OnShieldOut?.Invoke(other.gameObject.GetComponent<Bonus>());
        }
    }
}

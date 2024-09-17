using System;
using UnityEngine;

public class BulletCollisonHandler : MonoBehaviour
{
    public static event Action<BulletMovement> OnPlayerBulletCollided;
    [SerializeField] BulletMovement _bulletMovement;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(StringConstants.ENEMY))
        {
            OnPlayerBulletCollided?.Invoke(_bulletMovement);
        }
    }
}
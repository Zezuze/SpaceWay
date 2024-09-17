using System;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    public event Action OnDamageTaken;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(StringConstants.PLAYER_TAG) || other.gameObject.CompareTag(StringConstants.PLAYER_WEAPON_TAG))
            OnDamageTaken?.Invoke();
    }
}

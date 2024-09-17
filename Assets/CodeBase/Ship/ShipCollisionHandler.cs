using System;
using System.Collections;
using UnityEngine;

public class ShipCollisionHandler : MonoBehaviour
{
    public static event Action OnHealthDecreasedByCollision;
    public static event Action OnHealthIncreasedByCollision;

    public static event Action<Bonus> OnHealthTaken;
    public static event Action<Bonus> OnShieldTaken;

    private enum PlayerState
    {
        Normal,
        Shielded,
        Invulnerable
    }

    private PlayerState _currentState = PlayerState.Normal;
    private float _timeShieldActive = 3f;
    private float _delayTakeDamage = 1f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(StringConstants.SHIELD))
        {
            StartCoroutine(ActivateShield());
            OnShieldTaken?.Invoke(other.gameObject.GetComponent<Bonus>());
        }
        else if (other.gameObject.CompareTag(StringConstants.ENEMY) && _currentState == PlayerState.Normal)
            StartCoroutine(HandleDamage());
        else if (other.gameObject.CompareTag(StringConstants.HEALTH))
        {
            OnHealthIncreasedByCollision?.Invoke();
            OnHealthTaken?.Invoke(other.gameObject.GetComponent<Bonus>());
        }

    }

    private IEnumerator ActivateShield()
    {
        _currentState = PlayerState.Shielded;

        yield return new WaitForSeconds(_timeShieldActive);
        DisableShield();
    }

    private IEnumerator HandleDamage()
    {
        _currentState = PlayerState.Invulnerable;
        OnHealthDecreasedByCollision?.Invoke();

        yield return new WaitForSeconds(_delayTakeDamage);
        if (_currentState != PlayerState.Shielded)
            _currentState = PlayerState.Normal;
    }

    private void DisableShield() =>
        _currentState = PlayerState.Normal;
}

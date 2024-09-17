using System;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public static event Action<Transform> OnPositionChanged;

    private SpriteRenderer _spriteRenderer;
    private Transform _shipTransform;

    private float _horizontal;
    private float _vertical;
    
    private float _speed = 5;

    public void Init()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _shipTransform = GetComponent<Transform>();
    }

    private void Update() => Move();

    public void Move()
    {
        _horizontal = Input.GetAxis(StringConstants.HORIZONTAL);
        _vertical = Input.GetAxis(StringConstants.VERTICAL);

        Vector2 direction = new Vector2(_horizontal, _vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            transform.Translate(direction * _speed * Time.deltaTime, Space.World);
            OnPositionChanged?.Invoke(_shipTransform);
        }

        RotateShip();
    }

    private void RotateShip()
    {
        if (_horizontal > 0)
        {
            _spriteRenderer.flipX = true;
            ChangeShipRotation(-30, -5);
        }
        else if (_horizontal < 0)
        {
            _spriteRenderer.flipX = false;
            ChangeShipRotation(30, 5);
        }
        else ChangeShipRotation(0, 0);
    }

    private void ChangeShipRotation(float y, float z)
    {
        Vector3 currentRotation = _shipTransform.eulerAngles;
        float newYRotation = y;
        float newZRotation = z;
        Quaternion newRotation = Quaternion.Euler(currentRotation.x, newYRotation, newZRotation);
        _shipTransform.rotation = newRotation;
    }
}

using UnityEngine;

public class MapBoundaries : MonoBehaviour
{
    private Camera _mainCamera;
    private float _shipRadius = 1f;

    public void Init() => _mainCamera = Camera.main;
    
    private void OnEnable() => ShipMovement.OnPositionChanged += SetBoundariesForShip;
    private void OnDisable() => ShipMovement.OnPositionChanged -= SetBoundariesForShip;
    private void SetBoundariesForShip(Transform shipTransform) => shipTransform.position = SetPositionInsideMapBoundaries(shipTransform.position);

    private Vector3 SetPositionInsideMapBoundaries(Vector3 position)
    {
        float camHeight = _mainCamera.orthographicSize * 2;
        float camWidth = camHeight * _mainCamera.aspect;

        float minX = _mainCamera.transform.position.x - (camWidth / 2) + _shipRadius;
        float maxX = _mainCamera.transform.position.x + (camWidth / 2) - _shipRadius;
        float minY = _mainCamera.transform.position.y - (camHeight / 2) + _shipRadius;
        float maxY = _mainCamera.transform.position.y + (camHeight / 2) - _shipRadius;

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        return position;
    }
}

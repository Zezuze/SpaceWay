using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositions : MonoBehaviour, IService
{
    private Camera _mainCamera;
    private float _sphereDistance = 1f;
    private float _sphereRadius = 0.5f;

    private List<Vector3> _spawnPositions = new List<Vector3>();
    public IReadOnlyList<Vector3> SpawnPosition => _spawnPositions;


    public void Init()
    {
        SetPositions();
    }

    private void OnDrawGizmos() => DrawPositions();

    public void RemoveElementFromListByIndex(int index)
    {
        Vector3 position = _spawnPositions[index];
        _spawnPositions.RemoveAt(index);
        StartCoroutine(ReturnElementInList(position));
    }

    private IEnumerator ReturnElementInList(Vector3 position)
    {
        yield return new WaitForSeconds(1.5f);
        _spawnPositions.Add(position);
    }

    private void SetPositions()
    {
        _mainCamera = Camera.main;

        if (_mainCamera == null) return;

        float camHeight = _mainCamera.orthographicSize * 2;
        float camWidth = camHeight * _mainCamera.aspect;

        float maxY = _mainCamera.transform.position.y + (camHeight / 2);
        float minX = _mainCamera.transform.position.x - (camWidth / 2);
        float maxX = _mainCamera.transform.position.x + (camWidth / 2);

        float value = maxX + Mathf.Abs(minX);
        float spacing = (value - Mathf.Floor(value)) / (Mathf.Floor(value) - 2);

        Gizmos.color = Color.red;

        int length = (int)Mathf.Ceil(value - 2);
        for (int i = 1; i < length + 1; i++)
        {
            float positionX = spacing / 2 + spacing * (i - 1) + minX + i;
            Vector3 centerTop = new Vector3(positionX, maxY + _sphereDistance, 1f);
            _spawnPositions.Add(centerTop);
        }
    }

    private void DrawPositions()
    {
        _mainCamera = Camera.main;

        if (_mainCamera == null) return;

        float camHeight = _mainCamera.orthographicSize * 2;
        float camWidth = camHeight * _mainCamera.aspect;

        float maxY = _mainCamera.transform.position.y + (camHeight / 2);
        float minX = _mainCamera.transform.position.x - (camWidth / 2);
        float maxX = _mainCamera.transform.position.x + (camWidth / 2);

        float value = maxX + Mathf.Abs(minX);  // 13.3
        float spacing = (value - Mathf.Floor(value)) / (Mathf.Floor(value) - 2);

        DrawRectangle(value, minX, maxY);

        Gizmos.color = Color.red;

        int length = (int)Mathf.Ceil(value - 2);
        for (int i = 1; i < length + 1; i++)
        {
            float positionX = spacing / 2 + spacing * (i - 1) + minX + i;
            Vector3 centerTop = new Vector3(positionX, maxY + _sphereDistance, 0);

            Gizmos.DrawWireSphere(centerTop, _sphereRadius);
        }
    }

    private void DrawRectangle(float value, float minX, float maxY)
    {
        float rectHeight = 2f;
        float rectWidth = value;

        Vector3 topLeft = new Vector3(minX, maxY + _sphereDistance + 1, 0);
        Vector3 topRight = new Vector3(minX + rectWidth, maxY + _sphereDistance + 1, 0);
        Vector3 bottomLeft = new Vector3(minX, maxY + _sphereDistance - rectHeight + 1, 0);
        Vector3 bottomRight = new Vector3(minX + rectWidth, maxY + _sphereDistance - rectHeight + 1, 0);

        Gizmos.color = Color.blue;

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
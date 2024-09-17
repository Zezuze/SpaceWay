using System;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    public static event Action OnDistanceCovered;

    private Image _image;

    private bool _canSendMessage = true;

    private float _scrollSpeed = 0.5f;
    private int _count;
    private int _countToSpawn;

    private void Update() => ScrollBackground();

    public void Init()
    {
        _image = GetComponent<Image>();
        _image.material.mainTextureOffset = new Vector2();
        _countToSpawn = UnityEngine.Random.Range(2, 6);
    }

    private void OnEnable()
    {
        ShieldBonusSpawner.OnShieldsGone += DisableCanSendMessage;
    }

    private void OnDisable()
    {
        ShieldBonusSpawner.OnShieldsGone -= DisableCanSendMessage;
    }

    private void DisableCanSendMessage() => _canSendMessage = false;

    private void ScrollBackground()
    {
        Vector2 offset = _image.material.mainTextureOffset;
        offset += new Vector2(0, _scrollSpeed * Time.deltaTime);

        if (offset.y >= 1f)
        {
            offset.y = 0f;

            if (_canSendMessage) SendMessageOnDistanceCovered();
        }

        _image.material.mainTextureOffset = offset;
    }

    private void SendMessageOnDistanceCovered()
    {
        _count++;

        if (_count == _countToSpawn)
        {
            _count = 0;
            _countToSpawn = UnityEngine.Random.Range(2, 6);
            OnDistanceCovered?.Invoke();
        }
    }
}

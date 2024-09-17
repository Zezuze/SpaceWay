using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealthUI : MonoBehaviour
{
    [SerializeField] private Sprite[] _healthIcons;
    private RectTransform _rectTransform;
    private List<GameObject> _healthIconsList;

    private int _basicHealthCount = 3;
    private int _basicIconIndex = 0;

    private int _basicBlockWidth = 255;
    private int _widthIconsWithSpacing = 85;
    private int _iconWidth = 70;
    private int _iconHeight = 70;


    private void Start() => Init();

    public void Init()
    {
        _rectTransform = GetComponent<RectTransform>();
        _healthIconsList = new List<GameObject>();

        SetHealthIconsCount(_basicHealthCount, _basicIconIndex);
        SetSizeForIconsBlock(_basicBlockWidth);
    }

    private void OnEnable()
    {
        ShipHealth.OnDied += DisableHealthUI;
        ShipHealth.OnHealthDecreased += DecreaseHealthUI;
        ShipHealth.OnHealthImproved += ImproveHealthUI;
    }

    private void OnDisable()
    {
        ShipHealth.OnDied -= DisableHealthUI;
        ShipHealth.OnHealthDecreased -= DecreaseHealthUI;
        ShipHealth.OnHealthImproved -= ImproveHealthUI;
    }

    private void DisableHealthUI() =>
        _rectTransform.gameObject.SetActive(false);

    private void DecreaseHealthUI()
    {
        _basicHealthCount--;
        _basicBlockWidth -= _widthIconsWithSpacing;

        ChangeHealthUICount(false);
        SetSizeForIconsBlock(_basicBlockWidth);
    }

    private void ImproveHealthUI()
    {
        _basicHealthCount++;
        _basicBlockWidth += _widthIconsWithSpacing;

        ChangeHealthUICount(true);
        SetSizeForIconsBlock(_basicBlockWidth);
    }

    private void ChangeHealthUICount(bool state)
    {
        if (state)
        {
            GameObject firstInactiveObject = _healthIconsList.FirstOrDefault(x => !x.activeSelf);
            firstInactiveObject.SetActive(state);
        }
        else
        {
            GameObject firstActiveObject = _healthIconsList.FirstOrDefault(x => x.activeSelf);
            firstActiveObject.SetActive(state);
        }
    }

    private void SetHealthIconsCount(int iconsCount, int iconIndex)
    {
        for (int i = 0; i < iconsCount; i++)
        {
            GameObject healthIconObject = new GameObject($"HealthIcon {i}");

            Image healthIconImage = healthIconObject.AddComponent<Image>();
            healthIconImage.sprite = _healthIcons[iconIndex];

            RectTransform iconRectTransform = healthIconObject.GetComponent<RectTransform>();
            iconRectTransform.SetParent(_rectTransform, false);
            iconRectTransform.sizeDelta = new Vector2(_iconWidth, _iconHeight);

            _healthIconsList.Add(healthIconObject);
        }
    }

    private void SetSizeForIconsBlock(int newWidth)
    {
        Vector2 size = _rectTransform.sizeDelta;
        size.x = newWidth;
        _rectTransform.sizeDelta = size;
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private int _healthPerIcon = 1;
    [SerializeField] private float _spacing = 90f;
    [SerializeField] private float _iconSize = 80f;

    private readonly List<GameObject> _icons = new();

    public void Build(int maxHealth)
    {
        for (int i = 0; i < maxHealth / _healthPerIcon; i++)
        {
            var icon = new GameObject("HealthIcon", typeof(RectTransform), typeof(Image));
            icon.transform.SetParent(_container, false);

            var rect = (RectTransform)icon.transform;
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(_iconSize, _iconSize);
            rect.anchoredPosition = new Vector2(i * _spacing, 0f);

            var image = icon.GetComponent<Image>();
            image.sprite = _iconSprite;
            image.raycastTarget = false;

            _icons.Add(icon);
        }
    }

    public void SetHealth(int currentHealth)
    {
        int full = currentHealth / _healthPerIcon;
        for (int i = 0; i < _icons.Count; i++)
            _icons[i].SetActive(i < full);
    }
}
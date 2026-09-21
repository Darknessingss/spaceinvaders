using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _iconPrefab;
    [SerializeField] private int _healthPerIcon = 100;
    [SerializeField] private float _spacing = 1.25f;

    private readonly List<GameObject> _icons = new();

    public void Build(int maxHealth)
    {
        int iconCount = maxHealth / _healthPerIcon;

        for (int i = 0; i < iconCount; i++)
        {
            var icon = Instantiate(_iconPrefab, _container);
            icon.transform.localPosition = new Vector3(i * _spacing, 0f, 0f);
            _icons.Add(icon);
        }
    }

    public void SetHealth(int currentHealth)
    {
        int fullIcons = currentHealth / _healthPerIcon;

        for (int i = 0; i < _icons.Count; i++)
        {
            _icons[i].SetActive(i < fullIcons);
        }
    }
}
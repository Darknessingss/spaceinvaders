using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class WinScreen : MonoBehaviour
{
    public event Action RestartRequested;

    [SerializeField] private GameObject _root;
    [SerializeField] private Button _restartButton;

    private void Awake()
    {
        _restartButton.onClick.AddListener(() => RestartRequested?.Invoke());
        _root.SetActive(false);
    }

    public void Show()
    {
        _root.SetActive(true);
    }

    public void Hide()
    {
        _root.SetActive(false);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToastManager : Singleton<ToastManager>
{
    [SerializeField] private RectTransform _toastPrefab;
    private List<RectTransform> _toasts = new List<RectTransform>();

    public void AddToast(string message)
    {
        if (_toasts.Count >= 3)
        {
            return;
        }
        RectTransform toast = Instantiate(_toastPrefab, transform);
        toast.GetComponentInChildren<TMP_Text>().text = message;
        _toasts.Add(toast);
        InternalClock internalClock = new InternalClock(3f, gameObject);
        internalClock.e_OnTimerDone += () =>
        {
            _toasts.Remove(toast);
            internalClock.Delete();
            Destroy(toast.gameObject);
            UpdateToastsUI();
        };
        UpdateToastsUI();
    }

    private void UpdateToastsUI()
    {
        for (int i = 0; i < _toasts.Count; i++)
        {
            RectTransform toast = _toasts[i];
            toast.anchoredPosition = new Vector2(0, -i * (_toastPrefab.rect.height + 5));
        }
    }
}

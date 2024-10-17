using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BarUI : MonoBehaviour
{
    public bool IsScreenSpace;
    public bool ShowText;
    public int BreakpointSize = 0;
    private StatInt _stat;
    private TMP_Text _statText;
    // Screen Space UI
    private RectTransform _backgroundScreen;
    private RectTransform _fillScreen;
    // World Space UI
    private SpriteRenderer _backgroundWorld;
    private Transform _fillWorld;
    [SerializeField] private GameObject _breakpointPrefab;
    private List<GameObject> _breakpoints;

    void Awake()
    {
        _breakpoints = new List<GameObject>();
        if (ShowText)
        {
            _statText = transform.Find("StatText").GetComponent<TMP_Text>();
            _statText.gameObject.SetActive(true);
        }

        if (IsScreenSpace)
        {
            _backgroundScreen = transform.Find("Background").GetComponent<RectTransform>();
            _fillScreen = transform.Find("Fill").GetComponent<RectTransform>();
        }
        else
        {
            _backgroundWorld = transform.Find("Background").GetComponent<SpriteRenderer>();
            _fillWorld = transform.Find("FillBar").GetComponent<Transform>();
        }
    }

    public void RegisterStat(StatInt stat)
    {
        _stat = stat;
        _stat.e_OnStatChanged += UpdateBar;
        StartCoroutine(DelayedInitialize());
    }

    private IEnumerator DelayedInitialize()
    {
        yield return new WaitForEndOfFrame();
        // DrawBreakpoints();
        UpdateBar();
    }

    private void UpdateBar()
    {
        float statPercentage = (float)_stat.Current / _stat.Base;
        if (ShowText)
        {
            _statText.text = $"{_stat.Current}/{_stat.Base}";
        }
        if (IsScreenSpace)
        {
            _fillScreen.sizeDelta = new Vector2(_backgroundScreen.rect.width * statPercentage, _fillScreen.sizeDelta.y);
        }
        else
        {
            _fillWorld.localScale = new Vector3(statPercentage, 1, 1);
        }
    }

    public void UpdateBar(int current, int max)
    {
        float statPercentage = (float)current / max;
        if (ShowText)
        {
            _statText.text = $"{current}/{max}";
        }
        if (IsScreenSpace)
        {
            _fillScreen.sizeDelta = new Vector2(_backgroundScreen.rect.width * statPercentage, _fillScreen.sizeDelta.y);
        }
        else
        {
            _fillWorld.localScale = new Vector3(statPercentage, 1, 1);
        }
    }

    private void DrawBreakpoints()
    {
        for (int i = _breakpoints.Count - 1; i >= 0; i--)
        {
            Destroy(_breakpoints[i]);
        }
        _breakpoints.Clear();

        if (BreakpointSize == 0)
        {
            return;
        }

        if (IsScreenSpace)
        {

        }
        else
        {
            float barWidth = _backgroundWorld.bounds.size.x;
            int segmentCount = _stat.Base / BreakpointSize;
            float segmentSize = barWidth / segmentCount;
            for (int i = 1; i < segmentCount; i++)
            {
                GameObject breakpoint = Instantiate(_breakpointPrefab, transform);
                float xPosition = -(barWidth / 2) + i * segmentSize;
                breakpoint.transform.localPosition = new Vector3(xPosition, 0, 0);
                _breakpoints.Add(breakpoint);
            }
        }
    }
}
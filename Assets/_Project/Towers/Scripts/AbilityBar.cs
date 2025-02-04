using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _backgroundWorld;
    [SerializeField] private Transform _fillWorld;
    private InternalClock _internalClock;
    public bool Locked { get; set; }

    void Awake()
    {
        _backgroundWorld = transform.Find("Background").GetComponent<SpriteRenderer>();
        _fillWorld = transform.Find("FillBar").GetComponent<Transform>();
    }

    void Update()
    {
        if (!Locked && _internalClock != null)
        {
            UpdateBar();
        }
    }

    public void RegisterClock(InternalClock internalClock)
    {
        _internalClock = internalClock;
    }

    private void UpdateBar()
    {
        float statPercentage = _internalClock.TimeElapsed / _internalClock.TimeToWait;
        if (_fillWorld == null) { return; }
        _fillWorld.localScale = new Vector3(statPercentage, 1, 1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fillPercentage">Value between 0.0 and 1.0</param>
    public void SetFill(float fillPercentage)
    {
        if (_fillWorld == null) { return; }
        _fillWorld.localScale = new Vector3(fillPercentage, 1, 1);
    }
}

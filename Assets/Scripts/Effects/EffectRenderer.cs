using UnityEngine;
using System.Collections.Generic;


public class EffectRenderer : MonoBehaviour
{
    private EffectTracker _effectTracker;
    [SerializeField] private List<EffectIcon> _effectIcons;

    void Awake()
    {
        _effectTracker = GetComponent<EffectTracker>();
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    public void RenderEffects()
    {
        for (int i = 0; i < _effectTracker.EffectsApplied.Count; i++)
        {
            EffectIcon effectIcon = _effectIcons[i];
            effectIcon.Render(_effectTracker.EffectsApplied[i]);
        }
        for (int i = _effectTracker.EffectsApplied.Count; i < _effectIcons.Count; i++)
        {
            EffectIcon effectIcon = _effectIcons[i];
            effectIcon.Hide();
        }
    }
}
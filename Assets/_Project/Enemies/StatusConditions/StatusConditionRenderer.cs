using UnityEngine;
using System.Collections.Generic;


public class StatusConditionRenderer : MonoBehaviour
{
    private StatusConditionTracker _statusConditionTracker;
    [SerializeField] private List<StatusConditionIcon> _statusConditionIcons;

    void Awake()
    {
        _statusConditionTracker = GetComponent<StatusConditionTracker>();
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    public void RenderStatusConditions()
    {
        for (int i = 0; i < _statusConditionTracker.StatusConditionsApplied.Count; i++)
        {
            StatusConditionIcon statusConditionIcon = _statusConditionIcons[i];
            statusConditionIcon.Render(_statusConditionTracker.StatusConditionsApplied[i]);
        }
        for (int i = _statusConditionTracker.StatusConditionsApplied.Count; i < _statusConditionIcons.Count; i++)
        {
            StatusConditionIcon statusConditionIcon = _statusConditionIcons[i];
            statusConditionIcon.Hide();
        }
    }
}
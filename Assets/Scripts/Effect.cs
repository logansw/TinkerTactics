using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public float Duration;
    private Coroutine _durationCoroutine;

    public virtual void Start()
    {
        _durationCoroutine = StartCoroutine(DurationCoroutine());
    }

    // Removes the effect from the target after the duration has expired
    protected virtual IEnumerator DurationCoroutine()
    {
        yield return new WaitForSeconds(Duration);
        Remove();
    }

    public void Remove()
    {
        Destroy(this);
    }

    public void OnDisable()
    {
        if (_durationCoroutine != null)
        {
            StopCoroutine(_durationCoroutine);
        }
    }
}

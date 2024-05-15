using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellProjectile : MonoBehaviour
{
    public float Damage;
    private Coroutine _expandCoroutine;

    public void Initialize(float damage)
    {
        Damage = damage;
        _expandCoroutine = StartCoroutine(Expand());
    }

    public IEnumerator Expand()
    {
        float elapsedTime = 0;
        float duration = 0.25f;
        float startScale = 0.1f;
        float endScale = 1f;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.one * Mathf.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one * endScale;
        Destroy(gameObject);
    }

    void OnCollisionEnter2D()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ReceiveDamage(Damage);
            }
        }
    }
}

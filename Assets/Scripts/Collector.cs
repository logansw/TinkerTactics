using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Collector : MonoBehaviour
{
    private BoxCollider2D _collider;

    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.gameObject.GetComponent<ICollectable>() != null)
        {
            other.collider.gameObject.GetComponent<ICollectable>().Collect();
        }
    }
}

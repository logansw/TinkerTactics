using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : Component
{
    public static T s_Instance { get; private set; }

    void Awake() {
        if (s_Instance == null) {
            s_Instance = FindObjectOfType<T>();
        } else {
            Destroy(gameObject);
        }
    }
}
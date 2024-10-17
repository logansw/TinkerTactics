using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
    where T : Component
{
    public static T s_Instance { get; private set; }
    public static bool s_Initialized { get; private set; }

    void Awake()
    {
        if (s_Instance == null) 
        {
            s_Instance = FindObjectOfType<T>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public virtual void Initialize()
    {
        s_Initialized = true;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Tower))]
public class TowerHealth : MonoBehaviour
{
    private Tower _tower;
    public int Current;
    public int Max;
    [SerializeField] private TMP_Text _healthText;

    private void Awake()
    {
        Current = Max;
        _tower = GetComponent<Tower>();
        UpdateHealthText();
    }

    void UpdateHealthText()
    {
        _healthText.text = $"{Current} ❤";
    }

    public void ChangeHealth(int change)
    {
        Current += change;
        Current = Mathf.Clamp(Current, 0, Max);
        UpdateHealthText();
    }
}
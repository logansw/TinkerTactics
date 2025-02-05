using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

[RequireComponent(typeof(Button))]
public class MapLocation : MonoBehaviour
{
    public List<MapLocation> NextLocations;
    protected Button _button;
    private bool _initialized;
    private SceneType _sceneType;
    [SerializeField] private TMP_Text _locationText;
    
    public virtual void Initialize(SceneType sceneType)
    {
        if (_initialized) { return; }
        _button = GetComponent<Button>();
        _button.interactable = false;
        _button.onClick.AddListener(SetAsCurrentLocation);
        _initialized = true;
        _sceneType = sceneType;
        _button.onClick.AddListener(() => LoadScene(sceneType));
        
        string locationText;
        switch (sceneType)
        {
            case (SceneType.Battle):
                locationText = "Battle";
                break;
            case (SceneType.TinkerShop):
                locationText = "Tinker Shop";
                break;
            case (SceneType.TowerShop):
                locationText = "Tower Shop";
                break;
            default:
                locationText = "Error";
                break;
        }
        _locationText.text = locationText;
    }

    public void SetActive(bool active)
    {
        _button.interactable = active;
    }

    private void SetAsCurrentLocation()
    {
        MapManager.s_Instance.SetLocation(this);
        MapManager.s_Instance.OpenNextLocations();
    }

    private void LoadScene(SceneType sceneType)
    {
        SceneLoader.s_Instance.LoadScene(sceneType);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] private TowerDialog _towerDialog;
    [SerializeField] private IconTooltip _iconTooltip;

    public void ShowTowerDialogBattle(Tower tower)
    {
        _towerDialog.Initialize(tower);
        _towerDialog.gameObject.SetActive(true);
    }

    public void HideTowerDialogBattle()
    {
        _towerDialog.gameObject.SetActive(false);
    }

    public void ShowIconTooltip(ITooltipTargetable tooltipSource)
    {
        _iconTooltip.Initialize(tooltipSource);
        _iconTooltip.gameObject.SetActive(true);
    }

    public void HideIconTooltip()
    {
        _iconTooltip.gameObject.SetActive(false);
    }
    // private List<Popup> _openPopups;

    // void Update()
    // {
    //     Popup popup = null;
    //     // SelectPopup(popup);
    // }

    // public void RegisterPopup(Popup popup, Vector2 targetPosition)
    // {
    //     popup.transform.SetParent(transform);

    //     popup.OnPopupClosed += UnregisterPopup;
    //     _openPopups.Add(popup);
    // }

    // public void UnregisterPopup(Popup popup)
    // {
    //     _openPopups.Remove(popup);
    //     popup.OnPopupClosed -= UnregisterPopup;
    // }

    // private bool Overlaps(RectTransform rectTransformA, RectTransform rectTransformB)
    // {
    //     Vector3[] cornersA = new Vector3[4];
    //     Vector3[] cornersB = new Vector3[4];
    //     rectTransformA.GetWorldCorners(cornersA);
    //     rectTransformB.GetWorldCorners(cornersB);

    //     // Convert corners into Rects (Assumes bottom-left is corners[0], top-right is corners[2])
    //     Rect rectA = new Rect(cornersA[0].x, cornersA[0].y,
    //                           cornersA[2].x - cornersA[0].x,
    //                           cornersA[2].y - cornersA[0].y);

    //     Rect rectB = new Rect(cornersB[0].x, cornersB[0].y,
    //                           cornersB[2].x - cornersB[0].x,
    //                           cornersB[2].y - cornersB[0].y);

    //     // Finally, check if the two Rects overlap
    //     return rectA.Overlaps(rectB);
    // }

    // // private void SelectPopup(Popup selectedPopup)
    // // {
    // //     if (selectedPopup == null)
    // //     {
    // //         foreach (Popup popup in _openPopups)
    // //         {
    // //             if (!popup.IsLocked)
    // //             {
    // //                 popup.Close(); 
    // //             }
    // //         }
    // //         return;
    // //     }

    // //     selectedPopup
    // // }
}

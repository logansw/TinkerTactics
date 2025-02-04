using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int _height;
    public int Height
    {
        get
        {
            return _height;
        }
        private set
        {
            _height = value;
        }
    }

    public void Initialize(int height)
    {
        _height = height;
        SetColors(_height);
    }

    void OnEnable()
    {
        OnEnableTilePlot();
    }

    void OnDisable()
    {
        OnDisableTilePlot();
    }

#region Rendering
    private const float SHADOW_STRENGTH = 0.2f;
    private string[] OutlineColors = {"377DA0", "B3446B", "C4A264"};
    private string[] FloorColors = {"50B8C1", "D77070", "FAE186"};
    private string[] ShadowColors = {"000000", "000000", "000000"};
    [SerializeField] private SpriteRenderer _outlineSR;
    [SerializeField] private SpriteRenderer _floorSR;
    // Shadow notation:
    // L or T refers to the left or top of the sprite.
    // 1 or 2 refers to the position of the shape. The higher number is more towards the bottom right.
    // R or T or S refers to the shape: Rectangle, Triangle, Square.
    [SerializeField] private SpriteRenderer _shadowL1_R;
    [SerializeField] private SpriteRenderer _shadowL1_T;
    [SerializeField] private SpriteRenderer _shadowL2_R;
    [SerializeField] private SpriteRenderer _shadowL2_T;
    [SerializeField] private SpriteRenderer _shadowT1_R;
    [SerializeField] private SpriteRenderer _shadowT1_T;
    [SerializeField] private SpriteRenderer _shadowT2_R;
    [SerializeField] private SpriteRenderer _shadowT2_T;
    [SerializeField] private SpriteRenderer _shadowL_S;
    [SerializeField] private SpriteRenderer _shadowT_S;

    public void SetColors(int height)
    {
        _outlineSR.color = new HexColor(OutlineColors[height]);
        _floorSR.color = new HexColor(FloorColors[height]);
        _shadowL1_R.color = new HexColor(ShadowColors[height], SHADOW_STRENGTH);
        _shadowL1_T.color = new HexColor(ShadowColors[height], SHADOW_STRENGTH);
        _shadowL2_R.color = new HexColor(ShadowColors[height], SHADOW_STRENGTH);
        _shadowL2_T.color = new HexColor(ShadowColors[height], SHADOW_STRENGTH);
        _shadowT1_R.color = new HexColor(ShadowColors[height], SHADOW_STRENGTH);
        _shadowT1_T.color = new HexColor(ShadowColors[height], SHADOW_STRENGTH);
        _shadowT2_R.color = new HexColor(ShadowColors[height], SHADOW_STRENGTH);
        _shadowT2_T.color = new HexColor(ShadowColors[height], SHADOW_STRENGTH);
        _shadowL_S.color = new HexColor(ShadowColors[height], SHADOW_STRENGTH);
        _shadowT_S.color = new HexColor(ShadowColors[height], SHADOW_STRENGTH);
    }

    public void CalculateShadows(int westNeighborHeight, int northNeighborHeight, int northwestNeighborHeight)
    {
        _shadowL1_R.enabled = false;
        _shadowL1_T.enabled = false;
        _shadowL2_R.enabled = false;
        _shadowL2_T.enabled = false;
        _shadowT1_R.enabled = false;
        _shadowT1_T.enabled = false;
        _shadowT2_R.enabled = false;
        _shadowT2_T.enabled = false;
        _shadowL_S.enabled = false;
        _shadowT_S.enabled = false;

        int westDelta = westNeighborHeight - Height;
        if (westDelta >= 1)
        {
            _shadowL1_R.enabled = true;
            _shadowL1_T.enabled = true;
            _shadowL_S.enabled = true;
        }
        if (westDelta >= 2)
        {
            _shadowL2_R.enabled = true;
            _shadowL2_T.enabled = true;
        }

        int northDelta = northNeighborHeight - Height;
        if (northDelta >= 1)
        {
            _shadowT1_R.enabled = true;
            _shadowT1_T.enabled = true;
            _shadowT_S.enabled = true;
        }
        if (northDelta >= 2)
        {
            _shadowT2_R.enabled = true;
            _shadowT2_T.enabled = true;
        }

        int northwestDelta = northwestNeighborHeight - Height;
        if (northwestDelta >= 1)
        {
            _shadowL1_T.enabled = true;
            _shadowT1_T.enabled = true;
        }
        if (northwestDelta >= 2)
        {
            _shadowL_S.enabled = true;
            _shadowT_S.enabled = true;
            _shadowL2_T.enabled = true;
            _shadowT2_T.enabled = true;
        }
        
    }
#endregion

#region TilePlot
    public TilePlot TilePlot;
    private bool _queuedRemoveTilePlot;

    private void OnEnableTilePlot()
    {
        IdleState.e_OnIdleStateEnter += TryRemoveTilePlot;
        PlayingState.e_OnPlayingStateEnter += QueueRemoveTilePlot;
    }

    private void OnDisableTilePlot()
    {
        IdleState.e_OnIdleStateEnter -= TryRemoveTilePlot;
        PlayingState.e_OnPlayingStateEnter -= QueueRemoveTilePlot;
    }

    public void AddTilePlot()
    {
        TilePlot = gameObject.AddComponent<TilePlot>();
        TilePlot.IsActivated = true;
    }
    public void RemoveTilePlot()
    {
        if (TilePlot != null)
        {
            Destroy(TilePlot);
        }
        Height = 0;
        SetColors(Height);
        BlockManager.s_Instance.QueueRecalculateShadows();
    }

    private void QueueRemoveTilePlot()
    {
        if (TilePlot != null && TilePlot.IsOccupied())
        {
            _queuedRemoveTilePlot = true;
        }
    }

    private void TryRemoveTilePlot()
    {
        if (_queuedRemoveTilePlot)
        {
            RemoveTilePlot();
            _queuedRemoveTilePlot = false;
        }
    }
#endregion
}
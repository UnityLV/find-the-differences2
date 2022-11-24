using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomDetector : MonoBehaviour
{
    [SerializeField] private float _zoomTouchSensetivity;
    [SerializeField] private float _zoomScrollSensetivity;

    private ZoomToch _zoomTouchProcessor;
    private ZoomScroll _zoomScrollProcessor;

    private bool _isCanZoom = true;

    private void Awake()
    {
        _zoomTouchProcessor = new(_zoomTouchSensetivity);
        _zoomScrollProcessor = new(_zoomScrollSensetivity);
    }

    public bool TryGetZoom(out float zoomDelta, out Vector2 point)
    {
        if (_isCanZoom)
        {
            return _zoomTouchProcessor.TryGetZoom(out zoomDelta, out point) ||
                _zoomScrollProcessor.TryGetZoom(out zoomDelta, out point);                    
        }
        point = default;
        zoomDelta = default;
        return false;
    }

    public void BlockZoom()
    {
        _isCanZoom = false;
    }

    public void UnlockZoom()
    {
        _isCanZoom = true;

    }


}



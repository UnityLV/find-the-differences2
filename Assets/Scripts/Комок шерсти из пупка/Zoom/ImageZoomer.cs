using System.Collections;
using System;
using UnityEngine;

public class ImageZoomer
{
    public RectConfinder Confinder1 { get; }
    public RectConfinder Confinder2 { get; }

    private float _maxZoom = 2f;
    private float _minZoom = 1f;

    private Vector2 _lastZoomPoint;

    public ImageZoomer(RectConfinder confinder1, RectConfinder confinder2)
    {
        Confinder1 = confinder1;
        Confinder2 = confinder2;
    }

    public void Zoom(float zoomDelta)
    {
        Zoom(zoomDelta, _lastZoomPoint);
    }

    public void Zoom(float zoomDelta, Vector2 worldPoint)
    {
        _lastZoomPoint = worldPoint;

        var normalizedPoint = ScreenToRectNormalized(_lastZoomPoint, Confinder1.Inside);

        if (IsScreenPointOver(Confinder1.Inside, _lastZoomPoint))
        {
            Zoom(Confinder1.Inside, zoomDelta + 1f, normalizedPoint);
            Zoom(Confinder2.Inside, zoomDelta + 1f, normalizedPoint);
        }

        ClampZoom();

        Confinder1.ClampPositions();
        Confinder2.ClampPositions();
    }    

    private Vector2 ScreenToRectNormalized(Vector2 screenPoint, RectTransform rectTransform)
    {
        // Приводим к экранным координатам
        var r = rectTransform.rect;
        r.position = rectTransform.TransformPoint(r.position);
        r.size = rectTransform.TransformVector(r.size);

        var p = (screenPoint - r.min) / r.size;

        return p;
    }

    private void Zoom(RectTransform target, float factor, Vector2 normalizedOrigin)
    {
        var r = target.rect;

        var oldScale = target.localScale.x;
        var newScale = target.localScale.x * factor;

        if (newScale <= _maxZoom)
        {
            var offset = r.width * (target.pivot - normalizedOrigin) * (newScale - oldScale);

            target.localScale = newScale * Vector3.one;
            target.localPosition += (Vector3)offset;
        }

    }

    private void ClampZoom()
    {
        ClampRectScaleScale(Confinder1.Inside);
        ClampRectScaleScale(Confinder2.Inside);
    }

    private void ClampRectScaleScale(RectTransform rect)
    {
        if (rect.localScale.x < _minZoom)
        {
            rect.localScale = Vector3.one * _minZoom;
        }

        if (rect.localScale.x > _maxZoom)
        {
            rect.localScale = Vector3.one * _maxZoom;
        }
    }

    public bool IsScreenPointOver(RectTransform rectTransform, Vector2 sceenPoint)
    {
        var scale = rectTransform.lossyScale.x;
        var rect = rectTransform.rect;
        rect = new Rect(rect.position * scale, rect.size * scale);
        var bounds = new Bounds((Vector3)rect.center + rectTransform.position, (Vector3)rect.size + Vector3.forward * 100f);
        return bounds.Contains(sceenPoint);
    }
}

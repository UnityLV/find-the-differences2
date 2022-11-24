using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using LibraryForGames;

public class DoubleTapZoomer
{
    private RectConfinder _confinder1;
    private RectConfinder _confinder2;

    private readonly float _normalZoom = 1f;
    private readonly float _maxZoom = 2f;

    private ImageZoomer _zoomer;
    private float _smoothZoomSpeed = 10;

    public event UnityAction Complited;

    public DoubleTapZoomer(ImageZoomer zoomer)
    {
        _zoomer = zoomer;

        _confinder1 = _zoomer.Confinder1;
        _confinder2 = _zoomer.Confinder2;
    }

    public IEnumerator ApplyZoom(Vector2 position)
    {
        float currentZoom = _confinder1.Inside.localScale.x;
        float zoomToApply = CalculateTargetZoom(currentZoom);

        while (Tools.IsAlmostEquals(zoomToApply, currentZoom) == false)
        {
            float zoomDelta = Time.deltaTime * (zoomToApply - currentZoom) * _smoothZoomSpeed;
            _zoomer.Zoom(zoomDelta, position);
            currentZoom = _confinder1.Inside.localScale.x;
            yield return null;
        }
        Complited?.Invoke();
    }

    private float CalculateTargetZoom(float currentZoom) => Tools.IsAlmostEquals(currentZoom, _normalZoom) ? _maxZoom : _normalZoom;

}

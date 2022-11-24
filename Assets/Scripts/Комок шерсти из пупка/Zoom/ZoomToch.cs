using UnityEngine;

public class ZoomToch
{
    private readonly float _sensetivity = 0.01f;
    private readonly int _touchesCountToApplyZoom = 2;

    private Touch _touchFirst;
    private Touch _touchSecond;
    private Vector2 _touchFirstDirection;
    private Vector2 _touchSecondDirection;
    private float _touchesDistance;
    private float _touchesDirectionDistance;
    private float _zoom;

    public ZoomToch(float sensetivity)
    {
        _sensetivity = sensetivity;
    }

    public bool TryGetZoom(out float zoom,out Vector2 worldZoomPoint)
    {
        if (Input.touchCount == _touchesCountToApplyZoom)
        {
            CalculateTouches();

            zoom = CalculateZoom();

            if (zoom != 0)
            {
                worldZoomPoint = CalculateZoomPoint();
                return true;
            }
        }
        worldZoomPoint = default;
        zoom = default;
        return false;
    }

    private Vector2 CalculateZoomPoint()
    {
        if (Input.touchCount >= _touchesCountToApplyZoom)
            return Vector2.Lerp(Input.GetTouch(0).position, Input.GetTouch(1).position, 0.5f);

        return Vector2.zero;
    }

    private float CalculateZoom()
    {
        _zoom = _touchesDistance - _touchesDirectionDistance;

        return _zoom * _sensetivity;
    }

    private void CalculateTouches()
    {
        _touchFirst = Input.GetTouch(0);
        _touchSecond = Input.GetTouch(1);

        _touchFirstDirection = _touchFirst.position - _touchFirst.deltaPosition;
        _touchSecondDirection = _touchSecond.position - _touchSecond.deltaPosition;

        _touchesDistance = Vector2.Distance(_touchFirst.position, _touchSecond.position);
        _touchesDirectionDistance = Vector2.Distance(_touchFirstDirection, _touchSecondDirection);
    }
}

using UnityEngine;

public class ZoomScroll
{
    private float _sensetivity;

    public ZoomScroll(float sensetivity)
    {
        _sensetivity = sensetivity;
    }

    public bool TryGetZoom(out float zoomDelta,out Vector2 worldZoomPoint)
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            worldZoomPoint = Input.mousePosition;
            zoomDelta = Input.mouseScrollDelta.y * _sensetivity;
            return true;
        }
        worldZoomPoint = default;
        zoomDelta = default;
        return false;
    }
}
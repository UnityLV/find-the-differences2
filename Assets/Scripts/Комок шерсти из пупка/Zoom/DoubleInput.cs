using UnityEngine;

public class DoubleInput
{
    private RectConfinder _rectConfinder;

    private float _timeAfterLastTouch;
    private float _timeAfterLastClick;
    private int _lastTouchCount;    

    public DoubleInput(RectConfinder rectConfinder)
    {
        _rectConfinder = rectConfinder;
    }
    public void CoutTime()
    {
        _timeAfterLastTouch += Time.deltaTime;
        _timeAfterLastClick += Time.deltaTime;
        
    }

    public bool IsDouble(out Vector2 position)
    {
        return IsDoubleTap(out position) || IsDoubleClick(out position);
    }

    private bool IsDoubleTap(out Vector2 position)
    {
        if (IsUnickTouches())
        {
            float timeToDoubleToch = 0.3f;

            if (Input.touchCount == 1 && IsScreenPointOver(_rectConfinder.Outside, Input.GetTouch(0).position))
            {
                if (timeToDoubleToch > _timeAfterLastTouch)
                {
                    _timeAfterLastTouch = 0;
                    position = Input.GetTouch(0).position;
                    return true;
                }
                _timeAfterLastTouch = 0;
            }
        }

        position = Vector2.zero;
        return false;

        bool IsUnickTouches()
        {
            if (_lastTouchCount != Input.touchCount)
            {
                _lastTouchCount = Input.touchCount;
                return true;
            }
            _lastTouchCount = Input.touchCount;
            return false;
        }
    }
    private bool IsDoubleClick(out Vector2 position)
    {
        
        float timeToDoubleClick = 0.3f;
        

        if (Input.GetMouseButtonDown(0) && IsScreenPointOver(_rectConfinder.Outside, Input.mousePosition))
        {            
            
            if (timeToDoubleClick > _timeAfterLastClick)
            {

                _timeAfterLastClick = 0;
                position = Input.mousePosition;
                return true;
            }
            _timeAfterLastClick = 0;
        }
        

        position = Vector2.zero;
        return false;

        
    }
    

    private bool IsScreenPointOver(RectTransform rectTransform, Vector2 sceenPoint)
    {
        var scale = rectTransform.lossyScale.x;
        var rect = rectTransform.rect;

        rect = new Rect(rect.position * scale, rect.size * scale);
        var bounds = new Bounds((Vector3)rect.center + rectTransform.position, (Vector3)rect.size + Vector3.forward * 100f);
        return bounds.Contains(sceenPoint);
    }
}

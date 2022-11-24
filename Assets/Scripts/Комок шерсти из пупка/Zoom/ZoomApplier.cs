using LibraryForGames;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomApplier : MonoBehaviour
{
    [SerializeField] private RectConfinder _rectConfinder;
    [SerializeField] private RectConfinder _secondConfinder;

    [SerializeField] private ZoomDetector _detector;

    private ImageZoomer _imageZoomer;
    private DoubleTapZoomer _doubleTapZoomer;

    private Coroutine _coroutine;

    private void Awake()
    {
        _imageZoomer = new(_rectConfinder, _secondConfinder);
        _doubleTapZoomer = new(_imageZoomer);
    }
    private void OnEnable()
    {
        _doubleTapZoomer.Complited += ResetCoroutine;
    }   

    private void Update()
    {
        if (_detector.TryGetZoom(out float zoomDelta, out Vector2 point))        
            _imageZoomer.Zoom(zoomDelta, point);
        

        if (IsDoubleTap(out Vector2 position))        
            if (_coroutine == null)            
                _coroutine = StartCoroutine(_doubleTapZoomer.ApplyZoom(position));

    }

    private void OnDisable()
    {
        _doubleTapZoomer.Complited -= ResetCoroutine;        
    }

    public void ResetZoom()
    {
        gameObject.transform.localScale = Vector3.one;

        _imageZoomer.Zoom(0, Vector2.zero);

    } 

    private void ResetCoroutine() => _coroutine = null;

    private bool IsDoubleTap(out Vector2 position)
    {
        position = default;

        if (Input.touchCount >= 1)        
            position = Input.GetTouch(0).position;        
        
        return
            Input.touchCount >= 1 &&
            IsScreenPointOver(_rectConfinder.Outside, Input.GetTouch(0).position) &&
            Input.GetTouch(0).phase == TouchPhase.Began &&
            Input.GetTouch(0).tapCount == 2;
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

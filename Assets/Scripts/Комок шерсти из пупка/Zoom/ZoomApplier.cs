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
    private DoubleInput _doubleInput;

    private Coroutine _coroutine;  

    private bool _isCanZoom;

    private void Awake()
    {
        _imageZoomer = new(_rectConfinder, _secondConfinder);
        _doubleTapZoomer = new(_imageZoomer);
        _doubleInput = new(_rectConfinder);
    }
    private void OnEnable()
    {
        _doubleTapZoomer.Complited += ResetCoroutine;
    }   

    private void Update()
    {
        _doubleInput.CoutTime();        

        if (_isCanZoom == false)
        {
            return;
        }


        if (_detector.TryGetZoom(out float zoomDelta, out Vector2 point))        
            _imageZoomer.Zoom(-zoomDelta, point);        

        if (_doubleInput.IsDouble(out Vector2 position))        
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

        float outZoomDelta = -10;
        _imageZoomer.Zoom(outZoomDelta, Vector2.zero);

    } 

    private void ResetCoroutine() => _coroutine = null;
    
    public void BlockZoom()
    {
        _isCanZoom = false;
    }

    public void UnlockZoom()
    {
        _isCanZoom = true;

    }
}

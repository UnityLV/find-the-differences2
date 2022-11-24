using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using LibraryForGames;

public class DragAndDropDetector : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectConfinder[] _rectConfinders;

    private Vector2 _dragDelta;
    private float _maxDragMagnitude = 60;
    private int _touchCountToApplyGrag = 1;

    private WaitForSeconds _fateTime = new WaitForSeconds(0.3f);
    private bool _isCanDrag = true;
    private int _touchCountToZoom = 2;

    private void Update()
    {
        if (IsNeedWaitFateTime())
        {
            StartCoroutine(CountFateTime());
        }
    }

    private bool IsNeedWaitFateTime() => Input.touchCount == _touchCountToZoom && _isCanDrag;

    private IEnumerator CountFateTime()
    {
        _isCanDrag = false;
        yield return _fateTime;
        _isCanDrag = true;
    }    

    public void OnDrag(PointerEventData eventData)
    {
        Drag(eventData);

    }

    private void Drag(PointerEventData eventData)
    {
        if (IsAvalableForDrag())
        {
            foreach (var collider in _rectConfinders)
            {
                if (IsDefaultZoom(collider) == false)
                {
                    MoveCollider(eventData, collider);
                }
            }

            _dragDelta = eventData.delta;
        }

        bool IsAvalableForDrag() => Input.touchCount == _touchCountToApplyGrag ||
            (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            && _isCanDrag;
    }

    private bool IsDefaultZoom(RectConfinder collider) => Tools.IsAlmostEquals(collider.Inside.localScale.x, Vector3.one.x);

    private void MoveCollider(PointerEventData eventData, RectConfinder coliider)
    {
        coliider.transform.position += (Vector3)eventData.delta;

        coliider.ClampPositions();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isCanDrag)
        {
            StartCoroutine(SmoothContinusDrag(_dragDelta));          
        }
    }

    private IEnumerator SmoothContinusDrag(Vector2 moveVector)
    {
        float inertia = Mathf.Clamp(moveVector.magnitude, 0, _maxDragMagnitude);        

        while (Tools.IsAlmostEquals(inertia , 0) == false)
        {
            foreach (var collider in _rectConfinders)            
                TryMovePosition(moveVector, inertia, collider);

            float fadeCoeficient = 0.8f;
            inertia *= fadeCoeficient;
            yield return null;
        }
    }

    private void TryMovePosition(Vector2 moveVector, float inertia, RectConfinder collider)
    {
        if (IsDefaultZoom(collider) == false)
        {
            MoveCollider(moveVector, inertia, collider);
            collider.ClampPositions();
        }
    }

    private void MoveCollider(Vector3 moveVector, float inertia, RectConfinder collider)
    {
        collider.transform.position = 
            Vector3.MoveTowards(
            collider.transform.position,
            collider.transform.position + moveVector,
            inertia);
    }

   
}

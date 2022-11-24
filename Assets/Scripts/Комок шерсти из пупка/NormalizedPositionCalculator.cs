using UnityEngine;
using System;
using UnityEditor;

[RequireComponent(typeof(RectTransform))]
public class NormalizedPositionCalculator : MonoBehaviour
{
    [SerializeField] private Vector2 _normalizedPosition;
    [SerializeField] private Vector2 _scale;

    public (Vector2 position,Vector2 scale) CalculateNormalisedPosition()
    {
        if (transform.parent.gameObject.TryGetComponent(out RectTransform rectTransform))
        {
            Rect imageRect = rectTransform.rect;
            RectTransform thisRectTransform = GetComponent<RectTransform>();

            float xNormalized = thisRectTransform.anchoredPosition.x / (imageRect.xMax * 2);
            float yNormalized = thisRectTransform.anchoredPosition.y / (imageRect.yMax * 2);

            _normalizedPosition = new Vector2((float)Math.Round( xNormalized,2), (float)Math.Round(yNormalized, 2));
            _scale = thisRectTransform.localScale;
             
            return (position: _normalizedPosition, scale: _scale);

        }
        throw new Exception("На родительском объекта нет RectTransform");
    }

    private void OnDrawGizmos()
    {
        CalculateNormalisedPosition();
    }
    
}

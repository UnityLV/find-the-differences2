using UnityEngine;

public class RectConfinder : MonoBehaviour
{
    [SerializeField] private RectTransform _scaler;

    [SerializeField] private RectTransform _rectTransformInside;
    [SerializeField] private RectTransform _rectTransformOutside;

    public RectTransform Inside => _rectTransformInside;
    public RectTransform Outside => _rectTransformOutside;

    public Vector2 InsideMax { get; private set; }
    public Vector2 InsideMin { get; private set; }
    public Vector2 OutsideeMax { get; private set; }
    public Vector2 OutsudeMin { get; private set; }

    public void ClampPositions()
    {
        CalculateInsidePositions();
        CalculateOutsidePositions();

        ClampMaxPosition();
        ClampMinPosition();
    }

    private void CalculateOutsidePositions()//ToDo: выделить методы расширения
    {
        float xOutsideMax = (_rectTransformOutside.rect.xMax * _rectTransformOutside.localScale.x * _scaler.localScale.x) +
            _rectTransformOutside.position.x;

        float yOutsideMax = (_rectTransformOutside.rect.yMax * _rectTransformOutside.localScale.y * _scaler.localScale.y) +
            _rectTransformOutside.position.y;

        OutsideeMax = new Vector2(xOutsideMax, yOutsideMax);

        float xInsideMin = (_rectTransformOutside.rect.xMin * _rectTransformOutside.localScale.x * _scaler.localScale.x) +
            _rectTransformOutside.position.x;

        float yInsideMin = (_rectTransformOutside.rect.yMin * _rectTransformOutside.localScale.y * _scaler.localScale.y) +
            _rectTransformOutside.position.y;

        OutsudeMin = new Vector2(xInsideMin, yInsideMin);
    }

    private void CalculateInsidePositions()
    {
        float xInsideMax = (_rectTransformInside.rect.xMax * _rectTransformInside.localScale.x * _scaler.localScale.x) +
            _rectTransformInside.position.x;

        float yInsideMax = (_rectTransformInside.rect.yMax * _rectTransformInside.localScale.y* _scaler.localScale.y) +
            _rectTransformInside.position.y;

        InsideMax = new Vector2(xInsideMax, yInsideMax);

        float xInsideMin = (_rectTransformInside.rect.xMin * _rectTransformInside.localScale.x * _scaler.localScale.x) +
            _rectTransformInside.position.x;

        float yInsideMin = (_rectTransformInside.rect.yMin * _rectTransformInside.localScale.y* _scaler.localScale.y) +
            _rectTransformInside.position.y;

        InsideMin = new Vector2(xInsideMin, yInsideMin);
    }

    private void ClampMinPosition()
    {
        float offsetX = 0, offsetY = 0;

        if (InsideMin.x >= OutsudeMin.x)
        {
            offsetX = OutsudeMin.x - InsideMin.x;
        }

        if (InsideMin.y >= OutsudeMin.y)
        {
            offsetY = OutsudeMin.y - InsideMin.y;
        }

        _rectTransformInside.position += new Vector3(offsetX, offsetY);
    }

    private void ClampMaxPosition()
    {
        float offsetX = 0, offsetY = 0;

        if (InsideMax.x <= OutsideeMax.x)
        {
            offsetX = OutsideeMax.x - InsideMax.x;
        }

        if (InsideMax.y <= OutsideeMax.y)
        {
            offsetY = OutsideeMax.y - InsideMax.y;
        }

        _rectTransformInside.position += new Vector3(offsetX, offsetY);
    }

    private void OnDrawGizmos()
    {
        //CalculateInsidePositions();

        //CalculateOutsidePositions();

        //Gizmos.color = Color.red;

        //Gizmos.DrawSphere(OutsideeMax, 60);

        //Gizmos.color = Color.green;

        //Gizmos.DrawSphere(InsideMax, 60);

        //Gizmos.color = Color.blue;

        //Gizmos.DrawSphere(InsideMin, 60);

        //Gizmos.color = Color.black;

        //Gizmos.DrawSphere(OutsudeMin, 60);

    }

}

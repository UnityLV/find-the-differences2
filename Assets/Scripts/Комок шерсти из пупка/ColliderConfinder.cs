using UnityEngine;

public class ColliderConfinder : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _colliderInside;
    [SerializeField] private BoxCollider2D _colliderOutside;
    [SerializeField] private Vector3 _offset;
    
    public void ClampPositioins()
    {
        var x = Mathf.Clamp(_colliderInside.transform.localPosition.x,
            -(_colliderOutside.size.x - _colliderInside.size.x) / 2,
            (_colliderOutside.size.x - _colliderInside.size.x) / 2);

        var y = Mathf.Clamp(_colliderInside.transform.localPosition.y,
            -(_colliderOutside.size.y - _colliderInside.size.y) / 2,
            (_colliderOutside.size.y - _colliderInside.size.y) / 2);


        _colliderInside.transform.localPosition = new Vector3(x, y, 0) + _offset;

    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIRaycaster
{
    private GraphicRaycaster _raycaster;

    private PointerEventData _clickData;
    private List<RaycastResult> _clickResults;

    public UIRaycaster(GraphicRaycaster raycaster)
    {
        _raycaster = raycaster;
        _clickData = new PointerEventData(EventSystem.current);
        _clickResults = new List<RaycastResult>();
    }

    public List<RaycastResult> GetRaycastedUiElements(Vector3 point)
    {
        _clickData.position = point;
        _clickResults.Clear();

        _raycaster.Raycast(_clickData, _clickResults);

        return _clickResults;
    }    
}



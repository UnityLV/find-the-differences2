using UnityEngine;
using UnityEngine.UI;

public class DifferenceButtonActivator : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster _graphicRaycaster;    

    private UIRaycaster _raycaster;

    private void Awake()
    {
        _raycaster = new(_graphicRaycaster);
    }

    private void Update()
    {
        TryTouch();
        TryClick();
    }

    private void TryClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var results = _raycaster.GetRaycastedUiElements(Input.mousePosition);

            foreach (var result in results)
            {
                if (result.gameObject.TryGetComponent(out DifferenceButton button))
                {
                    button.Press();
                }
            }
        }
    }

    private void TryTouch()
    {
        if (IsAvalableForTouch())
        {
            var results = _raycaster.GetRaycastedUiElements(Input.GetTouch(0).position);

            foreach (var result in results)
            {
                if (result.gameObject.TryGetComponent(out DifferenceButton button))
                {
                    button.Press();
                }
            }
        }
    }

    private bool IsAvalableForTouch()
    {
        return Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended;
    }
}

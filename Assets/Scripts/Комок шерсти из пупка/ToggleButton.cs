using UnityEngine;

public class ToggleButton : BaseUIButton
{
    [SerializeField] private GameObject _objectToToggle;   

    public override void OnClick()
    {
        ToggleObject();
    }

    private void ToggleObject()
    {
        _objectToToggle.SetActive(_objectToToggle.activeInHierarchy == false);        
    }
}

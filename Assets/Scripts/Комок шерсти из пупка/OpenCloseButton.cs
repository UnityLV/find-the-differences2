using UnityEngine;

public class OpenCloseButton : BaseUIButton
{
    [SerializeField] private GameObject[] _toDeactivate;
    [SerializeField] private GameObject[] _toActivate;
    public override void OnClick()
    {
        Deactivate();
        Activate();
    }

    private void Deactivate()
    {
        foreach (var gameObject in _toDeactivate)
        {
            gameObject.SetActive(false);
        }
    }

    private void Activate()
    {
        foreach (var gameObject in _toActivate)
        {
            gameObject.SetActive(true);
        }
    }

    
}

using UnityEngine;

public class AudioToggleButton : ToggleButton
{
    [SerializeField] private AudioListener _audioListener;
    public override void OnClick()
    {
        base.OnClick();
        _audioListener.enabled = _audioListener.enabled == false;
    }
}

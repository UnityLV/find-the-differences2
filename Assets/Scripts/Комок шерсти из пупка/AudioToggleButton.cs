using UnityEngine;
using UnityEngine.Audio;

public class AudioToggleButton : ToggleButton
{
    [SerializeField] private AudioMixer _audioMixer;

    private const float MinVolume = -80f;
    private const string VolumeName = "MasterVolume";
    private float _currentVolume = 0;
    public override void OnClick()
    {
        base.OnClick();

        _audioMixer.SetFloat(VolumeName,  CalculateVolume());        
    }
    private float CalculateVolume()
    {
        return _currentVolume = _currentVolume == 0 ? MinVolume : 0;
    }
}

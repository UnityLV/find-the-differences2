using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class YandexAD : MonoBehaviour
{
    public event UnityAction Rewarded;

    [DllImport("__Internal")]
    private static extern void ShowFullscreenAdv();

    [DllImport("__Internal")]
    private static extern void ShowRewardedVideo();

    public void ShowFullscreenAD()
    {
#if !UNITY_EDITOR
        ShowFullscreenAdv();
#endif
    }

    public void ShowRewardAD()
    {
#if !UNITY_EDITOR
        ShowRewardedVideo();

#endif
    }

    public void OnRewardADOpen()
    {

    }

    public void OnRewardADClose()
    {
        Rewarded?.Invoke();
    }
    
    public void OnRewardADRewarded()
    {
        


    }


}

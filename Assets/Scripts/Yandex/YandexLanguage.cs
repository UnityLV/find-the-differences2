using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class YandexLanguage : MonoBehaviour
{
    public string PlayerLanguage { get; private set; } = "en";

    public event UnityAction<string> LanguageTaken;


    [DllImport("__Internal")]
    private static extern string GetLanguage();

    private void Awake()
    {
#if UNITY_WEBGL
        PlayerLanguage = GetLanguage();
#endif
        LanguageTaken?.Invoke(PlayerLanguage);   
    }
}

using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class YandexSaves : MonoBehaviour
{  
    public event UnityAction<string> StatsLoaded;


    [DllImport("__Internal")]
    private static extern void LoadStatsExtern();  

    [DllImport("__Internal")]
    private static extern void SaveStatsExtern(string data);        

    public void SetPlayerStats(string jsonData)
    {
        StatsLoaded?.Invoke(jsonData);
    }

    public void SavePlayerStats(string jsonData)
    {
#if !UNITY_EDITOR
        SaveStatsExtern(jsonData);

#endif
    }

    public void LoadStats()
    {
#if !UNITY_EDITOR
        LoadStatsExtern();

#endif
    }

}

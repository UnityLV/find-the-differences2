using System.Runtime.InteropServices;
using UnityEngine;

public class YandexRate : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void RateForGame();

    public void GetRateForGameButton()
    {
        RateForGame();
    }
}

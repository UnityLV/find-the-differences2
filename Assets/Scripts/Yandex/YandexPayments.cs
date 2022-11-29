using System.Runtime.InteropServices;
using UnityEngine;

public class YandexPayments : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void ByBalance500();

    public void ByBalance500Button()
    {
        ByBalance500();
    }

   
    
}

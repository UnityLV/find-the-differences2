using System.Runtime.InteropServices;
using UnityEngine;

public class YandexShortcut : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void CheckIsAvalableForShortcut();

    [DllImport("__Internal")]
    private static extern void CreateShortcut();

    public void ShortcutButton()
    {
        CheckIsAvalableForShortcut();
    }

    public void TakeIsAvalableForShortcutInfo(bool avable)
    {
        if (avable)
        {
            CreateShortcut();
        }
    }

    public void ShortcutHasBeenCreate()
    {

    }
}

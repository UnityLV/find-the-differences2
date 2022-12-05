using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public const int FrameRate = 60;

    private void Awake()
    {
        Application.targetFrameRate = FrameRate;
    }
}

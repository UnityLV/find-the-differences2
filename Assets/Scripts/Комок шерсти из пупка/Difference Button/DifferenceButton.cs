using UnityEngine;
using UnityEngine.Events;

public class DifferenceButton : MonoBehaviour
{
    public event UnityAction<DifferenceButton> Pressed;
    public event UnityAction<float> EngleRandomized;
    public bool IsPressed { get; private set; }

    public void Press()
    {
        if (IsPressed == false)
        {
            IsPressed = true;
            Pressed?.Invoke(this);
        }        
    }

    public void SetRandomizedEngle(float engle)
    {
        EngleRandomized?.Invoke(engle);
    }
}



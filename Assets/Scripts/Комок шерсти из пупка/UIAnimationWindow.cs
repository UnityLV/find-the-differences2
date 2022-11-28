using LibraryForGames;
using System;
using UnityEngine;

public class UIAnimationWindow : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _fateTime;

    private const string _showTrigger = "Show";
    private const string _hideTrigger = "Hide";

    public void Show()
    {
        _animator.SetTrigger(_showTrigger);
    }

    public void Hide()
    {
        _animator.SetTrigger(_hideTrigger);
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(Tools.LateCall(Disable, _fateTime));
        }

    }

    private void Disable() => gameObject.SetActive(false);


}

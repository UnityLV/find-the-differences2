using UnityEngine;
using System.Collections.Generic;
using LibraryForGames;
using UnityEngine.Events;

public class DifferenceButtonPromt : MonoBehaviour
{
    [SerializeField] private LevelBulder _levelBulder;

    private List<DifferenceButton> _differenceButtons;

    public int PromtUsedThisLevel { get; private set; }

    public event UnityAction<DifferenceButton> ShowedPromt;

    private void OnEnable()
    {
        _levelBulder.ButtonsCreated += OnButtonsCreated;
    }

    private void OnDisable()
    {
        _levelBulder.ButtonsCreated -= OnButtonsCreated;

    }
    private void OnButtonsCreated(List<DifferenceButton> buttons)
    {
        PromtUsedThisLevel = 0;
        _differenceButtons = buttons;
        
    }

    public void ShowPromt()
    {
        if (_differenceButtons != null)
        {
            var buttons = _differenceButtons.Randomize();
            foreach (var button in buttons)
            {
                if (button.IsPressed == false)
                {
                    PromtUsedThisLevel++;
                    ShowedPromt(button);
                    return;
                }
            }
        }        
    }

}

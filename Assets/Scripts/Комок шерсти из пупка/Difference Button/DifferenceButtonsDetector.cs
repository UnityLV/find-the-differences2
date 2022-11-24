using System.Collections.Generic;
using UnityEngine;
using LibraryForGames;
using UnityEngine.Events;
using System;
using Random = UnityEngine.Random;


public class DifferenceButtonsDetector : MonoBehaviour
{
    [SerializeField] private LevelBulder _levelBulder;

    private List<DifferenceButton> _buttons;
    private List<(DifferenceButton, DifferenceButton)> _pressedbuttons = new();

    public event UnityAction<IEnumerable<(DifferenceButton, DifferenceButton)>> AllButtonPressed;

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
        _pressedbuttons.Clear();

        UnsubscribeAllButtons();

        _buttons = buttons;

        SubscribeAllButtons();
    }

    private void SubscribeAllButtons()
    {
        if (_buttons != null)
        {
            foreach (var button in _buttons)
            {
                button.Pressed += OnButtonPressed;
            }
        }
    }

    private void UnsubscribeAllButtons()
    {
        if (_buttons != null)
        {
            foreach (var button in _buttons)
            {
                button.Pressed -= OnButtonPressed;
            }
        }
    }

    private void OnButtonPressed(DifferenceButton button)
    {
        button.Pressed -= OnButtonPressed;

        TryPressSameButton(button);

        CheckIsAllFindet();

    }

    private void TryPressSameButton(DifferenceButton targetButton)
    {        
        if (TryFindSameButton(targetButton,out DifferenceButton sameButton) && sameButton.IsPressed == false)
        {
            TryRandomRotateButtons(targetButton, sameButton);

            sameButton.Press();

            _pressedbuttons.Add((sameButton, targetButton));
        }


        bool TryFindSameButton(DifferenceButton searchedButton, out DifferenceButton sameButton)
        {
            foreach (var button in _buttons)
            {
                if (button.transform.localPosition.IsAlmostEquals(searchedButton.transform.localPosition, 3f) &&
                        searchedButton != button)
                {
                    sameButton = button;
                    return true;
                }
            }
            sameButton = default;
            return false;
        }

        void SetBothRandomEngle(DifferenceButton targetButton, DifferenceButton button)
        {
            float randomEngle = Random.Range(0, 360);
            targetButton.SetRandomizedEngle(randomEngle);
            button.SetRandomizedEngle(randomEngle);
        }

        void TryRandomRotateButtons(DifferenceButton targetButton, DifferenceButton sameButton)
        {
            bool isTargetButtonRound = targetButton.transform.localScale.x == targetButton.transform.localScale.y;
            bool isSameButtonRound = sameButton.transform.localScale.x == sameButton.transform.localScale.y;
            if (isTargetButtonRound && isSameButtonRound)
            {
                SetBothRandomEngle(targetButton, sameButton);
            }
        }
    }    

    private void CheckIsAllFindet()
    {
        if (IsAllPressed())
        {
            Debug.Log("All pressed");
            AllButtonPressed?.Invoke(_pressedbuttons);
        }
    }

    private bool IsAllPressed()
    {
        return _pressedbuttons.Count == _buttons.Count / 2;
    }

}



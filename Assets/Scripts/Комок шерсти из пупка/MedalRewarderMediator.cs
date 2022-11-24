using System;
using System.Collections.Generic;
using UnityEngine;

public class MedalRewarderMediator : MonoBehaviour
{
    [SerializeField] private LevelFinalizer _levelFinalizer;
    [SerializeField] private PlayerSaves _playerSaves;
    [SerializeField] private LevelButtonSpawner _buttonSpawner;
    [SerializeField] private DifferenceButtonPromt _buttonPromt;

    private List<LevelProgress> _levelProgresses;
    private IEnumerable<LevelButton> _buttons;

    private void OnEnable()
    {
        _levelFinalizer.LevelSolved += OnLevelSolved;
        _playerSaves.Loaded += OnLoaded;
        _buttonSpawner.ButtonsSpawned += OnButtonsSpawned;
    }    

    private void OnDisable()
    {
        _levelFinalizer.LevelSolved -= OnLevelSolved;
        _playerSaves.Loaded -= OnLoaded;
        _buttonSpawner.ButtonsSpawned -= OnButtonsSpawned;

    }

    private void OnLevelSolved(int levelIndex)
    {
        foreach (var button in _buttonSpawner.Buttons)
        {
            if (button.Index == levelIndex)
            {
                button.SetMedal(CalcuiateMedal(_buttonPromt.PromtUsedThisLevel));
                _playerSaves.SetLevelMedal(levelIndex, Medals.Gold);
            }
        }             
    }

    private Medals CalcuiateMedal(int promtUsed)
    {
        switch (promtUsed)
        {
            case 0: return Medals.Gold;
            case 1: return Medals.Silver;
            case 2: return Medals.Silver;
            case 3: return Medals.Bronze;
            default: return Medals.Non;                
        }
    }

    private void OnLoaded(PlayerProgress progres)
    {
        _levelProgresses = progres.LevelsCompleat;
        TrySetMedals();
    }

    private void OnButtonsSpawned(IEnumerable<LevelButton> buttons)
    {
        _buttons = buttons;
        TrySetMedals();
    }

    private void TrySetMedals()
    {
        if (_buttons != null && _levelProgresses != null)
        {
            SetMedals();
        }
    }

    private void SetMedals()
    {        
        foreach (var button in _buttonSpawner.Buttons)
        {
            foreach (var levels in _levelProgresses)
            {
                if (button.Index == levels.Index)
                {
                    button.SetMedal((Medals)levels.Medal);                    
                }
            }            
        }        
    }
}

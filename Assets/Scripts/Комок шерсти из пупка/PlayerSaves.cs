using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSaves : MonoBehaviour
{
    [SerializeField] private PlayerCurrency _playerCurrency;
    [SerializeField] private YandexSaves _yandexSaves;

    private PlayerProgress _playerProgress = new();


    public event UnityAction<PlayerProgress> Loaded;


    private void OnEnable()
    {
        _yandexSaves.StatsLoaded += OnStatsLoaded;
        _playerCurrency.AmountUppdate += OnAmountUppdate;

        _yandexSaves.LoadStats();
    }


    private void OnDisable()
    {
        _yandexSaves.StatsLoaded -= OnStatsLoaded;
        _playerCurrency.AmountUppdate -= OnAmountUppdate;


    }

    private void OnStatsLoaded(string JSONdata)
    {
        _playerProgress = JsonUtility.FromJson<PlayerProgress>(JSONdata);

        _playerCurrency.SetAmount(_playerProgress.Money);

        Loaded?.Invoke(_playerProgress);
    }

    private void OnAmountUppdate(int money)
    {
        _playerProgress.Money = money;        
    }

    public void SetLevelMedal(int levelIndex, Medals medal)
    {        
        if (IsLevelExistInSaves(levelIndex,out LevelProgress levelProgres ))
        {
            levelProgres.Medal = (int)medal;
        }
        else
        {
            _playerProgress.LevelsCompleat.Add(new LevelProgress(levelIndex, (int)medal));
        }
        
    }

    private bool IsLevelExistInSaves(int levelIndex,out LevelProgress targetLevel)
    {
        foreach (var level in _playerProgress.LevelsCompleat)
        {
            if (level.Index == levelIndex)
            {
                targetLevel = level;
                return true;
            }
        }
        targetLevel = default;
        return false;
    }

    public void Save()
    {
        SaveYandexData();
    }

    private void SaveYandexData() => _yandexSaves.SavePlayerStats(JsonUtility.ToJson(_playerProgress));
}

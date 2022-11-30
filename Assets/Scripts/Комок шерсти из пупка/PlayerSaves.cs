using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSaves : MonoBehaviour
{
    [SerializeField] private PlayerCurrency _playerCurrency;
    [SerializeField] private YandexSaves _yandexSaves;

    private PlayerProgress _playerProgress = new();

    private string _currencyKey = "Currency";

    public event UnityAction<PlayerProgress> Loaded;

    private void Start()
    {  
        LoadPrefs();
    }

    public void SetLevelMedal(int levelIndex, Medals medal)
    {
        _playerProgress.LevelsCompleat.Add(new LevelProgress(levelIndex,(int)medal));        
    }    

    private void LoadPrefs()
    {
        int levelsToSearch = 40;

        for (int i = 0; i < levelsToSearch; i++)
        {
            if (PlayerPrefs.HasKey($"Level{i}"))
            {
                _playerProgress.LevelsCompleat.Add(new LevelProgress(i, PlayerPrefs.GetInt($"Level{i}")));
            }
            else
            {
                _playerProgress.LevelsCompleat.Add(new LevelProgress(i, 0));
            }
            
        }

        if (PlayerPrefs.HasKey(_currencyKey))
        {
            _playerCurrency.SetAmount(PlayerPrefs.GetInt(_currencyKey));            
        }
        Loaded?.Invoke(_playerProgress);
    }

    public void Save()
    {
        PlayerPrefs.SetInt(_currencyKey, _playerCurrency.Amount);
        foreach (var level in _playerProgress.LevelsCompleat)
        {            
            PlayerPrefs.SetInt($"Level{level.Index}", level.Medal);            
        }

        Loaded?.Invoke(_playerProgress);
        PlayerPrefs.Save();
    }
}

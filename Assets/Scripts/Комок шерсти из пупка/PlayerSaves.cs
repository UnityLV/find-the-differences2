using UnityEngine;
using UnityEngine.Events;

public class PlayerSaves : MonoBehaviour
{
    private PlayerProgress _playerProgress = new();

    public event UnityAction<PlayerProgress> Loaded;

    private void Start()
    {
        LoadPrefs();       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearData();
        }
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
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
        Loaded?.Invoke(_playerProgress);
    }

    public void SavePrefs()
    {
        foreach (var level in _playerProgress.LevelsCompleat)
        {            
            PlayerPrefs.SetInt($"Level{level.Index}", level.Medal);            
        }

        Loaded?.Invoke(_playerProgress);
        PlayerPrefs.Save();
    }
}

using UnityEngine;
using UnityEngine.Events;

public sealed class NextLevelLoader : MonoBehaviour
{    
    [SerializeField] private Levels _levels;
    [SerializeField] private LevelStarter _levelStarter;

    [SerializeField] private RectTransform _levelsMenu;

    private int _currentLevelIndex = -1;    

    public event UnityAction AllLevelsEnded;
    public int CurrentLevelIndex => _currentLevelIndex;

    public void StartLevel(int levelIndex)
    {
        _currentLevelIndex = levelIndex;

        _levelStarter.StartLevel(_levels[_currentLevelIndex]);
    }

    public void LoadNextLevel()
    {
        _currentLevelIndex++;

        if (_currentLevelIndex < _levels.Length)
        {
            StartLevel(_currentLevelIndex);
        }
        else
        {
            ProcessEndAllLevels();
        }
    }

    private void ProcessEndAllLevels()
    {
        Debug.Log("Is last LEVEL");
        _levelsMenu.gameObject.SetActive(true);
        AllLevelsEnded?.Invoke();
    }
}

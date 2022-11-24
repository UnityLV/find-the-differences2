using UnityEngine;
using UnityEngine.Events;

public sealed class NextLevelLoader : MonoBehaviour
{    
    [SerializeField] private Levels _levels;
    [SerializeField] private LevelStarter _levelStarter;

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
            Debug.Log("Is last LEVEL");
            AllLevelsEnded?.Invoke();
        }
    }    
}

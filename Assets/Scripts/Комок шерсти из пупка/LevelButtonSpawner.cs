using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelButtonSpawner : MonoBehaviour
{
    [SerializeField] private NextLevelLoader _levelLoader;
    [SerializeField] private LevelButton _levelButtonTeplate;
    [SerializeField] private Levels _levels;

    [SerializeField] private RectTransform _buttonsParrent;

    private List<LevelButton> _buttons = new();
    private List<LevelButtonView> _buttonsView = new();
    public IEnumerable<LevelButton> Buttons => _buttons;
    public IEnumerable<LevelButtonView> ButtonsView => _buttonsView;

    public event UnityAction<IEnumerable<LevelButton>> ButtonsSpawned;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int levelIndex = 0; levelIndex < _levels.Length; levelIndex++)
        {
            var button = Instantiate(_levelButtonTeplate, _buttonsParrent);
            button.Init(_levelLoader, levelIndex);

            _buttons.Add(button);
            _buttonsView.Add(button.GetComponent<LevelButtonView>());
        }
        ButtonsSpawned?.Invoke(_buttons);
    }

}

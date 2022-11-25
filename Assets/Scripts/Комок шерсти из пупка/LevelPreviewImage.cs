using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public sealed class LevelPreviewImage : MonoBehaviour
{
    [SerializeField] private RectTransform _buttonsParrent;
    [SerializeField] private LevelButtonSpawner _levelButtonSpawner;

    [SerializeField] private Levels _levels;

    private ImageWebDownloader _imageDownloader = new();

    private void OnEnable()
    {
        _levelButtonSpawner.ButtonsSpawned += OnButtonsSpawned;
    }

    private void OnDisable()
    {
        _levelButtonSpawner.ButtonsSpawned -= OnButtonsSpawned;
    }

    private void OnButtonsSpawned(IEnumerable<LevelButton> _)
    {
        SetButtonsImages(_levelButtonSpawner.ButtonsView.ToArray());
    }

    private void SetButtonsImages(LevelButtonView[] levelButtons)
    {        
        for (int levelIndex = 0; levelIndex < _levels.Length; levelIndex++)
        {
            if (levelIndex == levelButtons[levelIndex].Button.Index)
            {
                if (_levels[levelIndex].Image1Url != string.Empty)
                {
                    StartCoroutine(_imageDownloader.SetImage(_levels[levelIndex].Image1Url, levelButtons[levelIndex].SetLevelSprite));
                }
                else
                {
                    levelButtons[levelIndex].SetLevelSprite(_levels[levelIndex].Image1);
                }
            }            
        }        
    }
}

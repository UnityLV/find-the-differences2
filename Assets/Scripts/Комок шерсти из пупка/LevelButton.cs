using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button),typeof(LevelButtonView))]
public class LevelButton : MonoBehaviour
{
    private NextLevelLoader _levelLoader;
    private Button _thisButton;
    public Medals Medal { get; private set; }

    public event UnityAction<Medals> MedalInstalled;
    public int Index { get; private set; }

    private void Awake()
    {
        _thisButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _thisButton.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        _thisButton.onClick.RemoveListener(OnClick);
    }

    public void Init(NextLevelLoader nextLevelLoader,int levelIndex)
    {
        _levelLoader = nextLevelLoader;
        Index = levelIndex;
    }

    private void OnClick()
    {
        _levelLoader.StartLevel(Index);
    }

    public void SetMedal(Medals medal)
    {
        Medal = medal;
        MedalInstalled?.Invoke(Medal);
    }
}

using LibraryForGames;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonView : MonoBehaviour
{
    [SerializeField] private LevelButton _levelButton;
    [SerializeField] private MedalsSprites _medalsSprites;
    [SerializeField] private Image _medalImage;    
    [SerializeField] private Image _levelImage;

    [SerializeField] private Animator _animator;

    private readonly string _showAnimationTrigger = "Show";
    public LevelButton Button => _levelButton;

    private void OnEnable()
    {
        _levelButton.MedalInstalled += OnMedalInstalled;        
    }

    private void OnDisable()
    {
        _levelImage.gameObject.transform.localScale = Vector3.one;

    }

    private void OnDestroy()
    {
        _levelButton.MedalInstalled -= OnMedalInstalled;        
    }

    private void OnMedalInstalled(Medals medal)
    {
        if (_medalImage.gameObject.activeSelf == false)        
            _medalImage.gameObject.SetActive(true);
        
        _medalImage.sprite = _medalsSprites[medal];
    }

    public void SetLevelSprite(Sprite sprite)
    {
        _levelImage.sprite = sprite;
        _animator.SetTrigger(_showAnimationTrigger);
    }    
}


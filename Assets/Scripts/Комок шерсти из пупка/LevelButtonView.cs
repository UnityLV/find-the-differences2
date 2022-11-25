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

    public LevelButton Button => _levelButton;

    private void OnEnable()
    {
        _levelButton.MedalInstalled += OnMedalInstalled;
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
        if (gameObject.activeSelf)
        {
            StartCoroutine(LevelImageAnimation());
        }
    }

    private IEnumerator LevelImageAnimation()
    {
        Vector3 startScale = 0.1f * Vector3.one;
        float speed = 20f;
        _levelImage.transform.localScale = startScale;

        while (Tools.IsAlmostEquals(_levelImage.transform.localScale, Vector3.one) == false)
        {
            _levelImage.transform.localScale = Vector3.Lerp(
                _levelImage.transform.localScale, Vector3.one, Time.deltaTime * speed);
            yield return null;
        }
        _levelImage.transform.localScale = Vector3.one;
    }
}

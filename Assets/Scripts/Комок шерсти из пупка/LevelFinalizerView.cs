using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public sealed class LevelFinalizerView : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LevelFinalizer _levelFinalizer;
    [SerializeField] private GameObject _starEffect;

    [SerializeField] private RectTransform _endLevelMenu;
    [SerializeField] private RectTransform _startParrent;

    private readonly float _particleLifeTime = 3f;
    private readonly WaitForSeconds _startEffectsDelay = new(1);

    private void OnEnable()
    {
        _levelFinalizer.HighlightAllButtons += OnHighlightAllButtons;
        _levelFinalizer.LevelEndedByForse += OnLevelEndedByForse;
    }    

    private void OnDisable()
    {
        _levelFinalizer.HighlightAllButtons -= OnHighlightAllButtons;
        _levelFinalizer.LevelEndedByForse -= OnLevelEndedByForse;
    }

    private void OnHighlightAllButtons(IEnumerable<(DifferenceButton, DifferenceButton)> buttons)
    {
        StartCoroutine(StartChainAnimation(buttons));
    }

    private void OnLevelEndedByForse(int levelIndex)
    {
        StopAllCoroutines();
    }

    private IEnumerator StartChainAnimation(IEnumerable<(DifferenceButton, DifferenceButton)> buttons)
    {
        yield return _startEffectsDelay;     

        foreach (var buttonsСortege in buttons.ToList())
        {            
            CreateStarEffect(buttonsСortege.Item1);
            CreateStarEffect(buttonsСortege.Item2);
            yield return _startEffectsDelay;
        }
        OnStartEffectDone();
    }

    private void OnStartEffectDone()
    {
        _endLevelMenu.gameObject.SetActive(true);
    }

    private void CreateStarEffect(DifferenceButton button)
    {
        var position = button.transform.position;
        var effect = Instantiate(_starEffect, position, Quaternion.identity, _startParrent);
        effect.transform.localScale = button.transform.localScale;
        
        Destroy(effect.gameObject, _particleLifeTime);
    }
}

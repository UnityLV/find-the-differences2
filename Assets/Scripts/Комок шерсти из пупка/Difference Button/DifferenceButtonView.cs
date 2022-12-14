using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using LibraryForGames;
using Random = UnityEngine.Random;

public class DifferenceButtonView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private DifferenceButton _button;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _openSounds;

    [SerializeField] private AnimationCurve _openImageSpeed;
    [SerializeField] private AnimationCurve _alphaImageSpeed;
    [SerializeField] private float _openedSpeed;

    private void OnEnable()
    {
        _button.Pressed += OnPressed;
        _button.EngleRandomized += OnEngleRandomized;
    }


    private void OnDisable()
    {
        _button.Pressed -= OnPressed;
        _button.EngleRandomized -= OnEngleRandomized;
    }

    private void OnPressed(DifferenceButton button)
    {
        float randomPich = Random.Range(0.7f, 1f);
        _audioSource.pitch = randomPich;
        _audioSource.PlayOneShot(_openSounds.RandomElement());
        StartCoroutine(OpenImage());
    }

    private void OnEngleRandomized(float engle)
    {
        _image.rectTransform.Rotate(new Vector3(0, 0, engle));
    }

    private IEnumerator OpenImage()
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * _openedSpeed;
            ChangeImageBy(time);

            yield return null;
        }
    }

    private void ChangeImageBy(float time)
    {
        _image.fillAmount = _openImageSpeed.Evaluate(time);

        _image.color = new Color(1, 1, 1, _alphaImageSpeed.Evaluate(time));
    }
}

using LibraryForGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class LevelBulder : MonoBehaviour
{
    [SerializeField] private DifferenceButton _differenceButtonPrefab;
    [SerializeField] private Canvas _canvas;

    [SerializeField] private Image _imageInCanvas1;
    [SerializeField] private Image _imageInCanvas2;

    [SerializeField] private ZoomApplier _zoomApplier1;
    [SerializeField] private ZoomApplier _zoomApplier2;
    [SerializeField] private DragAndDropDetector _dragAndDropDetector;

    private List<DifferenceButton> _currentDifferenceButtons;
    private ImageWebDownloader _imageWebDownloader = new();

    public event UnityAction<List<DifferenceButton>> ButtonsCreated;

    public void BuildLevel(Level level)
    {
        StopAllCoroutines();

        ResetZoom();

        MoveImageInStartPosition();
        TryClearPreviusButtons();
        PlaceImages(level);
        CreateDifferenceButtons(level);
    }

    private void MoveImageInStartPosition()
    {
        //_dragAndDropDetector.SlideUp();
    }

    private void ResetZoom()
    {
        _zoomApplier1.ResetZoom();
        _zoomApplier2.ResetZoom();
    }

    private void PlaceImages(Level level)
    {
        if (level.Image1 == null || level.Image2 == null)
        {
            StartCoroutine(_imageWebDownloader.SetImage(level.Image1Url, SetSpriteOnImage1));
            StartCoroutine(_imageWebDownloader.SetImage(level.Image2Url, SetSpriteOnImage2));

            LoadingAnimation loadingAnimation1 = _imageInCanvas1.GetComponentInChildren<LoadingAnimation>();
            loadingAnimation1.Enable();

            LoadingAnimation loadingAnimation2 = _imageInCanvas2.GetComponentInChildren<LoadingAnimation>();
            loadingAnimation2.Enable();
        }
        else
        {
            _imageInCanvas1.sprite = level.Image1;
            _imageInCanvas2.sprite = level.Image2;
        }
    }

    private void SetSpriteOnImage1(Sprite sprite)
    {
        LoadingAnimation loadingAnimation1 = _imageInCanvas1.GetComponentInChildren<LoadingAnimation>();
        loadingAnimation1.Disable();

        _imageInCanvas1.sprite = sprite;

        StartCoroutine(ImageAnimation(_imageInCanvas1));
    }

    private void SetSpriteOnImage2(Sprite sprite)
    {
        LoadingAnimation loadingAnimation2 = _imageInCanvas2.GetComponentInChildren<LoadingAnimation>();
        loadingAnimation2.Disable();

        _imageInCanvas2.sprite = sprite;

        StartCoroutine(ImageAnimation(_imageInCanvas2));
    }

    private IEnumerator ImageAnimation(Image image)
    {
        Vector3 startScale = 0.1f * Vector3.one;
        float speed = 20f;
        image.transform.localScale = startScale;

        while (Tools.IsAlmostEquals(image.transform.localScale, Vector3.one) == false)
        {
            image.transform.localScale = Vector3.Lerp(image.transform.localScale, Vector3.one, Time.deltaTime * speed);
            yield return null;
        }

        image.transform.localScale = Vector3.one;
    }

    private void CreateDifferenceButtons(Level level)
    {
        List<DifferenceButton> differenceButtons = new();

        var newButtons1 = PlaceDifferencesButtonsOnImage(_imageInCanvas1, level.DifferenceButtonConfigs);
        var newButtons2 = PlaceDifferencesButtonsOnImage(_imageInCanvas2, level.DifferenceButtonConfigs);

        differenceButtons.AddRange(newButtons1);
        differenceButtons.AddRange(newButtons2);

        _currentDifferenceButtons = differenceButtons;
        ButtonsCreated?.Invoke(_currentDifferenceButtons);
    }

    private void TryClearPreviusButtons()
    {
        if (_currentDifferenceButtons != null)
        {
            ClearPreviusButtons();
        }
    }

    private void ClearPreviusButtons()
    {
        foreach (var button in _currentDifferenceButtons)
        {
            Destroy(button.gameObject);
        }
    }

    private IEnumerable<DifferenceButton> PlaceDifferencesButtonsOnImage(
        Image image, IEnumerable<DifferenceButtonConfig> configs)
    {
        foreach (var config in configs)
        {
            Vector2 worldPosition = CalculateWorldPosition(image.rectTransform, config.Position);

            var differenceButton = Instantiate(_differenceButtonPrefab, worldPosition, Quaternion.identity,
                image.rectTransform);

            differenceButton.transform.localScale = config.Scale;

            yield return differenceButton;
        }
    }

    private Vector2 CalculateWorldPosition(RectTransform transform, Vector2 normalizedPosition)
    {
        Vector2 leftMin = transform.rect.min * _canvas.transform.localScale + (Vector2)transform.position;
        Vector2 rightMax = transform.rect.max * _canvas.transform.localScale + (Vector2)transform.position;

        float normalWidnt = (rightMax.x - leftMin.x) * normalizedPosition.x;
        float normalHight = (rightMax.y - leftMin.y) * normalizedPosition.y;

        return new Vector2(normalWidnt, normalHight) + leftMin;
    }
}
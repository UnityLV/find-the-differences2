using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelStarter : MonoBehaviour
{
    [SerializeField] private RectTransform[] _uiElementToDisable;
    [SerializeField] private RectTransform[] _uiElementToEneble;
    [SerializeField] private LevelBulder _levelBulder;

    [SerializeField] private Image _topImageInCanvas;
    [SerializeField] private Image _bottomImageInCanvas;

    [SerializeField] private ZoomDetector _zoomDetectorTop;
    [SerializeField] private ZoomDetector _zoomDetectorBottom;

    [SerializeField] private Sprite _defaulSprite;

    public void StartLevel(Level level)
    {
        DisableUI();
        EnebleUI();
        EnebleCanvasImages();
        UnlockZoom();

        _levelBulder.BuildLevel(level);
    }

    private void EnebleUI()
    {        
        foreach (var uiElement in _uiElementToEneble)
        {
            uiElement.gameObject.SetActive(false);
        }
    }

    private void DisableUI()
    {
        foreach (var uiElement in _uiElementToDisable)
        {
            uiElement.gameObject.SetActive(false);
        }
    }

    private void EnebleCanvasImages()
    {
        _topImageInCanvas.color = Color.white;
        _bottomImageInCanvas.color = Color.white;

        _topImageInCanvas.sprite = _defaulSprite;
        _bottomImageInCanvas.sprite = _defaulSprite;
    }

   
    private void UnlockZoom()
    {
        _zoomDetectorTop.UnlockZoom();
        _zoomDetectorBottom.UnlockZoom();
    }
}
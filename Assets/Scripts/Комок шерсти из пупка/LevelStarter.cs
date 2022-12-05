using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelStarter : MonoBehaviour
{
    [SerializeField] private RectTransform[] _uiElementToDisable;
    [SerializeField] private RectTransform[] _uiElementToEneble;
    [SerializeField] private UIAnimationWindow[] _uiElementsToAnimation;
    
    [SerializeField] private LevelBulder _levelBulder;

    [SerializeField] private Image _topImageInCanvas;
    [SerializeField] private Image _bottomImageInCanvas;

    [SerializeField] private ZoomApplier _zoomApplierTop;
    [SerializeField] private ZoomApplier _zoomApplierBottom;

    [SerializeField] private Sprite _defaulSprite;
    [SerializeField] private YandexAD _yandexAD;

    public void StartLevel(Level level)
    {
        _yandexAD.ShowFullscreenAD();

        DisableUI();
        EnebleUI();
        HideAnimatedUI();
        EnebleCanvasImages();
        UnlockZoom();

        _levelBulder.BuildLevel(level);
    }

    private void DisableUI()
    {
        foreach (var uiElement in _uiElementToDisable)
        {
            uiElement.gameObject.SetActive(false);
        }
    }

    private void EnebleUI()
    {        
        foreach (var uiElement in _uiElementToEneble)
        {
            uiElement.gameObject.SetActive(true);
        }
    }    

    private void HideAnimatedUI()
    {
        foreach (var element in _uiElementsToAnimation)
        {
            element.Hide();
        }
    }

    private void EnebleCanvasImages()
    {
        _topImageInCanvas.sprite = _defaulSprite;
        _bottomImageInCanvas.sprite = _defaulSprite;
    }

   
    private void UnlockZoom()
    {
        _zoomApplierTop.UnlockZoom();
        _zoomApplierBottom.UnlockZoom();
    }
}
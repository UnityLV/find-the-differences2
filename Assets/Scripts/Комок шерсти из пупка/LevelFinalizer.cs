using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelFinalizer : MonoBehaviour
{
    [SerializeField] private ZoomApplier _imageTop;
    [SerializeField] private ZoomApplier _imageBottom;

    [SerializeField] private ZoomDetector _zoomDetectorTop;
    [SerializeField] private ZoomDetector _zoomDetectorBottom;

    [SerializeField] private DifferenceButtonsDetector _differenceButtonsDetector;
    [SerializeField] private NextLevelLoader _levelLoader;

    [SerializeField] private PlayerSaves _playerSaves;    

    public event UnityAction<IEnumerable<(DifferenceButton, DifferenceButton)>> FocusOnAllButtons;

    public event UnityAction<int> LevelSolved;
    public event UnityAction<int> LevelEndedByForse;

    private void OnEnable()
    {
        _differenceButtonsDetector.AllButtonPressed += OnAllButtonPressed;
    }

    private void OnDisable()
    {
        _differenceButtonsDetector.AllButtonPressed -= OnAllButtonPressed;
    }

    private void OnAllButtonPressed(IEnumerable<(DifferenceButton, DifferenceButton)> buttons)
    {
        FocusOnAllButtons?.Invoke(buttons);

        EndLevel();
    }

    private void EndLevel()
    {
        ResetZoom();

        BlockZoom();

        LevelSolved?.Invoke(_levelLoader.CurrentLevelIndex);

        _playerSaves.Save();
    }

    public void ForsedEndLevel()
    {
        LevelEndedByForse?.Invoke(_levelLoader.CurrentLevelIndex);
    }

    private void BlockZoom()
    {
        _zoomDetectorTop.BlockZoom();
        _zoomDetectorBottom.BlockZoom();
    }

    private void ResetZoom()
    {
        _imageTop.ResetZoom();
        _imageBottom.ResetZoom();
    }
}

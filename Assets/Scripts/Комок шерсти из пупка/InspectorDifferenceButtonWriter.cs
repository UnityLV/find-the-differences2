using LibraryForGames;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectorDifferenceButtonWriter : MonoBehaviour
{
    [SerializeField] private Image _topImage;
    [SerializeField] private Image _bottomImage;

    [SerializeField] private Level _level;

    public void LogAllButtonsInfo()
    {
        var calculators = FindObjectsOfType<NormalizedPositionCalculator>();        

        _level.SetConfigs(ConvertToConfig(calculators));

        _level.SetImages(_topImage, _bottomImage);
    }    

    private IEnumerable<DifferenceButtonConfig> ConvertToConfig(NormalizedPositionCalculator[] calculators)
    {
        foreach (var calculator in calculators)
        {
            var config = calculator.CalculateNormalisedPosition();
            yield return new DifferenceButtonConfig(config.position, config.scale);
        }
    }
}

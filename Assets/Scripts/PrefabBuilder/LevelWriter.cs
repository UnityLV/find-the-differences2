using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LevelWriter : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private PictureHandler _pictureHandler;

    public void WriteLvel()
    {
        StartCoroutine(Handle());
    }
    private IEnumerator Handle()
    {
        yield return _pictureHandler.SetImages();
        _level.SetConfigs(CalculateConfigs(_pictureHandler.GetDifferences()));
        _level.SetUrls(_pictureHandler.FirstUrl, _pictureHandler.SecondUrl);
    }

    private IEnumerable<DifferenceButtonConfig> CalculateConfigs(IEnumerable<Difference> differences)
    {
        foreach (var difference in differences)
        {
            yield return difference.GetConfig();
        }
    }
}

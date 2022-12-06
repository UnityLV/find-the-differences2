using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
    [SerializeField] private DifferenceButtonConfig[] _differenceButtonConfigs;
    
    [field: SerializeField] public Sprite Image1 { get; private set; }
    [field: SerializeField] public Sprite Image2 { get; private set; }    

    [field: SerializeField] public string Image1Url { get; private set; }
    [field: SerializeField] public string Image2Url { get; private set; }   
    [field: SerializeField] public string PreviewUrl { get; private set; }   



    public IEnumerable<DifferenceButtonConfig> DifferenceButtonConfigs => _differenceButtonConfigs;

    public void SetConfigs(IEnumerable<DifferenceButtonConfig> enumerable)
    {
        _differenceButtonConfigs = enumerable.ToArray();
    }

    public void SetImages(Image topImage, Image bottomImage)
    {
        Image1 = topImage.sprite;
        Image2 = bottomImage.sprite;
    }

    public void SetUrls(string url1, string url2)
    {
        Image1Url = url1;
        Image2Url = url2;
    }
    public void SetImage1(Sprite sprite) => Image1 = sprite;

    public void SetImage2(Sprite sprite) => Image2 = sprite;

}

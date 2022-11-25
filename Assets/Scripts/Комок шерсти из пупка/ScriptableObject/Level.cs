﻿using System.Collections.Generic;
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

    public IEnumerable<DifferenceButtonConfig> DifferenceButtonConfigs => _differenceButtonConfigs;    

    public void SetConfigs(IEnumerable<DifferenceButtonConfig> config)
    {
        //_differenceButtonConfigs = config.ToArray();
    }

    public void SetImages(Image imageTop,Image imageBottom)
    {
        //Image1 = imageTop.sprite;
        //Image2 = imageBottom.sprite;
    }

    public void SetImage1(Sprite sprite) => Image1 = sprite;

    public void SetImage2(Sprite sprite) => Image2 = sprite;

}

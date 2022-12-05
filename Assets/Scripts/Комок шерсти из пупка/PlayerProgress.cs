using System.Collections.Generic;

[System.Serializable]
public class PlayerProgress
{
    public int Money;
    public List<LevelProgress> LevelsCompleat = new();       
}

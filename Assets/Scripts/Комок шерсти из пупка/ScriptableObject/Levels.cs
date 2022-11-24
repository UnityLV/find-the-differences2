using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "ScriptableObjects/Levels", order = 1)]
public class Levels : ScriptableObject
{
    [SerializeField] private Level[] _allLevels;

    public Level this[int index] => _allLevels[index];

    public int Length => _allLevels.Length;


}

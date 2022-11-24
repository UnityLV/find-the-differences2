using UnityEngine;

[CreateAssetMenu(fileName = nameof(MedalsSprites), menuName = "ScriptableObjects/" + nameof(MedalsSprites), order = 1)]
public class MedalsSprites : ScriptableObject
{
    [SerializeField] private Sprite[] _sprites;

     public Sprite this[Medals medals] => _sprites[(int)medals];

}

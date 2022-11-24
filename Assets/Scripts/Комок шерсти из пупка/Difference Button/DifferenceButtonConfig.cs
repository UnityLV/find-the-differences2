using UnityEngine;

[System.Serializable]
public class DifferenceButtonConfig
{
    [field: SerializeField] public Vector2 Position { get; private set; }

    [field: SerializeField] public Vector2 Scale { get; private set; } = Vector2.one;

    public DifferenceButtonConfig(Vector2 position, Vector2 scale)
    {
        Position = position;
        Scale = scale;
    }
}

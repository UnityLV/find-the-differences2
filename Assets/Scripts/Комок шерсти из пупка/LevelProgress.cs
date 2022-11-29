public class LevelProgress
{
    public int Index { get; }
    public int Medal { get; set; }

    public LevelProgress(int index, int medal)
    {
        Index = index;
        Medal = medal;
    }
}

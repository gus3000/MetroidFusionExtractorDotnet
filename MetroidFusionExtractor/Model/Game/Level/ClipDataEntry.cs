namespace MetroidFusionExtractor.Model.Game.Level;

public class ClipDataEntry
{
    public int X { get; init; }
    public int XMax { get; init; }
    public int Y { get; init; }

    public int YMax { get; init; }

    // public ClipDataBehavior ClipDataBehavior { get; init; }
    public ClipDataBlockBehavior ClipDataBlockBehavior { get; init; }
}
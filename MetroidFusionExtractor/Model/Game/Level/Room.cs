namespace MetroidFusionExtractor.Model.Game.Level;

public class Room
{
    public const int BlocksInMapWidth = 0xF;
    public const int BlocksInMapHeight = 0xA;

    public const int BlocksInMapMargin = 0x4;
    public int BlockWidth { get; init; }
    public int BlockHeight { get; init; }

    public int MapWidth => (BlockWidth - BlocksInMapMargin) / BlocksInMapWidth;
    public int MapHeight => (BlockHeight - BlocksInMapMargin) / BlocksInMapHeight;

    public int MapX { get; init; }
    public int MapY { get; init; }

    public Block[,] Blocks { get; init; }
}
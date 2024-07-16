using MetroidFusionExtractor.Model.Game.Level;
using MetroidFusionExtractor.Model.Memory.RomStruct;
using MetroidFusionExtractor.Model.Services.Memory;

namespace MetroidFusionExtractor.Model.Services.Game;

public class RoomFactory
{
    private readonly ClipDataFactory _clipDataFactory;
    private readonly RomService _romService;
    private readonly TilesetFactory _tilesetFactory;

    public RoomFactory(
        ClipDataFactory clipDataFactory,
        TilesetFactory tilesetFactory,
        RomService romService
    )
    {
        _clipDataFactory = clipDataFactory;
        _tilesetFactory = tilesetFactory;
        _romService = romService;
    }

    public Room Build(RomRoomEntry romRoomEntry)
    {
        var blockWidth = _romService.Read(romRoomEntry.clipDataPointer);
        var blockHeight = _romService.Read(romRoomEntry.clipDataPointer + 1);
        Console.WriteLine($"\troom is {blockWidth} by {blockHeight}");

        var tileset = _tilesetFactory.Build(romRoomEntry.tileset);

        var clipData = _clipDataFactory.Build(romRoomEntry.clipDataPointer);

        var blocks = new Block[blockWidth, blockHeight];
        for (var y = 0; y < blockHeight; y++)
        for (var x = 0; x < blockWidth; x++)
            blocks[x, y] = new Block
            {
                ClipData = clipData[x, y]
            };

        var room = new Room
        {
            BlockHeight = blockHeight,
            BlockWidth = blockWidth,
            MapX = romRoomEntry.mapXCoordinate,
            MapY = romRoomEntry.mapYCoordinate,
            Blocks = blocks
        };

        return room;
    }
}
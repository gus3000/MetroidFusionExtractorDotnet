using MetroidFusionExtractor.Model.Game.Level;
using MetroidFusionExtractor.Model.Memory.RomStruct;
using MetroidFusionExtractor.Model.Services.Memory;

namespace MetroidFusionExtractor.Model.Services.Game;

public class RoomFactory
{
    private readonly BgFactory _bgFactory;
    private readonly ClipDataFactory _clipDataFactory;
    private readonly RomService _romService;
    private readonly TilesetFactory _tilesetFactory;

    public RoomFactory(
        BgFactory bgFactory,
        ClipDataFactory clipDataFactory,
        TilesetFactory tilesetFactory,
        RomService romService
    )
    {
        _bgFactory = bgFactory;
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

        // var bg0 = _bgFactory.Build(romRoomEntry.bg0Pointer, romRoomEntry.bg0Properties, blockWidth, blockHeight);
        var clipData = _clipDataFactory.Build(romRoomEntry.clipDataPointer);

        var blocks = new Block[blockWidth, blockHeight];
        for (var y = 0; y < blockHeight; y++)
        for (var x = 0; x < blockWidth; x++)
            blocks[x, y] = new Block
            {
                // Bg0 = bg0[x, y],
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
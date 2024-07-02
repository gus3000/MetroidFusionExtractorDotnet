using MetroidFusionExtractor.Model.Game.Level;
using MetroidFusionExtractor.Model.Memory.RomStruct;
using MetroidFusionExtractor.Model.Services.Memory;

namespace MetroidFusionExtractor.Model.Services.Game;

public class RoomFactory
{
    private readonly RomService _romService;

    public RoomFactory(
        RomService romService
    )
    {
        _romService = romService;
    }

    public Room Build(RomRoomEntry romRoomEntry)
    {
        // Console.WriteLine($"Reading value at address 0x{romRoomEntry.clipDataPointer:X}");
        var blockWidth = _romService.Read(romRoomEntry.clipDataPointer);
        var blockHeight = _romService.Read(romRoomEntry.clipDataPointer + 1);
        // Console.WriteLine($"value was {width}");

        // int width = (blockWidth-4) / 0xF;
        // int height = (blockHeight-4) / 0xA;

        // Console.WriteLine($"room block size : (0x{blockWidth:X}, 0x{blockHeight:X}), which as map coordinates makes ({width},{height})");

        Block[,] blocks = new Block[blockWidth, blockHeight];
        for (int y = 0; y < blockHeight; y++)
        {
            for (int x = 0; x < blockWidth; x++)
            {
                blocks[x, y] = new Block
                {
                    ClipData = y * blockWidth + x,
                    // ClipData = 0,
                };
            }
        }

        var room = new Room
        {
            BlockHeight = blockHeight,
            BlockWidth = blockWidth,
            MapX = romRoomEntry.mapXCoordinate,
            MapY = romRoomEntry.mapYCoordinate,
            Blocks = blocks,
        };

        return room;
    }
}
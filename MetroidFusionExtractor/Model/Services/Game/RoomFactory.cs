using MetroidFusionExtractor.Model.Game.Level;
using MetroidFusionExtractor.Model.Memory.RomStruct;
using MetroidFusionExtractor.Model.Services.Compress;
using MetroidFusionExtractor.Model.Services.Memory;

namespace MetroidFusionExtractor.Model.Services.Game;

public class RoomFactory
{
    private readonly ClipDataFactory _clipDataFactory;
    private readonly RomService _romService;
    private readonly RleService _rleService;

    public RoomFactory(
        ClipDataFactory clipDataFactory,
        RomService romService,
        RleService rleService
    )
    {
        _clipDataFactory = clipDataFactory;
        _romService = romService;
        _rleService = rleService;
    }

    public Room Build(RomRoomEntry romRoomEntry)
    {
        // var clipdataEntries = _clipDataFactory.Build(romRoomEntry.clipDataPointer);
        // // Console.WriteLine($"Reading value at address 0x{romRoomEntry.clipDataPointer:X}");
        var blockWidth = _romService.Read(romRoomEntry.clipDataPointer);
        var blockHeight = _romService.Read(romRoomEntry.clipDataPointer + 1);
        Console.WriteLine($"room is {blockWidth} by {blockHeight}");
        //
        // for (int i = 0; i < 0x38; i++)
        // {
        //     var read = _romService.Read((uint)(romRoomEntry.clipDataPointer + i));
        //     Console.Write($"{read:X2} ");
        // }
        //
        // Console.WriteLine();
        // Console.Write("         ");
        // var total = 0;
        // for (uint i = 0; i < 0x36; i += 2)
        // {
        //     var read = _romService.Read(romRoomEntry.clipDataPointer + 3 + i);
        //     total += read;
        //     Console.Write($"{read:X2}    ");
        // }
        // Console.WriteLine();
        // Console.WriteLine($"total blocks read : {total} / {blockWidth*blockHeight}");
        //
        // // int width = (blockWidth-4) / 0xF;
        // // int height = (blockHeight-4) / 0xA;
        //
        // // Console.WriteLine($"room block size : (0x{blockWidth:X}, 0x{blockHeight:X}), which as map coordinates makes ({width},{height})");
        //
        // var blockRomClipData = _romService.ReadArray(romRoomEntry.clipDataPointer + 2, blockWidth * blockHeight);
        var blockRomClipData = _rleService.ReadCompressedData(romRoomEntry.clipDataPointer + 2, blockWidth * blockHeight);
        Console.WriteLine(
        $"blockRomData size = {blockWidth} x {blockHeight} = {blockRomClipData.Count} (0x{blockRomClipData.Count:X})"
        );
        // Console.WriteLine(string.Join(", ", blockRomData));
        Block[,] blocks = new Block[blockWidth, blockHeight];
        for (int y = 0; y < blockHeight; y++)
        {
            for (int x = 0; x < blockWidth; x++)
            {
                // Console.WriteLine($"\tAccessing block indice {y * blockWidth + x}");
                int clipIndex = (y * blockWidth + x) * 2;
                ushort clipData = (ushort)(blockRomClipData[clipIndex] + blockRomClipData[clipIndex + 1] << 8);

                blocks[x, y] = new Block
                {
                    ClipData = clipData,
                    // ClipData = y * blockWidth + x,
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
            // ClipDataEntries = clipdataEntries,
            Blocks = blocks,
        };

        return room;
    }
}
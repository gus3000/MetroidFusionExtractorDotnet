using MetroidFusionExtractor.Model.Services.Compress;
using MetroidFusionExtractor.Model.Services.Memory;

namespace MetroidFusionExtractor.Model.Services.Game;

public class ClipDataFactory
{
    private const int ClipDataTypeOffsetPointer = 0x8000000 + 0x647DC;
    private readonly int _clipDataTypeOffset;
    private readonly RleService _rleService;
    private readonly RomService _romService;

    public ClipDataFactory(
        RomService romService,
        RleService rleService
    )
    {
        _romService = romService;
        _rleService = rleService;
        _clipDataTypeOffset = _romService.Read(ClipDataTypeOffsetPointer);
        Console.WriteLine(
            $"Clip data type offset : {_clipDataTypeOffset}, read from pointer 0x{ClipDataTypeOffsetPointer:X}");
    }

    //TODO use model type ClipDataType (enum ?)
    public ushort[,] Build(uint clipDataPointer)
    {
        // Note : clipdata is compressed with RLE !

        var blockWidth = _romService.Read(clipDataPointer);
        var blockHeight = _romService.Read(clipDataPointer + 1);

        var clipDataGrid = new ushort[blockWidth, blockHeight];

        var blockRomClipData = _rleService.ReadCompressedData(clipDataPointer + 2, blockWidth * blockHeight);
        // Console.WriteLine(
        // $"blockRomData size = {blockWidth} x {blockHeight} = {blockRomClipData.Count} (0x{blockRomClipData.Count:X})"
        // );
        for (var y = 0; y < blockHeight; y++)
        for (var x = 0; x < blockWidth; x++)
        {
            var clipIndex = (y * blockWidth + x) * 2;
            var clipDataType = (ushort)((blockRomClipData[clipIndex] + blockRomClipData[clipIndex + 1]) << 8);
            clipDataGrid[x, y] = _romService.Read((uint)(_clipDataTypeOffset + clipDataType));
        }


        return clipDataGrid;
    }
}
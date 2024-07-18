using MetroidFusionExtractor.Model.Services.Compress;
using MetroidFusionExtractor.Model.Services.Memory;

namespace MetroidFusionExtractor.Model.Services.Game;

public class ClipDataFactory
{
    private readonly RleService _rleService;
    private readonly RomService _romService;

    public ClipDataFactory(
        RomService romService,
        RleService rleService
    )
    {
        _romService = romService;
        _rleService = rleService;
    }

    //TODO use model type ClipDataType (enum ?)
    public byte[,] Build(uint clipDataPointer)
    {
        // Note : clipdata is compressed with RLE !

        var blockWidth = _romService.Read(clipDataPointer);
        var blockHeight = _romService.Read(clipDataPointer + 1);

        var clipDataGrid = new byte[blockWidth, blockHeight];

        var blockRomClipData = _rleService.ReadCompressedData(clipDataPointer + 2, blockWidth * blockHeight);

        for (var y = 0; y < blockHeight; y++)
        for (var x = 0; x < blockWidth; x++)
        {
            var clipIndex = (y * blockWidth + x) * 2;

            clipDataGrid[x, y] = blockRomClipData[clipIndex];
        }


        return clipDataGrid;
    }
}
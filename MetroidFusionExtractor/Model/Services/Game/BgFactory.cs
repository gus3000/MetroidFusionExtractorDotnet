using MetroidFusionExtractor.Model.Game.Level;
using MetroidFusionExtractor.Model.Services.Compress;
using MetroidFusionExtractor.Model.Services.Memory;

namespace MetroidFusionExtractor.Model.Services.Game;

public class BgFactory
{
    private readonly RleService _rleService;
    private readonly RomService _romService;

    public BgFactory(
        RomService romService,
        RleService rleService
    )
    {
        _romService = romService;
        _rleService = rleService;
    }

    public ushort[,] Build(uint bgPointer, byte bgPropertiesRaw, byte blockWidth, byte blockHeight)
    {
        Console.WriteLine(
            $"\tbuilding bg from pointer 0x{bgPointer:X}, size = {blockWidth}x{blockHeight}={blockWidth * blockHeight}");

        var bgProperties = (BgProperties)bgPropertiesRaw;
        Console.WriteLine(
            $"\tbg0 properties : {bgProperties} -> rle ? {bgProperties.IsRleCompressed()}, lz77 ? {bgProperties.IsLz77Compressed()}");

        var bgBlocks = new ushort[blockWidth, blockHeight];

        if (bgProperties.IsLz77Compressed()) //Cannot handle that for now
            return bgBlocks;

        if (bgProperties.IsRleCompressed())
        {
            var blocksData = _rleService.ReadCompressedData(bgPointer, blockWidth * blockHeight);

            Console.WriteLine(blocksData);

            return bgBlocks;
        }

        for (var y = 0; y < blockHeight; y++)
        for (var x = 0; x < blockWidth; x++)
        {
            var delta = (uint)(y * blockWidth + x) * 2;
            bgBlocks[x, y] = _romService.Read(bgPointer + delta);
        }

        return bgBlocks;
    }
}
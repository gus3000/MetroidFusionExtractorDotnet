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

    public ushort[,] Build(uint bgPointer, byte bgProperties)
    {
        Console.WriteLine($"\tbg0 properties : 0x{bgProperties:X}");
        var blockWidth = _romService.Read(bgPointer);
        var blockHeight = _romService.Read(bgPointer + 1);

        var bgBlocks = new ushort[512, 512];

        return bgBlocks;
    }
}
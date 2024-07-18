using MetroidFusionExtractor.Model.Game.Palette;
using MetroidFusionExtractor.Model.Services.Memory;

namespace MetroidFusionExtractor.Model.Services.Game;

public class BackgroundPaletteFactory
{
    private readonly RomService _romService;

    public BackgroundPaletteFactory(
        RomService romService
    )
    {
        _romService = romService;
    }

    public BackgroundPalette Build(uint backgroundPalettePointer)
    {
        var palette = new BackgroundPalette();

        var dunnow = _romService.Read(backgroundPalettePointer);
        var rowsIThink = _romService.Read(backgroundPalettePointer + 1);

        // Console.WriteLine($"\t\tReading palette : 0x{dunnow} 0x{rowsIThink}");

        return palette;
    }
}
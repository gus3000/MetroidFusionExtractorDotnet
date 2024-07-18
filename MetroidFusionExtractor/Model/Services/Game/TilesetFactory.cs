using System.Runtime.InteropServices;
using MetroidFusionExtractor.Model.Game.Palette;
using MetroidFusionExtractor.Model.Memory;
using MetroidFusionExtractor.Model.Memory.RomStruct;
using MetroidFusionExtractor.Model.Services.Memory;

namespace MetroidFusionExtractor.Model.Services.Game;

public class TilesetFactory
{
    private const int KnownNumberOfTilesets = 0x62;
    private readonly BackgroundPaletteFactory _backgroundPaletteFactory;
    private readonly RomService _romService;

    public TilesetFactory(
        RomService romService,
        BackgroundPaletteFactory backgroundPaletteFactory
    )
    {
        _romService = romService;
        _backgroundPaletteFactory = backgroundPaletteFactory;
    }

    public Tileset Build(byte tileset)
    {
        if (tileset >= KnownNumberOfTilesets)
            throw new Exception(
                $"tileset is {tileset} (0x{tileset:X}) but there are only {KnownNumberOfTilesets} known tilesets");
        // Console.WriteLine($"\ttileset : 0x{tileset:X}");

        // Console.WriteLine($"\taddr = 0x{MemoryTilesetEntry.AddressTilesetEntries:X8} + 0x{tileset:X} * 0x{Marshal.SizeOf<RomTileset>():X}");

        var tileSetAddr = (uint)(MemoryTilesetEntry.AddressTilesetEntries + tileset * Marshal.SizeOf<RomTileset>());

        var romTileset = _romService.ReadObject<RomTileset>(tileSetAddr);
        // Console.WriteLine($"\ttileset read at address : 0x{tileSetAddr:X}");
        // Console.WriteLine($"\ttileset background : [0x{romTileset.rleBackgroundTilemapPointer:X}]");

        var palette = _backgroundPaletteFactory.Build(romTileset.rleBackgroundTilemapPointer);

        return new Tileset();
    }
}
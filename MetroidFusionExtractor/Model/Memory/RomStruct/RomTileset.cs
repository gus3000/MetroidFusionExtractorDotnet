using System.Runtime.InteropServices;

namespace MetroidFusionExtractor.Model.Memory.RomStruct;

[StructLayout(LayoutKind.Explicit, Pack = 1)]
public class RomTileset
{
    [FieldOffset(0x0)] [RomPointer] public readonly uint RleBackgroundGraphicsPointer;
    [FieldOffset(0x4)] [RomPointer] public readonly uint backgroundPalettePointer;
    [FieldOffset(0x8)] [RomPointer] public readonly uint lz77BackgroundGraphicsPointer;
    [FieldOffset(0xC)] [RomPointer] public readonly uint rleBackgroundTilemapPointer;
    [FieldOffset(0x10)] public readonly byte animatedTileset;
    [FieldOffset(0x11)] public readonly byte animatedPalette;
    [FieldOffset(0x12)] public readonly ushort unused;
}
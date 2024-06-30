using System.Runtime.InteropServices;

namespace MetroidFusionExtractor.Model.Memory.RomStruct;

[StructLayout(LayoutKind.Explicit, Pack = 1)]
public class RomRoomEntry
{
    public static ROM? Rom
    {
        get => _rom;
        set
        {
            if (_rom != null && _rom != value)
                throw new Exception("Tring to set RoomEntry.Rom several times, this is not intended behaviour");
            _rom = value;
        }
    }

    [FieldOffset(0x0)] public readonly byte tileset;
    [FieldOffset(0x1)] public readonly byte bg0Properties;
    [FieldOffset(0x2)] public readonly byte bg1Properties;
    [FieldOffset(0x3)] public readonly byte bg2Properties;
    [FieldOffset(0x4)] public readonly byte bg3Properties;
    [FieldOffset(0x8)] [RomPointer] public readonly uint bg0Pointer; //should add 0x3BFE00 to get actual address
    [FieldOffset(0xC)] [RomPointer] public readonly uint bg1Pointer;
    [FieldOffset(0x10)] [RomPointer] public readonly uint bg2Pointer;
    [FieldOffset(0x14)] [RomPointer] public readonly uint clipDataPointer;
    [FieldOffset(0x18)] [RomPointer] public readonly uint bg3Pointer;
    [FieldOffset(0x1C)] public readonly byte bg3Scrolling;
    [FieldOffset(0x1D)] public readonly byte transparency;
    [FieldOffset(0x20)] [RomPointer] public readonly uint defaultSpriteLayoutPointer;
    [FieldOffset(0x24)] public readonly byte defaultSpriteset;
    [FieldOffset(0x25)] public readonly byte firstSpritesetEvent;
    [FieldOffset(0x28)] [RomPointer] public readonly uint firstSpriteLayoutPointer;
    [FieldOffset(0x2C)] public readonly byte firstSpriteset;
    [FieldOffset(0x2D)] public readonly byte secondSpritesetEvent;
    [FieldOffset(0x30)] [RomPointer] public readonly uint secondSpriteLayoutPointer;
    [FieldOffset(0x34)] public readonly byte secondSpriteset;
    [FieldOffset(0x35)] public readonly byte mapXCoordinate;
    [FieldOffset(0x36)] public readonly byte mapYCoordinate;
    [FieldOffset(0x37)] public readonly byte effect;
    [FieldOffset(0x38)] public readonly byte effectYPosition;
    [FieldOffset(0x3A)] public readonly ushort musicTrack;
    private static ROM? _rom;


    public override string ToString()
    {
        return
            $"RoomEntry (0x{tileset:X}, {bg0Properties:X}, {bg1Properties:X}, {bg2Properties:X}, {bg3Properties:X}, 0x{bg0Pointer:X}, 0x{bg1Pointer:X}, 0x{bg2Pointer:X}, 0x{clipDataPointer:X}, 0x{bg3Pointer:X} ... {mapXCoordinate},{mapYCoordinate})";
        // $"RoomEntry (clipdata pointer : 0x{bg3Pointer:X})";
    }
}
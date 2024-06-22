using System.Runtime.InteropServices;
using MetroidFusionExtractor.Model.Memory;

namespace MetroidFusionExtractor.Model;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class RoomEntry
{
    public readonly byte tileset;
    public readonly byte bg0Properties;
    public readonly byte bg1Properties;
    public readonly byte bg2Properties;
    public readonly byte bg3Properties;
    public readonly uint bg0Pointer; //should add 0x3BFE00 to get actual address
    public readonly uint bg1Pointer;
    public readonly uint bg2Pointer;
    public readonly uint clipDataPointer;
    public readonly uint bg3Pointer;
    public readonly byte bg3Scrolling;
    public readonly byte transparency;
    public readonly uint defaultSpriteLayoutPointer;
    public readonly byte defaultSpriteset;
    public readonly byte firstSpritesetEvent;
    public readonly uint firstSpriteLayoutPointer;
    public readonly byte firstSpriteset;
    public readonly byte secondSpritesetEvent;
    public readonly uint secondSpriteLayoutPointer;
    public readonly byte secondSpriteset;
    public readonly byte mapXCoordinate;
    public readonly byte mapYCoordinate;
    public readonly byte effect;
    public readonly byte effectYPosition;
    public readonly ushort musicTrack;

    public override string ToString()
    {
        return
            $"RoomEntry (0x{tileset:X}, {bg0Properties:X}, {bg1Properties:X}, {bg2Properties:X}, {bg3Properties:X}, 0x{bg0Pointer:X} ... {mapXCoordinate},{mapYCoordinate})";
    }
}
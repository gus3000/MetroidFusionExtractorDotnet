namespace MetroidFusionExtractor.Model;

public class RoomEntry
{
    private byte tileset;
    private byte bg0Properties;
    private byte bg1Properties;
    private byte bg2Properties;
    private byte bg3Properties;
    private uint bg0Pointer;
    private uint bg1Pointer;
    private uint bg2Pointer;
    private uint clipDataPointer;
    private uint bg3Pointer;
    private byte bg3Scrolling;
    private byte transparency;
    private uint defaultSpriteLayoutPointer;
    private byte defaultSpriteset;
    private byte firstSpritesetEvent;
    private uint firstSpriteLayoutPointer;
    private byte firstSpriteset;
    private byte secondSpritesetEvent;
    private uint secondSpriteLayoutPointer;
    private byte secondSpriteset;
    private byte mapXCoordinate;
    private byte mapYCoordinate;
    private byte effet;
    private byte effectYPosition;
    private ushort musicTrack;
    
    
    
    public RoomEntry(List<byte> memory)
    {
        
    }
}
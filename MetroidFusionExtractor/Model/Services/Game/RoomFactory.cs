using MetroidFusionExtractor.Model.Game.Level;
using MetroidFusionExtractor.Model.Memory.RomStruct;

namespace MetroidFusionExtractor.Model.Services.Game;

public class RoomFactory
{
    public Room Build(RomRoomEntry romRoomEntry)
    {
        var room = new Room
        {
            Height = 1,
            Width = 1,
            MapX = romRoomEntry.mapXCoordinate,
            MapY = romRoomEntry.mapYCoordinate
        };
        
        return room;
    }
}
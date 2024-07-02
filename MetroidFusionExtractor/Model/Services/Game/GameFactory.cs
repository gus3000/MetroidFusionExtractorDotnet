using MetroidFusionExtractor.Model.Game;
using MetroidFusionExtractor.Model.Services.Memory;

namespace MetroidFusionExtractor.Model.Services.Game;

public class GameFactory
{
    private readonly RomService _romService;
    private readonly RoomFactory _roomFactory;

    public GameFactory(
        RomService romService,
        RoomFactory roomFactory
    )
    {
        _romService = romService;
        _roomFactory = roomFactory;
    }

    public GameInfo Build()
    {
        var gameInfo = new GameInfo();

        foreach (Area area in Enum.GetValues(typeof(Area)))
        foreach (var romRoomEntry in _romService.Rom.GetRooms(area))
        {
            var room = _roomFactory.Build(romRoomEntry);
            gameInfo.AddRoom(area, room);
        }


        return gameInfo;
    }
}
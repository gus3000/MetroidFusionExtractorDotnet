using MetroidFusionExtractor.Model.Game;

namespace MetroidFusionExtractor.Model.Services.Game;

public class GameFactory
{
    private readonly RoomFactory _roomFactory;

    public GameFactory(
        RoomFactory roomFactory
    )
    {
        _roomFactory = roomFactory;
    }

    public GameInfo Build(ROM rom)
    {
        var gameInfo = new GameInfo();

        foreach (var romRoomEntry in rom.GetRooms(Area.MainDeck))
        {
            var room = _roomFactory.Build(romRoomEntry);
            gameInfo.AddRoom(Area.MainDeck, room);
        }

        return gameInfo;
    }
}
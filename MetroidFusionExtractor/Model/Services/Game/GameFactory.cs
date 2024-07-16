using MetroidFusionExtractor.Model.Game;
using MetroidFusionExtractor.Model.Services.Memory;
using MetroidFusionExtractor.Model.Services.Memory.Factory;

namespace MetroidFusionExtractor.Model.Services.Game;

public class GameFactory
{
    private readonly RomFactory _romFactory;
    private readonly RomService _romService;
    private readonly RoomFactory _roomFactory;

    public GameFactory(
        RomService romService,
        RomFactory romFactory,
        RoomFactory roomFactory
    )
    {
        _romService = romService;
        _romFactory = romFactory;
        _roomFactory = roomFactory;
    }

    public GameInfo Build()
    {
        var gameInfo = new GameInfo();
        var rom = _romFactory.Build();

        int i;
        foreach (Area area in Enum.GetValues(typeof(Area)))
        {
            i = 0;
            foreach (var romRoomEntry in rom.GetRooms(area))
            {
                Console.WriteLine($"Building Room : {area} - {i++}");
                var room = _roomFactory.Build(romRoomEntry);
                gameInfo.AddRoom(area, room);
            }
        }


        return gameInfo;
    }
}
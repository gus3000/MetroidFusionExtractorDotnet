using MetroidFusionExtractor.Model.Game.Level;

namespace MetroidFusionExtractor.Model.Game;

public class GameInfo
{
    private readonly Dictionary<Area, List<Room>> _roomEntries;

    public GameInfo()
    {
        _roomEntries = new Dictionary<Area, List<Room>>();

        foreach (var area in Enum.GetValues(typeof(Area)).Cast<Area>()) _roomEntries[area] = new List<Room>();
    }

    public void AddRoom(Area area, Room entry)
    {
        _roomEntries[area].Add(entry);
    }

    public List<Room> GetRooms(Area area)
    {
        return _roomEntries[area];
    }

    public Room GetRoom(Area area, int index)
    {
        return _roomEntries[area][index];
    }
}
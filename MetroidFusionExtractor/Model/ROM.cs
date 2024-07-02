using MetroidFusionExtractor.Model.Game;
using MetroidFusionExtractor.Model.Memory.RomStruct;

namespace MetroidFusionExtractor.Model;

public class ROM
{
    public const string RomPath = "rom/Metroid Fusion (Europe) (En,Fr,De,Es,It).gba";


    private readonly Dictionary<Area, List<RomRoomEntry>> _roomEntries;
    // public List<RomRoomEntry> RoomEntries { get; } = new();

    public ROM()
    {
        _roomEntries = new Dictionary<Area, List<RomRoomEntry>>();

        foreach (var area in Enum.GetValues(typeof(Area)).Cast<Area>()) _roomEntries[area] = new List<RomRoomEntry>();
    }

    public List<RomRoomEntry> GetRooms(Area area)
    {
        return _roomEntries[area];
    }

    public void AddRoom(Area area, RomRoomEntry entry)
    {
        _roomEntries[area].Add(entry);
    }
}
using MetroidFusionExtractor.Model.Memory;
using MetroidFusionExtractor.Model.Memory.RomStruct;

namespace MetroidFusionExtractor.Model.Services.Memory.Factory;

public class RomFactory
{
    public RoomFactory RoomFactory { get; }

    public RomFactory(
        RoomFactory roomFactory
    )
    {
        RoomFactory = roomFactory;
    }

    public ROM Build(List<byte> data)
    {
        var rom = new ROM(data);
        var roomEntries = new List<RomRoomEntry>();

        for (var i = 0; i < MemoryRoomEntry.AmountMainDeck; i++)
        {
            var memoryRange = data.GetRange(
                MemoryRoomEntry.AddressMainDeck + MemoryRoomEntry.Size * i,
                MemoryRoomEntry.Size);
            var entry = RoomFactory.Build(memoryRange);
            // map[entry.mapXCoordinate, entry.mapYCoordinate] = true;
            // Console.WriteLine($"entry {i}: {entry}");
            roomEntries.Add(entry);
        }
        
        foreach (var (roomEntry, index) in roomEntries.WithIndex()) Console.WriteLine($"entry {index}: {roomEntry}");

        return rom;
    }
}
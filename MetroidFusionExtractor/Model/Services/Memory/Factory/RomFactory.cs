using MetroidFusionExtractor.Model.Game;
using MetroidFusionExtractor.Model.Memory;
using MetroidFusionExtractor.Model.Memory.RomStruct;

namespace MetroidFusionExtractor.Model.Services.Memory.Factory;

public class RomFactory
{
    public RomFactory(
        RomRoomFactory romRoomFactory
    )
    {
        RomRoomFactory = romRoomFactory;
    }

    public RomRoomFactory RomRoomFactory { get; }

    public ROM Build(List<byte> data)
    {
        var rom = new ROM();

        for (var i = 0; i < MemoryRoomEntry.AmountMainDeck; i++)
        {
            var memoryRange = data.GetRange(
                MemoryRoomEntry.AddressMainDeck + MemoryRoomEntry.Size * i,
                MemoryRoomEntry.Size
            );
            var entry = RomRoomFactory.Build(memoryRange);
            // map[entry.mapXCoordinate, entry.mapYCoordinate] = true;
            // Console.WriteLine($"entry {i}: {entry}");
            rom.AddRoom(Area.MainDeck, entry);
        }
        
        return rom;
    }
}
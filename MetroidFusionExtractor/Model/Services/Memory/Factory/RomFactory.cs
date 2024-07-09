using MetroidFusionExtractor.Model.Game;
using MetroidFusionExtractor.Model.Memory;

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

        foreach (Area area in Enum.GetValues(typeof(Area)))
        {
            var address = MemoryRoomEntry.GetAddress(area);
            var amount = MemoryRoomEntry.GetAmount(area);
            for (var i = 0; i < amount; i++)
            {
                var memoryRange = data.GetRange(
                    address + MemoryRoomEntry.Size * i,
                    MemoryRoomEntry.Size
                );
                var entry = RomRoomFactory.Build(memoryRange);
                rom.AddRoom(area, entry);
                // return rom; //FIXME remove
            }
        }

        return rom;
    }
}
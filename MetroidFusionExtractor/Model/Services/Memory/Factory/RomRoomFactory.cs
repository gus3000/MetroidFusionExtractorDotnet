using MetroidFusionExtractor.Model.Memory.RomStruct;

namespace MetroidFusionExtractor.Model.Services.Memory.Factory;

public class RomRoomFactory
{
    private readonly RomService _romService;

    public RomRoomFactory(
        RomService romService
    )
    {
        _romService = romService;
    }

    public RomRoomEntry Build(uint roomPointer)
    {
        return _romService.ReadObject<RomRoomEntry>(roomPointer);

        // return RomService.BytesToStruct<RomRoomEntry>(memoryRange.ToArray(), RomService.Endianness.BigEndian);
        // return _memoryService.BytesToStruct<RomRoomEntry>(memoryRange.ToArray(), MemoryService.Endianness.BigEndian);
    }
}
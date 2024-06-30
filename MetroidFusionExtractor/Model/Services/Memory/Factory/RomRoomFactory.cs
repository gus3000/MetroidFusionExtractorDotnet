using MetroidFusionExtractor.Model.Memory;
using MetroidFusionExtractor.Model.Memory.RomStruct;

namespace MetroidFusionExtractor.Model.Services.Memory.Factory;

public class RomRoomFactory
{
    public RomRoomEntry Build(List<byte> memoryRange)
    {
        return MemoryUtils.BytesToStruct<RomRoomEntry>(memoryRange.ToArray(), MemoryUtils.Endianness.BigEndian);
    }
}
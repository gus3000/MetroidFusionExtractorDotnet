using MetroidFusionExtractor.Model.Services.Memory.Factory;

namespace MetroidFusionExtractor.Model.Services.Memory;

public class RomService
{
    private readonly FileDataFactory _fileDataFactory;
    private readonly RomFactory _romFactory;
    private readonly List<byte> _data;

    public RomService(
        FileDataFactory fileDataFactory,
        RomFactory romFactory
    )
    {
        _fileDataFactory = fileDataFactory;
        _romFactory = romFactory;

        _data = _fileDataFactory.FileContents(ROM.RomPath);
        Rom = _romFactory.Build(_data);
    }

    public ROM Rom { get; init; }

    public byte Read(uint pointerValue)
    {
        const int ROM_PREFIX = 0x8000000;
        if (pointerValue < ROM_PREFIX)
            throw new Exception($"Pointer should be at least 0x{ROM_PREFIX:X}, value was 0x{pointerValue:X}");

        var address = (int)(pointerValue - ROM_PREFIX);

        if (address > _data.Count)
            throw new Exception($"Unable to access memory address 0x{address:X}, rom is only 0x{_data.Count} bytes !");

        return _data[address];
    }
}
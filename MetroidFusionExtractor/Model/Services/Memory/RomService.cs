using System.Runtime.InteropServices;
using MetroidFusionExtractor.Model.Services.Memory.Factory;

namespace MetroidFusionExtractor.Model.Services.Memory;

public class RomService
{
    private readonly List<byte> _data;

    private readonly FileDataFactory _fileDataFactory;

    // private readonly RomFactory _romFactory;
    private readonly MemoryService _memoryService;

    public RomService(
        FileDataFactory fileDataFactory,
        // RomFactory romFactory,
        MemoryService memoryService
    )
    {
        _fileDataFactory = fileDataFactory;
        // _romFactory = romFactory;
        _memoryService = memoryService;

        _data = _fileDataFactory.FileContents(ROM.RomPath);
        // Rom = _romFactory.Build(_data);
    }

    // public ROM Rom { get; init; }

    private int pointerValueToAddress(uint pointerValue)
    {
        const int ROM_PREFIX = 0x8000000;
        if (pointerValue < ROM_PREFIX) pointerValue += ROM_PREFIX;
        // throw new Exception($"Pointer should be at least 0x{ROM_PREFIX:X}, value was 0x{pointerValue:X}");
        var address = (int)(pointerValue - ROM_PREFIX);

        if (address > _data.Count)
            throw new Exception($"Unable to access memory address 0x{address:X}, rom is only 0x{_data.Count} bytes !");

        return address;
    }

    public byte Read(uint pointerValue)
    {
        return _data[pointerValueToAddress(pointerValue)];
    }

    public ushort ReadU16(uint pointerValue)
    {
        var address = pointerValueToAddress(pointerValue);
        return (ushort)((_data[address] << 8) + _data[address + 1]);
        // return (ushort)(_data[address] << 8 + _data[address + 1]);
    }

    public List<byte> ReadArray(uint pointerValue, int count)
    {
        var arrayAddress = pointerValueToAddress(pointerValue);

        return _data.GetRange(arrayAddress, count);
    }

    public T ReadObject<T>(uint pointerValue) where T : class
    {
        var array = ReadArray(pointerValue, Marshal.SizeOf<T>()).ToArray();
        return _memoryService.BytesToStruct<T>(array, MemoryService.Endianness.BigEndian);
    }
}
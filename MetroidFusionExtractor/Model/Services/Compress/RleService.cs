using MetroidFusionExtractor.Model.Services.Memory;

namespace MetroidFusionExtractor.Model.Services.Compress;

public class RleService : ICompressionService
{
    private readonly RomService _romService;

    public RleService(
        RomService romService
    )
    {
        _romService = romService;
    }

    public List<byte> ReadCompressedData(uint start, int length)
    {
        return DecompRLEMage(start, length);
    }

    public List<byte> DecompRLEMage(uint start, int length)
    {
        var currentPointer = start;
        int currentOutputOffset;
        var output = Enumerable.Repeat((byte)255, length * 2).ToList();
        var dest = 0;


        for (var p = 0; p < 2; p++)
        {
            // for each pass
            var destOffset = dest + p;
            var numBytes = _romService.Read(currentPointer++);
            while (true)
            {
                ushort amount;
                ushort compare;
                if (numBytes == 1)
                {
                    amount = _romService.Read(currentPointer++);
                    // Console.WriteLine($"amount : {amount}");
                    compare = 0x80;
                }
                else
                {
                    // numBytes == 2
                    amount = (ushort)((_romService.Read(currentPointer++) << 8) + _romService.Read(currentPointer++));
                    compare = 0x8000;
                }

                if (amount == 0) break;

                if ((amount & compare) != 0) // compressed
                {
                    amount %= compare;
                    var val = _romService.Read(currentPointer++);
                    // Console.WriteLine($"reading value {val}");
                    while (amount > 0)
                    {
                        currentOutputOffset = destOffset;
                        output[currentOutputOffset] = val;
                        destOffset += 2;
                        amount--;
                    }
                }

                else // uncompressed
                {
                    while (amount > 0)
                    {
                        currentOutputOffset = destOffset;
                        output[currentOutputOffset] = _romService.Read(currentPointer++);
                        destOffset += 2;
                        amount--;
                    }
                }
            }
        }

        // compressed size
        return output;
    }

    public List<byte> ReadCompressedDataClassic(uint start, int length)
    {
        var res = new List<byte>();
        var blockRomClipData = _romService.ReadArray(start, length);

        uint read = 0;
        uint memoryRead = 0;
        while (read <= length)
        {
            var value = _romService.Read(start + memoryRead);
            var toRead = _romService.Read(start + memoryRead + 1);

            Console.WriteLine(
                $"reading address 0x{start + memoryRead:X}, read value {value} to be inserted {toRead} times");
            // for (int i = 0; i < blockRomClipData[read + 1]; i++)
            for (var i = 0; i < toRead; i++) res.Add(value);

            read += toRead;
            read++;
            memoryRead += 2;
        }

        Console.WriteLine($"read {read} for length {length}");

        return res;
    }
}
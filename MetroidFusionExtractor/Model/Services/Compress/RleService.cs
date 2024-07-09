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

    public List<byte> DecompRLEMage(uint start, int length)
    {
        uint currentPointer = start;
        int currentOutputOffset;
        List<byte> output = Enumerable.Repeat((byte)255, length*2).ToList();
        int dest = 0;


        for (int p = 0; p < 2; p++)
        {
            // for each pass
            int destOffset = dest + p;
            byte numBytes = _romService.Read(currentPointer++);
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

                if (amount == 0)
                {
                    break;
                }

                if ((amount & compare) != 0) // compressed
                {
                    amount %= compare;
                    byte val = _romService.Read(currentPointer++);
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

    public List<byte> ReadCompressedData(uint start, int length)
    {
        return DecompRLEMage(start, length);
        Console.WriteLine($"Attempting to read RLE compressed data, of uncompressed size {length}");
        var res = new List<byte>(length);
        var currentPointer = start;

        // for (int i = 0; i < length; i++)
        // res.Add((byte) i);

        byte amountSize = _romService.Read(currentPointer++);
        Console.WriteLine($"amount size : {amountSize}");

        ushort amount;
        if (amountSize != 1)
            throw new NotImplementedException($"RLE : amount size = {amountSize}, only 1 supported");
        amount = _romService.Read(currentPointer++);
        Console.WriteLine($"amount to read : {amount}");

        // if (amount == 0)
        // break;

        ushort compressedFlag = 0x80;
        bool isCompressed = (amount & compressedFlag) != 0;
        Console.WriteLine($"is compressed ? {isCompressed}");
        if (isCompressed)
        {
            amount -= compressedFlag;
            Console.WriteLine($"actual amount : {amount}");
            byte valueToCopy = _romService.Read(currentPointer);
            Console.WriteLine($"Value : {valueToCopy}");
            for (int i = 0; i < amount; i++)
            {
                res.Add(valueToCopy);
                res.Add(0); //padding for now
            }
        }

        // TODO remove end padding
        for(int i=res.Count; i<length;i++)
            res.Add(0);
        
        return res;
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
            for (int i = 0; i < toRead; i++)
            {
                res.Add(value);
            }

            read += toRead;
            read++;
            memoryRead += 2;
        }

        Console.WriteLine($"read {read} for length {length}");

        return res;
    }
}
using MetroidFusionExtractor.Image;
using MetroidFusionExtractor.Model.Services.Memory.Factory;

namespace MetroidFusionExtractor.Model.Services;

public class RomParser
{
    private const int BufferSize = 1024;

    public RomParser(
        RomFactory romFactory,
        RoomFactory roomFactory
    )
    {
        RomFactory = romFactory;
        RoomFactory = roomFactory;
    }

    public RomFactory RomFactory { get; }
    public RoomFactory RoomFactory { get; }

    public void Parse(string romPath)
    {
        var fileStream = File.OpenRead(romPath);
        Console.WriteLine($"file size : {fileStream.Length}");
        var data = new List<byte>((int)fileStream.Length);
        int read;
        var buffer = new byte[BufferSize];
        do
        {
            read = fileStream.Read(buffer, 0, BufferSize);
            for (var i = 0; i < read; i++)
                data.Add(buffer[i]);
        } while (read > 0);

        Console.WriteLine($"data size : {data.Count}");

        var rom = RomFactory.Build(data);

        var mapViz = new MapViz(rom);
    }
}
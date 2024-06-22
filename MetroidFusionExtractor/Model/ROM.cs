using MetroidFusionExtractor.Model.Memory;

namespace MetroidFusionExtractor.Model;

public class ROM
{
    private const int BUFFER_SIZE = 1024;
    private List<byte> data;

    public ROM(string filePath)
    {
        InitData(filePath);
    }

    private void InitData(string filePath)
    {
        var fileStream = File.OpenRead(filePath);
        Console.WriteLine($"file size : {fileStream.Length}");
        data = new List<byte>((int)fileStream.Length);
        int read;
        byte[] buffer = new byte[BUFFER_SIZE];
        do
        {
            read = fileStream.Read(buffer, 0, BUFFER_SIZE);
            for (int i = 0; i < read; i++)
                data.Add(buffer[i]);
        } while (read > 0);

        Console.WriteLine($"data size : {data.Count}");
    }

    public void Debug()
    {
        var memoryRange = data.GetRange(RoomEntryAddress.MainDeck, RoomEntrySize.MainDeck);
        Console.WriteLine($"input data : {string.Join(",", memoryRange)}");
        // var entry = new RoomEntry(data.GetRange(RoomEntryAddress.MainDeck, RoomEntrySize.MainDeck));
        
        var entry = MemoryUtils.FromMemoryArray<RoomEntry>(memoryRange);
        Console.WriteLine($"entry : {entry.tileset}");
    }
}
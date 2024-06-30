using System.Diagnostics;

namespace MetroidFusionExtractor.Model.Services.Memory.Factory;

public class FileDataFactory
{
    private const int DefaultBufferSize = 1024;

    public List<byte> FileContents(string filePath, int bufferSize = DefaultBufferSize)
    {
        var fileStream = File.OpenRead(filePath);
        var data = new List<byte>((int)fileStream.Length);
        int read;
        var buffer = new byte[bufferSize];
        do
        {
            read = fileStream.Read(buffer, 0, bufferSize);
            for (var i = 0; i < read; i++)
                data.Add(buffer[i]);
        } while (read > 0);


        Debug.Assert(data.Count == fileStream.Length, "Data read size should equal total file size");
        return data;
    }
}
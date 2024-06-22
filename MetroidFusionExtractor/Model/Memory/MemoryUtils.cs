using System.Runtime.InteropServices;

namespace MetroidFusionExtractor.Model.Memory;

public class MemoryUtils
{
    public static T? FromMemoryArray<T>(List<byte> input)
    {
        // var outputSize = System.Runtime.InteropServices.Marshal.SizeOf(output);
        var outputSize = Marshal.SizeOf(typeof(T));
        Console.WriteLine($"input size : {input.Count}, output size : {outputSize}");

        GCHandle handle = GCHandle.Alloc(input.ToArray(), GCHandleType.Pinned);
        T? output;
        try
        {
            output = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
        }
        finally
        {
            handle.Free();
        }


        return output;
    }
}
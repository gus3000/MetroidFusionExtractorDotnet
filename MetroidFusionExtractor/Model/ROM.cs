using MetroidFusionExtractor.Model.Memory;
using MetroidFusionExtractor.Model.Memory.RomStruct;

namespace MetroidFusionExtractor.Model;

public class ROM
{
    public ROM(List<byte> data)
    {
    }

    // public List<RomRoomEntry> RoomEntries { get; private set; } = null!;

    private void InitRooms(List<byte> data)
    {
        // RomRoomEntry.Rom = this;

    }

    public void Debug()
    {
        // Console.WriteLine($"input data : {string.Join(",", memoryRange)}");
        // var entry = new RoomEntry(data.GetRange(RoomEntryAddress.MainDeck, RoomEntrySize.MainDeck));


        // var map = new bool[30, 30];
        // StringBuilder sb = new StringBuilder((int)map.LongLength);
        // for (int y = 0; y < map.GetLength(0); y++)
        // {
        //     for (int x = 0; x < map.GetLength(1); x++)
        //     {
        //         sb.Append(map[x, y] ? 'o' : ' ');
        //     }
        //
        //     sb.Append("\n");
        // }
        //
        // Console.WriteLine(sb);
    }
}
﻿using System.Text;
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
        // Console.WriteLine($"input data : {string.Join(",", memoryRange)}");
        // var entry = new RoomEntry(data.GetRange(RoomEntryAddress.MainDeck, RoomEntrySize.MainDeck));

        var map = new bool[30, 30];

        for (var i = 0; i < MemoryRoomEntry.AmountMainDeck; i++)
        {
            var memoryRange = data.GetRange(
                MemoryRoomEntry.AddressMainDeck + MemoryRoomEntry.Size * i,
                MemoryRoomEntry.Size);
            var entry = MemoryUtils.BytesToStruct<RoomEntry>(memoryRange.ToArray(), MemoryUtils.Endianness.BigEndian);
            // map[entry.mapXCoordinate, entry.mapYCoordinate] = true;
            Console.WriteLine($"entry {i}: {entry}, bg0 pointer = 0x{entry.bg0Pointer:X}");

        }

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
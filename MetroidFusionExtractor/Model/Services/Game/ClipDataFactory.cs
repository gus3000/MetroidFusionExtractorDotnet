using MetroidFusionExtractor.Model.Game.Level;
using MetroidFusionExtractor.Model.Services.Memory;

namespace MetroidFusionExtractor.Model.Services.Game;

public class ClipDataFactory
{
    private readonly RomService _romService;

    public ClipDataFactory(
        RomService romService
    )
    {
        _romService = romService;
    }

    public List<ClipDataEntry> Build(uint clipDataPointer)
    {
        // Note : clipdata is compressed with RLE !
        var clipDataEntries = new List<ClipDataEntry>();


        var clipDataEntry = BuildOne(clipDataPointer + 2);
        Console.WriteLine($"clipdata X = {clipDataEntry.X} -> {clipDataEntry.XMax}");
        Console.WriteLine($"clipdata Y = {clipDataEntry.Y} -> {clipDataEntry.YMax}");
        clipDataEntries.Add(clipDataEntry);

        return clipDataEntries;
    }

    private ClipDataEntry BuildOne(uint pointer)
    {
        return new ClipDataEntry
        {
            X = _romService.Read(pointer),
            XMax = _romService.Read(pointer + 1),
            Y = _romService.Read(pointer + 2),
            YMax = _romService.Read(pointer+3),
            ClipDataBlockBehavior = ClipDataBlockBehavior.AirOrSolid,
        };
    }
}
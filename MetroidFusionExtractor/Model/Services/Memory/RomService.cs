using MetroidFusionExtractor.Model.Services.Game;
using MetroidFusionExtractor.Model.Services.Memory.Factory;

namespace MetroidFusionExtractor.Model.Services.Memory;

public class RomService
{
    
    private readonly FileDataFactory _fileDataFactory;
    private readonly RomFactory _romFactory;

    public ROM Rom { get; }

    public RomService(
        FileDataFactory fileDataFactory,
        RomFactory romFactory
    )
    {
        _fileDataFactory = fileDataFactory;
        _romFactory = romFactory;
        
        var data = _fileDataFactory.FileContents(ROM.RomPath);
        Rom = _romFactory.Build(data);
    }
}
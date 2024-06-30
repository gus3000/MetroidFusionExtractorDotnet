using MetroidFusionExtractor.Image;
using MetroidFusionExtractor.Model.Services.Game;
using MetroidFusionExtractor.Model.Services.Memory.Factory;

namespace MetroidFusionExtractor.Model.Services;

public class RomParser
{
    private readonly FileDataFactory _fileDataFactory;
    private readonly GameFactory _gameFactory;
    private readonly RomFactory _romFactory;

    public RomParser(
        FileDataFactory fileDataFactory,
        RomFactory romFactory,
        GameFactory gameFactory
    )
    {
        _fileDataFactory = fileDataFactory;
        _romFactory = romFactory;
        _gameFactory = gameFactory;
    }

    public void Parse(string romPath)
    {
        var data = _fileDataFactory.FileContents(romPath);

        var rom = _romFactory.Build(data);
        var game = _gameFactory.Build(rom);


        var mapViz = new MapViz(rom);
    }
}
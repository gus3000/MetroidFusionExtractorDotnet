using MetroidFusionExtractor.Image;
using MetroidFusionExtractor.Model.Services.Game;
using MetroidFusionExtractor.Model.Services.Memory;

namespace MetroidFusionExtractor.Model.Services.Draw;

public class MapDrawer
{
    private readonly RomService _romService;
    private readonly GameFactory _gameFactory;

    public MapDrawer(
        RomService romService,
        GameFactory gameFactory
        )
    {
        _romService = romService;
        _gameFactory = gameFactory;
    }

    public void DrawMap()
    {
        var rom = _romService.Rom;
        var game = _gameFactory.Build(rom);
        var mapViz = new MapViz(game);

    }
}
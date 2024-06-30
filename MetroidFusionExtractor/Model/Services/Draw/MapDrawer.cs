using MetroidFusionExtractor.Image;
using MetroidFusionExtractor.Model.Services.Game;
using MetroidFusionExtractor.Model.Services.Memory;

namespace MetroidFusionExtractor.Model.Services.Draw;

public class MapDrawer
{
    private readonly GameFactory _gameFactory;

    public MapDrawer(
        GameFactory gameFactory
        )
    {
        _gameFactory = gameFactory;
    }

    public void DrawMap()
    {
        var game = _gameFactory.Build();
        var mapViz = new MapViz(game);

    }
}
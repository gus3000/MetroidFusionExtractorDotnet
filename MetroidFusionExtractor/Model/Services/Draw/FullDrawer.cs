using MetroidFusionExtractor.Image;
using MetroidFusionExtractor.Model.Services.Game;

namespace MetroidFusionExtractor.Model.Services.Draw;

public class FullDrawer
{
    private readonly GameFactory _gameFactory;

    public FullDrawer(
        GameFactory gameFactory
    )
    {
        _gameFactory = gameFactory;
    }

    public void DrawMap()
    {
        var game = _gameFactory.Build();
        var mapViz = new MapDrawer(game);
        mapViz.Draw();
    }
}
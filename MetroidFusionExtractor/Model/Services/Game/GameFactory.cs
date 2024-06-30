using MetroidFusionExtractor.Model.Game;

namespace MetroidFusionExtractor.Model.Services.Game;

public class GameFactory
{
    public GameInfo Build(ROM rom)
    {
        return new GameInfo();
    }
}
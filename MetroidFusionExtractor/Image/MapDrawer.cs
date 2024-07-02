using MetroidFusionExtractor.Model.Game;
using MetroidFusionExtractor.Model.Game.Level;
using MetroidFusionExtractor.Model.Math;
using SkiaSharp;

namespace MetroidFusionExtractor.Image;

public class MapDrawer : AbstractDrawer
{
    private const int UnitSize = 32;
    private const int Margin = 64;

    private const int MaxMapWidth = 32;
    private const int MaxMapHeight = 32;
    private readonly GameInfo _game;

    private readonly Random _random;


    public MapDrawer(GameInfo game)
    {
        _game = game;
        _random = new Random();
        // var bitmap = new SKBitmap(500, 500);
        
    }

    public void Draw()
    {
        var i = 0;
        foreach (var area in Enum.GetValues(typeof(Area)).Cast<Area>())
        {
            var mapSize = MapCoordToImage(MaxMapWidth, MaxMapHeight);
            InitSurface(mapSize);
            var rooms = _game.GetRooms(area);
            foreach (var room in rooms)
            {
                DrawRoom(room, $"{i}");
                i++;
            }

            Save($"map-{area}.png");
        }
    }

    private void DrawRoom(Room room, string name)
    {
        var typeface = SKTypeface.Default;
        var font = new SKFont(typeface);

        var whitePaint = new SKPaint(font);
        whitePaint.Color = SKColors.NavajoWhite;

        var paint = new SKPaint();
        paint.Color = new SKColor((uint)_random.NextInt64());
        var topLeft = MapCoordToImage(room.MapX, room.MapY);
        var paintedRect = new SKRect(topLeft.X, topLeft.Y, topLeft.X + UnitSize * room.MapWidth,
            topLeft.Y + UnitSize * room.MapHeight);
        Surface.Canvas.DrawRect(paintedRect, paint);
        var textBlob = SKTextBlob.Create(name, font);
        Surface.Canvas.DrawText(textBlob, paintedRect.Left, paintedRect.Bottom, whitePaint);
    }

    private Vector2I MapCoordToImage(int mapX, int mapY)
    {
        return MapCoordToImage(new Vector2I(mapX, mapY));
    }

    private Vector2I MapCoordToImage(Vector2I mapCoord)
    {
        return new Vector2I(
            mapCoord.X * UnitSize,
            mapCoord.Y * UnitSize
        );
    }
}
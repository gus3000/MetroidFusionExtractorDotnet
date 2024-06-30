using MetroidFusionExtractor.Model.Game;
using MetroidFusionExtractor.Model.Game.Level;
using MetroidFusionExtractor.Model.Math;
using SkiaSharp;

namespace MetroidFusionExtractor.Image;

public class MapViz
{
    private const int UnitSize = 32;
    private const int Margin = 64;

    private const int MaxMapWidth = 32;
    private const int MaxMapHeight = 32;

    private readonly Random _random;

    private readonly SKSurface _surface;

    public MapViz(GameInfo game)
    {
        _random = new Random();
        // var bitmap = new SKBitmap(500, 500);
        var mapSize = MapCoordToImage(MaxMapWidth, MaxMapHeight);
        var info = new SKImageInfo(mapSize.X, mapSize.Y);
        _surface = SKSurface.Create(info);

        var canvas = _surface.Canvas;


        var i = 0;
        foreach (var area in Enum.GetValues(typeof(Area)).Cast<Area>())
        {
            canvas.Clear(SKColors.Black);
            var rooms = game.GetRooms(area);
            foreach (var room in rooms)
            {
                DrawRoom(room, $"{i}");
                i++;
                // if (i >= 10)
                // break;
            }

            Save($"map-{area}.png");
        }
        // foreach (var roomEntry in rom.RoomEntries)
        // {
        //     DrawRoom(roomEntry, $"{i}");
        //     i++;
        //     if (i >= 10)
        //         break;
        // }
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
        var paintedRect = new SKRect(topLeft.X, topLeft.Y, topLeft.X + UnitSize * room.Width, topLeft.Y + UnitSize * room.Height);
        _surface.Canvas.DrawRect(paintedRect, paint);
        var textBlob = SKTextBlob.Create(name, font);
        _surface.Canvas.DrawText(textBlob, paintedRect.Left, paintedRect.Bottom, whitePaint);
    }

    private void Save(string filename)
    {
        var image = _surface.Snapshot();
        var imageData = image.Encode();

        const string folderPath = ".";
        using (var stream = File.OpenWrite(Path.Combine(folderPath, filename)))
        {
            imageData.SaveTo(stream);
        }
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
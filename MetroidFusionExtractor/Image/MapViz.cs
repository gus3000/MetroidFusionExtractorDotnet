using System.Drawing;
using System.Numerics;
using MetroidFusionExtractor.Model;
using MetroidFusionExtractor.Model.Math;
using SkiaSharp;


namespace MetroidFusionExtractor.Image;

public class MapViz
{
    private const int UnitSize = 32;
    private const int Margin = 64;

    private const int MaxMapWidth = 32;
    private const int MaxMapHeight = 32;

    private SKSurface _surface;

    private Random _random;

    public MapViz(ROM rom)
    {
        _random = new Random();
        // var bitmap = new SKBitmap(500, 500);
        var mapSize = MapCoordToImage(MaxMapWidth, MaxMapHeight);
        var info = new SKImageInfo(mapSize.X,mapSize.Y);
        _surface = SKSurface.Create(info);

        SKCanvas canvas = _surface.Canvas;

        canvas.Clear(SKColors.Black);

        foreach (var roomEntry in rom.RoomEntries)
        {
            DrawRoom(roomEntry);
        }
        
        Save( "map.png");
    }

    private void DrawRoom(RoomEntry room)
    {
        SKPaint paint = new SKPaint();
        paint.Color = new SKColor((uint)_random.NextInt64());
        var topLeft = MapCoordToImage(room.mapXCoordinate,room.mapYCoordinate);
        _surface.Canvas.DrawRect(topLeft.X,topLeft.Y, UnitSize, UnitSize, paint);
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
using MetroidFusionExtractor.Model.Game;
using MetroidFusionExtractor.Model.Game.Level;
using SkiaSharp;

namespace MetroidFusionExtractor.Image;

public class RoomDrawer : AbstractDrawer
{
    public const int BlockPixelWidth = 16;
    public const int BlockPixelHeight = 16;

    public RoomDrawer()
    {
    }

    public void Draw(Room room, string name)
    {
        InitSurface(RoomWidth(room), RoomHeight(room));
        for (int y = 0; y < room.BlockHeight; y++)
        {
            for (int x = 0; x < room.BlockWidth; x++)
            {
                var hue = room.Blocks[x, y].ClipData;
                SKColor color = SKColor.FromHsl(hue, 100, 50);
                Console.WriteLine($"color : {color}");
                DrawRect(x, y, color);
            }
        }

        Save($"{name}.png", "rooms");
    }

    private void DrawRect(int x, int y, SKColor color)
    {
        var paint = new SKPaint();
        paint.Color = color;
        var paintedRect = GetPaintedRect(x, y);
        Surface.Canvas.DrawRect(paintedRect, paint);
    }

    private SKRect GetPaintedRect(int x, int y)
    {
        return new SKRect(
            x * BlockPixelWidth,
            y * BlockPixelHeight,
            (x + 1) * BlockPixelWidth,
            (y + 1) * BlockPixelHeight
        );
    }

    private int RoomWidth(Room room) => room.BlockWidth * BlockPixelWidth;
    private int RoomHeight(Room room) => room.BlockHeight * BlockPixelHeight;
}
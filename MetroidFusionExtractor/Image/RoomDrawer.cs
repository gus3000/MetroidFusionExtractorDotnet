using MetroidFusionExtractor.Model.Game.Level;
using SkiaSharp;

namespace MetroidFusionExtractor.Image;

public class RoomDrawer : AbstractDrawer
{
    public const int BlockPixelWidth = 32;
    public const int BlockPixelHeight = 32;

    public void Draw(Room room, string name)
    {
        InitSurface(RoomWidth(room), RoomHeight(room));
        for (var y = 0; y < room.BlockHeight; y++)
        for (var x = 0; x < room.BlockWidth; x++)
            DrawBlock(x, y, room.Blocks[x, y]);

        Save($"{name}.png", "rooms");
    }

    private void DrawBlock(int x, int y, Block block)
    {
        var hue = block.ClipData;
        var color = SKColor.FromHsl(hue * 17 % 100, 100, 50);
        // Console.WriteLine($"color : {color}");
        DrawRect(x, y, color);

        DrawText(x, y, $"{block.ClipData:X}");
    }

    private void DrawRect(int x, int y, SKColor color)
    {
        var paint = new SKPaint();
        paint.Color = color;
        var paintedRect = GetPaintedRect(x, y);
        Surface.Canvas.DrawRect(paintedRect, paint);
    }

    private void DrawText(int x, int y, string text)
    {
        var paint = new SKPaint();
        paint.Color = SKColors.Black;
        var paintedRect = GetPaintedRect(x, y);
        var textStart = new SKPoint(paintedRect.Left, paintedRect.Bottom);
        Surface.Canvas.DrawText(text, textStart, paint);
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

    private int RoomWidth(Room room)
    {
        return room.BlockWidth * BlockPixelWidth;
    }

    private int RoomHeight(Room room)
    {
        return room.BlockHeight * BlockPixelHeight;
    }
}
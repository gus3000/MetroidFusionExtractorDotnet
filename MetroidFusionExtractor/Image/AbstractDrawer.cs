using MetroidFusionExtractor.Model.Math;
using SkiaSharp;

namespace MetroidFusionExtractor.Image;

public abstract class AbstractDrawer
{
    protected SKSurface Surface { get; private set; } = SKSurface.CreateNull(0, 0);

    // public abstract void Draw();

    protected void InitSurface(Vector2I vi) => InitSurface(vi.X, vi.Y);

    protected void InitSurface(int width, int height)
    {
        var info = new SKImageInfo(width, height);
        Surface = SKSurface.Create(info);
        var canvas = Surface.Canvas;
        canvas.Clear(SKColors.Black);
    }


    protected void Save(string filename, string folderPath = ".")
    {
        var image = Surface.Snapshot();
        var imageData = image.Encode();

        Directory.CreateDirectory(folderPath);
        using (var stream = File.OpenWrite(Path.Combine(folderPath, filename)))
        {
            imageData.SaveTo(stream);
        }
    }
}
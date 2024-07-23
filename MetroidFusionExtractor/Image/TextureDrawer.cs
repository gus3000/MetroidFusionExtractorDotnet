using MetroidFusionExtractor.Model.Game.Level;
using SkiaSharp;

namespace MetroidFusionExtractor.Image;

public class TextureDrawer
{
    private const string AtlasPath = "./customAssets/clipdata_debug.png";
    private Dictionary<ClipDataValue, SKBitmap> _bitmaps;
    private SKBitmap unknownBitmap;

    private const int TEX_WIDTH = 16;
    private const int TEX_HEIGHT = 16;

    private SKFont _font;
    private SKBitmap _atlas;

    public TextureDrawer()
    {
        _bitmaps = new Dictionary<ClipDataValue, SKBitmap>();
        _font = new SKFont();
        _font.Size = TEX_HEIGHT - 4;

        InitBitmaps();
    }

    private void InitBitmaps()
    {
        _atlas = SKBitmap.Decode(AtlasPath);
        if (_atlas == null)
            throw new Exception($"atlas not found : {AtlasPath}");

        foreach (ClipDataValue clipData in Enum.GetValues(typeof(ClipDataValue)))
        {
            var bitmap = new SKBitmap(TEX_WIDTH, TEX_HEIGHT);
            _atlas.ExtractSubset(bitmap, GetBounds(clipData));
            _bitmaps[clipData] = bitmap;
        }

        unknownBitmap = new SKBitmap(TEX_WIDTH, TEX_HEIGHT);
        unknownBitmap.Erase(SKColors.Red);
    }

    private SKRectI GetBounds(ClipDataValue clipDataValue)
    {
        int cellsPerLine = _atlas.Width / TEX_WIDTH;
        int x = ((int)clipDataValue % cellsPerLine) * TEX_WIDTH;
        int y = ((int)clipDataValue / cellsPerLine) * TEX_HEIGHT;
        return new SKRectI(x, y, x + TEX_WIDTH, y + TEX_HEIGHT);
    }

    public SKBitmap From(ClipDataValue clipdata)
    {
        if (_bitmaps.ContainsKey(clipdata))
            return _bitmaps[clipdata];

        var bitmap = unknownBitmap.Copy();
        var canvas = new SKCanvas(bitmap);

        var paint = new SKPaint();
        paint.Color = SKColors.Black;
        canvas.DrawText($"{(int)clipdata:X}", 0, TEX_HEIGHT, _font, paint);

        return bitmap;
    }
}
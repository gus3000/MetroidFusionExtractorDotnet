using MetroidFusionExtractor.Model.Game.Level;
using SkiaSharp;

namespace MetroidFusionExtractor.Image;

public class TextureDrawer
{
    private Dictionary<ClipData.Values, SKBitmap> _bitmaps;
    private SKBitmap unknownBitmap;

    private const int TEX_WIDTH = 16;
    private const int TEX_HEIGHT = 16;

    public TextureDrawer()
    {
        _bitmaps = new Dictionary<ClipData.Values, SKBitmap>();

        var solid = new SKBitmap(TEX_WIDTH, TEX_HEIGHT);
        solid.Erase(SKColors.White);
        _bitmaps[ClipData.Values.Solid] = solid;

        var air = new SKBitmap(TEX_WIDTH, TEX_HEIGHT);
        air.Erase(SKColors.Transparent);
        _bitmaps[ClipData.Values.Air] = air;

        unknownBitmap = new SKBitmap(TEX_WIDTH, TEX_HEIGHT);
        unknownBitmap.Erase(SKColors.Red);
    }

    public SKBitmap From(ClipData.Values clipdata)
    {
        return _bitmaps.ContainsKey(clipdata) ? _bitmaps[clipdata] : unknownBitmap;
    }
}
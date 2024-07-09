namespace MetroidFusionExtractor.Model.Services.Compress;

public interface ICompressionService
{
    // public List<byte> Decompress(List<byte> compressedData);
    public List<byte> ReadCompressedData(uint start, int length);
    
}
using MetroidFusionExtractor.Model.Game;

namespace MetroidFusionExtractor.Model.Memory;

public class MemoryRoomEntry
{
    public const int Size = 0x3C;

    public const int AmountMainDeck = 0x57;
    public const int AmountSector1 = 0x36;
    public const int AmountSector2 = 0x3D;
    public const int AmountSector3 = 0x27;
    public const int AmountSector4 = 0x30;
    public const int AmountSector5 = 0x34;
    public const int AmountSector6 = 0x29;

    public const int AddressMainDeck = 0x3C32A8;
    public const int AddressSector1 = 0x3C470C;
    public const int AddressSector2 = 0x3C53B4;
    public const int AddressSector3 = 0x3C6200;
    public const int AddressSector4 = 0x3C7754;
    public const int AddressSector5 = 0x3C6B24;
    public const int AddressSector6 = 0x3C8294;


    public static int GetAmount(Area area)
    {
        switch (area)
        {
            case Area.MainDeck: return AmountMainDeck;
            case Area.Sector1: return AmountSector1;
            case Area.Sector2: return AmountSector2;
            case Area.Sector3: return AmountSector3;
            case Area.Sector4: return AmountSector4;
            case Area.Sector5: return AmountSector5;
            case Area.Sector6: return AmountSector6;
            default:
                throw new ArgumentOutOfRangeException(nameof(area), area, null);
        }
    }

    public static int GetAddress(Area area)
    {
        switch (area)
        {
            case Area.MainDeck: return AddressMainDeck;
            case Area.Sector1: return AddressSector1;
            case Area.Sector2: return AddressSector2;
            case Area.Sector3: return AddressSector3;
            case Area.Sector4: return AddressSector4;
            case Area.Sector5: return AddressSector5;
            case Area.Sector6: return AddressSector6;
            default:
                throw new ArgumentOutOfRangeException(nameof(area), area, null);
        }
    }
}
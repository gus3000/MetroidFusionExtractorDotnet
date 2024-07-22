namespace MetroidFusionExtractor.Model.Game.Level;

[Flags]
public enum BgProperties
{
    None = 0b0,
    RleCompressed = 0b10000,
    DimRooms = 0b10010,
    ElevatorHall = 0b10011,
    NettoriPowerRoomsOrRestrictedLabRooms = 0b10100,
    Lz77Compressed = 0b1000000,
    NightmareSRoom = 0b1000001,
    AlwaysDarkRoom = 0b1000011,
    SaXRoom = 0b1000100,
    DarkRoomOnMainDeck = 0b1000101,
    ElectricWaterRoom = 0b1000110,
    AtmosphericStabilizerRoom = 0b1000111,
    MeltdownRoom = 0b1001000,
    NightmareShadow = 0b1001001,
    OmegaMetroidRoom = 0b1001010
}

public static class Extensions
{
    public static bool IsRleCompressed(this BgProperties bgProperties)
    {
        return (bgProperties & BgProperties.RleCompressed) != BgProperties.None;
    }

    public static bool IsLz77Compressed(this BgProperties bgProperties)
    {
        return (bgProperties & BgProperties.Lz77Compressed) != BgProperties.None;
    }
}
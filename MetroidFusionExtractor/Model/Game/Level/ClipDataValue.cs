namespace MetroidFusionExtractor.Model.Game.Level;

public enum ClipDataValue
{
    Air = 0x00,
    Platform = 0x01,
    Solid = 0x10,
    SteepFloorSlopeUp = 0x11,
    SteepFloorSlopeDown = 0x12,
    LowerSlightFloorSlopeUp = 0x13,
    UpperSlightFloorSlopeUp = 0x14,
    UpperSlightFloorSlopeDown = 0x15,
    LowerSlightFloorSlopeDown = 0x16,

    SteepCeilingSlopeUp = 0x21,
    SteepCeilingSlopeDown = 0x22,
    LowerSlightCeilingSlopeUp = 0x23,
    UpperSlightCeilingSlopeUp = 0x24,
    UpperSlightCeilingSlopeDown = 0x25,
    LowerSlightCeilingSlopeDown = 0x26,

    Unknown = 0xFF
}
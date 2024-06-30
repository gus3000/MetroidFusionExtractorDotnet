using System.Runtime.InteropServices;

namespace MetroidFusionExtractor.Model.Memory;

public class MemoryUtils
{
    public enum Endianness
    {
        BigEndian,
        LittleEndian
    }

    internal static T BytesToStruct<T>(byte[] rawData, Endianness endianness) where T : class
    {
        T result; // = default(T);

        MaybeAdjustEndianness(typeof(T), rawData, endianness);

        var handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);

        try
        {
            var rawDataPtr = handle.AddrOfPinnedObject();
            result = (T)Marshal.PtrToStructure(rawDataPtr, typeof(T));
        }
        finally
        {
            handle.Free();
        }

        return result;
    }

    private static void MaybeAdjustEndianness(Type type, byte[] data, Endianness endianness, int startOffset = 0)
    {
        if (BitConverter.IsLittleEndian == (endianness == Endianness.LittleEndian))
            // nothing to change => return
            return;

        foreach (var field in type.GetFields())
        {
            var fieldType = field.FieldType;
            if (field.IsStatic)
                // don't process static fields
                continue;

            if (fieldType == typeof(string))
                // don't swap bytes for strings
                continue;

            var isRomPointer = field.CustomAttributes.Any(attributeData => attributeData.AttributeType == typeof(RomPointerAttribute));
            if (isRomPointer)
                continue;

            var offset = Marshal.OffsetOf(type, field.Name).ToInt32();

            // handle enums
            if (fieldType.IsEnum)
                fieldType = Enum.GetUnderlyingType(fieldType);

            // check for sub-fields to recurse if necessary
            var subFields = fieldType.GetFields().Where(subField => subField.IsStatic == false).ToArray();

            var effectiveOffset = startOffset + offset;

            if (subFields.Length == 0)
                Array.Reverse(data, effectiveOffset, Marshal.SizeOf(fieldType));
            else
                // recurse
                MaybeAdjustEndianness(fieldType, data, endianness, effectiveOffset);
        }
    }
}
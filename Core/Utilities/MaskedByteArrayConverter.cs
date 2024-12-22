using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Utilities;

public class MaskedByteArrayConverter : JsonConverter<byte[]>
{
    public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Eğer okuma işlemi gerekiyorsa, bu durumda bir işleme gerek yok
        return Array.Empty<byte>();
    }

    public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
    {
        // Her zaman '****' yaz
        writer.WriteStringValue("****");
    }
}
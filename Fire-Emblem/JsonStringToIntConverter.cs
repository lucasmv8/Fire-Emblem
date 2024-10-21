using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fire_Emblem;

public class JsonStringToIntConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string jsonStringValue = reader.GetString();
        if (int.TryParse(jsonStringValue, out int intValue))
        {
            return intValue;
        }
        throw new JsonException($"Unable to convert \"{jsonStringValue}\" to {nameof(Int32)}.");
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}
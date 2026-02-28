using System.Text.Json;
using System.Text.Json.Serialization;

namespace SelfEnumPersonType.SmartEnum
{
    public class SelfEnumConverter<T> : JsonConverter<T> where T : IParsable<T>
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();
            return T.Parse(stringValue ?? string.Empty, null);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }
    }
}

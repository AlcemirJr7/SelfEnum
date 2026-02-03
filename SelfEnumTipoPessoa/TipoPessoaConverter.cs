using System.Text.Json;
using System.Text.Json.Serialization;

namespace SelfEnumTipoPessoa.SmartEnum
{
    public class TipoPessoaConverter : JsonConverter<TipoPessoa>
    {
        public override TipoPessoa Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();

            return TipoPessoa.Parse(stringValue ?? string.Empty, null);
        }

        public override void Write(Utf8JsonWriter writer, TipoPessoa value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}

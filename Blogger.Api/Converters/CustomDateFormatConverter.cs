using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blogger.Converters
{
    public class CustomDateFormatConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParseExact(reader.GetString(), "yyyy-MM-dd", null, DateTimeStyles.None, out var result))
                {
                    return result;
                }
            }
            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }
}

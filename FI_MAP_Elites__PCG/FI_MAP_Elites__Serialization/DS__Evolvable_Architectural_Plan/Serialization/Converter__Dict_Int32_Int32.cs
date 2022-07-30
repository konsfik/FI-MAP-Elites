using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json.Serialization;
using System.Text.Json;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class Converter__Dict_Int32_Int32 : JsonConverter<Dictionary<int, int>>
    {
        public override Dictionary<int, int> Read(
            ref Utf8JsonReader reader, 
            Type typeToConvert, 
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var value = new Dictionary<int, int>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return value;
                }

                string keyString = reader.GetString();

                if (!int.TryParse(keyString, out int keyAsInt32))
                {
                    throw new JsonException($"Unable to convert \"{keyString}\" to System.Int32.");
                }

                reader.Read();



                int itemValue = reader.GetInt32();

                value.Add(keyAsInt32, itemValue);
            }

            throw new JsonException("Error Occured");
        }

        public override void Write(
            Utf8JsonWriter writer, 
            Dictionary<int, int> value, 
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach (KeyValuePair<int, int> item in value)
            {
                writer.WriteString(item.Key.ToString(), item.Value.ToString());
            }

            writer.WriteEndObject();
        }
    }
}

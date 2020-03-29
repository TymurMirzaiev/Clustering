using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Clustering.KMeans.Library.Data.JsonConverters
{
    public class FloatJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(float) || objectType == typeof(float?));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
            {
                return token.ToObject<float>();
            }
            if (token.Type == JTokenType.String)
            {
                // customize this to suit your needs
                return float.Parse(token.ToString());
            }
            if (token.Type == JTokenType.Null && objectType == typeof(double?))
            {
                return null;
            }
            throw new JsonSerializationException("Unexpected token type: " +
                                                  token.Type.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

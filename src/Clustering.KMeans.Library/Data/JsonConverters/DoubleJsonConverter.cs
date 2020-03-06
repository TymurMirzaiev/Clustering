using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Clustering.KMeans.Library.Data.JsonConverters
{
    class DoubleJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(double) || objectType == typeof(double?));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
            {
                return token.ToObject<double>();
            }
            if (token.Type == JTokenType.String)
            {
                // customize this to suit your needs
                return Double.Parse(token.ToString(),
                       System.Globalization.CultureInfo.GetCultureInfo("es-ES"));
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

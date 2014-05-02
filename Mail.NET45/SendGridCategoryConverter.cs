using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SendGrid
{

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Thanks to Brian Rogers for his help from this post: http://stackoverflow.com/a/18997172/403765
    /// </remarks>
    internal class SendGridCategoryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true; // add your own logic
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            switch (token.Type)
            {
                case JTokenType.String:
                    return new List<string> { token.ToObject<string>() };
                case JTokenType.Array:
                    return token.ToObject<List<string>>();
            }
            throw new JsonSerializationException("Unexpected token type: " + token.Type.ToString());
        }


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Tbax.Json
{
    /// <summary>
    /// Represents a null entry in JSON
    /// </summary>
    public struct JsonNull : IJsonable
    {
        /// <summary>
        /// Creates a JsonNull from a given JSON string
        /// </summary>
        /// <param name="json">JSON to parse</param>
        /// <returns>A JsonNull object</returns>
        public static JsonNull FromJson(string json)
        {
            if (json.Trim().Equals("null", StringComparison.OrdinalIgnoreCase)) {
                return new JsonNull();
            }
            else {
                throw JsonException.UnexpectedJson();
            }
        }

        /// <summary>
        /// Always returns a null string object
        /// </summary>
        /// <returns>a null string object</returns>
        public override string ToString()
        {
            return "null";
        }

        public string ToJson(JsonWriterOptions options)
        {
            return "null";
        }
    }
}

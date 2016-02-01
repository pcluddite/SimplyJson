using System;

namespace Tbax.Json
{
    /// <summary>
    /// Represents an object that can be converted to json
    /// </summary>
    public abstract class JsonObject : IJsonObject
    {
        /// <summary>
        /// Represents a Null value
        /// </summary>
        public static readonly JsonObject Null = new JsonNull();

        /// <summary>
        /// Converts the JsonObject into a valid JSON string
        /// </summary>
        /// <param name="options">When overriden in a derived class, the JsonWriterOptions to acknowledge when formatting.</param>
        /// <returns>The JSON for this object</returns>
        public abstract string ToJson(JsonWriterOptions options);

        /// <summary>
        /// Converts the JsonObject into a valid JSON string using the default formatting options
        /// </summary>
        /// <returns>The JSON for this object</returns>
        public virtual string ToJson()
        {
            return ToJson(JsonWriterOptions.Default);
        }

        /// <summary>
        /// Represents a null entry in JSON
        /// </summary>
        internal class JsonNull : JsonObject
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
                    throw JsonException.InvalidFormat();
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

            public override string ToJson(JsonWriterOptions options)
            {
                return "null";
            }
        }
    }
}
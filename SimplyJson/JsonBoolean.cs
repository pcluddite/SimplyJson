using System;

namespace Tbax.Json
{
    /// <summary>
    /// A class representation of a Boolean value in JSON. This class can perform most operations directly as a bool implicitly.
    /// </summary>
    public sealed class JsonBoolean : JsonObject
    {
        // REMEMBER NOT TO USE IMPLICIT CONVERSIONS IN THIS CLASS (for safety reasons)
        private bool innerBool;

        /// <summary>
        /// Initializes a new JsonBoolean
        /// </summary>
        /// <param name="value">the bool value that this JsonBoolean represents</param>
        public JsonBoolean(bool value)
        {
            innerBool = value;
        }

        /// <summary>
        /// Converts the JsonBoolean into the bool value it represents
        /// </summary>
        /// <returns>the bool value this JsonBoolean represents</returns>
        public bool ToBoolean()
        {
            return innerBool;
        }

        /// <summary>
        /// Converts this object to a string that can be used in JSON
        /// </summary>
        /// <param name="options">the JsonWriterOptions to use for formatting</param>
        /// <returns>a valid JSON string</returns>
        public override string ToJson(JsonWriterOptions options)
        {
            return innerBool.ToString();
        }

        /// <summary>
        /// Converts the value of this instance to its equivalent string representation (either "True" or "False")
        /// </summary>
        public sealed override string ToString()
        {
            return innerBool.ToString();
        }

        /// <summary>
        /// Initializes a JsonBoolean object from given json
        /// </summary>
        /// <param name="json">json to parse</param>
        /// <returns>A JsonBoolean object</returns>
        public static JsonBoolean FromJson(string json)
        {
            bool b;
            if (bool.TryParse(json.Trim(), out b)) {
                return new JsonBoolean(b);
            }
            else {
                throw new FormatException("The string did not contain the expected json");
            }
        }

        /// <summary>
        /// Implicitly converts a JsonBoolean into the bool that it represents
        /// </summary>
        /// <param name="jBool">the JsonBoolean</param>
        /// <returns>the bool value the JsonBoolean represents</returns>
        public static implicit operator bool(JsonBoolean jBool)
        {
            return jBool.innerBool;
        }

        /// <summary>
        /// Implicitly converts a bool into a JsonBoolean representing it
        /// </summary>
        /// <param name="b">the bool value</param>
        /// <returns>the JsonBoolean representing the given double</returns>
        public static implicit operator JsonBoolean(bool b)
        {
            return new JsonBoolean(b);
        }
    }
}

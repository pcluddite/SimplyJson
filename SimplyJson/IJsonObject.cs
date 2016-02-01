
namespace Tbax.Json
{
    /// <summary>
    /// Interface for objects that can be converted into a valid JSON
    /// </summary>
    public interface IJsonObject
    {
        /// <summary>
        /// Converts the JsonObject into a valid JSON string
        /// </summary>
        /// <param name="options">When overriden in a derived class, the JsonWriterOptions to acknowledge when formatting.</param>
        /// <returns>The JSON for this object</returns>
        string ToJson(JsonWriterOptions options);
    }
}

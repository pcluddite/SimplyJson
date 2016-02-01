using System;
using System.Runtime.Serialization;

namespace Tbax.Json
{
    public class JsonException : Exception
    {
        public JsonException(string message)
            : base(message)
        {
        }

        public JsonException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public JsonException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        internal static JsonException InvalidFormat()
        {
            return new JsonException("string could not be parsed", new FormatException());
        }

        internal static JsonException EndOfFile(char c)
        {
            return new JsonException("missing closing '" + c + "'", new FormatException());
        }
    }
}
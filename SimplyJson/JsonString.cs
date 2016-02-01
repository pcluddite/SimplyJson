using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Tbax.Json
{
    /// <summary>
    /// Represents a string to be formatted in JSON
    /// </summary>
    public sealed class JsonString : IJsonObject
    {
        // REMEMBER NOT TO USE IMPLICIT CONVERSIONS IN THIS CLASS (for safety reasons)
        private string innerString;

        /// <summary>
        /// Initializes a new JsonString object
        /// </summary>
        /// <param name="value">the string that will be formatted for JSON</param>
        public JsonString(string value)
        {
            innerString = value;
        }

        /// <summary>
        /// Converts this object to the string that this object represents 
        /// </summary>
        /// <returns>the string that has not yet been formatted</returns>
        public override string ToString()
        {
            return innerString;
        }

        /// <summary>
        /// Converts this object to a string that can be used in JSON.
        /// </summary>
        /// <remarks>
        /// This will add double quotes around the string and format escaped characters.
        /// If the string contains double quotes, it will surround it with single quotes instead
        /// </remarks>
        /// <returns>a valid JSON string</returns>
        public string ToJSON()
        {
            if (innerString.Contains("\"")) {
                return string.Format("'{0}'", FormatAsJsonString(innerString, '\''));
            }
            else {
                return string.Format("\"{0}\"", FormatAsJsonString(innerString));
            }
        }

        /// <summary>
        /// Converts this object to a string that can be used in JSON
        /// </summary>
        /// <param name="options">the JsonWriterOptions to use for formatting</param>
        /// <returns>a valid JSON string</returns>
        public string ToJson(JsonWriterOptions options)
        {
            return ToJSON();
        }

        private static readonly Regex validString = new Regex(@"\""((\\"")|[^""])*\""|\'((\\')|[^'])*\'", RegexOptions.Compiled);

        /// <summary>
        /// Initializes a JsonString object from given JSON. This will return the first valid string.
        /// </summary>
        /// <param name="json">JSON to parse</param>
        /// <returns>A JsonString object</returns>
        public static JsonString FromJson(string json)
        {

            foreach (Match m in validString.Matches(json)) {
                return new JsonString(
                    FormatAsNormalString(m.Value.Trim().Substring(1, m.Value.Length - 2))
                    );
            }

            throw new FormatException("The string did not contain the expected JSON");
        }

        /// <summary>
        /// Implicitly converts a JsonString to a System.String
        /// </summary>
        /// <param name="jString">the JsonString</param>
        /// <returns>the new System.String object</returns>
        public static implicit operator String(JsonString jString)
        {
            return jString.innerString;
        }

        /// <summary>
        /// Implicitly converts a System.String to a JsonString
        /// </summary>
        /// <param name="str">the System.String</param>
        /// <returns>the new JsonString object</returns>
        public static implicit operator JsonString(String str)
        {
            return new JsonString(str);
        }

        internal static string FormatAsJsonString(string s)
        {
            if (s.Contains("\"")) {
                return FormatAsJsonString(s, '\'');
            }
            else {
                return FormatAsJsonString(s, '"');
            }
        }

        internal static string FormatAsJsonString(string str, char quote)
        {
            StringBuilder sb = new StringBuilder();
            char last = '\0';
            foreach (char cur in str) {
                switch (cur) {
                    case '\\':
                        sb.Append('\\');
                        sb.Append(cur);
                        break;
                    case '\'':
                    case '"':
                        if (quote == cur) {
                            sb.Append('\\');
                        }
                        sb.Append(cur);
                        break;
                    case '/':
                        if (last == '<') {
                            sb.Append('\\');
                        }
                        sb.Append(cur);
                        break;
                    case '\b': sb.Append("\\b"); break;
                    case '\t': sb.Append("\\t"); break;
                    case '\n': sb.Append("\\n"); break;
                    case '\f': sb.Append("\\f"); break;
                    case '\r': sb.Append("\\r"); break;
                    default:
                        if (cur < ' ') {
                            sb.Append("\\u" + ((int)cur).ToString("x4", CultureInfo.InvariantCulture));
                        }
                        else {
                            sb.Append(cur);
                        }
                        break;

                }
                last = cur;
            }

            return sb.ToString();
        }

        internal static string FormatAsNormalString(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < str.Length; index++) {
                switch (str[index]) {
                    case '\n':
                    case '\r':
                        throw new JsonException("unterminated string");
                    case '\\':
                        index++;
                        if (index >= str.Length) {
                            throw new JsonException("unterminated escape sequence");
                        }
                        switch (str[index]) {
                            case 'b': sb.Append('\b'); break;
                            case 't': sb.Append('\t'); break;
                            case 'n': sb.Append('\n'); break;
                            case 'f': sb.Append('\f'); break;
                            case 'r': sb.Append('\r'); break;
                            case '"': sb.Append('"'); break;
                            case '\'': sb.Append('\''); break;
                            case '\\': sb.Append('\\'); break;
                            case 'u':
                                index += 4;
                                if (index >= str.Length) {
                                    throw new JsonException("unterminated escape sequence. Expected four digit hex to follow '\\u'.");
                                }
                                sb.Append((char)ushort.Parse(str.Substring(index - 3, 4), NumberStyles.HexNumber));
                                break;
                            default:
                                throw new JsonException("unknown escape sequence: \\" + str[index]);
                        }
                        break;
                    default:
                        sb.Append(str[index]);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}

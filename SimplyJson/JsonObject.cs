/**
 *  SimplyJSON
 *  Copyright (C) 2014-2016 Timothy Baxendale
 *  
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 2.1 of the License, or (at your option) any later version.
 *  
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *  
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 *  USA
 **/
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

            public override string ToJson(JsonWriterOptions options)
            {
                return "null";
            }
        }
    }
}
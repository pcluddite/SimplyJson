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
    /// A class representation of a Boolean value in json. This class can perform most operations directly as a bool implicitly.
    /// </summary>
    public struct JsonBoolean : IJsonable, IComparable, IComparable<JsonBoolean>, IComparable<bool>, IEquatable<JsonBoolean>, IEquatable<bool>
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
        public string ToJson(JsonWriterOptions options)
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
                throw JsonException.UnexpectedJson();
            }
        }

        public int CompareTo(object obj)
        {
            JsonBoolean? jb = obj as JsonBoolean?;
            if (jb != null)
                return CompareTo(jb.Value);
            bool? b = obj as bool?;
            if (b != null)
                return CompareTo(b.Value);
            throw new ArgumentException("can only compare types JsonBoolean and bool", nameof(obj));
        }

        public int CompareTo(JsonBoolean other)
        {
            return innerBool.CompareTo(other.innerBool);
        }
        
        public bool Equals(JsonBoolean other)
        {
            return innerBool == other.innerBool;
        }

        public int CompareTo(bool other)
        {
            return innerBool.CompareTo(other);
        }

        public bool Equals(bool other)
        {
            return innerBool == other;
        }

        public override bool Equals(object obj)
        {
            JsonBoolean? jb = obj as JsonBoolean?;
            if (jb != null)
                return Equals(jb.Value);
            bool? b = obj as bool?;
            if (b != null)
                return Equals(b.Value);
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return innerBool.GetHashCode();
        }

        #region equality ops

        /// <summary>
        /// Determines if the bool values that two JsonBoolean objects represent are equal 
        /// </summary>
        /// <param name="first">The first JsonBoolean to compare</param>
        /// <param name="second">The second JsonBoolean to compare</param>
        /// <returns>True if the bool values are equal, otherwise false</returns>
        public static bool operator ==(JsonBoolean first, JsonBoolean second)
        {
            return first.innerBool == second.innerBool;
        }

        /// <summary>
        /// Determines if the bool values that two JsonBoolean objects represent are not equal 
        /// </summary>
        /// <param name="first">The first JsonBoolean to compare</param>
        /// <param name="second">The second JsonBoolean to compare</param>
        /// <returns>True if the bool values are not equal, otherwise false</returns>
        public static bool operator !=(JsonBoolean first, JsonBoolean second)
        {
            return first.innerBool != second.innerBool;
        }

        #endregion

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

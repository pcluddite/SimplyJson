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
    /// A class representation of a number value in json. This class can perform most operations directly as a decimal implicitly.
    /// </summary>
    public struct JsonNumber : IJsonable, IEquatable<JsonNumber>, IEquatable<decimal>, IComparable, IComparable<JsonNumber>, IComparable<decimal>
    {
        private decimal innerNumber; // REMEMBER NOT TO USE IMPLICIT CONVERSIONS IN THIS CLASS (for safety reasons)

        /// <summary>
        /// Initializes a new JsonNumber
        /// </summary>
        /// <param name="value">the decimal value of this JsonNumber</param>
        public JsonNumber(decimal value)
        {
            innerNumber = value;
        }

        /// <summary>
        /// Converts this object to a decimal
        /// </summary>
        /// <returns>the decimal this object represents</returns>
        public decimal ToDecimal()
        {
            return innerNumber;
        }

        /// <summary>
        /// Initializes a JsonNumber object from given JSON
        /// </summary>
        /// <param name="json">JSON to parse</param>
        /// <returns>A JsonNumber object</returns>
        public static JsonNumber FromJson(string json)
        {
            decimal d;
            if (decimal.TryParse(json.Trim(), out d)) {
                return new JsonNumber(d);
            }
            else {
                throw JsonException.UnexpectedJson();
            }
        }

        /// <summary>
        /// Converts this object to a string that can be used in JSON
        /// </summary>
        /// <param name="options">the JsonWriterOptions to use for formatting</param>
        /// <returns>a valid JSON number in a string</returns>
        public string ToJson(JsonWriterOptions options)
        {
            return innerNumber.ToString();
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation
        /// </summary>
        public override string ToString()
        {
            return innerNumber.ToString();
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation
        /// </summary>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <param name="format">A numeric format string</param>
        public string ToString(string format, IFormatProvider provider)
        {
            return innerNumber.ToString(format, provider);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        public string ToString(IFormatProvider provider)
        {
            return innerNumber.ToString(provider);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation, using the specified format
        /// </summary>
        /// <param name="format">A numeric format string</param>
        public string ToString(string format)
        {
            return innerNumber.ToString(format);
        }

        /// <summary>
        /// Determines whether the specified JsonNumber is equal to the current JsonNumber. A JsonNumber is equal if it represents the same decimal value. If the comparison is to any other object, this calls the base method Object.Equals()
        /// </summary>
        /// <param name="obj">The System.Object to compare to the current System.Object</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is JsonNumber) {
                return this.innerNumber == ((JsonNumber)obj).innerNumber;
            }
            else {
                return base.Equals(obj);
            }
        }

        /// <summary>
        /// Returns the hash code for this instance. Its hash code is equal to the hash code that this decimal represents
        /// </summary>
        /// <returns>the hash code</returns>
        public override int GetHashCode()
        {
            return innerNumber.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified JsonNumber is equal to the current JsonNumber. A JsonNumber is equal if it represents the same decimal value.
        /// </summary>
        /// <param name="other">The JsonNumber to compare to the current JsonNumber</param>
        /// <returns></returns>
        public bool Equals(JsonNumber other)
        {
            return innerNumber == other.innerNumber;
        }

        /// <summary>
        /// Determines whether the specified decimal is equal to the current JsonNumber. A JsonNumber is equal if it represents the same decimal value.
        /// </summary>
        /// <param name="other">The decimal to compare to the current JsonNumber</param>
        /// <returns></returns>
        public bool Equals(decimal other)
        {
            return innerNumber == other;
        }

        public int CompareTo(object obj)
        {
            JsonNumber? jd = obj as JsonNumber?;
            if (jd != null)
                return CompareTo(jd.Value);
            decimal? d = obj as decimal?;
            if (d != null)
                return CompareTo(d.Value);
            throw new ArgumentException("can only compare JsonNumber or decimal", nameof(obj));
        }

        public int CompareTo(JsonNumber other)
        {
            return CompareTo(other.innerNumber);
        }

        public int CompareTo(decimal other)
        {
            return innerNumber.CompareTo(other);
        }

        /// <summary>
        /// Returns a new JsonNumber object as the deincrement operation is performed on the decimal it represents
        /// </summary>
        /// <param name="first">the JsonNumber to deincrement</param>
        /// <returns></returns>
        public static JsonNumber operator --(JsonNumber first)
        {
            return new JsonNumber(first.innerNumber--);
        }

        /// <summary>
        /// Returns a new JsonNumber object as the increment operation is performed on the decimal it represents
        /// </summary>
        /// <param name="first">the JsonNumber to increment</param>
        /// <returns></returns>
        public static JsonNumber operator ++(JsonNumber first)
        {
            return new JsonNumber(first.innerNumber++);
        }

        #region equality ops

        /// <summary>
        /// Determines if the decimal values that two JsonNumber objects represent are equal 
        /// </summary>
        /// <param name="first">The first JsonNumber to compare</param>
        /// <param name="second">The second JsonNumber to compare</param>
        /// <returns>True if the decimal values are equal, otherwise false</returns>
        public static bool operator ==(JsonNumber first, JsonNumber second)
        {
            return first.innerNumber == second.innerNumber;
        }

        /// <summary>
        /// Determines if the decimal values that two JsonNumber objects represent are not equal 
        /// </summary>
        /// <param name="first">The first JsonNumber to compare</param>
        /// <param name="second">The second JsonNumber to compare</param>
        /// <returns>True if the decimal values are not equal, otherwise false</returns>
        public static bool operator !=(JsonNumber first, JsonNumber second)
        {
            return first.innerNumber != second.innerNumber;
        }

        /// <summary>
        /// Determines if the decimal value the first JsonNumber represents is less than the decimal value the second JsonNumber represents
        /// </summary>
        /// <param name="first">The first JsonNumber to compare</param>
        /// <param name="second">The second JsonNumber to compare</param>
        /// <returns>True if the first decimal value is less than the second, otherwise false</returns>
        public static bool operator <(JsonNumber first, JsonNumber second)
        {
            return first.innerNumber < second.innerNumber;
        }

        /// <summary>
        /// Determines if the decimal value the first JsonNumber represents is greater than the decimal value the second JsonNumber represents
        /// </summary>
        /// <param name="first">The first JsonNumber to compare</param>
        /// <param name="second">The second JsonNumber to compare</param>
        /// <returns>True if the first decimal value is greater than the second, otherwise false</returns>
        public static bool operator >(JsonNumber first, JsonNumber second)
        {
            return first.innerNumber > second.innerNumber;
        }

        /// <summary>
        /// Determines if the decimal value the first JsonNumber represents is less than or equal to the decimal value the second JsonNumber represents
        /// </summary>
        /// <param name="first">The first JsonNumber to compare</param>
        /// <param name="second">The second JsonNumber to compare</param>
        /// <returns>True if the first decimal value is less than or equal to the second, otherwise false</returns>
        public static bool operator <=(JsonNumber first, JsonNumber second)
        {
            return first.innerNumber <= second.innerNumber;
        }

        /// <summary>
        /// Determines if the decimal value the first JsonNumber represents is greater than or equal to the decimal value the second JsonNumber represents
        /// </summary>
        /// <param name="first">The first JsonNumber to compare</param>
        /// <param name="second">The second JsonNumber to compare</param>
        /// <returns>True if the first decimal value is is greater than or equal to the second, otherwise false</returns>
        public static bool operator >=(JsonNumber first, JsonNumber second)
        {
            return first.innerNumber >= second.innerNumber;
        }

        #endregion

        #region arithmetic ops

        /// <summary>
        /// Performs a modulus operation on the decimal values that two JsonNumber objects represent
        /// </summary>
        /// <param name="first">The first operand</param>
        /// <param name="second">The second operand</param>
        /// <returns>A new JsonNumber representing the resuling decimal</returns>
        public static JsonNumber operator %(JsonNumber first, JsonNumber second)
        {
            return new JsonNumber(first.innerNumber % second.innerNumber);
        }

        /// <summary>
        /// Performs an addition operation on the decimal values that two JsonNumber objects represent
        /// </summary>
        /// <param name="first">The first operand</param>
        /// <param name="second">The second operand</param>
        /// <returns>A new JsonNumber representing the resuling decimal</returns>
        public static JsonNumber operator +(JsonNumber first, JsonNumber second)
        {
            return new JsonNumber(first.innerNumber + second.innerNumber);
        }

        /// <summary>
        /// Performs a subtraction operation on the decimal values that two JsonNumber objects represent
        /// </summary>
        /// <param name="first">The first operand</param>
        /// <param name="second">The second operand</param>
        /// <returns>A new JsonNumber representing the resuling decimal</returns>
        public static JsonNumber operator -(JsonNumber first, JsonNumber second)
        {
            return new JsonNumber(first.innerNumber - second.innerNumber);
        }

        /// <summary>
        /// Performs a division operation on the decimal values that two JsonNumber objects represent
        /// </summary>
        /// <param name="first">The first operand</param>
        /// <param name="second">The second operand</param>
        /// <returns>A new JsonNumber representing the resuling decimal</returns>
        public static JsonNumber operator /(JsonNumber first, JsonNumber second)
        {
            return new JsonNumber(first.innerNumber / second.innerNumber);
        }

        /// <summary>
        /// Performs a multiplication operation on the decimal values that two JsonNumber objects represent
        /// </summary>
        /// <param name="first">The first operand</param>
        /// <param name="second">The second operand</param>
        /// <returns>A new JsonNumber representing the resuling decimal</returns>
        public static JsonNumber operator *(JsonNumber first, JsonNumber second)
        {
            return new JsonNumber(first.innerNumber * second.innerNumber);
        }

        #endregion

        /// <summary>
        /// Implicitly converts a JsonNumber into the decimal that it represents
        /// </summary>
        /// <param name="jD">the JsonNumber</param>
        /// <returns>the decimal value the JsonNumber represents</returns>
        public static implicit operator decimal(JsonNumber jD)
        {
            return jD.ToDecimal();
        }

        /// <summary>
        /// Implicitly converts a decimal into a JsonNumber representing it
        /// </summary>
        /// <param name="d">the decimal value</param>
        /// <returns>the JsonNumber representing the given decimal</returns>
        public static implicit operator JsonNumber(decimal d)
        {
            return new JsonNumber(d);
        }
    }
}

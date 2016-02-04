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
    /// A class representation of a number value in JSON. This class can perform most operations directly as a double implicitly.
    /// </summary>
    public struct JsonDouble : IJsonObject
    {
        private double innerDouble; // REMEMBER NOT TO USE IMPLICIT CONVERSIONS IN THIS CLASS (for safety reasons)

        /// <summary>
        /// Initializes a new JsonDouble
        /// </summary>
        /// <param name="value">the double value of this JsonDouble</param>
        public JsonDouble(double value)
        {
            innerDouble = value;
        }

        /// <summary>
        /// Converts this object to a double
        /// </summary>
        /// <returns>the double this object represents</returns>
        public double ToDouble()
        {
            return innerDouble;
        }

        /// <summary>
        /// Initializes a JsonDouble object from given JSON
        /// </summary>
        /// <param name="json">JSON to parse</param>
        /// <returns>A JsonDouble object</returns>
        public static JsonDouble FromJson(string json)
        {
            double d;
            if (double.TryParse(json.Trim(), out d)) {
                return new JsonDouble(d);
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
            return innerDouble.ToString();
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation
        /// </summary>
        public override string ToString()
        {
            return innerDouble.ToString();
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation
        /// </summary>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <param name="format">A numeric format string</param>
        public string ToString(string format, IFormatProvider provider)
        {
            return innerDouble.ToString(format, provider);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        public string ToString(IFormatProvider provider)
        {
            return innerDouble.ToString(provider);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation, using the specified format
        /// </summary>
        /// <param name="format">A numeric format string</param>
        public string ToString(string format)
        {
            return innerDouble.ToString(format);
        }

        /// <summary>
        /// Determines whether the specified JsonDouble is equal to the current JsonDouble. A JsonDouble is equal if it represents the same double value. If the comparison is to any other object, this calls the base method Object.Equals()
        /// </summary>
        /// <param name="obj">The System.Object to compare to the current System.Object</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is JsonDouble) {
                return this.innerDouble == ((JsonDouble)obj).innerDouble;
            }
            else {
                return base.Equals(obj);
            }
        }

        /// <summary>
        /// Returns the hash code for this instance. Its hash code is equal to the hash code that this double represents
        /// </summary>
        /// <returns>the hash code</returns>
        public override int GetHashCode()
        {
            return innerDouble.GetHashCode();
        }

        /// <summary>
        /// Returns a new JsonDouble object as the deincrement operation is performed on the double it represents
        /// </summary>
        /// <param name="first">the JsonDouble to deincrement</param>
        /// <returns></returns>
        public static JsonDouble operator --(JsonDouble first)
        {
            return new JsonDouble(first.innerDouble--);
        }

        /// <summary>
        /// Returns a new JsonDouble object as the increment operation is performed on the double it represents
        /// </summary>
        /// <param name="first">the JsonDouble to increment</param>
        /// <returns></returns>
        public static JsonDouble operator ++(JsonDouble first)
        {
            return new JsonDouble(first.innerDouble++);
        }

        /// <summary>
        /// Determines if the double values that two JsonDouble objects represent are equal 
        /// </summary>
        /// <param name="first">The first JsonDouble to compare</param>
        /// <param name="second">The second JsonDouble to compare</param>
        /// <returns>True if the double values are equal, otherwise false</returns>
        public static bool operator ==(JsonDouble first, JsonDouble second)
        {
            return first.innerDouble == second.innerDouble;
        }

        /// <summary>
        /// Determines if the double values that two JsonDouble objects represent are not equal 
        /// </summary>
        /// <param name="first">The first JsonDouble to compare</param>
        /// <param name="second">The second JsonDouble to compare</param>
        /// <returns>True if the double values are not equal, otherwise false</returns>
        public static bool operator !=(JsonDouble first, JsonDouble second)
        {
            return first.innerDouble != second.innerDouble;
        }

        /// <summary>
        /// Determines if the double value the first JsonDouble represents is less than the double value the second JsonDouble represents
        /// </summary>
        /// <param name="first">The first JsonDouble to compare</param>
        /// <param name="second">The second JsonDouble to compare</param>
        /// <returns>True if the first double value is less than the second, otherwise false</returns>
        public static bool operator <(JsonDouble first, JsonDouble second)
        {
            return first.innerDouble < second.innerDouble;
        }

        /// <summary>
        /// Determines if the double value the first JsonDouble represents is greater than the double value the second JsonDouble represents
        /// </summary>
        /// <param name="first">The first JsonDouble to compare</param>
        /// <param name="second">The second JsonDouble to compare</param>
        /// <returns>True if the first double value is greater than the second, otherwise false</returns>
        public static bool operator >(JsonDouble first, JsonDouble second)
        {
            return first.innerDouble > second.innerDouble;
        }

        /// <summary>
        /// Determines if the double value the first JsonDouble represents is less than or equal to the double value the second JsonDouble represents
        /// </summary>
        /// <param name="first">The first JsonDouble to compare</param>
        /// <param name="second">The second JsonDouble to compare</param>
        /// <returns>True if the first double value is less than or equal to the second, otherwise false</returns>
        public static bool operator <=(JsonDouble first, JsonDouble second)
        {
            return first.innerDouble <= second.innerDouble;
        }

        /// <summary>
        /// Determines if the double value the first JsonDouble represents is greater than or equal to the double value the second JsonDouble represents
        /// </summary>
        /// <param name="first">The first JsonDouble to compare</param>
        /// <param name="second">The second JsonDouble to compare</param>
        /// <returns>True if the first double value is is greater than or equal to the second, otherwise false</returns>
        public static bool operator >=(JsonDouble first, JsonDouble second)
        {
            return first.innerDouble >= second.innerDouble;
        }

        /// <summary>
        /// Performs a modulus operation on the double values that two JsonDouble objects represent
        /// </summary>
        /// <param name="first">The first operand</param>
        /// <param name="second">The second operand</param>
        /// <returns>A new JsonDouble representing the resuling double</returns>
        public static JsonDouble operator %(JsonDouble first, JsonDouble second)
        {
            return new JsonDouble(first.innerDouble % second.innerDouble);
        }

        /// <summary>
        /// Performs an addition operation on the double values that two JsonDouble objects represent
        /// </summary>
        /// <param name="first">The first operand</param>
        /// <param name="second">The second operand</param>
        /// <returns>A new JsonDouble representing the resuling double</returns>
        public static JsonDouble operator +(JsonDouble first, JsonDouble second)
        {
            return new JsonDouble(first.innerDouble + second.innerDouble);
        }

        /// <summary>
        /// Performs a subtraction operation on the double values that two JsonDouble objects represent
        /// </summary>
        /// <param name="first">The first operand</param>
        /// <param name="second">The second operand</param>
        /// <returns>A new JsonDouble representing the resuling double</returns>
        public static JsonDouble operator -(JsonDouble first, JsonDouble second)
        {
            return new JsonDouble(first.innerDouble - second.innerDouble);
        }

        /// <summary>
        /// Performs a division operation on the double values that two JsonDouble objects represent
        /// </summary>
        /// <param name="first">The first operand</param>
        /// <param name="second">The second operand</param>
        /// <returns>A new JsonDouble representing the resuling double</returns>
        public static JsonDouble operator /(JsonDouble first, JsonDouble second)
        {
            return new JsonDouble(first.innerDouble / second.innerDouble);
        }

        /// <summary>
        /// Performs a multiplication operation on the double values that two JsonDouble objects represent
        /// </summary>
        /// <param name="first">The first operand</param>
        /// <param name="second">The second operand</param>
        /// <returns>A new JsonDouble representing the resuling double</returns>
        public static JsonDouble operator *(JsonDouble first, JsonDouble second)
        {
            return new JsonDouble(first.innerDouble * second.innerDouble);
        }

        /// <summary>
        /// Implicitly converts a JsonDouble into the double that it represents
        /// </summary>
        /// <param name="jD">the JsonDouble</param>
        /// <returns>the double value the JsonDouble represents</returns>
        public static implicit operator double(JsonDouble jD)
        {
            return jD.ToDouble();
        }

        /// <summary>
        /// Implicitly converts a double into a JsonDouble representing it
        /// </summary>
        /// <param name="d">the double value</param>
        /// <returns>the JsonDouble representing the given double</returns>
        public static implicit operator JsonDouble(double d)
        {
            return new JsonDouble(d);
        }
    }
}

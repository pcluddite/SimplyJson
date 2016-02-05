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
using System.Runtime.Serialization;

namespace Tbax.Json
{
    /// <summary>
    /// Represents a FormatException pertaining to Tbax.Json parsing
    /// </summary>
    public class JsonException : FormatException
    {
        /// <summary>
        /// Initializes a new JsonException with a specified message
        /// </summary>
        /// <param name="message">the message for this exception</param>
        public JsonException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new JsonException with a specified message and inner exception
        /// </summary>
        /// <param name="message">the message for this exception</param>
        /// <param name="innerException">inner exception</param>
        public JsonException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a JsonException with serialization data
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public JsonException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        internal static JsonException UnexpectedJson()
        {
            return new JsonException("The string did not contain the expected json.");
        }

        internal static JsonException InvalidCollection()
        {
            return new JsonException("The format of the collection was invalid.");
        }

        internal static JsonException InvalidElementInCollection(string elem)
        {
            return new JsonException(string.Format("Unable to parse an element in the collection - {0}", elem));
        }

        internal static JsonException EndOfFile(char c)
        {
            return new JsonException("missing closing '" + c + "'");
        }
    }
}
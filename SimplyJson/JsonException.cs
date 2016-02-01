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
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

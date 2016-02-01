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
    /// Format options when create JSON strings
    /// </summary>
    [Flags]
    public enum JsonWriterOptions
    {
        /// <summary>
        /// The default options (MapItemNewLine, OpenBraceNewLine)
        /// </summary>
        Default = 12,
        /// <summary>
        /// No whitespace is to exist
        /// </summary>
        NoWhitespace = 1,
        /// <summary>
        /// Each item in an array is followed by a new line '\n'
        /// </summary>
        ArrayItemNewLine = 2,
        /// <summary>
        /// Each item in an object map is followed by a new line '\n'
        /// </summary>
        MapItemNewLine = 4, // Default
        /// <summary>
        /// Open braces '{' should be followed by a new line '\n'
        /// </summary>
        OpenBraceNewLine = 8, // Default
        /// <summary>
        /// Closing braces '}' should be followed by a new line '\n'
        /// </summary>
        CloseBraceNewLine = 16,
        /// <summary>
        /// Open brackets '[' should be followed by a new line '\n'
        /// </summary>
        OpenBracketNewLine = 32,
        /// <summary>
        /// Closed brackets ']' should be followed by a new line '\n'
        /// </summary>
        CloseBracketNewLine = 64
    }
}

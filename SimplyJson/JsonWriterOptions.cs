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

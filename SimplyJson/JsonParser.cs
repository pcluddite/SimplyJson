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
using System.Collections.Generic;

namespace Tbax.Json
{
    static internal class JsonParser
    {
        internal static IJsonable ExtractValue(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString)) {
                return new JsonNull();
            }
            for (int index = 0; index < jsonString.Length; index++) {
                if (char.IsWhiteSpace(jsonString[index])) {
                    continue;
                }
                if (jsonString[index] == '"' || jsonString[index] == '\'') {
                    return JsonString.FromJson(jsonString.Substring(index));
                }
                if (jsonString[index] == '[') {
                    return JsonArrayList.FromJson(jsonString.Substring(index));
                }
                if (jsonString[index] == '{') {
                    return JsonObject.FromJson(jsonString.Substring(index));
                }
                if (jsonString[index] == 't' || jsonString[index] == 'T' ||
                    jsonString[index] == 'f' || jsonString[index] == 'F') {
                    return JsonBoolean.FromJson(AdvanceToNextDelim(jsonString.Substring(index)));
                }
                if (char.IsNumber(jsonString[index])) {
                    return JsonDouble.FromJson(AdvanceToNextDelim(jsonString.Substring(index)));
                }
            }
            throw JsonException.InvalidCollection();
        }

        internal static List<string> ExtractArray(string text)
        {
            return ExtractCollection(text, new char[] { '[', ']' }, new char[] { '{', '}' });
        }

        internal static List<string> ExtractMap(string text)
        {
            return ExtractCollection(text, new char[] { '{', '}' }, new char[] { '[', ']' });
        }

        private static List<string> ExtractCollection(string text, char[] real, char[] other)
        {
            List<string> rawElems = new List<string>();
            char inQuote = '\0';
            bool escape = false;
            int offset = 1;
            int otherOffset = 0;
            int indexPrevious = 0; // The index of the last comma for the previous parameter
            for (int index = 1; index < text.Length; index++) {
                if (escape) { // It's an escape character. Skip.
                    escape = false;
                }
                else if (text[index] == '\\') { // The next one should be skipped.
                    escape = true;
                }
                else if (text[index] == '"') { // Double quotes! Ignore double quotes.
                    if (inQuote == '\0') {
                        inQuote = '"';
                    }
                    else if (inQuote == '"') {
                        inQuote = '\0';
                    }
                }
                else if (text[index] == '\'') { // Single quotes! Ignore single quotes.
                    if (inQuote == '\0') {
                        inQuote = '\'';
                    }
                    else if (inQuote == '\'') {
                        inQuote = '\0';
                    }
                }
                else if (inQuote == '\0') {
                    if (text[index] == other[0]) { // Uh oh. Different type of collection. Don't read those items.
                        otherOffset++;
                    }
                    else if (text[index] == other[1]) { // Whew! End of other brackets!
                        otherOffset--;
                    }
                    else if (otherOffset == 0) { // Yeah! No curly brackets!
                        if (char.IsWhiteSpace(text[index])) {
                            continue; // Who cares about whitespace?
                        }
                        if (text[index] == real[0]) {
                            offset++; // Arggh! Another bracket!
                        }
                        else if (text[index] == real[1]) {
                            offset--; // Good. End bracket.
                        }

                        if (offset == 1 && text[index] == ',') { // We're adding all of them items until we find the last bracket.
                            rawElems.Add(text.Substring(indexPrevious + 1, index - indexPrevious + 1).Trim()); // From one comma to the next. That's an element.
                            indexPrevious = index; // Start the next at the last. It makes sense. Think about it.
                        }

                        if (offset == 0) { // We found the last bracket!
                            rawElems.Add(text.Substring(indexPrevious + 1, index - indexPrevious - 1).Trim()); // Don't forget the last one!
                            return rawElems; // That was tough. Here's your collection.
                        }
                    }
                }
            }
            throw JsonException.EndOfFile(real[1]); // Somethin' went wrong.
        }

        private static string AdvanceToNextDelim(string text)
        {
            int index = 0;
            for (; index < text.Length; index++) {
                if (text[index] == '{' || text[index] == '"' || text[index] == '\'' ||
                    text[index] == '}' || text[index] == '[' || text[index] == ']' ||
                    text[index] == ',' ||
                    char.IsWhiteSpace(text[index])) {
                    return text.Remove(index);
                }
            }
            return text;
        }

        internal static string Indent(string text)
        {
            return "\t" + text.Replace("\n", "\n\t");
        }
    }
}
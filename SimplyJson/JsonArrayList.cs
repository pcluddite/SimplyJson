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
using System.Collections.Generic;
using System.Text;

namespace Tbax.Json
{
    /// <summary>
    /// This class represents a JSON array containing a variable number of elements
    /// </summary>
    public class JsonArrayList : JsonObject, IList<IJsonObject>, ICollection<IJsonObject>, IEnumerable<IJsonObject>
    {
        private List<IJsonObject> objs;

        public JsonArrayList()
        {
            objs = new List<IJsonObject>();
        }

        public JsonArrayList(int capacity)
        {
            objs = new List<IJsonObject>(capacity);
        }

        public JsonArrayList(IEnumerable<IJsonObject> other)
        {
            objs = new List<IJsonObject>(other);
        }
        
        /// <summary>
        /// Creates a new JsonArrayList object from a valid json string. This will return the first valid object.
        /// </summary>
        /// <param name="json">json string to parse</param>
        /// <returns>A JsonArrayList object</returns>
        public static JsonArrayList FromJson(string json)
        {
            JsonArrayList jArray = new JsonArrayList();

            List<string> rawItems = JsonParser.ExtractCollection(json, new char[] { '[', ']' }, new char[] { '{', '}' });

            foreach (string raw in rawItems) {
                jArray.Add(JsonParser.ExtractValue(raw));
            }

            return jArray;
        }

        /// <summary>
        /// Converts this JsonArrayList object to valid a json string
        /// </summary>
        /// <param name="options">The JsonWriterOptions to acknowledge when formatting.</param>
        /// <returns>The valid JsonArrayList as a string</returns>
        public override string ToJson(JsonWriterOptions options)
        {
            bool nospaces = (options & JsonWriterOptions.NoWhitespace) == JsonWriterOptions.NoWhitespace;
            bool onePerLine = (options & JsonWriterOptions.ArrayItemNewLine) == JsonWriterOptions.ArrayItemNewLine;

            StringBuilder json = new StringBuilder();

            if (nospaces) {
                json.Append("[");
            }
            else if ((options & JsonWriterOptions.OpenBracketNewLine) == JsonWriterOptions.OpenBracketNewLine) {
                json.AppendLine("[");
            }
            else {
                json.Append("[ ");
            }

            if (Count > 0) {

                for (int i = 0; i < Count - 1; i++) {
                    if (nospaces) {
                        json.Append(objs[i].ToJson(options));
                        json.Append(",");
                    }
                    else if (onePerLine) {
                        json.Append(JsonParser.Indent(objs[i].ToJson(options)));
                        json.AppendLine(",");
                    }
                    else {
                        json.Append(objs[i].ToJson(options));
                        json.Append(", ");
                    }
                }
                json.Append(objs[Count - 1].ToJson(options));
            }

            if (nospaces) {
                json.Append("]");
            }
            else if ((options & JsonWriterOptions.CloseBracketNewLine) == JsonWriterOptions.CloseBracketNewLine) {
                json.AppendLine("]");
            }
            else {
                json.AppendLine("]");
            }
            return json.ToString();
        }

        public int IndexOf(IJsonObject item)
        {
            return objs.IndexOf(item);
        }

        public void Insert(int index, IJsonObject item)
        {
            objs.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            objs.RemoveAt(index);
        }

        public IJsonObject this[int index]
        {
            get {
                return objs[index];
            }
            set {
                objs[index] = value;
            }
        }

        public void Add(IJsonObject item)
        {
            objs.Add(item);
        }

        public void Add(JsonArrayList item)
        {
            objs.Add(item);
        }

        public void Add(JsonBoolean item)
        {
            objs.Add(item);
        }

        public void Add(JsonDouble item)
        {
            objs.Add(item);
        }

        public void Add(JsonMap item)
        {
            objs.Add(item);
        }

        public void Add(JsonString item)
        {
            objs.Add(item);
        }

        public void Clear()
        {
            objs.Clear();
        }

        public bool Contains(IJsonObject item)
        {
            return objs.Contains(item);
        }

        public void CopyTo(IJsonObject[] array, int arrayIndex)
        {
            objs.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get {
                return objs.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(IJsonObject item)
        {
            return objs.Remove(item);
        }

        public IEnumerator<IJsonObject> GetEnumerator()
        {
            return objs.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
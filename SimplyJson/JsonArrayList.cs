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
    /// This class represents a json array containing a variable number of elements
    /// </summary>
    public class JsonArrayList : IJsonable, IList<IJsonable>, ICollection<IJsonable>, IEnumerable<IJsonable>
    {
        private List<IJsonable> objs;

        public JsonArrayList()
        {
            objs = new List<IJsonable>();
        }

        public JsonArrayList(int capacity)
        {
            objs = new List<IJsonable>(capacity);
        }

        public JsonArrayList(IEnumerable<IJsonable> other)
        {
            objs = new List<IJsonable>(other);
        }
        
        /// <summary>
        /// Creates a new JsonArrayList object from a valid json string. This will return the first valid object.
        /// </summary>
        /// <param name="json">json string to parse</param>
        /// <returns>A JsonArrayList object</returns>
        public static JsonArrayList FromJson(string json)
        {
            JsonArrayList jArray = new JsonArrayList();

            List<string> rawItems = JsonParser.ExtractArray(json);

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
        public string ToJson(JsonWriterOptions options)
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

        public int IndexOf(IJsonable item)
        {
            return objs.IndexOf(item);
        }

        public void Insert(int index, IJsonable item)
        {
            objs.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            objs.RemoveAt(index);
        }

        public IJsonable this[int index]
        {
            get {
                return objs[index];
            }
            set {
                objs[index] = value;
            }
        }

        public void Add(IJsonable item)
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

        public void Add(JsonNumber item)
        {
            objs.Add(item);
        }

        public void Add(JsonObject item)
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

        public bool Contains(IJsonable item)
        {
            return objs.Contains(item);
        }

        public void CopyTo(IJsonable[] array, int arrayIndex)
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

        public bool Remove(IJsonable item)
        {
            return objs.Remove(item);
        }

        public IEnumerator<IJsonable> GetEnumerator()
        {
            return objs.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
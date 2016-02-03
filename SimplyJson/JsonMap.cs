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
using System.Collections;
using System.Text;

namespace Tbax.Json
{
    /// <summary>
    /// This class represents a Json object that contains a set of keys that correspond to an assigned value
    /// </summary>
    public class JsonMap : JsonObject, IDictionary<string, IJsonObject>
    {
        private Dictionary<string, IJsonObject> dict;

        /// <summary>
        /// Initializes a new instance of the JsonMap class that is empty, has the default initial capacity, and uses the default equality comparer for the string.
        /// </summary>
        public JsonMap()
        {
            dict = new Dictionary<string, IJsonObject>();
        }

        /// <summary>
        /// Initializes a new instance of the JsonMap class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the JsonMap can contain.</param>
        /// <exception cref="ArgumentOutOfRangeException">capacity is less than 0.</exception>
        public JsonMap(int capacity)
        {
            dict = new Dictionary<string, IJsonObject>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the JsonMap class that is empty, has the default initial capacity, and uses the specified IEqualityComparer&lt;string&gt;
        /// </summary>
        /// <param name="comparer"></param>
        public JsonMap(IEqualityComparer<string> comparer)
        {
            dict = new Dictionary<string, IJsonObject>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the JsonMap class that contains elements copied from the specified IDictionary&lt;string, IJsonObject&gt; and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="dictionary"></param>
        public JsonMap(IDictionary<string, IJsonObject> dictionary)
        {
            dict = new Dictionary<string, IJsonObject>(dictionary);
        }

        /// <summary>
        /// Initializes a new instance of the JsonMap class that is empty, has the specified initial capacity, and uses the specified IEqualityComparer&lt;string&gt;.
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="comparer"></param>
        public JsonMap(int capacity, IEqualityComparer<string> comparer)
        {
            dict = new Dictionary<string, IJsonObject>(capacity, comparer);
        }

        /// <summary>
        /// Initializes a new instance of the JsonMap class that contains elements copied from the specified IDictionary&lt;string, IJsonObject&gt; and uses the specified IEqualityComparer&lt;string&gt;
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="comparer"></param>
        public JsonMap(IDictionary<string, IJsonObject> dictionary, IEqualityComparer<string> comparer)
        {
            dict = new Dictionary<string, IJsonObject>(dictionary, comparer);
        }
        
        /// <summary>
        /// Initializes a JsonMap object from given JSON. This will return the first valid object map.
        /// </summary>
        /// <param name="json">JSON to parse</param>
        /// <returns>A JsonMap object</returns>
        public static JsonMap FromJson(string json)
        {
            JsonMap jObject = new JsonMap();

            List<string> rawItems = JsonParser.ExtractMap(json);

            foreach (string raw in rawItems) {
                string key = JsonString.FromJson(raw).ToString();
                string value = raw.Substring(key.Length + 2).Trim(); // Acount for the quotes removed
                if (value.StartsWith(":")) {
                    value = value.Remove(0, 1).Trim();
                    if (value == string.Empty) {
                        jObject[key] = new JsonNull();
                    }
                    else {
                        jObject[key] = JsonParser.ExtractValue(value);
                    }
                }
                else {
                    throw JsonException.InvalidElementInCollection(value);
                }
            }

            return jObject;
        }

        /// <summary>
        /// Converts this JsonMap object to valid a JSON object map
        /// </summary>
        /// <param name="options">The JsonWriterOptions to acknowledge when formatting.</param>
        /// <returns>The valid JSON object map as a string</returns>
        public override string ToJson(JsonWriterOptions options)
        {
            bool nospaces = (options & JsonWriterOptions.NoWhitespace) == JsonWriterOptions.NoWhitespace;
            bool onePerLine = (options & JsonWriterOptions.MapItemNewLine) == JsonWriterOptions.MapItemNewLine;

            StringBuilder json = new StringBuilder();

            if (nospaces) {
                json.Append("{");
            }
            else if ((options & JsonWriterOptions.OpenBraceNewLine) == JsonWriterOptions.OpenBraceNewLine) {
                json.AppendLine("{");
            }
            else {
                json.AppendLine("{ ");
            }

            if (Count > 0) {
                int cnt = 1;
                foreach (var entry in this) {
                    if (nospaces) {
                        json.AppendFormat("\"{0}\":{1}", JsonString.FormatAsJsonString(entry.Key), entry.Value.ToJson(options));
                    }
                    else {
                        StringBuilder pair = new StringBuilder();
                        pair.AppendFormat("\"{0}\": {1}", JsonString.FormatAsJsonString(entry.Key), entry.Value.ToJson(options));
                        if (cnt < Count) {
                            pair.Append(",");
                        }
                        if (onePerLine) {
                            json.AppendLine(JsonParser.Indent(pair.ToString()));
                        }
                        else {
                            json.Append(pair);
                        }
                    }
                    cnt++;
                }
            }
            if (nospaces) {
                json.Append("}");
            }
            else if ((options & JsonWriterOptions.CloseBraceNewLine) == JsonWriterOptions.CloseBraceNewLine) {
                json.AppendLine("}");
            }
            else {
                json.AppendLine("}");
            }
            return json.ToString();
        }

        /// <summary>
        /// Adds an item with the provided key and value to the JsonMap
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, IJsonObject value)
        {
            __add(key, value);
        }

        /// <summary>
        /// Adds an item with the provided key and value to the JsonMap
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, JsonArrayList value)
        {
            __add(key, value);
        }

        /// <summary>
        /// Adds an item with the provided key and value to the JsonMap
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, JsonBoolean value)
        {
            __add(key, value);
        }

        /// <summary>
        /// Adds an item with the provided key and value to the JsonMap
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, JsonDouble value)
        {
            __add(key, value);
        }

        /// <summary>
        /// Adds an item with the provided key and value to the JsonMap
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, JsonMap value)
        {
            __add(key, value);
        }

        /// <summary>
        /// Adds an item with the provided key and value to the JsonMap
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, JsonString value)
        {
            __add(key, value);
        }

        private void __add(string key, IJsonObject value)
        {
            dict.Add(key, value);
        }

        /// <summary>
        /// Determines whether the JsonMap contains an element with the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return dict.ContainsKey(key);
        }

        /// <summary>
        /// Gets a collection containing the keys in the JsonMap
        /// </summary>
        public ICollection<string> Keys
        {
            get { return dict.Keys; }
        }

        /// <summary>
        /// Removes the value with the specified key from the JsonMap
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return dict.Remove(key);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(string key, out IJsonObject value)
        {
            return dict.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets a collection containing the values in the JsonMap
        /// </summary>
        public ICollection<IJsonObject> Values
        {
            get { return dict.Values; }
        }

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IJsonObject this[string key]
        {
            get {
                return dict[key];
            }
            set {
                dict[key] = value;
            }
        }

        void ICollection<KeyValuePair<string, IJsonObject>>.Add(KeyValuePair<string, IJsonObject> item)
        {
            ((IDictionary<string, IJsonObject>)dict).Add(item);
        }

        /// <summary>
        /// Removes all items from the JsonMap
        /// </summary>
        public void Clear()
        {
            dict.Clear();
        }

        bool ICollection<KeyValuePair<string, IJsonObject>>.Contains(KeyValuePair<string, IJsonObject> item)
        {
            return ((IDictionary<string, IJsonObject>)dict).Contains(item);
        }

        void ICollection<KeyValuePair<string, IJsonObject>>.CopyTo(KeyValuePair<string, IJsonObject>[] array, int arrayIndex)
        {
            ((IDictionary<string, IJsonObject>)dict).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of key/value pairs contained in the JsonMap
        /// </summary>
        public int Count
        {
            get { return dict.Count; }
        }

        bool ICollection<KeyValuePair<string, IJsonObject>>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<KeyValuePair<string, IJsonObject>>.Remove(KeyValuePair<string, IJsonObject> item)
        {
            return ((IDictionary<string, IJsonObject>)dict).Remove(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, IJsonObject>> GetEnumerator()
        {
            return dict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dict.GetEnumerator();
        }
    }
}
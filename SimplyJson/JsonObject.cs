﻿/**
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
    public class JsonObject : IJsonable, IDictionary<string, IJsonable>
    {
        private Dictionary<string, IJsonable> dict;

        /// <summary>
        /// Initializes a new instance of the JsonObject class that is empty, has the default initial capacity, and uses the default equality comparer for the string.
        /// </summary>
        public JsonObject()
        {
            dict = new Dictionary<string, IJsonable>();
        }

        /// <summary>
        /// Initializes a new instance of the JsonObject class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the JsonObject can contain.</param>
        /// <exception cref="ArgumentOutOfRangeException">capacity is less than 0.</exception>
        public JsonObject(int capacity)
        {
            dict = new Dictionary<string, IJsonable>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the JsonObject class that is empty, has the default initial capacity, and uses the specified IEqualityComparer&lt;string&gt;
        /// </summary>
        /// <param name="comparer"></param>
        public JsonObject(IEqualityComparer<string> comparer)
        {
            dict = new Dictionary<string, IJsonable>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the JsonObject class that contains elements copied from the specified IDictionary&lt;string, IJsonable&gt; and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="dictionary"></param>
        public JsonObject(IDictionary<string, IJsonable> dictionary)
        {
            dict = new Dictionary<string, IJsonable>(dictionary);
        }

        /// <summary>
        /// Initializes a new instance of the JsonObject class that is empty, has the specified initial capacity, and uses the specified IEqualityComparer&lt;string&gt;.
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="comparer"></param>
        public JsonObject(int capacity, IEqualityComparer<string> comparer)
        {
            dict = new Dictionary<string, IJsonable>(capacity, comparer);
        }

        /// <summary>
        /// Initializes a new instance of the JsonObject class that contains elements copied from the specified IDictionary&lt;string, IJsonable&gt; and uses the specified IEqualityComparer&lt;string&gt;
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="comparer"></param>
        public JsonObject(IDictionary<string, IJsonable> dictionary, IEqualityComparer<string> comparer)
        {
            dict = new Dictionary<string, IJsonable>(dictionary, comparer);
        }

        /// <summary>
        /// Initializes a JsonObject object from given json. This will return the first valid object map.
        /// </summary>
        /// <param name="json">json to parse</param>
        /// <returns>A JsonObject object</returns>
        public static JsonObject FromJson(string json)
        {
            JsonObject jObject = new JsonObject();

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
        /// Converts this JsonObject object to valid a json object map
        /// </summary>
        /// <param name="options">The JsonWriterOptions to acknowledge when formatting.</param>
        /// <returns>The valid json object map as a string</returns>
        public string ToJson(JsonWriterOptions options)
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
        /// Adds an item with the provided key and value to the JsonObject
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, IJsonable value)
        {
            __add(key, value);
        }

        /// <summary>
        /// Adds an item with the provided key and value to the JsonObject
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, JsonArrayList value)
        {
            __add(key, value);
        }

        /// <summary>
        /// Adds an item with the provided key and value to the JsonObject
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, JsonBoolean value)
        {
            __add(key, value);
        }

        /// <summary>
        /// Adds an item with the provided key and value to the JsonObject
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, JsonDouble value)
        {
            __add(key, value);
        }

        /// <summary>
        /// Adds an item with the provided key and value to the JsonObject
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, JsonObject value)
        {
            __add(key, value);
        }

        /// <summary>
        /// Adds an item with the provided key and value to the JsonObject
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, JsonString value)
        {
            __add(key, value);
        }

        private void __add(string key, IJsonable value)
        {
            dict.Add(key, value);
        }

        /// <summary>
        /// Determines whether the JsonObject contains an element with the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return dict.ContainsKey(key);
        }

        /// <summary>
        /// Gets a collection containing the keys in the JsonObject
        /// </summary>
        public ICollection<string> Keys
        {
            get { return dict.Keys; }
        }

        /// <summary>
        /// Removes the value with the specified key from the JsonObject
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
        public bool TryGetValue(string key, out IJsonable value)
        {
            return dict.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets a collection containing the values in the JsonObject
        /// </summary>
        public ICollection<IJsonable> Values
        {
            get { return dict.Values; }
        }

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IJsonable this[string key]
        {
            get {
                return dict[key];
            }
            set {
                dict[key] = value;
            }
        }

        void ICollection<KeyValuePair<string, IJsonable>>.Add(KeyValuePair<string, IJsonable> item)
        {
            ((IDictionary<string, IJsonable>)dict).Add(item);
        }

        /// <summary>
        /// Removes all items from the JsonObject
        /// </summary>
        public void Clear()
        {
            dict.Clear();
        }

        bool ICollection<KeyValuePair<string, IJsonable>>.Contains(KeyValuePair<string, IJsonable> item)
        {
            return ((IDictionary<string, IJsonable>)dict).Contains(item);
        }

        void ICollection<KeyValuePair<string, IJsonable>>.CopyTo(KeyValuePair<string, IJsonable>[] array, int arrayIndex)
        {
            ((IDictionary<string, IJsonable>)dict).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of key/value pairs contained in the JsonObject
        /// </summary>
        public int Count
        {
            get { return dict.Count; }
        }

        bool ICollection<KeyValuePair<string, IJsonable>>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<KeyValuePair<string, IJsonable>>.Remove(KeyValuePair<string, IJsonable> item)
        {
            return ((IDictionary<string, IJsonable>)dict).Remove(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, IJsonable>> GetEnumerator()
        {
            return dict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dict.GetEnumerator();
        }
    }
}
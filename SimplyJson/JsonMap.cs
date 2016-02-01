using System;
using System.Collections.Generic;
using System.Text;

namespace Tbax.Json
{
    /// <summary>
    /// This class represents a JSON object that contains a set of keys that correspond to an assigned value
    /// </summary>
    public class JsonMap : JsonObject, IDictionary<string, IJsonObject>
    {
        private Dictionary<string, IJsonObject> dict;

        public JsonMap()
        {
            dict = new Dictionary<string, IJsonObject>();
        }

        public JsonMap(int capacity)
        {
            dict = new Dictionary<string, IJsonObject>(capacity);
        }

        public JsonMap(IEqualityComparer<string> comparer)
        {
            dict = new Dictionary<string, IJsonObject>(comparer);
        }

        public JsonMap(IDictionary<string, IJsonObject> dictionary)
        {
            dict = new Dictionary<string, IJsonObject>(dictionary);
        }

        public JsonMap(int capacity, IEqualityComparer<string> comparer)
        {
            dict = new Dictionary<string, IJsonObject>(capacity, comparer);
        }

        public JsonMap(IDictionary<string, IJsonObject> dictionary, IEqualityComparer<string> comparer)
        {
            dict = new Dictionary<string, IJsonObject>(dictionary, comparer);
        }

        /// <summary>
        /// Sets the IJsonObject at a given key. If the key does not exist, one is added.
        /// </summary>
        /// <param name="key">The key to locate</param>
        /// <param name="jObj">The IJsonObject value for the key</param>
        /// <returns>The former value of the JsonMap at that key. If the object did not previously exist, returns null.</returns>
        public IJsonObject Put(string key, IJsonObject jObj)
        {
            return __put(key, jObj);
        }

        public IJsonObject Put(string key, JsonArrayList jObj)
        {
            return __put(key, jObj);
        }

        public IJsonObject Put(string key, JsonBoolean jObj)
        {
            return __put(key, jObj);
        }

        public IJsonObject Put(string key, JsonDouble jObj)
        {
            return __put(key, jObj);
        }

        public IJsonObject Put(string key, JsonMap jObj)
        {
            return __put(key, jObj);
        }

        public IJsonObject Put(string key, JsonObject jObj)
        {
            return __put(key, jObj);
        }

        public IJsonObject Put(string key, JsonString jObj)
        {
            return __put(key, jObj);
        }

        private IJsonObject __put(string key, IJsonObject jObj)
        {
            IJsonObject ret;
            if (ContainsKey(key)) {
                ret = this[key];
                this[key] = jObj;
            }
            else {
                ret = null;
                Add(key, jObj);
            }
            return ret;
        }

        /// <summary>
        /// Gets an IJsonObject at a given key. If the key does not exist, null is returned.
        /// </summary>
        /// <param name="key">the key to locate</param>
        /// <returns>the IJsonObject if found, otherwise null</returns>
        public IJsonObject Get(string key)
        {
            IJsonObject val;
            if (TryGetValue(key, out val)) {
                return val;
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// Initializes a JsonMap object from given JSON. This will return the first valid object map.
        /// </summary>
        /// <param name="json">JSON to parse</param>
        /// <returns>A JsonMap object</returns>
        public static JsonMap FromJson(string json)
        {
            JsonMap jObject = new JsonMap();

            List<string> rawItems = JsonParser.ExtractCollection(json, new char[] { '{', '}' }, new char[] { '[', ']' });

            foreach (string raw in rawItems) {
                string key = JsonString.FromJson(raw).ToString();
                string value = raw.Substring(key.Length + 2).Trim(); // Acount for the quotes removed
                if (value.StartsWith(":")) {
                    value = value.Remove(0, 1).Trim();
                    if (value.Equals("")) {
                        jObject.Put(key, new JsonNull());
                    }
                    else {
                        jObject.Put(key, JsonParser.ExtractValue(value));
                    }
                }
                else {
                    throw new Exception("Could not extract key value pair in item collection!");
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

        public void Add(string key, IJsonObject value)
        {
            __add(key, value);
        }

        public void Add(string key, JsonArrayList value)
        {
            __add(key, value);
        }

        public void Add(string key, JsonBoolean value)
        {
            __add(key, value);
        }

        public void Add(string key, JsonDouble value)
        {
            __add(key, value);
        }

        public void Add(string key, JsonMap value)
        {
            __add(key, value);
        }

        public void Add(string key, JsonString value)
        {
            __add(key, value);
        }

        private void __add(string key, IJsonObject value)
        {
            dict.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return dict.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return dict.Keys; }
        }

        public bool Remove(string key)
        {
            return dict.Remove(key);
        }

        public bool TryGetValue(string key, out IJsonObject value)
        {
            return dict.TryGetValue(key, out value);
        }

        public ICollection<IJsonObject> Values
        {
            get { return dict.Values; }
        }

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

        public IEnumerator<KeyValuePair<string, IJsonObject>> GetEnumerator()
        {
            return dict.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return dict.GetEnumerator();
        }
    }
}
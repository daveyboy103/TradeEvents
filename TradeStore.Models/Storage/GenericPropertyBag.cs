using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EmployeeModels.Interfaces;

namespace TradeStore.Models.Storage
{
    public class GenericPropertyBag : IGenericPropertyBag
    {
        private readonly IDictionary<string, object> _dictionary = new Dictionary<string, object>();
        
        public GenericPropertyBag()
        {
        }

        public GenericPropertyBag(IGenericPropertyBag properties) : this()
        {
            foreach (KeyValuePair<string,object> property in properties)
            {
                Add(property.Key, property.Value);
            }
        }

        public T GetPropertyValue<T>(string key, bool throwOnInvalidCast = true)
        {
            T obj = default(T);

            if (!_dictionary.ContainsKey(key))
                throw new KeyNotFoundException($"Value with [{key} not found");

            if (_dictionary[key] is T)
                obj = (T)_dictionary[key];
            else if (throwOnInvalidCast)
                throw new InvalidCastException(
                    $"Value with key [{key}] was found but was not of the requested type [{typeof(T)}]");
            return obj;
        }
        
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, object> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return _dictionary.Remove(item);
        }

        public int Count => _dictionary.Count();

        public bool IsReadOnly => false;
        public void Add(string key, object value)
        {
            _dictionary.Add(key, value);
            // string.Intern(key);
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return  _dictionary.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            bool flag = false;
            value = null;

            if (_dictionary.ContainsKey(key))
            {
                value = _dictionary[key];
                flag = true;
            }

            return flag;
        }

        public object this[string key]
        {
            get => _dictionary[key];
            set {}
        }

        public ICollection<string> Keys => _dictionary.Keys;
        public ICollection<object> Values => _dictionary.Values;
    }
}
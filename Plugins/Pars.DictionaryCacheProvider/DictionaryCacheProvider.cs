using System;
using System.Collections.Generic;
using Pars.PluginContracts;
using System.Configuration;

namespace Pars.DictionaryCacheProvider
{
    public class DictionaryCacheProvider : ICache
    {
        public string CacheProvider => "DictionaryCache";

        private static readonly Dictionary<string, CacheItem> Cache = new Dictionary<string, CacheItem>();
        public int ParsCacheOffsetTime => int.Parse(ConfigurationManager.AppSettings["ParsCacheOffsetTime"]);
        
        
        public bool Add(string key, object obj)
        {
            try
            {

                Cache.Add(key, new CacheItem()
                {
                    Value = obj,
                    Created = DateTime.Now.AddSeconds(ParsCacheOffsetTime).Ticks
                });

                return Has(key);

            }
            catch
            {

                return false;
            }
        }
        
        public bool AddOrUpdate(string key, object obj)
        {
            if (Has(key)) Remove(key);

            return Add(key, obj);
        }

        public T Get<T>(string key) where T : class
        {
            if (!Cache.ContainsKey(key)) return null;

            if ((Cache[key].Created + ParsCacheOffsetTime) > DateTime.Now.Ticks) return (T)Convert.ChangeType(Cache[key].Value, typeof(T));

            Cache.Remove(key);
            return null;
        }

        public bool Has(string key)
        {
            return Cache.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return Cache.Remove(key);
        }

        internal sealed class CacheItem
        {
            public object Value { get; set; }
            public long Created { get; set; }
        }
    }
}

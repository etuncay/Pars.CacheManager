using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using Pars.PluginContracts;
using System.Configuration;

namespace Pars.MemoryCacheProvider
{
    public class MemoryCacheProvider :ICache
    {
        public string CacheProvider => "MemoryCache";

        public int ParsCacheOffsetTime => int.Parse(ConfigurationManager.AppSettings["ParsCacheOffsetTime"]);

        public bool Add(string key, object obj)
        {
            try
            {
                var offset = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(ParsCacheOffsetTime) };
                MemoryCache.Default.Add(key, obj, offset);

                return Has(key);
            }
            catch(Exception ex)
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
            T cachedData = MemoryCache.Default.Get(key) as T;
            
            return cachedData;
        }

        public bool Has(string key)
        {
            var cache = MemoryCache.Default.Get(key, null);
            
            return cache != null;
        }

        public bool Remove(string key)
        {
            try
            {
                var cache = MemoryCache.Default.Remove(key, null);

                return cache != null;
            }
            catch
            {
                return false;
            }
        }
    }
}

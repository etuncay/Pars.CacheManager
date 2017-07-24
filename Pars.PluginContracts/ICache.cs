using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pars.PluginContracts
{
    public interface ICache
    {
        string CacheProvider { get; }
        int ParsCacheOffsetTime { get; }

        bool Has(string key);
        T Get<T>(string key) where T : class;
        bool Add(string key, object obj);
        bool AddOrUpdate(string key, object obj);
        bool Remove(string key);
    }
}

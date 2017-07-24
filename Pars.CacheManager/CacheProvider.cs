using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Pars.GenericPluginLoader;
using Pars.PluginContracts;

namespace Pars.CacheManager
{
    public static class CacheProvider
    {
        public static ICache Creator()
        {
            var pluginPath = ConfigurationManager.AppSettings["ParsCacheProviderPath"];
            var provider = ConfigurationManager.AppSettings["ParsCacheProvider"];
            
            ICollection<ICache> plugins = GenericPluginLoader<ICache>.LoadPlugins(pluginPath);

            var plugin = plugins.FirstOrDefault(q => q.CacheProvider== provider);

            return plugin;
        }

    }
}

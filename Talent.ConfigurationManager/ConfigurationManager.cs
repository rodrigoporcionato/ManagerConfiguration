using System;
using System.Collections.Generic;
using System.Text;
using Talent.Infrastructure.CacheProvider;
using Talent.Infrastructure.CacheProvider.Redis;

namespace Talent.ConfigurationManager
{
    public class ConfigurationManager
    {
        private ICacheProvider _cacheProvider;

        public ConfigurationManager()
        {
            _cacheProvider = new RedisCacheProvider();
        }

        public T GetKey<T>(string key)
        {
            T result = default;

            if (_cacheProvider.IsInCache(key))
            {
               result = _cacheProvider.Get<T>(key);
            }
            else
            {
                //database            
            }

            return result;
        }

    }
}

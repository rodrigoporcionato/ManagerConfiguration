using ServiceStack.Redis;
using System;

namespace Talent.Infrastructure.CacheProvider.Redis
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly RedisEndpoint _endPoint;

        public RedisCacheProvider()
        {
            _endPoint = new RedisEndpoint("", 123, "", 1);
        }
        public T Get<T>(string key)
        {
            T result = default;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                var wrapper = client.As<T>();
                result = wrapper.GetValue(key);
            }
            return result;
        }

        public bool IsInCache(string key)
        {
            bool isInCache = false;

            using (RedisClient client = new RedisClient(_endPoint))            
            {
                isInCache = client.ContainsKey(key);
            }

            return isInCache;
        }

        public bool Remove(string key)
        {
            bool removed = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                removed = client.Remove(key);
            }

            return removed;
        }

        public void Set<T>(string key, T value)
        {   
            this.Set<T>(key, value, TimeSpan.Zero);            
        }

        public void Set<T>(string key, T value, TimeSpan timeout)
        {
            using RedisClient client = new RedisClient(_endPoint);
            client.As<T>().SetValue(key, value, timeout);
        }
    }
}

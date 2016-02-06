using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDemo.Web.Neo4j.Cache
{
    public class Neo4jCacheQuery : ICacheQuery<string, string>
    {
        private const int CAPACITY = 100;

        private Cache<string, string> Cache;

        public long Count
        {
            get { return Cache.Count; }
        }

        public string this[string key]
        {
            get { return Remove(key); }
            set { Add(key, value); }
        }

        public Neo4jCacheQuery(int capacity)
        {
            Cache = new Cache<string, string>(capacity);
        }

        public Neo4jCacheQuery()
            : this(CAPACITY)
        {
        }

        public void Add(string key, string value)
        {
            Cache.Packet(key, value);
        }

        public string Remove(string key)
        {
            string result = Cache[key];
            Cache.ExcludeLastItem();

            return result;
        }
    }
}

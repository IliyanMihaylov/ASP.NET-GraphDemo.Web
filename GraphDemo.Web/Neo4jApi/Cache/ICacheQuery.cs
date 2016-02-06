using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDemo.Web.Neo4j.Cache
{
    public interface ICacheQuery<TKey, TValue>
    {
        TValue this[TKey key] { get; set; }
        long Count { get; }

        void Add(TKey key, TValue value);
        TValue Remove(TKey key);
    }
}

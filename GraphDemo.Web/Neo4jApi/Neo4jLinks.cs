using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDemo.Web.Neo4jApi
{
    public class Neo4jLinks
    {
        public static List<object> CalculateLinks<T>(List<T> nodes, IEnumerable<Relation<T>> relations, IComparer<T> comaprer)
        {
            Dictionary<T, int> cache = new Dictionary<T, int>();
            List<object> result = new List<object>();
            
            int indexSource = 0;
            int indexTarget = 0;

            foreach (Relation<T> ralation in relations)
            {
                indexSource = GetIndex(cache, nodes, ralation.Source, comaprer);
                indexTarget = GetIndex(cache, nodes, ralation.Target, comaprer);

                result.Add(new { source = indexSource, target = indexTarget });
            }

            return result;
        }

        public static List<object> CalculateLinks<TNode, TRelation>(List<Node<TNode>> nodes, IEnumerable<RelationshipInstance<TRelation>> relations) 
            where TRelation : class, new()
        {
            Dictionary<long, int> cache = new Dictionary<long, int>();
            List<object> result = new List<object>();
            List<long> nodeId = nodes.Select(x => x.Reference.Id).ToList();

            int indexSource = 0;
            int indexTarget = 0;

            foreach (RelationshipInstance<TRelation> ralation in relations)
            {
                indexSource = GetIndex(cache, nodeId, ralation.StartNodeReference.Id, Comparer<long>.Default);
                indexTarget = GetIndex(cache, nodeId, ralation.EndNodeReference.Id, Comparer<long>.Default);

                result.Add(new { source = indexSource, target = indexTarget });
            }

            return result;
        }

        public static int GetIndex<T>(Dictionary<T, int> cache, List<T> items, T item, IComparer<T> comparer)
        {
            int index = -1;

            if (cache.ContainsKey(item))
                index = cache[item];
            else
            {
                index = items.BinarySearch(item, comparer); // Will always exist.
                cache[item] = index;
            }

            return index;
        }

    }
}

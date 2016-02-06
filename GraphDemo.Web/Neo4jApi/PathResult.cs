using GraphDemo.Web.Models.Neo4j;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDemo.Web.Neo4jApi
{
    public class PathsResult<TNode>
    {
        public IEnumerable<Node<TNode>> Nodes { get; set; }
        public IEnumerable<RelationshipInstance<object>> Relationships { get; set; }
    }

    public class Relation<T>
    {
        public T Source { get; set; }
        public T Target { get; set; }
    }

    public class NodeModelComparer : IComparer<NodeModel>
    {
        public static readonly NodeModelComparer Instance = new NodeModelComparer();

        public int Compare(NodeModel x, NodeModel y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    public class NodeComparer<T> : IComparer<Node<T>>
    {
        public static readonly NodeComparer<T> Instance = new NodeComparer<T>();

        public int Compare(Node<T> x, Node<T> y)
        {
            return x.Reference.Id.CompareTo(y.Reference.Id);
        }
    }
}

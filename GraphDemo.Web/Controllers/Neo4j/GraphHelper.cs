using GraphDemo.Web.Models.Neo4j;
using GraphDemo.Web.Neo4jApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDemo.Web.Controllers.Neo4j
{
    public class GraphHelper
    {
        public static Graph LoadGraph()
        {
            List<NodeModel> nodes = WebApiConfig.GraphClient.Cypher
                    .Match("(n:Location)")
                    .Return((n) => n.As<NodeModel>())
                    .Results.ToList();

            nodes.Sort(NodeModelComparer.Instance);

            var relsQuery = WebApiConfig.GraphClient.Cypher
                    .Match("((from:Location)<-[р:CONNECTED_TO]->(to:Location))")
                    .Return((from, to) => new Relation<NodeModel>
                    {
                        Source = from.As<NodeModel>(),
                        Target = to.As<NodeModel>()
                    });

            List<Relation<NodeModel>> relations = relsQuery.Results.ToList();

            List<object> nodeLinks = Neo4jLinks.CalculateLinks(nodes, relations, NodeModelComparer.Instance);

            return new Graph { Nodes = nodes, NodeLinks = nodeLinks };
        }

    }

    public class Graph
    {
        public List<NodeModel> Nodes { get; set; }
        public List<object> NodeLinks { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Neo4jClient;
using Neo4jClient.Cypher;
using GraphDemo.Web.Neo4jApi;
using GraphDemo.Web.Models.Neo4j;
using System.Diagnostics;

namespace GraphDemo.Web.Controllers.Neo4j
{
    [RoutePrefix("shortestPath")]
    public class PathController : ApiController
    {
        [HttpGet]
        [Route(Name = "Path")]
        public IHttpActionResult ShortestPath(string from, string to)
        {
            if (from == null || to == null)
            {
                Trace.WriteLine("Method NodeController.Insert name is null or link is null.");

                return Ok();
            }

            if (WebApiConfig.GraphClient == null)
            {
                Trace.WriteLine("Neo4j database is not initialised.");

                return Ok();
            }

            var pathsQuery = WebApiConfig.GraphClient.Cypher
                     .Match("p = shortestPath((a:Location)-[*..150]-(b:Location))")
                     .Where((NodeModel a) => a.Name == from)
                     .AndWhere((NodeModel b) => b.Name == to)
                     .Return(p => new PathsResult<NodeModel>
                     {
                         Nodes = Return.As<IEnumerable<Node<NodeModel>>>("nodes(p)"),
                         Relationships = Return.As<IEnumerable<RelationshipInstance<object>>>("rels(p)")
                     }).Limit(1);

            PathsResult<NodeModel> res = pathsQuery.Results.FirstOrDefault();

            if (res == null)
                return Ok();

            List<Node<NodeModel>> sortedNodes = res.Nodes.ToList();
            sortedNodes.Sort(NodeComparer<NodeModel>.Instance);

            List<object> links = Neo4jLinks.CalculateLinks(sortedNodes, res.Relationships);

            return Ok(new { nodes = sortedNodes.Select(x => x.Data), links = links });
        }
    }
}

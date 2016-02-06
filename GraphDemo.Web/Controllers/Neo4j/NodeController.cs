using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GraphDemo.Web.Controllers.Neo4j
{
    [RoutePrefix("nodeInsert")]
    public class NodeController : ApiController
    {
        [HttpGet]
        [Route(Name = "Insert")]
        public IHttpActionResult Insert(string name, string link)
        {
            if (name == null || link == null)
            {
                Trace.WriteLine("Method NodeController.Insert - Name is null or Link is null.");

                return Ok();
            }

            if (WebApiConfig.GraphClient == null)
            {
                Trace.WriteLine("Neo4j database is not initialised.");

                return Ok();
            }

            try
            {
                string createQuery = $"({name}: Location {{ Name: '{name}' }})";
                WebApiConfig.GraphClient.Cypher
                    .Merge(createQuery)
                    .ExecuteWithoutResults();

                string linkQuery = $"({name})<-[:CONNECTED_TO {{ distance: 1 }}]->({link})";
                WebApiConfig.GraphClient.Cypher
                   .Match($"({name}:Location {{ Name:'{name}' }}), ({link}:Location {{ Name:'{link}' }})")
                   .Merge(linkQuery)
                   .ExecuteWithoutResults();

                Graph graph = GraphHelper.LoadGraph();

                return Ok(new { nodes = graph.Nodes, links = graph.NodeLinks });
            }
            catch (Exception exc)
            {
                Trace.WriteLine(exc.Message);
            }

            return Ok();
        }
    }
}

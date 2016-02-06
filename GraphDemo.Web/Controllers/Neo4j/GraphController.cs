using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Neo4jClient;
using Neo4jClient.Cypher;
using GraphDemo.Web.Neo4jApi;
using GraphDemo.Web.Models.Neo4j;
using System.Diagnostics;

namespace GraphDemo.Web.Controllers.Neo4j
{
    [RoutePrefix("graph")]
    public class GraphController : ApiController
    {
        [HttpGet]
        [Route("{limit:int?}", Name = "Index")]
        public IHttpActionResult Index(int limit = 100)
        {
            if (WebApiConfig.GraphClient == null)
            {
                Trace.WriteLine("Neo4j database is not initialised.");

                return Ok();
            }

            if (WebApiConfig.Cache.Count != 0)
            {
                string query = WebApiConfig.Cache.Remove(User.Identity.GetUserId());
                CypherQuery cypherQquery = new CypherQuery(query, null, CypherResultMode.Set);
                ((IRawGraphClient)WebApiConfig.GraphClient).ExecuteCypher(cypherQquery);
            }

            Graph graph = GraphHelper.LoadGraph();

            return Ok(new { nodes = graph.Nodes, links = graph.NodeLinks });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace GraphDemo.Web.Controllers.Neo4j
{
    public class Neo4jController : Controller
    {
        // GET: Neo4j
        public ActionResult Index(string query)
        {
            WebApiConfig.Cache.Add(User.Identity.GetUserId(), query);
            return View();
        }
    }
}
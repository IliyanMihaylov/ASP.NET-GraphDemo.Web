using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDemo.Web.Controllers.Google
{
    public class GoogleDriveSettings
    {
        public const string CLIENT_ID = "48487136346-rd0p0ae865jcluvu9b4mtdr66lbl74gi.apps.googleusercontent.com";
        public const string CLIENT_SECRET = "Kxd55r2ifXdp6ehcS4MlVuEq";
        public const string FOLDER = "Drive.Api.Auth.Store";
        
        public const string APP_NAME = "Graph Demo Web";

        public static readonly HashSet<string> AllowExtensions = new HashSet<String>()
        {
            "cypher"
        };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDemo.Web.Controllers.Google
{
    public class File
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string DownloadUrl { get; set; }
        public string Description { get; set; }
        public string Kind { get; set; }

        public long? Version { get; set; }
        public string WebContentLink { get; set; }
        public string WebViewLink { get; set; }

        public string Type { get; set; }
        public string Size { get; set; }
    }
}

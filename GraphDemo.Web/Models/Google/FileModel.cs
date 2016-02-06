using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDemo.Web.Models.Google
{
    public class FileModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string DownloadUrl { get; set; }

        public string Type { get; set; }
        public string Size { get; set; }

        public bool Selected { get; set; }
    }
}

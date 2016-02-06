using Google.Apis.Drive.v2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDemo.Web.Controllers.Google.Data
{
    public interface IDriveDataService
    {
        Task<FileList> FilesAsync { get; }

        Task<string> ReadFileAsync(string url);
        string ReadFile(string url);
    }
}

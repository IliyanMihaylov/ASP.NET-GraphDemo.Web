using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDemo.Web.Controllers.Google.Data
{
    public class DriveDataService : IDriveDataService
    {
        private DriveService ConnectionService;

        public DriveDataService(DriveService connectionService)
        {
            if (connectionService == null)
                throw new ArgumentNullException("connectionService == null");

            ConnectionService = connectionService;
        }

        public Task<FileList> FilesAsync
        {
            get
            {
                return ConnectionService.Files.List().ExecuteAsync();
            }
        }

        public async Task<string> ReadFileAsync(string url)
        {
            if (ConnectionService == null)
                return string.Empty;

            Stream stream = await ConnectionService.HttpClient.GetStreamAsync(url);
            StreamReader reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        public string ReadFile(string url)
        {
            return ReadFileAsync(url).Result;
        }
    }
}

using Google.Apis.Drive.v2.Data;
using GraphDemo.Web.Controllers.Google.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDemo.Web.Controllers.Google
{
    public class GoogleDriveService
    {
        public IDriveDataService DataService { get; private set; }

        public GoogleDriveService(IDriveDataService dataService)
        {
            DataService = dataService;
        }

        public IEnumerable<File> FilterFiles(ICollection<string> allowExtensions)
        {
            FileList list = DataService.FilesAsync.Result;

            IEnumerable<File> files = list.Items
               .Where(x => x.FileExtension != null && allowExtensions.Contains(x.FileExtension.ToLower()))
               .Select(x =>
                    new File()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        CreatedDate = x.CreatedDate,
                        DownloadUrl = x.DownloadUrl,
                        Type = x.FileExtension,
                        Size = x.FileSize.HasValue ? x.FileSize.Value.ConvertSizeToString() : "0 B",
                        Description = x.Description,
                        Kind = x.Kind,
                        Version = x.Version,
                        WebContentLink = x.WebContentLink,
                        WebViewLink = x.WebViewLink
                    });

            return files;
        }

        public DownloadedContent Download(string url)
        {
            return DownloadAsync(url).Result;
        }

        public async Task<DownloadedContent> DownloadAsync(string url)
        {
            string result = await DataService.ReadFileAsync(url);

            DownloadedContent file = new DownloadedContent()
            {
                Content = result
            };

            return file;
        }
    }
}

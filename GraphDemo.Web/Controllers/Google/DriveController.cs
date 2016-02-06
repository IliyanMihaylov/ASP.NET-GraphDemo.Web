using GraphDemo.Web.Models.Google;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GraphDemo.Web.Controllers.Google
{
    public class DriveController : Controller
    {
        // GET: Drive
        [Authorize]
        public async Task<ActionResult> Drive(CancellationToken cancellationToken)
        {
            try
            {
                var drive = await GoogleFactory.ConnectToGoogleDrive(this, cancellationToken);

                if (drive.DriveService == null)
                    return Redirect(drive.Redirect);

                IEnumerable<FileModel> files = drive.DriveService.FilterFiles(GoogleDriveSettings.AllowExtensions)
                    .Select(x => new FileModel()
                    {
                        Id = x.Id,
                        CreatedDate = x.CreatedDate,
                        DownloadUrl = x.DownloadUrl,
                        Selected = false,
                        Size = x.Size,
                        Title = x.Title,
                        Type = x.Type
                    }).ToList();

                return View(files);
            }
            catch (Exception exc)
            {
                Trace.WriteLine(exc.Message);

                return View("Error");
            }
        }

        public async Task<ActionResult> Download(string url)
        {
            try
            {
                ConnectionResult drive = await GoogleFactory.ConnectToGoogleDrive(this, new CancellationToken());

                if (drive.DriveService == null)
                    return Redirect(drive.Redirect);

                DownloadedContent file = await drive.DriveService.DownloadAsync(url);

                return View(file);
            }
            catch (Exception exc)
            {
                Trace.WriteLine(exc.Message);

                return View("Error");
            }
        }
    }
}
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using GraphDemo.Web.Controllers.Google.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace GraphDemo.Web.Controllers.Google
{
    public class GoogleFactory
    {
        private static Dictionary<string, GoogleDriveService> ServiceCache = new Dictionary<string, GoogleDriveService>();

        public static async Task<ConnectionResult> ConnectToGoogleDrive(Controller controller, CancellationToken token)
        {
            string userId = controller.User.Identity.GetUserId();

            GoogleDriveService service = null;
            if (ServiceCache.TryGetValue(userId, out service))
                return new ConnectionResult(service, null);

            var result = await new AuthorizationCodeMvcApp(controller, new AppFlowMetadata()).AuthorizeAsync(token);

            if (result.Credential == null)
            {
                return new ConnectionResult(null, result.RedirectUri);
            }

            var Service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = result.Credential,
                ApplicationName = GoogleDriveSettings.APP_NAME
            });

            service = new GoogleDriveService(new DriveDataService(Service));

            ServiceCache[userId] = service;

            return new ConnectionResult(service, null);
        }
    }

    public class ConnectionResult
    {
        public GoogleDriveService DriveService { get; set; }
        public string Redirect { get; set; }

        public ConnectionResult(GoogleDriveService driveService, string redirect)
        {
            DriveService = driveService;
            Redirect = redirect;
        }
    }
}

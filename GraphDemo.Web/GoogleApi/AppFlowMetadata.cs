using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v2;
using Google.Apis.Util.Store;

namespace GraphDemo.Web.Controllers.Google
{
    public class AppFlowMetadata : FlowMetadata
    {
        private static readonly IAuthorizationCodeFlow flow =
           new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
           {
               ClientSecrets = new ClientSecrets
               {
                   ClientId = GoogleDriveSettings.CLIENT_ID,
                   ClientSecret = GoogleDriveSettings.CLIENT_SECRET
               },
               Scopes = new[] { DriveService.Scope.Drive },
               DataStore = new FileDataStore(GoogleDriveSettings.APP_NAME)
           });

        public override string GetUserId(Controller controller)
        {
            string usrID = controller.User.Identity.GetUserId();
            return usrID;
        }

        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }
}

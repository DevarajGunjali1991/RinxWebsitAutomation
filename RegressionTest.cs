using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RinxAPIEndPointAutomation;
using System.Security.Principal;
using System.Threading;
using WTD_Automation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace APIRinxTest
{
    [TestClass]
    public class RegressionTest
    {
        public ExtentReports _extent;
        public ExtentTest _log;
        public string _tokenBearer;
        public string _accesToken;
        [TestInitialize]
        [System.Obsolete]


        public void Setup()
        {
            // This method will be executed before each test method

            _extent = Report.getInstance();

        }

        [TestMethod]
        public void VerifyLinks()
        {
            _log = _extent.CreateTest("Verify Links");

            var verifyLinks = new VerifyLinks();
            var response = verifyLinks.GetVerifyLinks();
            // Assert.AreEqual("Magic link sent successfully.", response.Message);
            _log.Log(Status.Info, "Pass");


        }

        [TestMethod, TestCategory("Priority2")]
        public void SendLink()
        {
            _log = _extent.CreateTest("Verify SendLink");
            string paload = @"{ ""email"": ""devaraj@eagle.net""}";

            var link = new APIHelper<VerifyLinkDTO>();
            var url = link.SetUrl("send_link/");
            var request = link.CreatePostRequest(paload);
            var response = link.GetResponse(url, request);
            VerifyLinkDTO content = link.GetContent<VerifyLinkDTO>(response);
            Assert.AreEqual("Magic link sent successfully.", content.Message);
            _log.Log(Status.Info, "Pass");
            Thread.Sleep(20000);

        }

        [TestMethod, TestCategory("Priority1")]
        public void LogInEndPointsUsersEmailIsNotVerified()
        {
            _log = _extent.CreateTest("Verify LogInEndPoints");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<LogInDTO>();
            var url = link.SetUrl("login/");
            _log.Log(Status.Info, "Verify LogInEndPoints " + url);
            var request = link.CreatePostRequest(paload);
            _log.Log(Status.Info, "request " + request);
            var response = link.GetResponse(url, request);
            _log.Log(Status.Info, "response " + response);
            LogInDTO content = link.GetContent<LogInDTO>(response);
            _log.Log(Status.Info, "content " + content);
            Assert.AreEqual("<User already logged in on another device.", content.Error);
            _log.Log(Status.Info, "content " + content.Error);
            _log.Log(Status.Pass, "Verify LogInEndPoints");
            Thread.Sleep(10000);

        }

        [TestMethod, TestCategory("Priority3")]
        public void LogInUserAlreadyLoggedInOnAnotherDevice()
        {
            _log = _extent.CreateTest("Verify LogInEndPoints");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<UserAlreadyLoggedInOnAnotherDevice>();
            var url = link.SetUrl("login/");
            _log.Log(Status.Info, "Verify LogInEndPoints " + url);
            var request = link.CreatePostRequest(paload);
            _log.Log(Status.Info, "request " + request);
            var response = link.GetResponse(url, request);
            _log.Log(Status.Info, "response " + response);
            UserAlreadyLoggedInOnAnotherDevice content = link.GetContent<UserAlreadyLoggedInOnAnotherDevice>(response);
            _log.Log(Status.Info, "content " + content);
            Assert.AreEqual("User already logged in on another device.", content.Error);
            _log.Log(Status.Info, "content " + content.Error);
            _log.Log(Status.Pass, "Verify LogInEndPoints");
            Thread.Sleep(10000);

        }



        [TestMethod, TestCategory("Priority4")]
        public void VerifyLoggedOutUserWithEmailSuccessfully()
        {
            _log = _extent.CreateTest("Verify LogOutEndPoints");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<LoggedOutUserWithEmailSuccessfully>();
            var url = link.SetUrl("logout/");
            _log.Log(Status.Info, "Verify LogInEndPoints " + url);
            var request = link.CreatePostRequest(paload);
            _log.Log(Status.Info, "request " + request);
            var response = link.GetResponse(url, request);
            _log.Log(Status.Info, "response " + response);
            LoggedOutUserWithEmailSuccessfully content = link.GetContent<LoggedOutUserWithEmailSuccessfully>(response);
            _log.Log(Status.Info, "content " + content);
            Assert.AreEqual("Logged out user with email devaraj@eagle.net successfully.", content.Message);
            _log.Log(Status.Info, "content " + content.Message);
            _log.Log(Status.Pass, "Verify LogInEndPoints");
            Thread.Sleep(10000);

        }

        [TestMethod, TestCategory("Priority5")]
        public void VerifyLoginSuccessfuly()
        {
            _log = _extent.CreateTest("Verify LogInEndPoints");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<LoginSuccessful>();
            var url = link.SetUrl("login/");
            _log.Log(Status.Info, "Verify LogInEndPoints " + url);
            var request = link.CreatePostRequest(paload);
            _log.Log(Status.Info, "request " + request);
            var response = link.GetResponse(url, request);
            _log.Log(Status.Info, "response " + response);
            LoginSuccessful content = link.GetContent<LoginSuccessful>(response);
            _log.Log(Status.Info, "content " + content);
            Assert.AreEqual("Login successful.", content.message);
            _log.Log(Status.Info, "content " + content.message);
            _log.Log(Status.Info, "content " + content.refresh_token);
            _log.Log(Status.Info, "content " + content.access_token);
            _tokenBearer = content.access_token;
            _log.Log(Status.Pass, "Verify LogInEndPoints");


            _log = _extent.CreateTest("Verify LogInEndPoints");
            string paload1 = "{\"refresh\":\"" + _tokenBearer + "\"}";

            _log.Log(Status.Info, "request data " + paload1);
            var link1 = new APIHelper<LoginSuccessful>();
            var url1 = link1.SetUrl("token/refresh/");
            _log.Log(Status.Info, "Verify LogInEndPoints " + url1);
            var request1 = link1.CreatePostRequest(paload1);
            _log.Log(Status.Info, "request " + request1);
            var response1 = link1.GetResponse(url1, request1);
            _log.Log(Status.Info, "response " + response1);
            LoginSuccessful content1 = link1.GetContent<LoginSuccessful>(response1);

            _accesToken = content1.access;

            _log.Log(Status.Info, "content " + content1.access);
            Thread.Sleep(10000);


            _log = _extent.CreateTest("Verify UserProfileEndPoints");
            string payload = @"{
   
     ""name"": ""jhon deo"",
    ""display_name"": ""jhon"",
    ""bio"": ""my art collection"",
    ""gender"": ""male"",
    ""age"": 30,
    ""instagram_profile"":  ""url/for/indtagram"",
    ""x_profile"":  ""url/for/x"",
    ""Personal_website"": ""url for personal website""}";
            _log.Log(Status.Info, "request data " + paload);


            var link3 = new APIHelper<ProfileDTO>();
            var url3 = link.SetUrl("user-profile/");
            _log.Log(Status.Info, "Verify UserProfileEndPoints " + url3);
            var request3 = link.CreatePostRequest(paload);

            request.AddHeader("Authorization", "Bearer " + _accesToken);
            _log.Log(Status.Info, "request " + request3);
            var response3 = link.GetResponse(url, request3);
            _log.Log(Status.Info, "response " + response3);
            ProfileDTO content3 = link.GetContent<ProfileDTO>(response3);
            _log.Log(Status.Info, "content " + content);
            //Assert.AreEqual("Login successful.", content.);
            //_log.Log(Status.Info, "content " + content.Message);
            _log.Log(Status.Pass, "Verify ProfileEndPoints");
            Thread.Sleep(10000);







        }


        [TestMethod]
        public void VerifyRefreshToken()
        {




        }




        [TestMethod, TestCategory("Priority4")]
        public void VerifyUserProfileSuccessfuly()
        {
            _log = _extent.CreateTest("Verify UserProfileEndPoints");
            string paload = @"{
   
     ""name"": ""jhon deo"",
    ""display_name"": ""jhon"",
    ""bio"": ""my art collection"",
    ""gender"": ""male"",
    ""age"": 30,
    ""instagram_profile"":  ""url/for/indtagram"",
    ""x_profile"":  ""url/for/x"",
    ""Personal_website"": ""url for personal website""}";
            _log.Log(Status.Info, "request data " + paload);


            var link = new APIHelper<ProfileDTO>();
            var url = link.SetUrl("user-profile/");
            _log.Log(Status.Info, "Verify UserProfileEndPoints " + url);
            var request = link.CreatePostRequest(paload);

            request.AddHeader("Authorization", "Bearer " + _accesToken);
            _log.Log(Status.Info, "request " + request);
            var response = link.GetResponse(url, request);
            _log.Log(Status.Info, "response " + response);
            ProfileDTO content = link.GetContent<ProfileDTO>(response);
            _log.Log(Status.Info, "content " + content);
            Assert.AreEqual("Login successful.", content.Message);
            _log.Log(Status.Info, "content " + content.Message);
            _log.Log(Status.Pass, "Verify ProfileEndPoints");
            Thread.Sleep(10000);

        }


        [TestMethod, TestCategory("Priority6")]
        public void VerifyEmailSentWithTheChangeEmailLink()
        {
            _log = _extent.CreateTest("Verify EmailSentWithTheChangeEmailLinkEndPoint");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<EmailSentWithTheChangeEmailLink>();
            var url = link.SetUrl("send_email/");

            _log.Log(Status.Info, "Verify EmailSentWithTheChangeEmailLinkEndPoint " + url);
            var request = link.CreatePostRequest(paload);
            request.AddHeader("Authorization", "Bearer " + _accesToken);
            _log.Log(Status.Info, "request " + request);
            var response = link.GetResponse(url, request);
            _log.Log(Status.Info, "response " + response);
            EmailSentWithTheChangeEmailLink content = link.GetContent<EmailSentWithTheChangeEmailLink>(response);
            _log.Log(Status.Info, "content " + content);
            Assert.AreEqual("Email sent with the change email link.", content.Message);
            _log.Log(Status.Info, "content " + content.Message);
            _log.Log(Status.Pass, "Verify EmailSentWithTheChangeEmailLink");
            Thread.Sleep(10000);

        }


        [TestMethod, TestCategory("Priority7")]
        public void VerifyEmailSendUpdateEmaillLink()
        {
            _log = _extent.CreateTest("Verify EmailSentWithTheChangeEmailLinkEndPoint");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<EmailSentWithTheChangeEmailLink>();
            var url = link.SetUrl("send_update_email/");



        }

        [TestMethod, TestCategory("Priority8")]
        public void VerifyMedia()
        {
            _log = _extent.CreateTest("Verify User Media EndPoint");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<Media>();
            var url = link.SetUrl("media/");
            var request = link.CreatGetRequist();
            request.AddHeader("Authorization", "Bearer " + _accesToken);



        }




        [TestMethod, TestCategory("Priority8")]
        public void VerifUserMedia()
        {
            _log = _extent.CreateTest("Verify UserMedia(EndPoint");
           
            var link = new APIHelper<UserMedia>();
            var url = link.SetUrl("user_media / ");
            var request = link.CreatGetRequist();
            request.AddHeader("Authorization", "Bearer " + _accesToken);


        }


        [TestMethod, TestCategory("Priority9")]
        public void VerifPlaylist()
        {
            _log = _extent.CreateTest("Verify VerifPlaylist");
           
            var link = new APIHelper<EmailSentWithTheChangeEmailLink>();
            var url = link.SetUrl("playlist/ ");
            var request = link.CreatGetRequist();
            request.AddHeader("Authorization", "Bearer " + _accesToken);



        }


        [TestMethod, TestCategory("Priority10")]
        public void VerifCreatePlaylist()
        {
            _log = _extent.CreateTest("Verify EmailSentWithTheChangeEmailLinkEndPoint");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<EmailSentWithTheChangeEmailLink>();
            var url = link.SetUrl("create_playlist/");

        }

        [TestMethod, TestCategory("Priority11")]
        public void VerifUserPlaylist()
        {
            _log = _extent.CreateTest("Verify EmailSentWithTheChangeEmailLinkEndPoint");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<EmailSentWithTheChangeEmailLink>();
            var url = link.SetUrl("user_playlist/");

        }


        [TestMethod, TestCategory("Priority12")]
        public void VerifUpload()
        {
            _log = _extent.CreateTest("Verify EmailSentWithTheChangeEmailLinkEndPoint");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<EmailSentWithTheChangeEmailLink>();
            var url = link.SetUrl("user_playlist/");

        }


        [TestMethod, TestCategory("Priority13")]
        public void VerifSearch()
        {
            _log = _extent.CreateTest("Verify EmailSentWithTheChangeEmailLinkEndPoint");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<EmailSentWithTheChangeEmailLink>();
            var url = link.SetUrl("search/");

        }


        [TestMethod, TestCategory("Priority14")]
        public void VerifMedium()
        {
            _log = _extent.CreateTest("Verify EmailSentWithTheChangeEmailLinkEndPoint");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<EmailSentWithTheChangeEmailLink>();
            var url = link.SetUrl("medium/");

        }

        [TestMethod, TestCategory("Priority15")]
        public void VerifRevenueca()
        {
            _log = _extent.CreateTest("Verify EmailSentWithTheChangeEmailLinkEndPoint");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<EmailSentWithTheChangeEmailLink>();
            var url = link.SetUrl("revenuecta/ ");

        }


        [TestMethod, TestCategory("Priority16")]
        public void VerifInvitation()
        {
            _log = _extent.CreateTest("Verify EmailSentWithTheChangeEmailLinkEndPoint");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<EmailSentWithTheChangeEmailLink>();
            var url = link.SetUrl("invitation/ ");

        }

        [TestMethod, TestCategory("Priority17")]
        public void VerifLike()
        {
            _log = _extent.CreateTest("Verify EmailSentWithTheChangeEmailLinkEndPoint");
            string paload = @"{""email"" : ""devaraj@eagle.net""}";
            _log.Log(Status.Info, "request data " + paload);
            var link = new APIHelper<EmailSentWithTheChangeEmailLink>();
            var url = link.SetUrl("like/");

        }






        [TestCleanup]
        public void TearDown()
        {
            // This method will be executed after each test method
            // Clean up resources, close connections, etc.
            // For example:
            //client = null;
            _extent.Flush();
        }
    }
}

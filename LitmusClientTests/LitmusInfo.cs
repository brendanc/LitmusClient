using LitmusClient.Entities;
using System.Collections.Generic;
using System.Configuration;

namespace LitmusClientTests
{
    /// <summary>
    /// Static helpers for running tests.
    /// </summary>
    public static class LitmusInfo
    {
        #region Credentials

        public static string Subdomain
        {
            get { return ConfigurationManager.AppSettings["Subdomain"]; }
        }

        public static string User
        {
            get { return ConfigurationManager.AppSettings["User"]; }
        }

        public static string Password
        {
            get { return ConfigurationManager.AppSettings["Password"]; }
        }

        #endregion Credentials

        #region Test Values

        public const string Subject = "Test email subject";
        public const string Body = "<html><body><p>This is a kitten:</p><img src=\"http://placekitten.com/200/300\" alt=\"kitten\"></img></body></html>";
        public const string Url = "http://google.com";
        public const string ReportName = "Test report name";
        public const string TestName = "Teste Name";
        public const string UpdateTestName = "Update Test Name";
        public const bool PublicSharing = true;
        public static int TestSetId = 0; // enter a valid test id for one of your tests
        public static int Version = 1;
        public static int ResultId = 0;
        public static int ReportId = 0;

        public static List<TestingApplication> TestEmailClients
        {
            get
            {
                var emailClients = new List<TestingApplication>();
                emailClients.Add(new TestingApplication { ApplicationCode = "gmailnew", ResultType = "email" });
                emailClients.Add(new TestingApplication { ApplicationCode = "notes8", ResultType = "email" });
                return emailClients;
            }
        }

        public static List<TestingApplication> TestPageClients
        {
            get
            {
                var pageClients = new List<TestingApplication>();
                pageClients.Add(new TestingApplication { ApplicationCode = "chrome2", ResultType = "page" });
                return pageClients;
            }
        }

        #endregion Test Values

        public readonly static string CheckState = LitmusClient.Entities.CheckState.Ticked;
    }
}
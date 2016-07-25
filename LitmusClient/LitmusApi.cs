using LitmusClient.Entities;
using LitmusClient.RequestBodies;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace LitmusClient
{
    /// <summary>
    /// Primary class for interacting with the Litmus API
    /// </summary>
    public class LitmusApi
    {
        public readonly RestClient _restClient;

        #region Resource Routes

        public const string AuthenticationResource = "accounts.xml";
        public const string TestResource = "tests.xml";
        public const string GetTestResource = "tests/{testId}.xml";
        public const string GetTestVersionResource = "tests/{testId}/versions/{version}.xml";
        public const string CreateTestVersionResource = "tests/{testId}/versions.xml";
        public const string PoolTestVersionResource = "tests/{testId}/versions/{version}/poll.xml";
        public const string GetResultResource = "tests/{testId}/versions/{version}/results/{resultId}.xml";
        public const string UpdateResultResource = "tests/{testId}/versions/{version}/results/{resultId}.xml";
        public const string RetestResultResource = "tests/{testId}/versions/{version}/results/{resultId}/retest.xml";
        public const string CreateEmailResource = "emails.xml";
        public const string GetEmailClientsResource = "emails/clients.xml";
        public const string CreatePageTestResource = "pages.xml";
        public const string GetPageClientsResource = "pages/clients.xml";
        public const string GetReportResource = "reports/{reportId}.xml";
        public const string CreateReportResource = "reports.xml";

        #endregion Resource Routes

        public LitmusApi(Account account)
        {
            _restClient = new RestClient(account.LitmusBaseUrl);
            _restClient.Authenticator = new HttpBasicAuthenticator(account.Username, account.Password);
        }

        /// <summary>
        /// Make a very basic request to the api to validate the account details (username,password and subdomain) are correct
        /// </summary>
        /// <returns></returns>
        public bool AccountIsAuthorized()
        {
            var request = new RestRequest(AuthenticationResource, Method.GET);
            var response = _restClient.Execute(request);
            return response.StatusCode == HttpStatusCode.OK &&
                !string.IsNullOrEmpty(response.Content);
        }

        public async Task<bool> AccountIsAuthorizedAsync()
        {
            var request = new RestRequest(AuthenticationResource, Method.GET);
            var tcs = new TaskCompletionSource<bool>();
            _restClient.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.StatusCode == HttpStatusCode.OK &&
                    !string.IsNullOrEmpty(response.Content));
            });
            return await tcs.Task;
        }

        #region Test Set Methods

        /// <summary>
        /// Fetches the first page of existing tests for this account
        /// </summary>
        /// <returns></returns>
        public List<TestSet> GetTests()
        {
            var request = new RestRequest(TestResource, Method.GET);
            return Execute<List<TestSet>>(request);
        }

        public async Task<List<TestSet>> GetTestsAsync()
        {
            var request = new RestRequest(TestResource, Method.GET);
            return await ExecuteAsync<List<TestSet>>(request);
        }

        /// <summary>
        /// Fetch a single test
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        public TestSet GetTest(int testId)
        {
            var request = ConstructRequest(
                GetTestResource,
                Method.GET,
                testId);
            return Execute<TestSet>(request);
        }

        public async Task<TestSet> GetTestAsync(int testId)
        {
            var request = ConstructRequest(
                GetTestResource,
                Method.GET,
                testId);
            return await ExecuteAsync<TestSet>(request);
        }

        /// <summary>
        /// Updates a test in your account.
        /// This is used for publishing results publicly or changing a test's title.
        /// </summary>
        /// <param name="testId">Required. The ID of the test you wish to retrieve</param>
        /// <param name="publicSharing">If set to <c>true</c> [public sharing].</param>
        /// <param name="name">The name of the test.</param>
        /// <returns></returns>
        public TestSet UpdateTest(int testId, bool publicSharing, string name)
        {
            var request = ConstructRequest(
                GetTestResource,
                Method.GET,
                testId);
            request.XmlSerializer = new LitmusPostSerializer();
            request.AddBody(new UpdateTestBody(publicSharing, name));
            return Execute<TestSet>(request);
        }

        public async Task<TestSet> UpdateTestAsync(int testId, bool publicSharing, string name)
        {
            var request = ConstructRequest(
                GetTestResource,
                Method.GET,
                testId);
            request.XmlSerializer = new LitmusPostSerializer();
            request.AddBody(new UpdateTestBody(publicSharing, name));
            return await ExecuteAsync<TestSet>(request);
        }

        #endregion Test Set Methods

        #region Test Set Version Methods

        /// <summary>
        /// Returns details of a single version for a particular test.
        /// </summary>
        /// <param name="testId">Required. The ID of the test you wish to retrieve.</param>
        /// <param name="version">Required. The version of this test you wish to retrieve.</param>
        /// <returns></returns>
        public TestSetVersion GetTestVersion(int testId, int version = 1)
        {
            var request = ConstructRequest(
                GetTestVersionResource,
                Method.GET,
                testId,
                version);
            return Execute<TestSetVersion>(request);
        }

        public async Task<TestSetVersion> GetTestVersionAsync(int testId, int version = 1)
        {
            var request = ConstructRequest(
                GetTestVersionResource,
                Method.GET,
                testId,
                version);
            return await ExecuteAsync<TestSetVersion>(request);
        }

        /// <summary>
        /// Creates a new version of a test.
        /// Creating a new version of a web page test will re-test the same URL immediately.
        /// Creating a new version of an email test will return a new email address in the url_or_guid field and the received field will be false.
        /// You'll need to send an email to that address for received to become true and your screenshots to be generated.
        /// The location field of the headers returned will include a link to the newly created test.
        /// </summary>
        /// <param name="testId">Required. The ID of the test you wish to retrieve.</param>
        /// <returns></returns>
        public TestSetVersion CreateTestVersion(int testId)
        {
            var request = ConstructRequest(
                CreateTestVersionResource,
                Method.POST,
                testId);
            return Execute<TestSetVersion>(request);
        }

        public async Task<TestSetVersion> CreateTestVersionAsync(int testId)
        {
            var request = ConstructRequest(
                CreateTestVersionResource,
                Method.POST,
                testId);
            return await ExecuteAsync<TestSetVersion>(request);
        }

        /// <summary>
        /// To reduce the strain on the Litmus servers, and to reduce the bandwidth you use to check for test completion, there is a special poll method that can be used. The XML document returned will give an indication as to the status of each result.
        /// You may want to wait for every result to complete, or you may wish to return each result as it completes.
        /// You can check the status of the poll method and fetch the whole test version when the state for a particular result changes.
        /// </summary>
        /// <param name="testId">Required. The ID of the test you wish to retrieve.</param>
        /// <param name="version">Required. The version you wish to poll.</param>
        /// <returns></returns>
        public TestSetVersion PollTestVersion(int testId, int version = 1)
        {
            var request = ConstructRequest(
                PoolTestVersionResource,
                Method.GET,
                testId,
                version);
            return Execute<TestSetVersion>(request);
        }

        public async Task<TestSetVersion> PollTestVersionAsync(int testId, int version = 1)
        {
            var request = ConstructRequest(
                PoolTestVersionResource,
                Method.GET,
                testId,
                version);
            return await ExecuteAsync<TestSetVersion>(request);
        }

        #endregion Test Set Version Methods

        #region Result Methods

        /// <summary>
        /// Used to retrieve details of a single result, useful when used in conjunction with the versions/poll method while waiting for individual results to complete.
        /// </summary>
        /// <param name="testId">Required. The ID of the test you wish to retrieve.</param>
        /// <param name="version">Required. The version of this test you wish to retrieve.</param>
        /// <param name="resultId">Required. The ID of the result you wish to retrieve.</param>
        /// <returns></returns>
        public Result GetResult(int testId, int version, int resultId)
        {
            var request = ConstructRequest(
                GetResultResource,
                Method.GET,
                testId,
                version,
                resultId);
            return Execute<Result>(request);
        }

        public async Task<Result> GetResultAsync(int testId, int version, int resultId)
        {
            var request = ConstructRequest(
                GetResultResource,
                Method.GET,
                testId,
                version,
                resultId);
            return await ExecuteAsync<Result>(request);
        }

        /// <summary>
        /// This method is used to update the properties of a result.
        /// Currently the only operation supported by this is to set the compatibility state of a result (whether it appears with a green tick or red cross in the Litmus web interface).
        /// This is set via the check_state parameter which support either 'ticked', 'crossed' or 'nostate' as valid values.
        /// A result which returns nil for check_state is considered to be 'nostate'.
        /// </summary>
        /// <param name="testId">Required. The ID of the test you wish to retrieve.</param>
        /// <param name="version">Required. The version of this test you wish to retrieve.</param>
        /// <param name="resultId">Required. The ID of the result you wish to retrieve.</param>
        /// <returns></returns>
        public Result UpdateResult(int testId, int version, int resultId, string checkState)
        {
            var request = ConstructRequest(
                UpdateResultResource,
                Method.PUT,
                testId,
                version,
                resultId);
            request.XmlSerializer = new LitmusPostSerializer();
            request.AddBody(new UpdateResultBody(checkState));
            return Execute<Result>(request);
        }

        public async Task<Result> UpdateResultAsync(int testId, int version, int resultId, string checkState)
        {
            var request = ConstructRequest(
                UpdateResultResource,
                Method.PUT,
                testId,
                version,
                resultId);
            request.XmlSerializer = new LitmusPostSerializer();
            request.AddBody(new UpdateResultBody(checkState));
            return await ExecuteAsync<Result>(request);
        }

        /// <summary>
        /// Triggers a retest of just this client. Behaviour differs between page and email tests.
        /// For email tests we simply reuse the email source that was sent previously, this means it is best for just attempting to retest if an error occurred with a particular client.
        /// For page tests, this will revisit the url supplied when you started the test, meaning that any changes since the original test will be captured.
        /// Normally retesting like this is just best when an error occurs, if you've made changes to your email or page then testing by creating a new version is best.
        /// </summary>
        /// <param name="testId">Required. The ID of the test you wish to retrieve.</param>
        /// <param name="version">Required. The version of this test you wish to retrieve.</param>
        /// <param name="resultId">Required. The ID of the result you wish to retrieve.</param>
        /// <returns></returns>
        public bool RetestResult(int testId, int version, int resultId)
        {
            var request = ConstructRequest(
                RetestResultResource,
                Method.POST,
                testId,
                version,
                resultId);
            var response = _restClient.Execute(request);
            return response.StatusCode == HttpStatusCode.Created;
        }

        public async Task<bool> RetestResultAsync(int testId, int version, int resultId)
        {
            var request = ConstructRequest(
                RetestResultResource,
                Method.POST,
                testId,
                version,
                resultId);
            var tcs = new TaskCompletionSource<bool>();
            _restClient.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.StatusCode == HttpStatusCode.Created);
            });
            return await tcs.Task;
        }

        #endregion Result Methods

        #region Email Methods

        /// <summary>
        /// Create an email test and return the TestSet.  In this case you will want to grab the test.TestSetVersions[0].UrlOrGuid
        /// and send your email into that address.
        /// </summary>
        /// <param name="testingApplications"></param>
        /// <returns></returns>
        public TestSet CreateEmailTest(List<TestingApplication> testingApplications)
        {
            return CreateEmailTest(testingApplications, "", "");
        }

        public async Task<TestSet> CreateEmailTestAsync(List<TestingApplication> testingApplications)
        {
            return await CreateEmailTestAsync(testingApplications, "", "");
        }

        /// <summary>
        /// Create an email test passing in a subject and body to test.
        /// </summary>
        /// <param name="testingApplications"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public TestSet CreateEmailTest(List<TestingApplication> testingApplications, string subject, string body)
        {
            var request = new RestRequest(CreateEmailResource, Method.POST);
            request.XmlSerializer = new LitmusPostSerializer();
            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Xml;
            request.AddBody(new CreateEmailTestBody(testingApplications, subject, body));
            return Execute<TestSet>(request);
        }

        public async Task<TestSet> CreateEmailTestAsync(List<TestingApplication> testingApplications, string subject, string body)
        {
            var request = new RestRequest(CreateEmailResource, Method.POST);
            request.XmlSerializer = new LitmusPostSerializer();
            request.AddHeader("Accept", "application/xml");
            request.AddBody(new CreateEmailTestBody(testingApplications, subject, body));
            return await ExecuteAsync<TestSet>(request);
        }

        /// <summary>
        /// Fetches all the available email clients we can test on.
        /// </summary>
        /// <returns></returns>
        public List<TestingApplication> GetEmailClients()
        {
            var request = new RestRequest(GetEmailClientsResource, Method.GET);
            return Execute<List<TestingApplication>>(request);
        }

        public async Task<List<TestingApplication>> GetEmailClientsAsync()
        {
            var request = new RestRequest(GetEmailClientsResource, Method.GET);
            return await ExecuteAsync<List<TestingApplication>>(request);
        }

        #endregion Email Methods

        #region Page Methods

        /// <summary>
        /// Create a Litmus page test.
        /// </summary>
        /// <param name="testingApplications"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public TestSet CreatePageTest(List<TestingApplication> testingApplications, string url)
        {
            var request = new RestRequest(CreatePageTestResource, Method.POST);
            request.XmlSerializer = new LitmusPostSerializer();
            request.AddHeader("Accept", "application/xml");
            request.AddBody(new CreatePageTestBody(testingApplications, url));
            return Execute<TestSet>(request);
        }

        public async Task<TestSet> CreatePageTestAsync(List<TestingApplication> testingApplications, string url)
        {
            var request = new RestRequest(CreatePageTestResource, Method.POST);
            request.XmlSerializer = new LitmusPostSerializer();
            request.AddHeader("Accept", "application/xml");
            request.AddBody(new CreatePageTestBody(testingApplications, url));
            return await ExecuteAsync<TestSet>(request);
        }

        /// <summary>
        /// Fetches all the available page clients we can test on
        /// </summary>
        /// <returns></returns>
        public List<TestingApplication> GetPageClients()
        {
            var request = new RestRequest(GetPageClientsResource, Method.GET);
            return Execute<List<TestingApplication>>(request);
        }

        public async Task<List<TestingApplication>> GetPageClientsAsync()
        {
            var request = new RestRequest(GetPageClientsResource, Method.GET);
            return await ExecuteAsync<List<TestingApplication>>(request);
        }

        #endregion Page Methods

        #region Report Methods

        /// <summary>
        /// Shows a list of the 20 most recent lists you've created within your Litmus account.
        /// </summary>
        /// <returns></returns>
        public List<Report> GetReports()
        {
            var request = new RestRequest(CreateReportResource, Method.GET);
            return Execute<List<Report>>(request);
        }

        public async Task<List<Report>> GetReportsAsync()
        {
            var request = new RestRequest(CreateReportResource, Method.GET);
            return await ExecuteAsync<List<Report>>(request);
        }

        /// <summary>
        /// Retrieves a single specified report (campaign).
        /// Note that we also include links to download the full report content in CSV format.
        /// You can use this to programmatically collect all the data for a campaign that is displayed in our charts and tables.
        /// </summary>
        /// <param name="reportId">The report ID parameter.</param>
        /// <returns></returns>
        public ReportContent GetReport(int reportId)
        {
            var request = ConstructRequest(
                GetReportResource,
                Method.GET,
                null,
                null,
                null,
                reportId);
            request.AddUrlSegment("reportId", reportId.ToString());
            return Execute<ReportContent>(request);
        }

        public async Task<ReportContent> GetReportAsync(int reportId)
        {
            var request = ConstructRequest(
                GetReportResource,
                Method.GET,
                null,
                null,
                null,
                reportId);
            return await ExecuteAsync<ReportContent>(request);
        }

        /// <summary>
        /// Creates a new report if you have available credits.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Report CreateReport(string name)
        {
            var request = new RestRequest(CreateReportResource, Method.POST);
            request.XmlSerializer = new LitmusPostSerializer();
            request.AddBody(new CreateReportBody(name));
            return Execute<Report>(request);
        }

        public async Task<Report> CreateReportAsync(string name)
        {
            var request = new RestRequest(CreateReportResource, Method.POST);
            request.XmlSerializer = new LitmusPostSerializer();
            request.AddBody(new CreateReportBody(name));
            return await ExecuteAsync<Report>(request);
        }

        #endregion Report Methods

        #region Helpers

        public Dictionary<string, object> ConstructDictionary(
            int? testId,
            int? version,
            int? resultId,
            int? reportId = null)
        {
            var result = new Dictionary<string, object>();
            if (testId.HasValue)
                result.Add("testId", testId);
            if (version.HasValue)
                result.Add("version", version);
            if (resultId.HasValue)
                result.Add("resultId", resultId);
            if (reportId.HasValue)
                result.Add("reportId", reportId);
            return result;
        }

        public RestRequest ConstructRequest(
            string resource,
            Method method,
            int? testId = null,
            int? version = null,
            int? resultId = null,
            int? reportId = null)
        {
            var segments = ConstructDictionary(testId, version, resultId, reportId);
            var request = new RestRequest(resource, method);
            if (segments != null)
            {
                foreach (var segment in segments)
                {
                    request.AddUrlSegment(segment.Key, segment.Value.ToString());
                }
            }
            return request;
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var response = _restClient.Execute<T>(request);
            return response.Data;
        }

        public async Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var tcs = new TaskCompletionSource<T>();
            _restClient.ExecuteAsync<T>(request, response =>
            {
                tcs.SetResult(response.Data);
            });
            return await tcs.Task;
        }

        #endregion Helpers
    }
}
using System.Collections.Generic;
using System.Net;
using LitmusClient.Litmus;
using RestSharp;
using RestSharp.Deserializers;

namespace LitmusClient
{
    /// <summary>
    /// Primary class for interacting with the Litmus API
    /// </summary>
    public class LitmusApi
    {
        private readonly RestClient restClient;

        public LitmusApi(Account account)
        {
            this.restClient = new RestClient(account.LitmusBaseUrl);
            this.restClient.Authenticator = new HttpBasicAuthenticator(account.Username, account.Password);
        }

        /// <summary>
        /// Make a very basic request to the api to validate the account details (username,password and subdomain) are correct
        /// </summary>
        /// <returns></returns>
        public bool AccountIsAuthorized()
        {
            var request = new RestRequest("accounts.xml", Method.GET);
            var response = restClient.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK) return false;
            return !string.IsNullOrEmpty(response.Content);
        }

        /// <summary>
        /// Fetches all the available email clients we can test on.
        /// </summary>
        /// <returns></returns>
        public List<TestingApplication> GetEmailClients()
        {
            var request = new RestRequest("emails/clients.xml", Method.GET);
            var clients =  ExecuteGet<List<TestingApplication>>(request);
            return clients;
        }
       
        /// <summary>
        /// Fetches all the available page clients we can test on
        /// </summary>
        /// <returns></returns>
        public List<TestingApplication> GetPageClients()
        {
            var request = new RestRequest("pages/clients.xml", Method.GET);
            var clients = ExecuteGet<List<TestingApplication>>(request);
            return clients;
        }

        /// <summary>
        /// Fetches the first page of existing tests for this account
        /// </summary>
        /// <returns></returns>
        public List<TestSet>GetTests()
        {
            var request = new RestRequest("tests.xml", Method.GET);
            var tests = ExecuteGet<List<TestSet>>(request);
            return tests; 
        }

        /// <summary>
        /// Fetch a single test 
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        public TestSet GetTest(int testId)
        {
            var testRequest = new RestRequest(string.Format("tests/{0}.xml",testId), Method.GET);
            var test = ExecuteGet<TestSet>(testRequest);
            return test;
        }

        /// <summary>
        /// Use Litmus' poll endpoint to see when test results complete.  If no version is passed in default to 1.
        /// </summary>
        /// <param name="testSetId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public List<Result>PollForTestResults(int testSetId,int version = 1)
        {
            var pollRequest = new RestRequest(string.Format("tests/{0}/versions/{1}/poll.xml", testSetId,version), Method.GET);
            var results = ExecuteGet<List<Result>>(pollRequest);
            return results;
        }

        /// <summary>
        /// Create an email test and return the TestSet.  In this case you will want to grab the test.TestSetVersions[0].UrlOrGuid
        /// and send your email into that address.
        /// </summary>
        /// <param name="testingApplications"></param>
        /// <returns></returns>
        public TestSet CreateEmailTest(List<TestingApplication>testingApplications)
        {
            return CreateEmailTest(testingApplications, "", "");
        }

        /// <summary>
        /// Create an email test passing in a subject and body to test.
        /// </summary>
        /// <param name="testingApplications"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public TestSet CreateEmailTest(List<TestingApplication>testingApplications,string subject,string body)
        {
            var request = new RestRequest("emails.xml", Method.POST);
            request.XmlSerializer = new LitmusPostSerializer();
            request.AddHeader("Accept", "application/xml");
            request.AddBody(new CreateEmailTestRequest(testingApplications,subject,body));
            var test = ExecutePost<TestSet>(request);
            return test; 
        }

        /// <summary>
        /// Create a Litmus page test.
        /// </summary>
        /// <param name="testingApplications"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public TestSet CreatePageTest(List<TestingApplication>testingApplications,string url)
        {
            var request = new RestRequest("pages.xml", Method.POST);
            request.XmlSerializer = new LitmusPostSerializer();
            request.AddHeader("Accept", "application/xml");
            request.AddBody(new CreatePageTestRequest(testingApplications,url));
            var test = ExecutePost<TestSet>(request);
            return test; 
        }

        /// <summary>
        /// Execute a RestRequest and deserialize into an object using the RestSharp library
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private T ExecuteGet<T>(RestRequest request) where T : new()
        {  
            var response = restClient.Execute<T>(request);
            return response.Data;
        }

        /// <summary>
        /// Because Litmus' POST and GET objects are different we handle POSTS a little different.
        /// POST using our custom serializer then deserialize the response using the default RestSharp deserializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private T ExecutePost<T>(RestRequest request) where T : new()
        {
            var response = restClient.Post(request);
            var deserializer = new XmlDeserializer();
            return deserializer.Deserialize<T>(response);
        }
    }
}
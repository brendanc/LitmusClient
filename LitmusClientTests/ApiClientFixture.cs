using System;
using System.Collections.Generic;
using System.Threading;
using LitmusClient;
using LitmusClient.Litmus;
using NUnit.Framework;

namespace LitmusClientTests
{
    [TestFixture]
    public class ApiClientFixture
    {
        private LitmusApi client;

        [SetUp]
        public void SetUp()
        {
            client = new LitmusApi(new Account(LitmusInfo.Subdomain, LitmusInfo.User, LitmusInfo.Pass));
        }

        [Test]
        public void AccountIsAuthorized_WithValidAccount_ShouldReturnTrue()
        {
            Assert.That(client.AccountIsAuthorized(),Is.True);
        }

        [Test]
        public void AccountIsAuthorized_WithInvalidPassword_ShouldReturnFalse()
        {
            var testClient = new LitmusApi(new Account(LitmusInfo.Subdomain, LitmusInfo.User, LitmusInfo.Pass + "XZXZXZXZX"));
            Assert.That(testClient.AccountIsAuthorized(),Is.False);
        }

        [Test]
        public void AccountIsAuthorized_WithInvalidSubdomain_ShouldReturnFalse()
        {
            var testClient = new LitmusApi(new Account(LitmusInfo.Subdomain + "XZXZXZXZX", LitmusInfo.User, LitmusInfo.Pass));
            Assert.That(testClient.AccountIsAuthorized(), Is.False); 
        }

        [Test]
        public void GetEmailClients_WithValidCredentials_ShouldReturnValidCollection()
        {
            var clients = client.GetEmailClients();
            Assert.That(clients.Count,Is.GreaterThan(0));
            foreach (var testingApplication in clients)
            {
                Assert.That(string.IsNullOrEmpty(testingApplication.ApplicationCode),Is.Not.True);
                Assert.That(string.IsNullOrEmpty(testingApplication.ApplicationLongName), Is.Not.True);
                Assert.That(string.IsNullOrEmpty(testingApplication.PlatformName), Is.Not.True);
                Assert.That(string.IsNullOrEmpty(testingApplication.ResultType), Is.Not.True);
            }
        }

        [Test]
        public void GetPageClients_WithValidCredentials_ShouldReturnValidCollection()
        {
            var clients = client.GetPageClients();
            Assert.That(clients.Count, Is.GreaterThan(0));
            foreach (var testingApplication in clients)
            {
                Assert.That(string.IsNullOrEmpty(testingApplication.ApplicationCode), Is.Not.True);
                Assert.That(string.IsNullOrEmpty(testingApplication.ApplicationLongName), Is.Not.True);
                Assert.That(string.IsNullOrEmpty(testingApplication.PlatformName), Is.Not.True);
                Assert.That(string.IsNullOrEmpty(testingApplication.ResultType), Is.Not.True);
            }  
        }

        [Test]
        public void GetDefaultEmailClients_WithValidCredentials_ShouldReturnValidCollection()
        {
            var clients = client.GetDefaultEmailClients();
            Assert.That(clients.Count, Is.GreaterThan(0));
            foreach (var testingApplication in clients)
            {
                Assert.That(string.IsNullOrEmpty(testingApplication.ApplicationCode), Is.Not.True);
                Assert.That(string.IsNullOrEmpty(testingApplication.ApplicationLongName), Is.Not.True);
                Assert.That(string.IsNullOrEmpty(testingApplication.PlatformName), Is.Not.True);
            }
        }

        [Test]
        public void GetDefaultPageClients_WithValidCredentials_ShouldReturnValidCollection()
        {
            var clients = client.GetDefaultPageClients();
            Assert.That(clients.Count, Is.GreaterThan(0));
            foreach (var testingApplication in clients)
            {
                Assert.That(string.IsNullOrEmpty(testingApplication.ApplicationCode), Is.Not.True);
                Assert.That(string.IsNullOrEmpty(testingApplication.ApplicationLongName), Is.Not.True);
                Assert.That(string.IsNullOrEmpty(testingApplication.PlatformName), Is.Not.True);
            }
        }

        [Test]
        public void GetTests_WithValidAccount_ShouldFetchTests()
        {
            var tests = client.GetTests();
            Assert.That(tests.Count,Is.GreaterThan(0));
            foreach (var testSet in tests)
            {
                Assert.That(string.IsNullOrEmpty(testSet.Name), Is.False);
                Assert.That(string.IsNullOrEmpty(testSet.State),Is.False);
                Assert.That(string.IsNullOrEmpty(testSet.Service),Is.False);
                Assert.That(string.IsNullOrEmpty(testSet.UrlOrGuid), Is.False);
                Assert.That(testSet.Id, Is.GreaterThan(0));
                Assert.That(testSet.CreatedAt,Is.GreaterThan(new DateTime(2005,1,1)));  //Litmus was founded in 2005 so there shouldn't be any tests created before then :-)
            }
        }

        [Test]
        public void GetTest_WithValidTestId_ShouldFetchTest()
        {
            var testSet = client.GetTest(LitmusInfo.TestSetId);
            Assert.That(string.IsNullOrEmpty(testSet.Name), Is.False);
            Assert.That(string.IsNullOrEmpty(testSet.State), Is.False);
            Assert.That(string.IsNullOrEmpty(testSet.Service), Is.False);
            Assert.That(string.IsNullOrEmpty(testSet.UrlOrGuid), Is.False);
            Assert.That(testSet.Id, Is.GreaterThan(0));
            Assert.That(testSet.CreatedAt, Is.GreaterThan(new DateTime(2005, 1, 1)));  //Litmus was founded in 2005 so there shouldn't be any tests created before then :-)
        }

        [Test]
        public void GetTest_WithValidTestId_ShouldReturnAtLeastOneVersion()
        {
            var testSet = client.GetTest(LitmusInfo.TestSetId);
            Assert.That(testSet.TestSetVersions.Count,Is.GreaterThanOrEqualTo(1));

            foreach (var testSetVersion in testSet.TestSetVersions)
            {
                Assert.That(testSetVersion.Version,Is.GreaterThanOrEqualTo(1));
                Assert.That(string.IsNullOrEmpty(testSetVersion.UrlOrGuid),Is.False);
            }
        }

        [Test]
        public void GetTest_WithValidTestId_ShouldContainResults()
        {
            var testSet = client.GetTest(LitmusInfo.TestSetId);
            Assert.That(testSet.TestSetVersions[0].Results.Count, Is.GreaterThanOrEqualTo(1));
            var version = testSet.TestSetVersions[0];
            foreach (var result in version.Results)
            {
                Assert.That(result.StartedAt, Is.GreaterThan(new DateTime(2005, 1, 1)));   
            }
        }

        [Test]
        public void PollForResult_WithValidTestId_ShouldReturnResults()
        {
            var results = client.PollForTestResults(LitmusInfo.TestSetId);
            Assert.That(results.Count,Is.GreaterThan(0));
        }

        /// <summary>
        /// This test will actually create a blank Litmus test so it is set to ignore otherwise we would have to grab the
        /// email address and send in an email. 
        /// </summary>
        [Test,Ignore]
        public void CreateEmailTest_WithValidCredentials_ShouldCreateLitmusEmailTest()
        {
            var emailClients = new List<TestingApplication>();
            emailClients.Add(new TestingApplication() { ApplicationCode = "hotmail", ResultType = "email" });
            emailClients.Add(new TestingApplication() { ApplicationCode = "gmailnew", ResultType = "email" });
            emailClients.Add(new TestingApplication() { ApplicationCode = "notes8", ResultType = "email" });
            var emailAddress = client.CreateEmailTest(emailClients).TestSetVersions[0].UrlOrGuid;
            Console.WriteLine(emailAddress);
            Assert.That(!string.IsNullOrEmpty(emailAddress));
            Assert.That(emailAddress.Contains("@emailtests.com"));
        }

        
        [Test]
        public void CreateEmailTest_WithSubjectAndBody_ShouldCreateLitmusEmailTest()
        {
            var emailClients = new List<TestingApplication>();
            emailClients.Add(new TestingApplication() { ApplicationCode = "hotmail", ResultType = "email" });
            emailClients.Add(new TestingApplication() { ApplicationCode = "gmailnew", ResultType = "email" });
            emailClients.Add(new TestingApplication() { ApplicationCode = "notes8", ResultType = "email" });
            var subject = string.Format("Test email created by c# wrapper on {0}", DateTime.Now);
            var test = client.CreateEmailTest(emailClients, subject, "<html><body><p>This is a kitten:</p><img src=\"http://placekitten.com/200/300\" alt=\"kitten\"></img></body></html>");
            Thread.Sleep(20000);
            Assert.That(test.TestSetVersions[0].Received);
            Assert.That(test.TestSetVersions[0].Results.Count, Is.EqualTo(3));
        }

        [Test]
        public void CreatePageTest_WithValidUrl_ShouldCreateLitmusPageTest()
        {
            var pageClients = new List<TestingApplication>();
            pageClients.Add(new TestingApplication() { ApplicationCode = "chrome2", ResultType = "page" });
            pageClients.Add(new TestingApplication() { ApplicationCode = "ie7", ResultType = "page" });
            pageClients.Add(new TestingApplication() { ApplicationCode = "ie6", ResultType = "page" });
            var url = "http://google.com";
            var test = client.CreatePageTest(pageClients, url);
            Assert.That(test.TestSetVersions[0].UrlOrGuid == url);
            Assert.That(test.TestSetVersions[0].Results.Count,Is.EqualTo(3));
        }



    }
}
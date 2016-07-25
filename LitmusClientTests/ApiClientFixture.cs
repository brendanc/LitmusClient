using LitmusClient;
using LitmusClient.Entities;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LitmusClientTests
{
    [TestFixture]
    public class ApiClientFixture
    {
        private LitmusApi _client;

        [SetUp]
        public void SetUp()
        {
            _client = new LitmusApi(new Account(LitmusInfo.Subdomain, LitmusInfo.User, LitmusInfo.Password));
        }

        [Test]
        [Category("Authorization")]
        public void AccountIsAuthorized_WithValidAccount_ShouldReturnTrue()
        {
            Assert.That(_client.AccountIsAuthorized(), Is.True);
        }

        [Test]
        [Category("Authorization")]
        public void AccountIsAuthorized_WithInvalidPassword_ShouldReturnFalse()
        {
            var testClient = new LitmusApi(new Account(LitmusInfo.Subdomain, LitmusInfo.User, LitmusInfo.Password + "XZXZXZXZX"));
            Assert.That(testClient.AccountIsAuthorized(), Is.False);
        }

        [Test]
        [Category("Authorization")]
        public void AccountIsAuthorized_WithInvalidSubdomain_ShouldReturnFalse()
        {
            var testClient = new LitmusApi(new Account(LitmusInfo.Subdomain + "XZXZXZXZX", LitmusInfo.User, LitmusInfo.Password));
            Assert.That(testClient.AccountIsAuthorized(), Is.False);
        }

        [Test]
        [Category("Authorization")]
        public async Task AccountIsAuthorizedAsync_WithValidAccount_ShouldReturnTrue()
        {
            Assert.That(await _client.AccountIsAuthorizedAsync(), Is.True);
        }

        [Test]
        [Category("Authorization")]
        public async Task AccountIsAuthorizedAsync_WithInvalidPassword_ShouldReturnFalse()
        {
            var testClient = new LitmusApi(new Account(LitmusInfo.Subdomain, LitmusInfo.User, LitmusInfo.Password + "XZXZXZXZX"));
            Assert.That(await testClient.AccountIsAuthorizedAsync(), Is.False);
        }

        [Test]
        [Category("Authorization")]
        public async Task AccountIsAuthorizedAsync_WithInvalidSubdomain_ShouldReturnFalse()
        {
            var testClient = new LitmusApi(new Account(LitmusInfo.Subdomain + "XZXZXZXZX", LitmusInfo.User, LitmusInfo.Password));
            Assert.That(await testClient.AccountIsAuthorizedAsync(), Is.False);
        }

        #region Test Set Methods

        [Test]
        [Category("Test Set Methods")]
        public void GetTests_WithValidAccount_ShouldFetchTests()
        {
            var testSets = _client.GetTests();
            Assert.That(testSets.Count, Is.GreaterThan(0));
            foreach (var testSet in testSets)
            {
                AssertHelper.AssertTestSet(testSet, true);
            }
        }

        [Test]
        [Category("Test Set Methods")]
        public async Task GetTestsAsync_WithValidAccount_ShouldFetchTests()
        {
            var testSets = await _client.GetTestsAsync();
            Assert.That(testSets.Count, Is.GreaterThan(0));
            foreach (var testSet in testSets)
            {
                AssertHelper.AssertTestSet(testSet, true);
            }
        }

        [Test]
        [Category("Test Set Methods")]
        public void GetTest_WithValidTestId_ShouldFetchTest()
        {
            var testSets = _client.GetTests();
            var testSet = testSets.First();
            testSet = _client.GetTest(testSet.Id);
            AssertHelper.AssertTestSet(testSet, true);
        }

        [Test]
        [Category("Test Set Methods")]
        public async Task GetTestAsync_WithValidTestId_ShouldFetchTest()
        {
            var testSets = await _client.GetTestsAsync();
            var testSet = testSets.First();
            testSet = await _client.GetTestAsync(testSet.Id);
            AssertHelper.AssertTestSet(testSet, true);
        }

        [Test]
        [Category("Test Set Methods")]
        public void UpdateTest_ShouldChangeTestName()
        {
            var testSets = _client.GetTests();
            var testSet = testSets.First();
            testSet = _client.UpdateTest(testSet.Id, LitmusInfo.PublicSharing, LitmusInfo.UpdateTestName);
            AssertHelper.AssertTestSet(testSet, false);
            Assert.AreEqual(LitmusInfo.PublicSharing, testSet.PublicSharing);
            Assert.AreEqual(LitmusInfo.UpdateTestName, testSet.Name);
        }

        [Test]
        [Category("Test Set Methods")]
        public async Task UpdateTestAsync_ShouldChangeTestName()
        {
            var testSets = await _client.GetTestsAsync();
            var testSet = testSets.First();
            testSet = await _client.UpdateTestAsync(testSet.Id, LitmusInfo.PublicSharing, LitmusInfo.UpdateTestName);
            AssertHelper.AssertTestSet(testSet, false);
            Assert.AreEqual(LitmusInfo.PublicSharing, testSet.PublicSharing);
            Assert.AreEqual(LitmusInfo.UpdateTestName, testSet.Name);
        }

        #endregion Test Set Methods

        #region Test Set Version Methods

        [Test]
        [Category("Test Set Version Methods")]
        public void PollForResult_WithValidTestId_ShouldReturnResults()
        {
            var testSet = _client.CreateEmailTest(LitmusInfo.TestEmailClients, LitmusInfo.Subject, LitmusInfo.Body);
            var testSetVersion = _client.PollTestVersion(testSet.Id);
            AssertHelper.AssertIncompleteTestSetVersion(testSetVersion);
        }

        [Test]
        [Category("Test Set Version Methods")]
        public async Task PollForResultAsync_WithValidTestId_ShouldReturnResults()
        {
            var testSet = await _client.CreateEmailTestAsync(LitmusInfo.TestEmailClients, LitmusInfo.Subject, LitmusInfo.Body);
            var testSetVersion = await _client.PollTestVersionAsync(testSet.Id);
            AssertHelper.AssertIncompleteTestSetVersion(testSetVersion);
        }

        [Test]
        [Category("Test Set Version Methods")]
        public void CreateTestVersion_ShouldReturnTestSetVersion()
        {
            var testSet = _client.CreateEmailTest(LitmusInfo.TestEmailClients, LitmusInfo.Subject, LitmusInfo.Body);
            var testSetVersion = _client.CreateTestVersion(testSet.Id);
            AssertHelper.AssertTestSetVersion(testSetVersion, false);
            Assert.That(testSetVersion.Version, Is.GreaterThan(1));
        }

        [Test]
        [Category("Test Set Version Methods")]
        public async Task CreateTestVersionAsync_ShouldReturnTestSetVersion()
        {
            var testSet = await _client.CreateEmailTestAsync(LitmusInfo.TestEmailClients, LitmusInfo.Subject, LitmusInfo.Body);
            var testSetVersion = await _client.CreateTestVersionAsync(testSet.Id);
            AssertHelper.AssertTestSetVersion(testSetVersion, false);
            Assert.That(testSetVersion.Version, Is.GreaterThan(1));
        }

        [Test]
        [Category("Test Set Version Methods")]
        public void GetTestVersion_ShouldReturnSpecificTestVersion()
        {
            var testSet = _client.CreateEmailTest(LitmusInfo.TestEmailClients, LitmusInfo.Subject, LitmusInfo.Body);
            var createdTestSetVersion = _client.CreateTestVersion(testSet.Id);
            TestSetVersion poolTestSetVersion = null;
            var cycles = 0;
            do
            {
                Thread.Sleep(60 * 1000);
                poolTestSetVersion = _client.PollTestVersion(testSet.Id, createdTestSetVersion.Version);
                cycles++;
            } while (poolTestSetVersion.Results.Any(r => r.State != ResultState.Complete) || cycles > 5);
            if (cycles > 5)
                Assert.Fail("The maximum amount of retries have been reached!");
            var testSetVersion = _client.GetTestVersion(testSet.Id, createdTestSetVersion.Version);
            AssertHelper.AssertTestSetVersion(testSetVersion, true);
            Assert.AreEqual(createdTestSetVersion.Version, testSetVersion.Version);
            Assert.AreEqual(ResultState.Complete, testSetVersion.Results.First().State);
        }

        [Test]
        [Category("Test Set Version Methods")]
        public async Task GetTestVersionAsync_ShouldReturnSpecificTestVersion()
        {
            var testSet = await _client.CreateEmailTestAsync(LitmusInfo.TestEmailClients, LitmusInfo.Subject, LitmusInfo.Body);
            var createdTestSetVersion = await _client.CreateTestVersionAsync(testSet.Id);
            TestSetVersion poolTestSetVersion = null;
            var cycles = 0;
            do
            {
                Thread.Sleep(60 * 1000);
                poolTestSetVersion = await _client.PollTestVersionAsync(testSet.Id, createdTestSetVersion.Version);
                cycles++;
            } while (poolTestSetVersion.Results.Any(r => r.State != ResultState.Complete) || cycles > 5);
            if (cycles > 5)
                Assert.Fail("The maximum amount of retries have been reached!");
            var testSetVersion = _client.GetTestVersion(testSet.Id, createdTestSetVersion.Version);
            AssertHelper.AssertTestSetVersion(testSetVersion, true);
            Assert.AreEqual(createdTestSetVersion.Version, testSetVersion.Version);
            Assert.AreEqual(ResultState.Complete, testSetVersion.Results.First().State);
        }

        #endregion Test Set Version Methods

        #region Email Methods

        [Test]
        [Category("Email Methods")]
        public void GetEmailClients_WithValidCredentials_ShouldReturnValidCollection()
        {
            var testingApplications = _client.GetEmailClients();
            Assert.That(testingApplications.Count, Is.GreaterThan(0));
            foreach (var testingApplication in testingApplications)
            {
                AssertHelper.AssertTestingApplication(testingApplication);
                Assert.True(testingApplication.ResultType == ResultType.Email || testingApplication.ResultType == ResultType.Spam);
            }
        }

        [Test]
        [Category("Email Methods")]
        public async Task GetEmailClientsAsync_WithValidCredentials_ShouldReturnValidCollection()
        {
            var testingApplications = await _client.GetEmailClientsAsync();
            Assert.That(testingApplications.Count, Is.GreaterThan(0));
            foreach (var testingApplication in testingApplications)
            {
                AssertHelper.AssertTestingApplication(testingApplication);
                Assert.True(testingApplication.ResultType == ResultType.Email || testingApplication.ResultType == ResultType.Spam);
            }
        }

        /// <summary>
        /// This test will actually create a blank Litmus test so it is set to ignore otherwise we would have to grab the
        /// email address and send in an email.
        /// </summary>
        [Test, Ignore]
        [Category("Email Methods")]
        public void CreateEmailTest_WithValidCredentials_ShouldCreateLitmusEmailTest()
        {
            var testSet = _client.CreateEmailTest(LitmusInfo.TestEmailClients);
            AssertHelper.AssertTestSet(testSet, false);
        }

        [Test, Ignore]
        [Category("Email Methods")]
        public async Task CreateEmailTestAsync_WithValidCredentials_ShouldCreateLitmusEmailTest()
        {
            var testSet = await _client.CreateEmailTestAsync(LitmusInfo.TestEmailClients);
            AssertHelper.AssertTestSet(testSet, false);
        }

        [Test]
        [Category("Email Methods")]
        public void CreateEmailTest_WithSubjectAndBody_ShouldCreateLitmusEmailTest()
        {
            var testSet = _client.CreateEmailTest(LitmusInfo.TestEmailClients, LitmusInfo.Subject, LitmusInfo.Body);
            AssertHelper.AssertTestSet(testSet, false);
        }

        [Test]
        [Category("Email Methods")]
        public async Task CreateEmailTestAsync_WithSubjectAndBody_ShouldCreateLitmusEmailTest()
        {
            var testSet = await _client.CreateEmailTestAsync(LitmusInfo.TestEmailClients, LitmusInfo.Subject, LitmusInfo.Body);
            AssertHelper.AssertTestSet(testSet, false);
        }

        #endregion Email Methods

        #region Page Methods

        [Test]
        [Category("Page Methods")]
        public void GetPageClients_WithValidCredentials_ShouldReturnValidCollection()
        {
            var testingApplications = _client.GetPageClients();
            Assert.That(testingApplications.Count, Is.GreaterThan(0));
            foreach (var testingApplication in testingApplications)
            {
                AssertHelper.AssertTestingApplication(testingApplication);
                Assert.AreEqual(ResultType.Page, testingApplication.ResultType);
            }
        }

        [Test]
        [Category("Page Methods")]
        public async Task GetPageClientsAsync_WithValidCredentials_ShouldReturnValidCollection()
        {
            var testingApplications = await _client.GetPageClientsAsync();
            Assert.That(testingApplications.Count, Is.GreaterThan(0));
            foreach (var testingApplication in testingApplications)
            {
                AssertHelper.AssertTestingApplication(testingApplication);
                Assert.AreEqual(ResultType.Page, testingApplication.ResultType);
            }
        }

        [Test]
        [Category("Page Methods")]
        public void CreatePageTest_WithValidUrl_ShouldCreateLitmusPageTest()
        {
            var testSet = _client.CreatePageTest(LitmusInfo.TestPageClients, LitmusInfo.Url);
            AssertHelper.AssertTestSet(testSet, false);
        }

        [Test]
        [Category("Page Methods")]
        public async Task CreatePageTestAsync_WithValidUrl_ShouldCreateLitmusPageTest()
        {
            var testSet = await _client.CreatePageTestAsync(LitmusInfo.TestPageClients, LitmusInfo.Url);
            AssertHelper.AssertTestSet(testSet, false);
        }

        #endregion Page Methods

        #region Report Methods

        [Test]
        [Category("Report Methods")]
        public void CreateReport_WithName_ShouldCreateLitmusReport()
        {
            var report = _client.CreateReport(LitmusInfo.ReportName);
            AssertHelper.AssertReport(report);
            Assert.AreEqual(LitmusInfo.ReportName, report.Name);
            LitmusInfo.ReportId = report.Id;
        }

        [Test]
        [Category("Report Methods")]
        public async Task CreateReportAsync_WithName_ShouldCreateLitmusReport()
        {
            var report = await _client.CreateReportAsync(LitmusInfo.ReportName);
            AssertHelper.AssertReport(report);
            Assert.AreEqual(LitmusInfo.ReportName, report.Name);
            LitmusInfo.ReportId = report.Id;
        }

        [Test]
        [Category("Report Methods")]
        public void GetReports_ShouldListLitmusReports()
        {
            var reports = _client.GetReports();
            Assert.That(reports.Count, Is.InRange(1, 20));
            foreach (var report in reports)
                AssertHelper.AssertReport(report);
        }

        [Test]
        [Category("Report Methods")]
        public async Task GetReportsAsync_ShouldListLitmusReports()
        {
            var reports = await _client.GetReportsAsync();
            Assert.That(reports.Count, Is.InRange(1, 20));
            foreach (var report in reports)
                AssertHelper.AssertReport(report);
        }

        [Test]
        [Category("Report Methods")]
        public void GetReport_WithId_ShouldGetLitmusReport()
        {
            var reports = _client.GetReports();
            var report = reports.First();
            var reportContent = _client.GetReport(report.Id);
            AssertHelper.AssertReportContent(reportContent);
        }

        [Test]
        [Category("Report Methods")]
        public async Task GetReportAsync_WithId_ShouldGetLitmusReport()
        {
            var reports = await _client.GetReportsAsync();
            var report = reports.First();
            var reportContent = await _client.GetReportAsync(report.Id);
            AssertHelper.AssertReportContent(reportContent);
        }

        #endregion Report Methods

        #region Result Methods

        [Test]
        [Category("Result Methods")]
        public void GetResult_ShouldReturnResult()
        {
            var testSet = _client.CreateEmailTest(LitmusInfo.TestEmailClients, LitmusInfo.Subject, LitmusInfo.Body);
            var testSetVersion = _client.PollTestVersion(testSet.Id);
            var result = testSetVersion.Results.First();
            result = _client.GetResult(testSet.Id, testSetVersion.Version, result.Id);
            AssertHelper.AssertResult(result, false);
        }

        [Test]
        [Category("Result Methods")]
        public async Task GetResultAsync_ShouldReturnResult()
        {
            var testSet = await _client.CreateEmailTestAsync(LitmusInfo.TestEmailClients, LitmusInfo.Subject, LitmusInfo.Body);
            var testSetVersion = await _client.PollTestVersionAsync(testSet.Id);
            var result = testSetVersion.Results.First();
            result = await _client.GetResultAsync(testSet.Id, testSetVersion.Version, result.Id);
            AssertHelper.AssertResult(result, false);
        }

        [Test]
        [Category("Result Methods")]
        public void UpdateResult_ShouldReturnUpdatedResult()
        {
            var testSet = _client.CreateEmailTest(LitmusInfo.TestEmailClients, LitmusInfo.Subject, LitmusInfo.Body);
            var testSetVersion = _client.PollTestVersion(testSet.Id);
            var result = testSetVersion.Results.First();
            result = _client.UpdateResult(testSet.Id, testSetVersion.Version, result.Id, LitmusInfo.CheckState);
            AssertHelper.AssertResult(result, false);
        }

        [Test]
        [Category("Result Methods")]
        public async Task UpdateResultAsync_ShouldReturnResult()
        {
            var testSet = await _client.CreateEmailTestAsync(LitmusInfo.TestEmailClients, LitmusInfo.Subject, LitmusInfo.Body);
            var testSetVersion = await _client.PollTestVersionAsync(testSet.Id);
            var result = testSetVersion.Results.First();
            result = await _client.UpdateResultAsync(testSet.Id, testSetVersion.Version, result.Id, LitmusInfo.CheckState);
            AssertHelper.AssertResult(result, false);
        }

        [Test]
        [Category("Result Methods")]
        public void RetestResult_ShouldReturnTrue()
        {
            var testSet = _client.CreateEmailTest(LitmusInfo.TestEmailClients, LitmusInfo.Subject, LitmusInfo.Body);
            var testSetVersion = _client.PollTestVersion(testSet.Id);
            var result = testSetVersion.Results.First();
            var retestResult = _client.RetestResult(testSet.Id, testSetVersion.Version, result.Id);
            Assert.IsTrue(retestResult);
        }

        [Test]
        [Category("Result Methods")]
        public async Task RetestResultAsync_ShouldReturnTrue()
        {
            var testSet = await _client.CreateEmailTestAsync(LitmusInfo.TestEmailClients, LitmusInfo.Subject, LitmusInfo.Body);
            var testSetVersion = await _client.PollTestVersionAsync(testSet.Id);
            var result = testSetVersion.Results.First();
            var retestResult = await _client.RetestResultAsync(testSet.Id, testSetVersion.Version, result.Id);
            Assert.IsTrue(retestResult);
        }

        #endregion Result Methods
    }
}
using LitmusClient.Entities;
using LitmusClient.RequestBodies;
using NUnit.Framework;
using System.Collections.Generic;

namespace LitmusClientTests
{
    [TestFixture]
    public class CreatePageTestBodyFixture
    {
        [Test]
        public void CreatePageTestBody_WithApplicationsAndDefaults_ShouldOutputInCorrectFormat()
        {
            const string xml = @"
                <test_set>
                    <applications type=""array"">
                    <application>
                        <code>chrome2</code>
                    </application>
                    <application>
                        <code>ie7</code>
                    </application>
                    <application>
                        <code>ie6</code>
                    </application>
                    </applications>
                    <save_defaults>false</save_defaults>
                    <use_defaults>false</use_defaults>
                    <url>http://google.com</url>
                </test_set>";
            var pageClients = new List<TestingApplication>();
            pageClients.Add(new TestingApplication { ApplicationCode = "chrome2", ResultType = "page" });
            pageClients.Add(new TestingApplication { ApplicationCode = "ie7", ResultType = "page" });
            pageClients.Add(new TestingApplication { ApplicationCode = "ie6", ResultType = "page" });
            var request = new CreatePageTestBody(pageClients, "http://google.com");

            var cleanRequest = AssertHelper.CleanXml(request.ToString());
            var cleanXml = AssertHelper.CleanXml(xml);
            Assert.That(cleanRequest == cleanXml);
        }
    }
}
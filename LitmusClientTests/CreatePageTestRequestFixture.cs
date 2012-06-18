using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LitmusClient;
using LitmusClient.Litmus;
using NUnit.Framework;

namespace LitmusClientTests
{
    [TestFixture]
    public class CreatePageTestRequestFixture
    {
        [Test]
        public void NewTestRequest_WithUrl_ShouldOutputInCorrectFormat()
        {
            const string xml = @"<test_set>
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
            pageClients.Add(new TestingApplication(){ApplicationCode = "chrome2",ResultType = "page"});
            pageClients.Add(new TestingApplication() {ApplicationCode = "ie7", ResultType = "page"});
            pageClients.Add(new TestingApplication() {ApplicationCode = "ie6", ResultType = "page"});
            var request = new CreatePageTestRequest(pageClients, "http://google.com");

            var cleanSpacing = new Regex(@"\s+", RegexOptions.None);
            var cleanRequest = cleanSpacing.Replace(request.ToString(), "");
            var cleanXml = cleanSpacing.Replace(xml, "");
            Console.WriteLine(cleanRequest);
            Console.WriteLine(cleanXml);
            Assert.That(cleanRequest == cleanXml);
        }
    }
}
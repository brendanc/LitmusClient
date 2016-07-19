using LitmusClient;
using LitmusClient.Litmus;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LitmusClientTests
{
    [TestFixture]
    public class CreateEmailTestRequestFixture
    {
        [Test]
        public void NewTestRequest_WithOnlyTestingApplications_ShouldOutputInCorrectFormat()
        {
            const string xml = @"<test_set>
                                      <applications type=""array"">
                                        <application>
                                          <code>hotmail</code>
                                        </application>
                                        <application>
                                          <code>gmailnew</code>
                                        </application>
                                        <application>
                                          <code>notes8</code>
                                        </application>
                                      </applications>
                                      <save_defaults>false</save_defaults>
                                      <use_defaults>false</use_defaults>
                                    </test_set>";
            var emailClients = new List<TestingApplication>();
            emailClients.Add(new TestingApplication() { ApplicationCode = "hotmail", ResultType = "email" });
            emailClients.Add(new TestingApplication() { ApplicationCode = "gmailnew", ResultType = "email" });
            emailClients.Add(new TestingApplication() { ApplicationCode = "notes8", ResultType = "email" });
            var request = new CreateEmailTestRequest(emailClients);

            var cleanSpacing = new Regex(@"\s+", RegexOptions.None);
            var cleanRequest = cleanSpacing.Replace(request.ToString(), "");
            var cleanXml = cleanSpacing.Replace(xml, "");
            Assert.That(cleanRequest == cleanXml);
        }

        [Test]
        public void NewTestRequest_WithSubjectAndBody_ShouldOutputInCorrectFormat()
        {
            const string xml = @"<test_set>
                                      <applications type=""array"">
                                        <application>
                                          <code>hotmail</code>
                                        </application>
                                        <application>
                                          <code>gmailnew</code>
                                        </application>
                                        <application>
                                          <code>notes8</code>
                                        </application>
                                      </applications>
                                      <save_defaults>false</save_defaults>
                                      <use_defaults>false</use_defaults>
                                      <email_source>
                                         <body><![CDATA[<html><body><p>Here is an email body!</p></body></html>]]></body>
                                         <subject>My test email to Litmus</subject>
                                      </email_source>
                                    </test_set>";
            var emailClients = new List<TestingApplication>();
            emailClients.Add(new TestingApplication() { ApplicationCode = "hotmail", ResultType = "email" });
            emailClients.Add(new TestingApplication() { ApplicationCode = "gmailnew", ResultType = "email" });
            emailClients.Add(new TestingApplication() { ApplicationCode = "notes8", ResultType = "email" });
            var request = new CreateEmailTestRequest(emailClients, "My test email to Litmus", "<html><body><p>Here is an email body!</p></body></html>");

            var cleanSpacing = new Regex(@"\s+", RegexOptions.None);
            var cleanRequest = cleanSpacing.Replace(request.ToString(), "");
            var cleanXml = cleanSpacing.Replace(xml, "");
            Console.WriteLine(cleanRequest);
            Console.WriteLine(cleanXml);
            Assert.That(cleanRequest == cleanXml);
        }
    }
}
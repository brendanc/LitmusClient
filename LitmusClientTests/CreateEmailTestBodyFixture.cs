using LitmusClient.Entities;
using LitmusClient.RequestBodies;
using NUnit.Framework;
using System.Collections.Generic;

namespace LitmusClientTests
{
    [TestFixture]
    public class CreateEmailTestBodyFixture
    {
        [Test]
        public void CreateEmailTestBody_WithOnlyTestingApplications_ShouldOutputInCorrectFormat()
        {
            const string xml = @"
                <test_set>
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
            emailClients.Add(new TestingApplication { ApplicationCode = "hotmail", ResultType = "email" });
            emailClients.Add(new TestingApplication { ApplicationCode = "gmailnew", ResultType = "email" });
            emailClients.Add(new TestingApplication { ApplicationCode = "notes8", ResultType = "email" });
            var request = new CreateEmailTestBody(emailClients);

            var cleanRequest = AssertHelper.CleanXml(request.ToString());
            var cleanXml = AssertHelper.CleanXml(xml);
            Assert.AreEqual(cleanXml, cleanRequest);
        }

        [Test]
        public void CreateEmailTestBody_WithSubjectAndBody_ShouldOutputInCorrectFormat()
        {
            const string xml = @"
                <test_set>
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
            emailClients.Add(new TestingApplication { ApplicationCode = "hotmail", ResultType = "email" });
            emailClients.Add(new TestingApplication { ApplicationCode = "gmailnew", ResultType = "email" });
            emailClients.Add(new TestingApplication { ApplicationCode = "notes8", ResultType = "email" });
            var request = new CreateEmailTestBody(emailClients, "My test email to Litmus", "<html><body><p>Here is an email body!</p></body></html>");

            var cleanRequest = AssertHelper.CleanXml(request.ToString());
            var cleanXml = AssertHelper.CleanXml(xml);
            Assert.AreEqual(cleanXml, cleanRequest);
        }
    }
}
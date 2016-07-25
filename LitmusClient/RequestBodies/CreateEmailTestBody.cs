using System;
using System.Collections.Generic;
using System.Xml.Linq;
using LitmusClient.Entities;

namespace LitmusClient.RequestBodies
{
    /// <summary>
    /// Generates the xml to create a Litmus email test
    /// </summary>
    public class CreateEmailTestBody : CreateBody
    {
        /// <summary>
        /// Create request for an email test.  You can pass in a subject and body and it will be uploaded to Litmus or
        /// choose to not pass it in.  In this case you will get an email address on the TestSetVersion object, you will need to send
        /// your email to that object.
        /// </summary>
        /// <param name="testingApplications"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public CreateEmailTestBody(List<TestingApplication> testingApplications, string subject = "", string body = "")
        {
            if (string.IsNullOrEmpty(subject) && !string.IsNullOrEmpty(body))
                subject = "(No subject)";
            if (!string.IsNullOrEmpty(subject) && string.IsNullOrEmpty(body))
                throw new ArgumentException("You can't pass in a subject and not pass in a body.");

            Applications = testingApplications;
            if (!string.IsNullOrEmpty(body))
            {
                ExtraElements = new List<XElement>();
                var emailSourceElement = new XElement("email_source");
                emailSourceElement.Add(new XElement("body", new XCData(body)));
                emailSourceElement.Add(new XElement("subject", subject));
                ExtraElements.Add(emailSourceElement);
            }
        }
    }
}
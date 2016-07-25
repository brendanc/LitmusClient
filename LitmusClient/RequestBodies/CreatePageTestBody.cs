using System.Collections.Generic;
using System.Xml.Linq;
using LitmusClient.Entities;

namespace LitmusClient.RequestBodies
{
    public class CreatePageTestBody
        : CreateBody
    {
        /// <summary>
        /// Create request for a page test to be sent into Litmus
        /// </summary>
        /// <param name="testingApplications"></param>
        /// <param name="url"></param>
        public CreatePageTestBody(List<TestingApplication> testingApplications, string url)
        {
            Applications = testingApplications;
            ExtraElements = new List<XElement>();
            ExtraElements.Add(new XElement("url", url));
        }
    }
}
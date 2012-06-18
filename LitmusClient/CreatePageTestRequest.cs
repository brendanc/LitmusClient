using System.Collections.Generic;
using System.Xml.Linq;
using LitmusClient.Litmus;

namespace LitmusClient
{
    public class CreatePageTestRequest : CreateRequest
    {
        /// <summary>
        /// Create request for a page test to be sent into Litmus
        /// </summary>
        /// <param name="testingApplications"></param>
        /// <param name="url"></param>
        public CreatePageTestRequest(List<TestingApplication>testingApplications,string url)
        {
            this.Applications = testingApplications;
            this.Applications = testingApplications;
            ExtraElements = new List<XElement>();
            ExtraElements.Add(new XElement("url",url));
        }
    }
}
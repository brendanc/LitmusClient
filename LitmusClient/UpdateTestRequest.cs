using System.Collections.Generic;
using System.Xml.Linq;

namespace LitmusClient
{
    public class UpdateTestRequest
        : CreateRequest
    {
        public UpdateTestRequest(bool publicSharing, string name)
        {
            this.ExtraElements = new List<XElement>();
            var testSetElement = new XElement("test_set");
            testSetElement.Add(new XElement("public_sharing", publicSharing.ToString()));
            testSetElement.Add(new XElement("name", name));
            ExtraElements.Add(testSetElement);
        }
    }
}
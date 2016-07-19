using System.Collections.Generic;
using System.Xml.Linq;

namespace LitmusClient
{
    public class CreateReportRequest
        : CreateRequest
    {
        public CreateReportRequest(string name)
        {
            ExtraElements = new List<XElement>();
            ExtraElements.Add(new XElement("name", name));
        }
    }
}
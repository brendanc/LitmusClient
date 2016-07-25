using System.Xml.Linq;

namespace LitmusClient.RequestBodies
{
    public class CreateReportBody
    {
        public readonly string _name;

        public CreateReportBody(string name)
        {
            _name = name;
        }

        public override string ToString()
        {
            var document = new XDocument();
            var reportElement = new XElement("report");
            var nameElement = new XElement("name", _name);
            reportElement.Add(nameElement);
            document.Add(reportElement);
            return document.ToString();
        }
    }
}
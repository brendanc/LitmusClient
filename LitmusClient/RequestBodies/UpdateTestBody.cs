using System.Xml.Linq;

namespace LitmusClient.RequestBodies
{
    public class UpdateTestBody
    {
        public readonly bool _publicSharing;

        public readonly string _name;

        public UpdateTestBody(bool publicSharing, string name)
        {
            _publicSharing = publicSharing;
            _name = name;
        }

        public override string ToString()
        {
            var document = new XDocument();
            var testSetElement = new XElement("test_set");
            testSetElement.Add(new XElement("public_sharing", _publicSharing));
            testSetElement.Add(new XElement("name", _name));
            document.Add(testSetElement);
            return document.ToString();
        }
    }
}
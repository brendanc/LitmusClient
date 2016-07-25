using System.Xml.Linq;

namespace LitmusClient.RequestBodies
{
    public class UpdateResultBody
    {
        public readonly string _checkState;

        public UpdateResultBody(string checkState)
        {
            _checkState = checkState;
        }

        public override string ToString()
        {
            var document = new XDocument();
            var resultElement = new XElement("result");
            resultElement.Add(new XElement("check_state", _checkState));
            document.Add(resultElement);
            return document.ToString();
        }
    }
}
using System.Collections.Generic;
using System.Security;
using System.Xml.Linq;
using LitmusClient.Litmus;

namespace LitmusClient
{
    /// <summary>
    /// Abstract base class for all create requests sent to Litmus because the format is basically the same for all.
    /// </summary>
    public abstract class CreateRequest
    {
        /// <summary>
        /// Collection of clients to run your test against.
        /// </summary>
        public List<TestingApplication> Applications { get; set; }

        /// <summary>
        /// Do you want to save these clients as your Litmus defaults?
        /// </summary>
        public bool SaveDefaults { get; set; }

        /// <summary>
        /// Do you want to ignore the passed in collection and use your Litmus defailts?
        /// </summary>
        public bool UseDefaults { get; set; }
        
        /// <summary>
        /// The XDocument that will be posted to Litmus
        /// </summary>
        protected XDocument SerializedRequest { get; set; }

        /// <summary>
        /// Inheriting classes can add elements to the POST by adding them in here.
        /// </summary>
        protected List<XElement> ExtraElements { get; set; }
       
        /// <summary>
        /// Generate the XDocument that will be posted to Litmus
        /// </summary>
        private void Serialize()
        {
            SerializedRequest = new XDocument();
            var testSetEelemnt = new XElement("test_set");
            var appsElement = new XElement("applications");
            appsElement.Add(new XAttribute("type", "array"));
            foreach (var testingApplication in Applications)
            {
                var app = new XElement("application");
                app.Add(new XElement("code", testingApplication.ApplicationCode));
                appsElement.Add(app);
            }
            var saveDefaultsElement = new XElement("save_defaults", SaveDefaults.ToString().ToLower());
            var useDefaultsEement = new XElement("use_defaults", UseDefaults.ToString().ToLower());
            testSetEelemnt.Add(appsElement);
            testSetEelemnt.Add(saveDefaultsElement);
            testSetEelemnt.Add(useDefaultsEement);

            if (ExtraElements != null && ExtraElements.Count != 0)
            {
                foreach (var extraNode in ExtraElements)
                {
                    testSetEelemnt.Add(extraNode);
                }
            }


            SerializedRequest.Add(testSetEelemnt);

         
        }

        /// <summary>
        /// To make serialization easier just ovveride ToString so we don't have to bother with casting objects
        /// in our custom serializer.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Serialize();
            return SerializedRequest.ToString();
        }
    }
}
using LitmusClient.RequestBodies;
using NUnit.Framework;

namespace LitmusClientTests
{
    [TestFixture]
    public class CreateReportTestBodyFixture
    {
        [Test]
        public void CreateReportTestBody_WithName_ShouldOutputInCorrectFormat()
        {
            const string xml = @"
                <report>
                    <name>Test name</name>
                </report>";
            var name = "Test name";
            var request = new CreateReportBody(name);

            var cleanRequest = AssertHelper.CleanXml(request.ToString());
            var cleanXml = AssertHelper.CleanXml(xml);
            Assert.AreEqual(cleanXml, cleanRequest);
        }
    }
}
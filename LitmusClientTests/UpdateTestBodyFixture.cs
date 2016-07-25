using LitmusClient.RequestBodies;
using NUnit.Framework;

namespace LitmusClientTests
{
    [TestFixture]
    public class UpdateTestBodyFixture
    {
        [Test]
        public void UpdateTestBody_WithSharingAndName_ShouldOutputInCorrectFormat()
        {
            const string xml = @"
                <test_set>
                    <public_sharing>true</public_sharing>
                    <name>Newsletter example</name>
                </test_set> ";
            var privateSharing = true;
            var name = "Newsletter example";
            var request = new UpdateTestBody(privateSharing, name);

            var cleanRequest = AssertHelper.CleanXml(request.ToString());
            var cleanXml = AssertHelper.CleanXml(xml);
            Assert.AreEqual(cleanXml, cleanRequest);
        }
    }
}
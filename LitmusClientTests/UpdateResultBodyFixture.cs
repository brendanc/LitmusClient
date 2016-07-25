using LitmusClient.Entities;
using LitmusClient.RequestBodies;
using NUnit.Framework;

namespace LitmusClientTests
{
    [TestFixture]
    public class UpdateResultBodyFixture
    {
        [Test]
        public void UpdateResultBody_WithSharingAndName_ShouldOutputInCorrectFormat()
        {
            const string xml = @"
                <result>
                    <check_state>ticked</check_state>
                </result>";
            var checkState = CheckState.Ticked;
            var request = new UpdateResultBody(checkState);

            var cleanRequest = AssertHelper.CleanXml(request.ToString());
            var cleanXml = AssertHelper.CleanXml(xml);
            Assert.AreEqual(cleanXml, cleanRequest);
        }
    }
}
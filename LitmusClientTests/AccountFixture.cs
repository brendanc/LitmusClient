using LitmusClient;
using LitmusClient.Litmus;
using NUnit.Framework;

namespace LitmusClientTests
{
    [TestFixture]
    public class AccountFixture
    {
        [Test]
        public void NewAccount_WithEmptyUser_ShouldThrow()
        {
            Assert.Throws<System.ArgumentException>(
                () => new Account("test", null, "password"));
        }

        [Test]
        public void NewAccount_WithEmptyPassowrd_ShouldThrow()
        {
            Assert.Throws<System.ArgumentException>(
                () => new Account("test", "user", null));

        }

        [Test]
        public void NewAccount_WithEmptySubdomain_ShouldThrow()
        {
            Assert.Throws<System.ArgumentException>(
                () => new Account(null, "user", "password"));

        }

        [Test]
        public void NewAccount_WithValidParams_ShouldSetProperties()
        {
            var account = new Account("test", "user", "password");
            Assert.That(account.Username,Is.EqualTo("user"));
            Assert.That(account.Password,Is.EqualTo("password"));
            Assert.That(account.Subdomain,Is.EqualTo("test"));
        }

        [Test]
        public void NewAccount_WithSubdomain_ShouldReturnFormattedUrl()
        {
            var account = new Account("test", "user", "password");
            Assert.That(account.LitmusBaseUrl,Is.EqualTo("https://test.litmus.com/"));
        }
    }
}
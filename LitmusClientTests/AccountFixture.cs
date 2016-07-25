using System;
using LitmusClient.Entities;
using NUnit.Framework;

namespace LitmusClientTests
{
    [TestFixture]
    [Category("Authorization")]
    public class AccountFixture
    {
        [Test, ExpectedException(typeof(ArgumentException))]
        public void NewAccount_WithEmptyUser_ShouldThrow()
        {
            var account = new Account("test", null, "password");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void NewAccount_WithEmptyPassword_ShouldThrow()
        {
            var account = new Account("test", "user", null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void NewAccount_WithEmptySubdomain_ShouldThrow()
        {
            var account = new Account(null, "user", "password");
        }

        [Test]
        public void NewAccount_WithValidParams_ShouldSetProperties()
        {
            var account = new Account("test", "user", "password");
            Assert.That(account.Username, Is.EqualTo("user"));
            Assert.That(account.Password, Is.EqualTo("password"));
            Assert.That(account.Subdomain, Is.EqualTo("test"));
        }

        [Test]
        public void NewAccount_WithSubdomain_ShouldReturnFormattedUrl()
        {
            var account = new Account("test", "user", "password");
            Assert.That(account.LitmusBaseUrl, Is.EqualTo("https://test.litmus.com/"));
        }
    }
}
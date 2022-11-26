using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
namespace Library.Tests;
using Library;


    [TestFixture]
    public class UserManagerTests
    {
        [SetUp]
        public void Setup()
        {   
        }
        
        [Test]
        public void CreateUserTest()
        {
            UserManager.Instance.CreateUser("Franco", "San Martin", "12345", "employer", "Uruguay", "123456789", "Franco@email.com");

            int ExpectedCount = 1;
            Assert.AreEqual(ExpectedCount, UserManager.Instance.Users.Count);

            foreach (Employer item in UserManager.Instance.Users)
            {
                string ExpectedName     = "Franco";
                string ExpectedLastName = "San Martin";
                string ExpectedID       = "12345";
                string ExpectedLocation = "Uruguay";
                string ExpectedNumber   = "123456789";
                string ExpectedEmail    = "Franco@email.com";

                Assert.AreEqual(ExpectedName, item.Name);
                Assert.AreEqual(ExpectedLastName, item.LastName);
                Assert.AreEqual(ExpectedID, item.ID);
                Assert.AreEqual(ExpectedLocation, item.Location);
                Assert.AreEqual(ExpectedNumber, item.contactNumber);
                Assert.AreEqual(ExpectedEmail, item.contactEmail);
            }
        }

        [Test]
        public void CheckCredentialsTest()
        {
            string ID = "12345";
            UserManager.Instance.CreateUser("Franco", "San Martin", ID, "employer", "Uruguay", "123456789", "Franco@email.com");
            IUser User = new Employer("Franco", "San Martin", ID, "Uruguay", "123456789", "Franco@email.com");

            bool Expected = true;

            Assert.AreEqual(Expected, UserManager.Instance.CheckCredentials(User, ID));
        }

        [Test]
        public void LoginTest()
        {
            string ID = "12345";
            UserManager.Instance.CreateUser("Franco", "San Martin", ID, "employer", "Uruguay", "123456789", "Franco@email.com");
            string ExpectedName     = "Franco";
            string ExpectedLastName = "San Martin";
            string ExpectedID       = ID;
            string ExpectedLocation = "Uruguay";
            string ExpectedNumber   = "123456789";
            string ExpectedEmail    = "Franco@email.com";

            Assert.AreEqual(ExpectedName, ((Employer)UserManager.Instance.Login(ID)).Name);
            Assert.AreEqual(ExpectedLastName, ((Employer)UserManager.Instance.Login(ID)).LastName);
            Assert.AreEqual(ExpectedID, ((Employer)UserManager.Instance.Login(ID)).ID);
            Assert.AreEqual(ExpectedLocation, ((Employer)UserManager.Instance.Login(ID)).Location);
            Assert.AreEqual(ExpectedNumber, ((Employer)UserManager.Instance.Login(ID)).contactNumber);
            Assert.AreEqual(ExpectedEmail, ((Employer)UserManager.Instance.Login(ID)).contactEmail);
        }
    }


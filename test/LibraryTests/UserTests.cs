using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
namespace Library.Tests;
using Library;


    [TestFixture]
    public class UserTests
    {
        [SetUp]
        public void Setup()
        {   
        }
        
        [Test]
        public void CreateUserTest()
        {
            UserManager.Instance.CreateUser("Franco", "San Martin", "12345", "employer", "Uruguay", "123456789", "Franco@email.com");
            List<IUser> list = new List<IUser>();
            list = UserManager.Instance.GetEmployers();
            
            int ExpectedCount = 1;
            Assert.AreEqual(ExpectedCount, list.Count);

            foreach (Employer item in list)
            {
                string ExpectedName     = "Franco";
                string ExpectedLastName = "San Martin";
                string ExpectedID       = "12345";
                string ExpectedRol      = "employer";
                string ExpectedLocation = "Uruguay";
                string ExpectedNumber   = "123456789";
                string ExpectedEmail    = "Franco@email.com";

                Assert.AreEqual(ExpectedName, item.Name);
                Assert.AreEqual(ExpectedLastName, item.LastName);
                Assert.AreEqual(ExpectedID, item.ID);
                Assert.AreEqual(ExpectedRol, item.Rol);
                Assert.AreEqual(ExpectedLocation, item.Location);
                Assert.AreEqual(ExpectedNumber, item.contactNumber);
                Assert.AreEqual(ExpectedEmail, item.contactEmail);
            }
        }
    }


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Supreme_Mobile.Controllers;
using System.Threading.Tasks;
using Supreme_Mobile.Models;

namespace SupremeTests
{
    [TestClass]
    public class SecurityTests
    {
        [TestMethod]
        public void APILoginTest()
        {
            //arrange

            //act

            //asert
        }
        [TestMethod]
        public void CreateAPIUserTest()
        {
            //arrange
            var security = new SecurityController();

            NewUsersModel tokenModel = new NewUsersModel
            {
                UserID = 1,
                UserName = "testuser",
                Password = "pass1234",
                CompanyName = "Test",
                Address = "Nairobi",
                CreatedBy = "Sys"
            };

            //act
            var login = security.CreateAPIUser(tokenModel);

            //assert
            Assert.IsNotNull(login);
        }
    }
}

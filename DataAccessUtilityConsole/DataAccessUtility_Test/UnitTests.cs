using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessUtilityConsole;
using DataAccessUtility;


namespace DataAccessUtility_Test
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void AddCustomerTest()
        {
            //Arrange
            string actual = string.Empty;
            string connectionName = "mssqltips";
            Program P = new Program();
            
            //Act
            actual = P.AddCustomer(connectionName);

            //Assert
            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void GetCustomerListTest()
        {
            //Arrange
            string actual = string.Empty;
            string connectionName = "mssqltips";
            Program P = new Program();

            //Act
            actual = P.GetCustomerList(connectionName);

            //Assert
            Assert.AreEqual(string.Empty, actual);
        }
    }
}

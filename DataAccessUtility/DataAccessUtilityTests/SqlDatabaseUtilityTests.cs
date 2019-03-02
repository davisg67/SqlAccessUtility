using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace DataAccessUtility.Tests
{
    [TestClass()]
    public class SqlDatabaseUtilityTests
    {
        [TestMethod()]
        public void GetConnectionTest()
        {
            //Arrange
            string errorString = string.Empty;
            string connectionString = "data source=LAPTOP-EC9HHRLQ;initial catalog=mssqltips;Integrated Security=True;";
            //SqlDatabaseUtility utility = new SqlDatabaseUtility();

            //Act
            //SqlConnection cn = utility.GetConnection(connectionString, ref errorString);
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            //Assert
            Assert.AreEqual(ConnectionState.Open, cn.State); 
        }

        [TestMethod()]
        public void ExecuteQueryTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExecuteCommandTest()
        {
            Assert.Fail();
        }
    }
}
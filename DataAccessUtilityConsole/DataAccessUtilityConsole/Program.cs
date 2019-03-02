using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessUtility;
using System.Data;
using System.Data.SqlClient;


namespace DataAccessUtilityConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program P = new Program();
            string result = string.Empty;
            string connectionName = "mssqltips";

            result = P.AddCustomer(connectionName);
            if (result != string.Empty)
            {
                Console.WriteLine(result);
                Console.ReadLine();
            }
            else
            {
                result = P.GetCustomerList(connectionName);
                if (result != string.Empty)
                    Console.WriteLine(result);

                Console.ReadLine(); //Hold the console window for user input (key press).
            }
        }

        
        public string AddCustomer(string connectionName)
        {
            string sqlError = string.Empty;
            SqlDatabaseUtility dbutility = new SqlDatabaseUtility();

            // add a customer
            Dictionary<string, SqlParameter> cmdParameters = new Dictionary<string, SqlParameter>();
            cmdParameters["name"] = new SqlParameter("name", "Smith");
            cmdParameters["state"] = new SqlParameter("state", "MD");

            dbutility.ExecuteCommand(connectionName, "dbo.AddCustomer", cmdParameters, ref sqlError);
            return sqlError;
        }

        public string GetCustomerList(string connectionName)
        {
            string sqlError = string.Empty;
            SqlDatabaseUtility dbutility = new SqlDatabaseUtility();
            Dictionary<string, SqlParameter> queryParameters = new Dictionary<string, SqlParameter>();

            //Passing in the connection name, stored procedure name, and the parameter collection.
            DataSet ds = dbutility.ExecuteQuery(connectionName, "dbo.GetCustomerList", queryParameters, ref sqlError);
            DataTable t = ds.Tables[0];
            foreach (DataRow r in t.Rows)
            {
                Console.WriteLine(string.Format("{0}\t{1}\t{2}",
                  r[0].ToString(),
                  r[1].ToString(),
                  r[2].ToString()));
            }

            return sqlError;
        }
    }
}

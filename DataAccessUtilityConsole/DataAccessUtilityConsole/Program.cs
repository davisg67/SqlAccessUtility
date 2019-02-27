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
    class Program
    {
        static void Main(string[] args)
        {
            string sqlError = string.Empty;
            SqlDatabaseUtility dbutility = new SqlDatabaseUtility();

            // add a customer
            Dictionary<string, SqlParameter> cmdParameters = new Dictionary<string, SqlParameter>();
            cmdParameters["name"] = new SqlParameter("name", "Smith");
            cmdParameters["state"] = new SqlParameter("state", "MD");
            dbutility.ExecuteCommand("mssqltips", "dbo.AddCustomer", cmdParameters, ref sqlError);

            if (sqlError != string.Empty)
            {
                Console.WriteLine(sqlError);
                Console.ReadLine();
            }
            else
            {
                //Get the customer list.
                Dictionary<string, SqlParameter> queryParameters = new Dictionary<string, SqlParameter>();

                //Passing in the connection name, stored procedure name, and the parameter collection.
                DataSet ds = dbutility.ExecuteQuery("mssqltips", "dbo.GetCustomerList", queryParameters, ref sqlError);
                DataTable t = ds.Tables[0];
                foreach (DataRow r in t.Rows)
                {
                    Console.WriteLine(string.Format("{0}\t{1}\t{2}",
                      r[0].ToString(),
                      r[1].ToString(),
                      r[2].ToString()));
                }

                if (sqlError != string.Empty)
                    Console.WriteLine(sqlError);

                Console.ReadLine(); //Hold the console window for user input (key press).
            }

            
        }
    }
}

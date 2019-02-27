using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessUtility
{
    public class SqlDatabaseUtility
    {
        //Opens the database connection.
        public SqlConnection GetConnection(string connectionName, ref string sqlConnError)
        {
            sqlConnError = string.Empty;
            string cnstr = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            SqlConnection cn = new SqlConnection(cnstr);

            try
            {
                cn.Open();
                return cn;
            }
            catch (SqlException ex)
            {
                sqlConnError = "GetConnection() error: " + ex.ToString();
                return cn;  
            }
            
        }

        //Executes a stored procedure that performs a query
        public DataSet ExecuteQuery(string connectionName, string storedProcName, Dictionary<string, SqlParameter> procParameters, ref string sqlQueryError)
        {
            string sqlConnError = string.Empty;
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection cn = GetConnection(connectionName, ref sqlConnError))
                {
                    sqlQueryError = sqlConnError;
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = storedProcName;
                        // assign parameters passed in to the command
                        foreach (var procParameter in procParameters)
                        {
                            cmd.Parameters.Add(procParameter.Value);
                        }


                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }
                return ds;
            }
            catch(SqlException ex)
            {
                if (sqlQueryError == string.Empty)
                {
                    sqlQueryError = "ExecuteQuery() error: " + ex.ToString();
                }
                else
                {
                    sqlQueryError += "   ExecuteQuery() error: " + ex.ToString();
                }
               
                return ds;
            }
            
        }

        //Executes a stored procedure that performs an insert, update or delete.
        public int ExecuteCommand(string connectionName, string storedProcName, Dictionary<string, SqlParameter> procParameters, ref string sqlCmdError)
        {
            int rc = -1;
            string sqlConnError = string.Empty;

            try
            {
                using (SqlConnection cn = GetConnection(connectionName, ref sqlConnError))
                {
                    sqlCmdError = sqlConnError;
                    // create a SQL command to execute the stored procedure
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = storedProcName;
                        // assign parameters passed in to the command
                        foreach (var procParameter in procParameters)
                        {
                            cmd.Parameters.Add(procParameter.Value);
                        }
                        rc = cmd.ExecuteNonQuery();
                    }
                }
                return rc;  //The number of rows inserted, update or deleted by the command.
            }
            catch(SqlException ex)
            {
                if (sqlCmdError == string.Empty)
                {
                    sqlCmdError = "ExecuteCommand() error: " + ex.ToString();
                }
                else
                {
                    sqlCmdError += "   ExecuteCommand() error: " + ex.ToString();
                }

                return rc;
            }
            
        }


    }
}

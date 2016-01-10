using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Aostar.MVP.Update.Config.Classes
{
    public class DbOperator:IDisposable
    {
        static DbConnection _dbConn;
        static MySqlConnection mySqlConn;
        static OracleConnection oracleConn;
        public DbOperator(DbConnection dbConn)
        {
            _dbConn = dbConn;
            int timeOutTimes = 0;
            switch (_dbConn.DbType)
            {
                case EDbType.MySql:
                    mySqlConn = new MySqlConnection(_dbConn.ConnectionString);
                    do
                    {
                        try
                        {
                            mySqlConn.Open();
                            timeOutTimes = 3;
                        }
                        catch (Exception e)
                        {
                            if (Regex.IsMatch(e.Message.ToUpper(), "timeout|超时"))
                            {
                                timeOutTimes++;
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                throw e;
                            }
                        }
                    } while (timeOutTimes < 3);
                    break;
                case EDbType.Oracle:
                    oracleConn = new OracleConnection(_dbConn.ConnectionString);
                    do
                    {
                        try
                        {
                            oracleConn.Open();
                            timeOutTimes = 3;
                        }
                        catch (Exception e)
                        {
                            if (Regex.IsMatch(e.Message.ToUpper(), "timeout|超时"))
                            {
                                timeOutTimes++;
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                throw e;
                            }
                        }
                    } while (timeOutTimes < 3);
                    break;
            }
        }

        public string ExecuteScalar(string sql)
        {
            string result = "";
            switch (_dbConn.DbType)
            {
                case EDbType.MySql:
                    object resultObj=new MySqlCommand(sql, mySqlConn).ExecuteScalar();
                    result = resultObj ==null?"":resultObj.ToString();
                    break;
                case EDbType.Oracle:
                    object oracleResultObj = new OracleCommand(sql, oracleConn).ExecuteScalar();
                    result = oracleResultObj == null ? "" : oracleResultObj.ToString();
                    break;
            }
            return result;
        }

        public DataSet Execute(string sql)
        {
            DataSet result = new DataSet();
            switch (_dbConn.DbType)
            {
                case EDbType.MySql:
                    MySqlCommand command = new MySqlCommand(sql, mySqlConn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(result);
                    break;
                case EDbType.Oracle:
                    //Todo
                    break;
            }
            return result;
        }


        public int ExecuteNonQuery(string sql)
        {
            int affectRows = 0;
            switch (_dbConn.DbType)
            {
                case EDbType.MySql:
                    MySqlCommand command = new MySqlCommand(sql, mySqlConn);
                    affectRows = command.ExecuteNonQuery();
                    break;
                case EDbType.Oracle:
                    //Todo
                    break;
            }
            return affectRows;
        }

        public void Dispose()
        {
            if (mySqlConn != null)
            {
                mySqlConn.Close();
                mySqlConn.Dispose();
            }

            if (oracleConn != null)
            {
                oracleConn.Close();
                oracleConn.Dispose();
            }
        }
    }
}

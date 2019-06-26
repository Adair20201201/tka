using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace TKA.Business
{
    /// <summary>
    /// 操作Access数据库类
    /// </summary>
    public class Access
    {
        private object obj = new object();
        private OleDbConnection Conn;
        private DataSet ds = new DataSet();
        /// <summary>
        /// 构造函数
        /// </summary>
        public Access()
        {
            Conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=log.mdb");
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Route">mdb所在位置</param>
        public Access(string Route)
        {
            Conn = new OleDbConnection();
            Conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Route;
        }
        public DataTable Select(string Mold)
        {
            Conn.Open();
            string s;
            if (Mold.Contains("警告"))
            {
                s = "SELECT * FROM Log WHERE Thingsmold = 'Warning'";
            }
            else if (Mold.Contains("操作"))
            {
                s = "SELECT * FROM Log WHERE Thingsmold = 'Operation'";
            }
            else
            {
                s = "SELECT * FROM Log ";
            }
            OleDbCommand command = new OleDbCommand(s, Conn);
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            da.Fill(ds);
            Conn.Close();
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public DataTable Select(string Start, string End, string Mold)
        {
            OleDbCommand command;
            Conn.Open();
            if (Mold.Contains("警告"))
            {
                command = new OleDbCommand("SELECT * FROM Log WHERE (Thingtime BETWEEN #" + Start + "# AND #" + End + "#) AND Thingsmold = 'Warning'", Conn);
            }
            else if (Mold.Contains("操作"))
            {
                command = new OleDbCommand("SELECT * FROM Log WHERE (Thingtime BETWEEN #" + Start + "# AND #" + End + "#) AND Thingsmold = 'Operation'", Conn);
            }
            else
            {
                command = new OleDbCommand("SELECT * FROM Log WHERE Thingtime BETWEEN #" + Start + "# AND #" + End + "#", Conn);
            }
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            da.Fill(ds);
            Conn.Close();
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 插入警告信息
        /// </summary>
        /// <param name="time"></param>
        /// <param name="thing"></param>
        /// <returns></returns>
        public bool InsertWarning(string time, string thing)
        {
            lock (obj)
            {

                Conn.Open();

                string s = "INSERT INTO Log (Thingsmold,Thingtime, Things) VALUES ('Warning','" + time + "','" + thing + "')";
                OleDbCommand command = new OleDbCommand(s, Conn);
                //OleDbDataAdapter da = new OleDbDataAdapter(command);
                //da.Fill(ds);
                int res = command.ExecuteNonQuery();
                Conn.Close();
                if (res > 0)
                {
                    return true;
                }
                Conn.Close();
                return true;
            }
        }
        /// <summary>
        /// 插入操作信息
        /// </summary>
        /// <param name="time"></param>
        /// <param name="thing"></param>
        /// <returns></returns>
        public bool InsertOperation(string time, string thing)
        {
            lock(obj)
            {
                Conn.Open();
                string s = "INSERT INTO Log (Thingsmold, Thingtime, Things) VALUES ('Operation','" + time + "','" + thing + "')";
                OleDbCommand command = new OleDbCommand(s, Conn);
                //OleDbDataAdapter da = new OleDbDataAdapter(command);
                //da.Fill(ds);
                int res = command.ExecuteNonQuery();
                Conn.Close();
                if (res > 0)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 删除今天之前的数据
        /// </summary>
        /// <returns></returns>
        public bool DeleteBeforeToday()
        {
            var time = DateTime.Now.ToShortDateString();
            Conn.Open();
            string s = "delete from Log where Thingtime < #" + time + "#";
            OleDbCommand command = new OleDbCommand(s, Conn);
            //OleDbDataAdapter da = new OleDbDataAdapter(command);
            //da.Fill(ds);
            int res = command.ExecuteNonQuery();
            Conn.Close();
            if (res > 0)
            {
                return true;
            }
            return false;
        }
    }
}

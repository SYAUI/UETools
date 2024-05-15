using System;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using System.Data.SQLite;
using System.Configuration;
using System.Data.Common;


namespace DBUtility
{
    public class SQLiteHelper
    {
        public static string connectionString = CreateConnectionString();
        public SQLiteHelper()
        {
        }



        //数据库连接
        private static SQLiteConnection _DBc = null;

        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string CreateConnectionString()
        {

            string dbName = ConfigurationManager.AppSettings["SQLiteDB"];
            string sqlLitePath = "data source=" + System.Environment.CurrentDirectory + "\\" + dbName + ";version=3;";
            return sqlLitePath;
        }

        /// <summary>
        /// 连接对象
        /// </summary>
        public static SQLiteConnection DBc
        {
            get
            {
                if (_DBc == null) _DBc = new SQLiteConnection(connectionString);
                return SQLiteHelper._DBc;
            }
            set { SQLiteHelper._DBc = value; }
        }

        /// <summary>
        /// 创建命令
        /// </summary>
        public static SQLiteCommand CreateCommand(SQLiteConnection connection, string commandText, params SQLiteParameter[] commandParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand(commandText, DBc);
            if (commandParameters.Length > 0)
            {
                foreach (SQLiteParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
            return cmd;
        }
        /// <summary>
        /// 创建参数
        /// </summary>
        public static SQLiteParameter CreateParameter(string parameterName, System.Data.DbType parameterType, object parameterValue)
        {
            SQLiteParameter parameter = new SQLiteParameter();
            parameter.DbType = parameterType;
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            return parameter;
        }

        /// <summary>
        /// 查询value是否存在
        /// </summary>
        public static bool IsDataExists(string tableName, string columnName, string value)
        {
            bool result = false;
            SQLiteCommand cmd = DBc.CreateCommand();
            cmd.CommandText = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = @value";
            cmd.Parameters.AddWithValue("@value", value);
            if (DBc.State == ConnectionState.Closed)
                DBc.Open();
            if ((long)cmd.ExecuteScalar() > 0)
            {
                result = true;
            }
            DBc.Clone();
            return result;
        }


        /// <summary>
        /// 执行ExecuteNonQuery方法
        /// </summary>
        public static int ExecuteNonQuery(string commandText, params object[] paramList)
        {

            SQLiteCommand cmd = DBc.CreateCommand();
            cmd.CommandText = commandText;
            if (paramList != null)
            {
                foreach (SQLiteParameter parm in paramList)
                    cmd.Parameters.Add(parm);
            }
            if (DBc.State == ConnectionState.Closed)
                DBc.Open();
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            DBc.Close();

            return result;
        }


    } 
}

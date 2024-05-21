

using System.Configuration;

namespace UnrealEngineTools
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            CheckDB();
            ApplicationConfiguration.Initialize();
            Application.Run(new Form_Manager());
        }

        static void CheckDB()
        {
            //��ֹ���ݿⲻ����
            string? dbName = ConfigurationManager.AppSettings["SQLiteDB"];
            if(dbName == null)
            {
                dbName = "Mdata.db";
            }
            if (!File.Exists(dbName))
            {
                DBUtility.SQLiteHelper.CreateNewTable("blueprintnode", "name TEXT,src TEXT,desc TEXT,image BLOB");
                return;
            }
        }

    }
}
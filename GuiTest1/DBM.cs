using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace GuiTest1
{
    class DBM
    {
        private static SQLiteConnection dbConn;
        private static SQLiteCommand command;
        private static SQLiteDataReader reader;
        private static string sql;

        static void Main(string[] args)
        {
            /// Instructions
            /// SQLInitialise() - Must be run first
            /// SQLGetTableData(string tableName) - Run to retrieve list of table data
            /// SQLWriteToTable(string tableName, List<string> data) - Run to write data to table
        }
        public static void SQLInitialise()
        {
            dbConn = new SQLiteConnection("Data Source=database.db;Version=3;"); /// Assign it to a db connnection
            dbConn.Open(); /// Open the database
            sql = "";
            command = new SQLiteCommand(sql, dbConn); /// Use sql command on database
            reader = command.ExecuteReader(); /// Setup reader
        }

        public static List<List<string>> SQLGetTableData(string tableName)
        {
            string sql = "SELECT * FROM " + tableName; /// Setup sql command
            command = new SQLiteCommand(sql, dbConn); /// assign command to var
            reader = command.ExecuteReader(); /// Setup reader
            List<List<string>> data = new List<List<string>>(); /// Create list of string lists
            switch (tableName) /// Depending on table name
            {
                case "servers":
                    while (reader.Read())
                        data.Add(new List<string> { reader["id"].ToString(), reader["name"].ToString(), reader["ip"].ToString(), reader["port"].ToString() });
                    break;
            }
            reader.Close();
            return data;
        }

        public static List<List<object>> SQLRaw(string rawCom, string tableName)
        {
            string sql = rawCom; /// Setup sql command
            command = new SQLiteCommand(sql, dbConn); /// assign command to var
            reader = command.ExecuteReader(); /// Setup reader
            List<List<object>> data = new List<List<object>>(); /// Create list of string lists
            switch (tableName) /// Depending on table name
            {
                case "servers":
                    while (reader.Read())
                        data.Add(new List<object> { reader["id"].ToString(), reader["name"].ToString(), reader["ip"].ToString(), reader["port"].ToString() });
                    break;
            }       
            reader.Close();
            return data;
        }
        public static void SQLWriteToTable(List<string> data, string tableName)
        {
            switch (tableName) /// Depending on table name
            {
                case "servers":
                    string serverName = data[0];
                    string serverIP = data[1];
                    int serverPort = Convert.ToInt16(data[2]);

                    sql = "INSERT INTO servers (name, ip, port) values ('" + serverName + "','" + serverIP + "','" + serverPort + "')";
                    command = new SQLiteCommand(sql, dbConn); /// Setup command
                    command.ExecuteNonQuery(); /// Insert new entry into database

                    break;
            }
        }

        public static void SQLUpdateTable(string tablename, string conditional, string identifier, string column, string newval)
        {
            switch (conditional)
            {
                case "id":
                    {
                        sql = "UPDATE " + tablename + " SET " + column + " = '" + newval + "' WHERE id = " + Convert.ToInt16(identifier);
                        command = new SQLiteCommand(sql, dbConn); /// Setup command
                        command.ExecuteNonQuery(); /// Insert new entry into database
                        break;
                    }
                default:
                    {
                        sql = "UPDATE " + tablename + " SET " + column + " = '" + newval + "' WHERE " + conditional + " = " + identifier;
                        command = new SQLiteCommand(sql, dbConn); /// Setup command
                        command.ExecuteNonQuery(); /// Insert new entry into database
                        break;
                    }
            }
        }
    }
}

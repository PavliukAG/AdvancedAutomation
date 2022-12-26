using System;
using System.Data.SqlClient;

namespace Singleton 
{
    public class SingletonDb
    {
        private static readonly string connectionString = "UserId=admin;Password=mypass;host=localhost;database=Test";
        private static SqlConnection _connection;
        private SingletonDb() {}
        public static SqlConnection getDbConnection()
        {
            try
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(connectionString);
                    _connection.Open();
                    Console.WriteLine("Connection is created.");
                }
            }catch(Exception e)
            {
               Console.WriteLine(e.Message);
            }
            return _connection;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = SingletonDb.getDbConnection();
        }
    }
}
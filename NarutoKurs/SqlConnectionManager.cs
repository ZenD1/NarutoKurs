using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarutoKurs
{
    internal class SqlConnectionManager
    {
        // Строка подключения к вашей базе данных
        public const string ConnectionString = @"Data Source=LAPTOP-GI7CLN64\SQLSTANDART;Initial Catalog=Kursnaruto;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public SqlConnection GetSqlConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Соединение успешно установлено");
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при установлении соединения: " + ex.Message);
                return null;
            }
        }

        // Метод для закрытия подключения
        public void CloseSqlConnection(SqlConnection connection)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                Console.WriteLine("Соединение успешно закрыто");
            }
        }
    }
}
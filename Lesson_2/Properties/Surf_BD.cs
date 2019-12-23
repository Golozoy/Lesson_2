using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;
using System.Diagnostics;
using System.Threading;

namespace Lesson_2.Properties
{
    class Surf_BD
    {
        public static List<string> _Logs = new List<string>();
        public static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=usersdb;Integrated Security=True";
        // string sqlExpression = "INSERT INTO Users (name, possword) VALUES ('User', 123)";
        public static string sqlExpression = "SELECT * FROM Users";
        public static void Work_with_BD( string name_, string possword_)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                

                // SqlCommand command = new SqlCommand(sqlExpression, connection);
                // int number = command.ExecuteNonQuery();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов
                    //Console.WriteLine("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));

                    while (reader.Read()) // построчно считываем данные
                    {

                        object name = reader.GetValue(1);
                        if(!name.ToString().Contains(name_) &&!name_.Contains(name.ToString()) )
                        {
                            Console.WriteLine("Неправельно введен никнейм, в следующий раз повезет");
                            Thread.Sleep(1500);
                            Process.Start(Assembly.GetExecutingAssembly().Location);
                            Environment.Exit(0);
                        }
                        object possword = reader.GetValue(2);
                        if (!possword.ToString().Contains(possword_) && !possword_.Contains(possword.ToString()))
                        {
                            Console.WriteLine("Неправельно введен пароль, в следующий раз повезет");
                            Thread.Sleep(1500);
                            Process.Start(Assembly.GetExecutingAssembly().Location);
                            Environment.Exit(0);
                        }
                        Console.WriteLine("Подключение открыто");

                        // Вывод информации о подключении
                        Console.WriteLine("Свойства подключения:");
                        Console.WriteLine("\tСтрока подключения: {0}", connection.ConnectionString);
                        Console.WriteLine("\tБаза данных: {0}", connection.Database);
                        Console.WriteLine("\tСервер: {0}", connection.DataSource);
                        Console.WriteLine("\tВерсия сервера: {0}", connection.ServerVersion);
                        Console.WriteLine("\tСостояние: {0}", connection.State);
                        Console.WriteLine("\tWorkstationld: {0}", connection.WorkstationId);
                        Console.WriteLine("\n------------------------------------------------------\n");

                        Console.WriteLine("{0} \t{1}", name, possword);
                    }
                }
                reader.Close();
            }
            //Console.WriteLine("Подключение закрыто...");
        }
        public static void Cyka_logs(string user_name)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Logs;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                for (int i = 0; i < _Logs.Count(); i++)
                {
                    string add_log = $"INSERT INTO Logs (name, time_log) VALUES ('{user_name}', '{_Logs[i]}')";
                        SqlCommand com = new SqlCommand(add_log, connection);
                        com.ExecuteNonQuery();
                }                                        
                _Logs.Clear();
            }
           // Console.WriteLine("\nПодключение закрыто...");
            Thread.Sleep(1500);
        }


    }

}


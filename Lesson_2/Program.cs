using System;
using System.Text;
using System.Threading;

namespace Lesson_2.Properties
{
    class Program
    {
        public static string us_name;
        static void Main()
        {
                Console.ForegroundColor = ConsoleColor.Green;
            string stroka = " Добро пожаловать в программу \n Для начала необходимо представиться\n Введи свой никнейм: ";
            foreach (var el in stroka)
            {
                Console.Write(el);
                Thread.Sleep(80);
            }
            //Console.WriteLine(" Добро подаловать в программу \n Для начала необходимо авторизироваться\n Введи свой никнейм: ");//никнейм User
            string user_name = Console.ReadLine();
            us_name = user_name;
            stroka = " Введи свой пароль:";
            foreach (var el in stroka)
            {
                Console.Write(el);
                Thread.Sleep(80);
            }
           // Console.WriteLine(" Введи свой пароль:"); // possword:123
            string user_possword = Console.ReadLine();
            

            Surf_BD.Work_with_BD(user_name, Hach.Hashcode(user_possword));
            
            Console.WriteLine("Если вы хотите ввести адрес файла нажмите на (Tab), в противном случии будет использован адресс по умолчанию");
            ConsoleKeyInfo input;
            input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.Tab)
            {
                Console.WriteLine("Укажите путь к файлу:");
                string file = @"";
                file += Console.ReadLine();
                Lesson_2.Properties.Parsing pr = new Lesson_2.Properties.Parsing(file);
            }
            else
            {
                string s = @"C:\Users\lupac\Desktop\Conf";
                Lesson_2.Properties.Parsing p = new Lesson_2.Properties.Parsing(s);
            }
            
        }

    }
    class Hach
    {
        public static string Hashcode(string stroka)
        {
            string hasVal = null;
            System.Security.Cryptography.SHA512 sHA512 = System.Security.Cryptography.SHA512.Create();
            byte[] date = sHA512.ComputeHash(Encoding.UTF8.GetBytes(stroka));
            for (int i = 0; i < date.Length; i++)
            {
                hasVal += (date[i].ToString("x2")); 
            }

           // Console.WriteLine(hasVal);
            return hasVal;
        }
    }
 
}
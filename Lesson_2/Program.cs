using System;
using System.IO;
using System.Collections.Generic;

namespace вторая_лаба2
{
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Если вы хотите ввести адрес файла нажмите на (Tab), в противном случии будет использован адресс по умолчанию");
            ConsoleKeyInfo input;
            input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.Tab)
            {
                Console.WriteLine("Укажите путь к файлу:");
                string file = @"";
                file += Console.ReadLine();
                Parsing pr = new Parsing(file);
            }
            else
            {
                string s = @"C:\Users\lupac\Desktop\Conf";
                Parsing p = new Parsing(s);
            }
        }

    }

    class Parsing
    {
        private string adress;
        public Parsing(string adress)
        {
            this.adress = adress;
            Pars(adress);
        }
        public static void Pars(string s)
        {
            ConsoleKeyInfo input;
            int _1;
            string faind_objeckt, com;
            List<string> temp = new List<string>();
            List<string> temp_2 = new List<string>();
            List<string> stroka = new List<string>();
            Correct(ref stroka, s);
            foreach (string leg in File.ReadAllLines(s))
                stroka.Add(leg);
            do
            {
                Print(stroka);
                byte flag = 0;
                bool fl = true;
                bool net_v_comente = false;
                Console.WriteLine("\nВведите прибор который хотите найти:");
                do
                {
                    faind_objeckt = Console.ReadLine();
                } while (!Corect_Input(ref faind_objeckt));
                for (int i = 0; i < stroka.Count; i++)
                {
                    if (stroka[i].Contains(faind_objeckt))
                    {
                        if (stroka[i].IndexOf("#") == 0)
                            continue;
                        if (stroka[i].IndexOf(";") == 0)
                            continue;
                        com = Coment(ref stroka, ref temp, ref temp_2, ref i, ref fl);
                        if (!stroka[i].Contains(";") && !stroka[i].Contains("#") && fl) // убираем лишнии пробелы
                        {
                            temp.AddRange(stroka[i].Split(' '));
                            foreach (var item in temp)
                            {
                                if (item != "")
                                    temp_2.Add(item);
                            }
                        }
                        if (com != "")
                            temp_2.Add(com);
                        if (temp_2.Count == 1)  // это Мишен ГЕНИАЛЬНЫЙ КОСТЫЛЬ самая сложнаю часть всего кода и самая главная, от сюда
                            _1 = 0;
                        else
                            _1 = 1;    // и до сюда
                        for (int j = 0; j < temp_2.Count; j++)
                        {
                            if (temp_2[j].IndexOf("#") == 0)
                                _1 = j - 1;
                            if (temp_2[j].IndexOf(";") == 0)
                                _1 = j - 1;
                        }
                        if (faind_objeckt.Length == stroka[i].Length) // выполняеется если только имя параметра как (Пр. client)
                        {
                            Console.WriteLine("\nВведите новое значение параметра");
                            do
                            {
                                temp_2[_1] = Console.ReadLine();
                            } while (!Corect_Input(ref temp_2, ref _1));
                            if (_1 == 0)
                                stroka[i] = temp_2[_1] + " " + com;
                            else
                                stroka[i] = temp_2[0] + " " + temp_2[_1] + com;
                            Console.WriteLine($"\nЭто новое вырожение:\n\t{stroka[i]}\n");
                            net_v_comente = true;
                            temp.Clear();
                            temp_2.Clear();
                            File.WriteAllLines(s, stroka);
                            flag = 1;
                            continue;
                        }
                        if (faind_objeckt.Length != stroka[i].IndexOf(' '))// возращаем комент
                        {
                            if (!fl && com != "")
                            {
                                stroka[i] += com;
                            }
                            continue;
                        }
                        Console.WriteLine("\nВведите новое значение параметра");
                        do
                        {
                            temp_2[_1] = Console.ReadLine();
                        } while (!Corect_Input(ref temp_2, ref _1));
                            if (_1 == 0)
                                stroka[i] = temp_2[0] + " " + com;
                            else
                                stroka[i] = stroka[i].Substring(0, faind_objeckt.Length + 1) + temp_2[_1] + " " + com;
                        Console.WriteLine($"\nЭто новое вырожение:\n\t{stroka[i]}");
                        net_v_comente = true;
                        temp.Clear();
                        temp_2.Clear();
                        File.WriteAllLines(s, stroka);
                        flag = 1;
                        if (net_v_comente) continue;
                        
                    }
                }
                if (flag == 0)
                    Console.WriteLine("\nЭлемент не найден\n");
                Console.WriteLine("Для выхода жмите на (Esc), любая другая клавиша повторяет круг");
                input = Console.ReadKey(true);
            }
            while (!(input.Key == ConsoleKey.Escape));
        }
        static string Coment(ref List<string> stroka, ref List<string> temp, ref List<string> temp_2, ref int i, ref bool fl)
        {
            string com;
            if (stroka[i].IndexOf("#") > 1)
            {
                com = stroka[i].Substring(stroka[i].IndexOf("#"));
                stroka[i] = stroka[i].Substring(0, stroka[i].IndexOf("#"));
                temp.AddRange(stroka[i].Split(' '));
                foreach (var item in temp)
                {
                    if (item != "")
                        temp_2.Add(item);
                }
                fl = false;
                return com;
            }
            if (stroka[i].IndexOf(";") > 1)
            {
                com = stroka[i].Substring(stroka[i].IndexOf(";"));
                stroka[i] = stroka[i].Substring(0, stroka[i].IndexOf(";"));
                temp.AddRange(stroka[i].Split(' '));
                foreach (var item in temp)
                {
                    if (item != "")
                        temp_2.Add(item);
                }
                fl = false;
                return com;
            }
            return "";
        }

        static bool Corect_Input(ref string ch)
        {
            if (ch.Length == 0 && ch.StartsWith(""))
            {
                Console.WriteLine("Введено пустое значение, чего быть не может, повторите ввод");
                return false;
            }
            if (ch.Contains(" ") || ch.Contains(";") || ch.Contains("#"))
            {
                Console.WriteLine("Имя параметра не должно содержать пробелов и знаков комментария (; или #), повторите ввод");
                return false;
            }
            else
                return true;
        }
        static bool Corect_Input(ref List<string> ch, ref int i)
        {
            if (ch[i].Length == 0 && ch[i].StartsWith(""))
            {
                Console.WriteLine("Введено пустое значение, чего быть не может, повторите ввод");
                return false;
            }
            if (ch[i].Contains(";") || ch[i].Contains("#"))
            {
                Console.WriteLine("Имя параметра не должно содержать знаков комментария (; или #), повторите ввод");
                return false;
            }
            else
                return true;
        }

        static void Print(List<string> Po)
        {
            Console.WriteLine("\nЭто список всех приборов:\n");
            foreach (string el in Po)
            {
                if (el.IndexOf("#") == 0) continue;
                if (el.IndexOf("#") > 1)
                    el.Substring(0, el.IndexOf("#"));
                if (el.IndexOf(";") > 1)
                    el.Substring(0, el.IndexOf(";"));
                if (el.IndexOf(";") == 0) continue;
                if (el.Length == 0) continue;
                Console.WriteLine($"\t{el}");
            }
            Console.WriteLine("--------------------------------------------");
        }
        static void Correct(ref List<string> stroka, string s)
        {
            string[] wo = new string[3];
            string[] word = new string[3];
            foreach (string leg in File.ReadAllLines(s))
                stroka.Add(leg);
            for (int i = 0; i < stroka.Count; i++)
            {
                if (stroka[i].IndexOf('#') != stroka[i].IndexOf(' ') + 1 && stroka[i].IndexOf('#') != -1)
                    stroka[i] = stroka[i].Insert(stroka[i].IndexOf('#'), " ");
                if (stroka[i].IndexOf('\t') != -1)
                {
                    word = stroka[i].Split('\t');
                    stroka[i] = null;
                    for (int f = 0; f < word.Length; f++)
                        stroka[i] += word[f] + " ";
                }
                if (stroka[i].IndexOf(' ') == 0)
                {
                    wo = stroka[i].Split(' ');
                    stroka[i] = null;
                    for (int f = 0; f < wo.Length; f++)
                    {
                        if (wo[f] == "")
                            continue;
                        stroka[i] += wo[f] + " ";
                    }
                }
            }
            File.WriteAllLines(s, stroka);
            stroka.Clear();
        }

    }
}
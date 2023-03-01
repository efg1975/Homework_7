using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace Homework_7
{
    public class Repository
    {
        public Worker[] workers;
        private string path = @"Personal.csv";
        int index = 0;

        public Repository()
        {
            this.path = path;
            this.index = 0;
            this.workers = new Worker[100];
        }

        public Worker[] GetAllWorkers() //чтение данных из файла, возврат массива считанных экземпляров
        {
            if (File.Exists(this.path))
            {
                using (StreamReader line = new StreamReader(this.path, Encoding.Unicode))
                {
                    string str;

                    while ((str = line.ReadLine()) != null)
                    {
                        string[] data = str.Split('#');
                        AddWorker(new Worker(int.Parse(data[0]), Convert.ToDateTime(data[1]), data[2], int.Parse(data[3]), int.Parse(data[4]), data[5], data[6]));
                    }
                }
            }
            else
            {
                Console.WriteLine("Файл не существует. Создайте его.");
            }
            return workers;
        }

        public void AllWorkersPrint() //печать всего массива
        {
            if (File.Exists(this.path))
            {
                Console.WriteLine($"{"ID",2}{"Дата и время",25}{"ФИО сотрудника",40}{"Возраст",10}" +
                                      $"{"Рост",10}{"Дата рождения",15}{"Место рождения",30}");

                for (int i = 0; i < index; i++)
                {
                    Console.WriteLine(this.workers[i].Print());
                }
            }
            else { }
        }

        public Worker GetWorkerById()//выбор сотрудника с запрашиваемым ID
        {
            GetAllWorkers();
            if (File.Exists(this.path))
            {
                Console.WriteLine("Введите ID сотрудника");
                int i = int.Parse(Console.ReadLine());
                if (i > 0 && i <= this.index)
                {
                    Console.WriteLine(this.workers[i - 1].Print());
                }
                else
                {
                    Console.WriteLine($"Сотрудника с ID {i} не существует\n" +
                        $"Выберите ID в пределах от 1 до {this.index}");
                }
                return this.workers[i];
            }
            else
            {
                return workers[0];
            }
        }

        public bool DeleteWorker()//удаление сотрудника по ID
        {
            if (File.Exists(this.path))
            {
                StreamReader line = new StreamReader(this.path, Encoding.Unicode);
                List<string> lst = new List<string>(0);

                Console.Write("Введите ID удаляемого сотрудника\n");
                int id = int.Parse(Console.ReadLine());

                    string str;
                    while ((str = line.ReadLine()) != null)
                    {
                        lst.Add(str);
                    }
                    line.Close();

                if(id > 0 && id <= lst.Count)
                {
                    lst.RemoveAt(id - 1);
                    File.Delete(this.path);

                    using (StreamWriter sw = new StreamWriter(path, true, Encoding.Unicode))
                    {
                        for (int i = 0; i < lst.Count; i++)
                        {
                            sw.WriteLine(lst[i], Encoding.Unicode);
                        }
                    }
                    Console.WriteLine($"Сотрудник с ID = {id} успешно удален");
                }
                else
                {
                    Console.WriteLine($"Сотрудника с ID {id} не существует\n" +
                        $"Выберите ID в пределах от 1 до {lst.Count}");
                }     
            }
            else
            {
                Console.WriteLine("Файл не существует. Создайте его");
            }
            return true;
        }
        
        public void AddWorker(Worker worker) //присваивается worker уникальный ID
        {
            this.workers[index] = worker;
            this.index++;
        }

        public void AddNewWorker()//добавление нового worker в файл
        {
            using (StreamWriter line = new StreamWriter(path, true, Encoding.Unicode))
            {
                
                char key = 'д';
                do
                {
                    string record = string.Empty;
                    Console.WriteLine("Введите порядковый номер записи");
                    record += $"{Console.ReadLine()}#";

                    string now = DateTime.Now.ToShortTimeString();
                    Console.WriteLine($"Время записи {DateTime.Now}");
                    record += $"{DateTime.Now}#";

                    Console.WriteLine("Введите ФИО сотрудника");
                    record += $"{Console.ReadLine()}#";

                    Console.WriteLine("Введите возраст сотрудника");
                    record += $"{Console.ReadLine()}#";

                    Console.WriteLine("Введите рост сотрудника");
                    record += $"{Console.ReadLine()}#";

                    Console.WriteLine("Введите дату рождения");
                    record += $"{Console.ReadLine()}#";

                    Console.WriteLine("Введите место рождения");
                    record += $"{Console.ReadLine()}#";

                    line.WriteLine(record);
                    Console.Write("Продожить н/д\n");
                    key = Console.ReadKey(true).KeyChar;
                }
                while (char.ToLower(key) == 'д');
            }
        }

        public Worker[] GetWorkersBetweenTwoDates()//фильтрация по дате записи
        {
            GetAllWorkers();

            if (File.Exists(this.path))
            {
                Console.WriteLine("Введите начальную дату");
                DateTime firstDate = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Введите конечную дату");
                DateTime secondDate = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine($"{"ID",2}{"Дата и время",25}{"ФИО сотрудника",40}{"Возраст",10}" +
                                          $"{"Рост",10}{"Дата рождения",15}{"Место рождения",30}");

                var selectedWorker = from p in workers
                                     where p.Date >= firstDate.AddHours(-1)
                                     where p.Date <= secondDate.AddDays(1)
                                     select p;

                foreach (var i in selectedWorker)
                {
                    Console.WriteLine(i.Print());
                }
                return workers;
            }
            else
            {
                return workers;
            }
        }

        public Worker[] GetWorkersBetweenAge()//фильтрация по возрасту
        {
            GetAllWorkers();

            if (File.Exists(this.path))
            {
            Console.WriteLine("Введите начальный возраст сотрудника");
            int age1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите конечный возраст сотрудника");
            int age2 = int.Parse(Console.ReadLine());

            Console.WriteLine($"{"ID",2}{"Дата и время",25}{"ФИО сотрудника",40}{"Возраст",10}" +
                                      $"{"Рост",10}{"Дата рождения",15}{"Место рождения",30}");

            var selectedWorker = from p in workers
                                 where p.Age >= age1
                                 where p.Age <= age2
                                 select p;

            foreach (var i in selectedWorker)
            {
                Console.WriteLine(i.Print());
            }
            return workers;
            }
            else 
            {
                return workers;
            }
        }
    }
}









        

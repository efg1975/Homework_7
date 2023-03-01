using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Homework_7
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository rep = new Repository();
            
            Console.WriteLine("Выберите операцию:\n" +
                "1 - ввод нового сотрудника\n" +
                "2 - просмотр списка сотрудников\n" +
                "3 - просмотр сотрудника по ID\n" +
                "4 - фильтрация по дате записи\n" +
                "5 - фильтрация по возрасту\n" +
                "6 - удаление сотрудника по ID\n" +
                "0 - выход");
            int num = int.Parse(Console.ReadLine());
            switch (num)
            {
                case 0: break;
                case 1: rep.AddNewWorker(); break;
                case 2: rep.GetAllWorkers(); rep.AllWorkersPrint(); break;
                case 3: rep.GetWorkerById(); break;
                case 4: rep.GetWorkersBetweenTwoDates(); break;
                case 5: rep.GetWorkersBetweenAge(); break;
                case 6: rep.DeleteWorker(); break;
            }
        }
        
    }
}
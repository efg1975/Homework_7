using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Homework_7
{
    public struct Worker
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string FIO { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public string DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }

        public Worker (int ID, DateTime date, string FIO, int age, int height, string dateOfBirth, string placeOfBirth)
        {
            this.Id = ID;
            this.Date = date;
            this.FIO = FIO;
            this.Age = age;
            this.Height = height;
            this.DateOfBirth = dateOfBirth;
            this.PlaceOfBirth = placeOfBirth;
        }
        public string Print()
        {
            return $"{this.Id,2} {this.Date,25} {this.FIO,40} {this.Age,10} {this.Height,10} {this.DateOfBirth, 15} {this.PlaceOfBirth, 30}";
        }

    }
}

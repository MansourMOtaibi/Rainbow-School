using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainbowSchool
{
    class Program
    {
        class Teacher
        {
            Teacher(int _id , string _name , int _class, string _section)
            {
                this.ID = _id;
                this.Name = _name;
                this.Class = _class;
                this.Section = _section;
            }
            public int ID { get; set; }
            public string Name { get; set; }
            public int Class { get; set; }
            public string Section { get; set; }
        }
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}

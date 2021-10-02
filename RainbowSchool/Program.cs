using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainbowSchool
{
    class Program
    {
        class Teacher
        {
            public Teacher(int _id , string _name , int _class, string _section)
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

            public string ToOneLine()
            {
                return $"{ID},{Name},{Class},{Section}";
            }
        }

        class DataWarehouse
        {
            static string TextFilePath = "DataWarehouse.txt";

            public DataWarehouse()
            {

                // Check : Create file if not exist
                if (!File.Exists(TextFilePath))
                {
                    var txtFile = File.Create(TextFilePath);
                    txtFile.Close();
                }

                GetTeachersDataFromTXT();
            }

            // Display a teacher's data
            private void DisplayATeacher(Teacher _teacher)
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine($"- ID      : {_teacher.ID}");
                Console.WriteLine($"- Name    : {_teacher.Name}");
                Console.WriteLine($"- Class   : {_teacher.Class}");
                Console.WriteLine($"- Section : {_teacher.Section}");
                Console.WriteLine("-------------------------------------------");
            }

            // Display all teachers' data
            public void DisplayTeachers()
            {
                if (this.Teachers != null && this.Teachers.Count() > 0)
                {
                    foreach (var teacher in this.Teachers)
                    {
                        DisplayATeacher(teacher);
                    }
                }
                else
                {
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("-    Sorry , there are no data exist !    -");
                    Console.WriteLine("-------------------------------------------");
                }
            }

            // Add a new teacher
            public void AddTeacher(Teacher _teacher)
            {
                using (StreamWriter sw = File.AppendText(TextFilePath))
                {
                    sw.WriteLine(_teacher.ToOneLine());
                    this.Teachers.Add(_teacher);
                }
            }

            // Delete a teacher
            public void DeleteTeacher(int _id)
            {
                int i = 0;
                foreach (var teacher in this.Teachers)
                {
                    if(_id == teacher.ID)
                    {
                        List<string> linesList = File.ReadAllLines(TextFilePath).ToList();
                        linesList.RemoveAt(i);
                        File.WriteAllLines(TextFilePath , linesList.ToArray());
                        GetTeachersDataFromTXT();
                        break;
                    }
                    i++;
                }
                Console.WriteLine("*** There Are No Teacher With This ID !!");
            }

            // Updating Teacher By ID
            public void UpdateTeacher(int _id , Teacher _teacher)
            {
                foreach (var teacher in this.Teachers)
                {
                    if(teacher.ID == _id)
                    {
                        teacher.ID = _teacher.ID;
                        teacher.Name = _teacher.Name;
                        teacher.Class = _teacher.Class;
                        teacher.Section = _teacher.Section;
                        File.WriteAllLines(TextFilePath, TeachersToLinesArray());
                        GetTeachersDataFromTXT();
                    }
                }
            }

            // convert List of Teachers to lines array
            private string[] TeachersToLinesArray()
            {
                List<string> _teachers = new List<string>();
                foreach (var teacher in this.Teachers)
                {
                    _teachers.Add(teacher.ToOneLine());
                }

                return _teachers.ToArray();
            }

            // Append all teachers' Data 
            private void GetTeachersDataFromTXT()
            {
                this.Teachers = new List<Teacher>();
                // read all lines from the file
                string[] _lines = File.ReadAllLines(TextFilePath);

                if (_lines != null && _lines.Length > 0)
                {
                    foreach (var line in _lines)
                    {
                        string[] columns = line.Split(',');
                        this.Teachers.Add(new Teacher(Convert.ToInt32(columns[0]), columns[1], Convert.ToInt32(columns[2]), columns[3]));
                    }
                }
            }

            public List<Teacher> Teachers { get; set; }
        }

        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            var DB = new DataWarehouse();
            DB.DisplayTeachers();
            Console.ReadKey();
            //DB.AddTeacher(new Teacher(6, "Faisal Mohammed", 101, "Math"));
            //DB.AddTeacher(new Teacher(8, "Saad Mohammed", 101, "Math"));
            //DB.AddTeacher(new Teacher(6, "Ahmed Mohammed", 304, "Math"));
            //DB.AddTeacher(new Teacher(6, "Somone Mohammed", 505, "Math"));
            //DB.DisplayTeachers();
            //Console.ReadKey();
            DB.DeleteTeacher(8);
            DB.DisplayTeachers();
            Console.ReadKey();
            DB.UpdateTeacher(7,new Teacher(11, "Mansour", 511, "CS"));
            DB.DisplayTeachers();
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}

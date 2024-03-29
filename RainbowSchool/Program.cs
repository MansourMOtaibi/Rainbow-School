using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RainbowSchool
{
    class Program
    {
        class Teacher
        {
            public Teacher(int _id, string _name, int _class, string _section)
            {
                this.ID = _id;
                this.Name = _name;
                this.Class = _class;
                this.Section = _section;
            }

            public Teacher(string _name, int _class, string _section)
            {
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
                Console.Clear();
                Header();
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
            public int AddTeacher(Teacher _teacher)
            {
                _teacher.ID = Teachers != null && Teachers.Count() > 0 ? Teachers.Count() : 0;
                using (StreamWriter sw = File.AppendText(TextFilePath))
                {
                    sw.WriteLine(_teacher.ToOneLine());
                    this.Teachers.Add(_teacher);
                    return _teacher.ID;
                }
            }

            // Delete a teacher
            public bool DeleteTeacher(int _id)
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
                        return true;
                    }
                    i++;
                }
                Console.WriteLine("*** There Are No Teacher With This ID !!");
                return false;
            }

            // Updating Teacher By ID
            public void UpdateTeacher(int _id , Teacher _teacher)
            {
                foreach (var teacher in this.Teachers)
                {
                    if(teacher.ID == _id)
                    {
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

            private bool IsTeacherExist(int _id)
            {
                return Teachers.FirstOrDefault(m => m.ID == _id) != null;
            }

            #region Rainbow School UI
            public void Header()
            {
                Console.WriteLine("===========================================");
                Console.WriteLine("|          *  Rainbow School   *          |");
                Console.WriteLine("|          Teachers' Data System          |");
                Console.WriteLine("|          By : Mansour Alotaibi          |");
                Console.WriteLine("===========================================");
                Thread.Sleep(200);
            }

            public void MainMenu()
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("-   Please Choose one of this Actions :   -");
                Console.WriteLine("-  [S]Show  [A]Add  [U]Update  [D]Delete  -");
                Console.WriteLine("-------------------------------------------");
                ConsoleKeyInfo choosedAction = Console.ReadKey();
                switch (choosedAction.Key)
                {
                    case ConsoleKey.S:
                        this.DisplayTeachers();
                        Thread.Sleep(200);
                        MainMenu();
                        break;
                    case ConsoleKey.A:
                        AddTeacherUI();
                        Thread.Sleep(200);
                        MainMenu();
                        break;
                    case ConsoleKey.U:
                        UpdateTeacherUI();
                        Thread.Sleep(200);
                        MainMenu();
                        break;
                    case ConsoleKey.D:
                        DeleteTeacherUI();
                        Thread.Sleep(200);
                        MainMenu();
                        break;
                    default:
                        Console.WriteLine(" <= Sorry!! .. You Choose a wrong action!-");
                        Thread.Sleep(200);
                        MainMenu();
                        break;
                }

            }

            public void AddTeacherUI()
            {
                Console.Clear();
                Header();
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("-          Adding a New Teacher           -");
                Console.WriteLine("-------------------------------------------");
                Thread.Sleep(200);
                string _name, _class, _section;
                int _classNumber;
                bool IsNumber = false;

                Console.Write("- Insert Teacher's Name    : ");
                _name = Console.ReadLine();
                
                Console.Write("- Insert Teacher's Class   : ");
                _class = Console.ReadLine();
                IsNumber = Int32.TryParse(_class, out _classNumber);

                while (!IsNumber)
                {
                    Console.WriteLine("* The inserted value is not a NUNMBER");
                    Console.WriteLine("* Please insert a valid NUMBER !");
                    Console.Write("- Insert Teacher's Class   : ");
                    _class = Console.ReadLine();
                    IsNumber = Int32.TryParse(_class, out _classNumber);
                }

                Console.Write("- Insert Teacher's Section : ");
                _section = Console.ReadLine();

                int teacherId = AddTeacher(new Teacher(_name, _classNumber, _section));
                Thread.Sleep(200);
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine($"- Teacher With ID {teacherId} Added Succefully   ");
            }

            public void UpdateTeacherUI()
            {
                Console.Clear();
                Header();
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("-            Updating a Teacher           -");
                Console.WriteLine("-------------------------------------------");
                Thread.Sleep(200);
                string _id, _name, _class, _section;
                int _idNumber = 0;
                int _classNumber;
                bool IsNumber = false;
                bool IsExist = false;

                while (!IsExist)
                {
                    Console.Write("- Which Teacher you will Update ? (Teacher's ID) : ");
                    _id = Console.ReadLine();
                    IsNumber = Int32.TryParse(_id, out _idNumber);

                    while (!IsNumber)
                    {
                        Console.WriteLine("* The inserted value is not a NUNMBER");
                        Console.WriteLine("* Please insert a valid NUMBER !");
                        Console.Write("- Insert Teacher's ID   : ");
                        _id = Console.ReadLine();
                        IsNumber = Int32.TryParse(_id, out _idNumber);
                    }

                    IsExist = IsTeacherExist(_idNumber);
                    if (!IsExist)
                    {
                        Console.WriteLine("* The inserted ID is not EXIST !!");
                        Console.WriteLine("* Please insert a valid ID !");
                        Thread.Sleep(200);
                    }
                    else
                    {
                        DisplayATeacher(this.Teachers.FirstOrDefault(m => m.ID == _idNumber));
                    }
                }

                Console.Write("- Insert the new Name    : ");
                _name = Console.ReadLine();

                Console.Write("- Insert the new Class   : ");
                _class = Console.ReadLine();
                IsNumber = Int32.TryParse(_class, out _classNumber);

                while (!IsNumber)
                {
                    Console.WriteLine("* The inserted value is not a NUNMBER");
                    Console.WriteLine("* Please insert a valid NUMBER !");
                    Console.Write("- Insert the new Class   : ");
                    _class = Console.ReadLine();
                    IsNumber = Int32.TryParse(_class, out _classNumber);
                }

                Console.Write("- Insert the new Section : ");
                _section = Console.ReadLine();

                UpdateTeacher(_idNumber, new Teacher(_name, _classNumber, _section));
                Thread.Sleep(200);
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine($"- Teacher With ID {_idNumber} Updated Succefully   ");
            }

            public void DeleteTeacherUI()
            {
                Console.Clear();
                Header();
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("-            Deleting a Teacher           -");
                Console.WriteLine("-------------------------------------------");
                Thread.Sleep(200);

                string _id;
                int _idNumber = 0;
                bool IsNumber = false;
                bool IsExist = false;

                while (!IsExist)
                {
                    Console.Write("- Which Teacher you will delete ? (Teacher's ID) : ");
                    _id = Console.ReadLine();
                    IsNumber = Int32.TryParse(_id, out _idNumber);

                    while (!IsNumber)
                    {
                        Console.WriteLine("* The inserted value is not a NUNMBER");
                        Console.WriteLine("* Please insert a valid NUMBER !");
                        Console.Write("- Insert Teacher's ID   : ");
                        _id = Console.ReadLine();
                        IsNumber = Int32.TryParse(_id, out _idNumber);
                    }

                    IsExist = IsTeacherExist(_idNumber);
                    if (!IsExist)
                    {
                        Console.WriteLine("* The inserted ID is not EXIST !!");
                        Console.WriteLine("* Please insert a valid ID !");
                        Thread.Sleep(200);
                    }
                    else
                    {
                        DisplayATeacher(this.Teachers.FirstOrDefault(m => m.ID == _idNumber));
                        if(DeleteTeacher(_idNumber))
                            Console.WriteLine($"- Teacher With ID {_idNumber} Deleted Succefully   ");
                        else
                            Console.WriteLine($"- Teacher With ID {_idNumber} not deleted !   ");
                        Thread.Sleep(200);
                    }
                }


            }
            #endregion

            public List<Teacher> Teachers { get; set; }
        }

        static void Main(string[] args)
        {
            var DB = new DataWarehouse();
            DB.Header();
            DB.MainMenu();
        }
    }
}

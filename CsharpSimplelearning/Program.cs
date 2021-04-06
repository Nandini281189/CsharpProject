using System;
using System.Collections.Generic;
using System.IO;

namespace CsharpSimplelearning
{
    class Program
    {
        static string dire = @"c:\temp";
        static string serialFile = Path.Combine(dire, "teachers.bin");
        static int temp = 0;
        static Random random = new Random();
        static void Main(string[] args)
        {
            bool showOpt = true;
            while (showOpt)
            {
                showOpt = Menu();
            }



            Console.Write("Press any key to close the application");
            Console.ReadKey();

        }

        private static bool Menu()
        {
            Console.WriteLine("Teacher Management\r");
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Select operation to perform");
            Console.WriteLine("\t1 - Add Teacher ");
            Console.WriteLine("\t2 - Update Teacher");
            Console.WriteLine("\t3 - See all teacher");
            Console.WriteLine("\t4 - Exit");
            Console.Write("Your option? ");
            Console.WriteLine("Type a number, and then press Enter");
            temp = Convert.ToInt32(Console.ReadLine());
            switch (temp)
            {
                case 1:
                    Console.WriteLine("You selected to add teacher");
                    Console.WriteLine("Enter teacher name");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter class taken");
                    string classTaken = Console.ReadLine();
                    Console.WriteLine("Enter section name");
                    string section = Console.ReadLine();
                    Teacher teach = new Teacher(random.Next(), name, classTaken, section);
                    List<Teacher> Listteacher = getListTeacher();
                    Listteacher.Add(teach);
                    saveTeacherFile(Listteacher);
                    return true;
                case 2:
                    Console.WriteLine("You selected to update teacher");
                    Console.WriteLine("Enter Teacher id to update");
                    int idfromUser = Convert.ToInt32(Console.ReadLine());
                    List<Teacher> teachers = getListTeacher();
                    foreach (Teacher item in teachers)
                    {
                        if (item.Id == idfromUser)
                        {
                            Console.WriteLine("Enter teacher name");
                            string updatedName = Console.ReadLine();
                            Console.WriteLine("Enter class taken");
                            string updatedClassTaken = Console.ReadLine();
                            Console.WriteLine("Enter section name");
                            string updatedSection = Console.ReadLine();
                            item.Name = updatedName;
                            item.ClassTaken = updatedClassTaken;
                            item.Section = updatedSection;
                            saveTeacherFile(teachers);
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid id entered,Please enter a valid ID");
                        }
                    }
                    return true;
                case 3:
                    Console.WriteLine("You selected to see all teacher");
                    List<Teacher> teacherListFile = getListTeacher();
                    foreach (Teacher item in teacherListFile)
                    {
                        Console.WriteLine(item);
                    }

                    return true;
                case 4:
                    return false;
                default:
                    Console.WriteLine("Invalid option selected ");
                    return true;

            }
        }

        private static List<Teacher> getListTeacher()
        {
            using (Stream stream = File.Open(serialFile, FileMode.OpenOrCreate))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                if (stream.Length < 1)
                {
                    return new List<Teacher>();
                }
                else
                {
                    List<Teacher> teacherListFromFile = (List<Teacher>)bformatter.Deserialize(stream);
                    return teacherListFromFile;
                }

            }
        }

        private static void saveTeacherFile(List<Teacher> teacherList)
        {
            using (Stream stream = File.Open(serialFile, FileMode.OpenOrCreate))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, teacherList);
            }
        }


    }
}

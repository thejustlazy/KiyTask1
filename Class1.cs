using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

namespace Kiy
{
    class RegexTask
    {
        private IEnumerable<string> FirstDirectory { get; set; }
        private IEnumerable<string> SecondDirectory { get; set; }
        private string Result { get; set; }
        public RegexTask(string FirstArg, string SecondArg, string ThirdArg)
        {
            try
            {
                FirstDirectory = Directory.EnumerateFiles(FirstArg, "*", SearchOption.AllDirectories);
                SecondDirectory = Directory.EnumerateFiles(SecondArg, "*", SearchOption.AllDirectories);
                Result = ThirdArg;
            }
            catch (FileNotFoundException e)
            {
                Console.Clear();
                Console.WriteLine(e.FileName + " не существует");
                Console.ReadKey();
                Environment.Exit(1);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message + " не существует");
                Console.ReadKey();
                Environment.Exit(1);
            }

        }
        public void Task()
        {
            try
            {
                if (FirstDirectory.Count() == 0) { Console.WriteLine("Папка #1 пуста." + " Нажмите любую клавишу.."); Console.ReadKey(); return; }
                if (SecondDirectory.Count() == 0) { File.AppendAllLines(Result, FirstDirectory); return; }
                var Collection = SecondDirectory.SelectMany(n => FirstDirectory.Select(a => Path.GetFileName(a))).Except(FirstDirectory.SelectMany(n => SecondDirectory.Select(a => Path.GetFileName(a))));
                var result = Collection.SelectMany(n => FirstDirectory.Where(a => a.Contains(n)));
                File.AppendAllLines(Result, result);
                    Console.WriteLine("Для продолжения нажмите любую кнопку");
                    Console.ReadKey();
            }
            catch (ArgumentException e)
            {
                Console.Clear();
                Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
                return;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return;
            }
            catch (DirectoryNotFoundException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return;
            }
        }
        
    }

}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema3_with_TPL
{
    class Program
    {
        private const string FileName1 = "file1.txt";
        private const string FileName2 = "file2.txt";
        private const string FileName3 = "file3.txt";
        private const string FileName4 = "file4.txt";
        private const string FileName5 = "file5.txt";

        static void Main(string[] args)
        {
            //compute the average for numbers on each line for each file
            AverageNumbersPerLine();

            //compute the average for numbers in all files
            AverageNumbersinAllFiles();

            //compute the sum for each number on each line from all files
            Task<int> task1 = new Task<int>(() => SumPerFile(0, FileName1));

            Task<int> task2 = task1.ContinueWith(taskPrecedent => SumPerFile(taskPrecedent.Result, FileName2));

            Task<int> task3 = task2.ContinueWith(taskPrecedent => SumPerFile(taskPrecedent.Result, FileName3));

            Task<int> task4 = task3.ContinueWith(taskPrecedent => SumPerFile(taskPrecedent.Result, FileName4));
            Task<int> task5 = task4.ContinueWith(taskPrecedent => SumPerFile(taskPrecedent.Result, FileName5) / 5);
            task1.Start();

        }

        private static void AverageNumbersinAllFiles()
        {
            Task<int> task1 = new Task<int>(() => SumPerFile(0, FileName1));

            Task<int> task2 = task1.ContinueWith(taskPrecedent => SumPerFile(taskPrecedent.Result, FileName2));

            Task<int> task3 = task2.ContinueWith(taskPrecedent => SumPerFile(taskPrecedent.Result, FileName3));

            Task<int> task4 = task3.ContinueWith(taskPrecedent => SumPerFile(taskPrecedent.Result, FileName4));
            Task<int> task5 = task4.ContinueWith(taskPrecedent => SumPerFile(taskPrecedent.Result, FileName5) / 5);
            task1.Start();
            Console.WriteLine($"The average for numbers in all files is {task5.Result}");
        }

        private static void AverageNumbersPerLine()
        {
            Task task1 = new Task<double>(() => AveragePerFile(FileName1));
            Task task2 = new Task<double>(() => AveragePerFile(FileName2));
            Task task3 = new Task<double>(() => AveragePerFile(FileName3));
            Task task4 = new Task<double>(() => AveragePerFile(FileName4));
            Task task5 = new Task<double>(() => AveragePerFile(FileName5));

            //  Task<int> task2 = task1.ContinueWith(taskPrecedent => Method2(taskPrecedent.Result));

            //  Task task3 = task2.ContinueWith(taskPrecedent => Method3(taskPrecedent.Result));

            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();
            task5.Start();

            //var list = task1.Result;

            //foreach (var i in list)
            //{
            //    Console.WriteLine(i);
            //}

            // task3.Wait();
            Console.WriteLine("End");

            Console.ReadKey();
        }

        static double AveragePerFile(string FileName)
        {
            try
            {
                string[] lines = File.ReadAllLines(FileName);

                double sum = 0;
                Parallel.ForEach(lines, d =>
                {
                    lock (lines)
                    {
                        sum += Convert.ToDouble(d);
                    }
                });
                Console.WriteLine($"Average on file {FileName} is {sum / lines.Count()}");
                return sum / lines.Count();

            }

            catch (Exception e)
            {
                Console.WriteLine($"We encoutered an error on line {e.StackTrace}\n{e.Message}");
                return 0;
            }
        }
        static int SumPerFile(int sum, string FileName)
        {
            try
            {
                string[] lines = File.ReadAllLines(FileName);

                int suma = 0;
                Parallel.ForEach(lines, d =>
                {
                    lock (lines)
                    {
                        suma += Convert.ToInt32(d);
                    }
                });
                Console.WriteLine($"Sum on file {FileName} is {sum}");
                return sum+suma;

            }

            catch (Exception e)
            {
                Console.WriteLine($"We encoutered an error on line {e.StackTrace}\n{e.Message}");
                return 0;
            }
        }

    }
}


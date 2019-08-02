using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Problem3
{
    class Program
    {
        private const string FileName1 = "file1.txt";
        private const string FileName2 = "file2.txt";
        private const string FileName3 = "file3.txt";
        private const string FileName4 = "file4.txt";
        private const string FileName5 = "file5.txt";
        //static Semaphore objSem = new Semaphore(1, 5, "AveragePerFile");

        static void Main(string[] args)
        {
            //var thread1 = new Thread(Proces1);
            //thread1.Start();

            var thread2 = new Thread(Proces2);
            thread2.Start();

            //var thread3 = new Thread(Proces3);
            //thread3.Start();

            var thread4 = new Thread(Proces4);
            thread4.Start();

            var thread5 = new Thread(Proces5);
            thread5.Start();

            var thread6 = new Thread(Proces6);
            thread6.Start();
        }

        public static void Proces1()
        {
           Console.WriteLine($"Media file 1 - {AveragePerFile(FileName1)}");
        }

        public static void Proces2()
        {
            Console.WriteLine($"Media file 2 - {AveragePerFile(FileName2)}");
        }
        public static void Proces3()
        {
            Console.WriteLine($"Media file 3 - {AveragePerFile(FileName3)}");
        }
        public static void Proces4()
        {
            Console.WriteLine($"Media file 4 - {AveragePerFile(FileName4)}");
        }
        public static void Proces5()
        {
            Console.WriteLine($"Media file 5 - {AveragePerFile(FileName5)}");
        }

        public static void Proces6()
        {
            Console.WriteLine($"Media tuturor numerelor din toate fisierele:{AveragePerFile(FileName1) + AveragePerFile(FileName2) + AveragePerFile(FileName3) + AveragePerFile(FileName4) + AveragePerFile(FileName5)}");
        }
        static double AveragePerFile(string FileName)
        {
            try
            {
                string[] lines = File.ReadAllLines(FileName);
                
                double sum=0;
                Parallel.ForEach(lines, d =>
                {
                    lock (lines)
                    {
                        sum += Convert.ToDouble(d);
                    }
                });
                return (sum / lines.Count());

            }

            catch (Exception e)
            {
               Console.WriteLine($"We encouter an error on line {e.StackTrace}\n{e.Message}");
                return 0;
            }
        }
    }
}

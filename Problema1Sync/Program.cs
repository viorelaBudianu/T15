using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1Sync
{
    class Program
    {
        private const string FileName = "data.txt";
        static void Main(string[] args)
        {
            string IDFile = @"C:\Users\vbudianu\Desktop\VIO\t15\ConsoleApp3file\IDS2.txt";
            try
            {
                if (File.Exists(IDFile))
                {
                    // daca avem deja fisierul
                    File.Delete(IDFile);
                }

                // Create the file.

                string[] lines = File.ReadAllLines(FileName);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
    }
}

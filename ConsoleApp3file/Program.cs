using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3file
{
    class Program
    {
        private const string FileName = "data.txt";
        private const string IDFile = "IDS.txt";
        static void Main(string[] args)
        {
            try
            {
.
                if (File.Exists(IDFile))
                {
                    // daca avem deja fisierul
                    File.Delete(IDFile);
                }

                // Create the file.
                using (FileStream CODELIST = File.Create(IDFile))
                {
                    StreamReader streamReader = 
                }
                catch

        }
    }
}

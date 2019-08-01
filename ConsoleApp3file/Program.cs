using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp3file
{
    internal class Obiect
    {
        internal string caractere;
        internal int count;

        public Obiect(string caracter, int count)
        {
            this.caractere = caracter;
            this.count = count;
        }
    }
    class Program
    {
        private const string FileName = "data.txt";
        private const string IDFile = "IDS.txt";
        static void Main(string[] args)
        {
            try
            {
                if (File.Exists(IDFile))
                {
                    // daca avem deja fisierul
                    File.Delete(IDFile);
                }

                // Create the file.
                using (FileStream IdList = File.Create(IDFile))
                {
                    string[] lines = File.ReadAllLines(FileName);
                    const string ID = @"(?<a>\d+)\t";
                    const string Value = @"\t(?<b>.*$)";
                    Dictionary<int, Obiect> Lista = new Dictionary<int, Obiect>();
                    

                    foreach (var i in lines)
                    {
                        Match IDcuvant = Regex.Match(i, ID);
                        Match Cuvant = Regex.Match(i, Value);
                        
                        if (!Lista.ContainsKey(Convert.ToInt32(IDcuvant)))
                        {
                            Lista.Add(Convert.ToInt32(IDcuvant), new Obiect(Convert.ToString(Cuvant), 1));
                        }
                        else
                        {
                            Lista[Convert.ToInt32(IDcuvant)].count = Lista[Convert.ToInt32(IDcuvant)].count++;
                        }
                    }

                    StreamWriter sw = File.AppendText(IDFile);
                    


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            } 


        }
    }
}

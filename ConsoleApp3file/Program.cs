using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp3file
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

                
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                string[] lines = File.ReadAllLines(FileName);
                
                const string ID = @"\d+";
                const string Value = @"\d+\t(?<b>.*$)";
                Dictionary<int, Obiect> Lista = new Dictionary<int, Obiect>();
                Console.WriteLine(lines[1]);
                Match IDcuvant1 = Regex.Match(lines[1], ID);
                Console.WriteLine(IDcuvant1.Groups[0].Value);
                foreach (var i in lines)
                {
                    Match IDcuvant = Regex.Match(i, ID);
                    Match Cuvant = Regex.Match(i, Value);
                    var IDc = IDcuvant.Groups[0].Value.ToString();
                    Console.WriteLine(IDcuvant.Groups[0].Value);

                    
                    if (!Lista.ContainsKey(Int32.Parse(IDc)))
                    {
                        Lista.Add(Int32.Parse(IDc), new Obiect(Convert.ToString(Cuvant), 1));
                    }
                    else
                    {

                        Lista[Int32.Parse(IDc)].count = Lista[Int32.Parse(IDc)].count+1;
                    }
                }
                using (FileStream IdList = File.Create(IDFile))
                {
                   // StreamWriter sw = File.AppendText(IDFile);
                    foreach (var i in Lista)
                    {
                        Byte[] line = new UTF8Encoding(true).GetBytes($"{Convert.ToString(i.Key)} are {i.Value.count} grupuri de caractere distincte\n");
                        IdList.Write(line, 0, line.Length);
                    }

                    Byte[] time = new UTF8Encoding(true).GetBytes($"Time spent: {stopwatch.ElapsedMilliseconds.ToString()} ms");
                    IdList.Write(time, 0, time.Length);


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace + e.Message);
            }


        }
    }
}

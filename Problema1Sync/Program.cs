using ConsoleApp3file;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Problema1Sync
{
    class Program
    {
        private const string FileName = "data.txt";
        static void Main(string[] args)
        {
            string IDFile = @"C:\Users\vbudianu\Desktop\VIO\t15\Problema1Sync\IDs2Sync.txt";
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

                ConcurrentDictionary<int, Obiect> dictionar = new ConcurrentDictionary<int, Obiect>();
                const string ID = @"\d+";
                const string Value = @"\d+\t(?<b>.*$)";

                Parallel.ForEach(lines, l =>
                {
                    lock (lines)
                    {
                        Match IDcuvant = Regex.Match(l, ID);
                        Match Cuvant = Regex.Match(l, Value);
                        var IDc = IDcuvant.Groups[0].Value.ToString();

                        if (!dictionar.ContainsKey(Int32.Parse(IDc)))
                        {
                            dictionar.TryAdd(Int32.Parse(IDc), new Obiect(Convert.ToString(Cuvant), 1));
                           
                        }
                        else
                        {
                            dictionar[Int32.Parse(IDc)].count = dictionar[Int32.Parse(IDc)].count + 1;
                        }
                    }
                });

                using (FileStream IdList = File.Create(IDFile))
                {
                   
                        Parallel.ForEach(dictionar, d =>
                        {
                            lock (dictionar)
                            {
                                Byte[] line = new UTF8Encoding(true).GetBytes($"Folosind Concurent Dictionary {Convert.ToString(d.Key)} are {d.Value.count} grupuri de caractere distincte\n");
                                IdList.Write(line, 0, line.Length);
                            }
                        });
                    Byte[] time = new UTF8Encoding(true).GetBytes($"Time spent: {stopwatch.ElapsedMilliseconds} ms");
                    IdList.Write(time, 0, time.Length);
                }
                            


            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


    }
}

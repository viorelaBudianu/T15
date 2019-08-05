using System;
using System.Text;

namespace ParallelLetter
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] cuvant1 = new string[] { "ANA", "ARE", "MERE" };
            var rezultat = ParallelLetter.Calculate(cuvant1);
            foreach (var e in rezultat)
            {
                Console.WriteLine($"{e.Key} apare de {e.Value} ori");
            }
        }
    }
}

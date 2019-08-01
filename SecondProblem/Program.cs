using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondProblem
{


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Afisare().Result);
        }

        static async Task<string> Afisare()
        {
            StringBuilder a = new StringBuilder($"Today is { DateTime.Today:D}\n");
            a.Append($"Rezultatul este: {await ComputePi()}"); //asteapta ca metoda ComputePi sa fie executata, si dupa afiseaza 

            return a.ToString();
        }

        static async Task<double> ComputePi()

        {
            var sum = 0.0;

            var step = 1e-9;

            for (var i = 0; i < 1000000000; i++)

            {

                var x = (i + 0.5) * step;

                sum = sum + 4.0 / (1.0 + x * x);

            }

            return sum * step;

        }
    }
}

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelLetter
{
    public static class ParallelLetter
    {
        public static Dictionary<char, int> Calculate(IEnumerable<string> texts)
        {
            var dict = new ConcurrentDictionary<char, int>();
            //var dict = new Dictionary<char, int>();

            var lockObject = new object();
            Parallel.ForEach(texts, text =>
            {
                Parallel.ForEach(text.ToLower(), letter =>
                {
                    lock (lockObject)
                    {
                        if (IsValidCharacter(letter))
                        {
                            if (!dict.ContainsKey(letter))
                            {

                                dict.TryAdd(letter, 1);
                            }
                            else
                            {
                                dict[letter]++;
                            }
                        }
                    }
                });
            });

            return dict.ToDictionary(k => k.Key, v => v.Value);
        }

        private static bool IsValidCharacter(char text)
        {
            return char.IsLetter(text) && !char.IsNumber(text);
        }
    }
}

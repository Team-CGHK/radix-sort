using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace RadixSort
{
    class Program
    {
        private const int N = 1200000;
        private const int Length = 20;

        //let the strings consist of lowercase letters
        private const char FirstChar = 'a';
        private const int AlphabetSize = 26;

        static void Main(string[] args)
        {
            Random r = new Random();

            //generate data set
            string[] data0 = new string[N],
                     data1 = new string[N];
            char[] chars = new char[Length];
            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < Length; ++j)
                    chars[j] = (char)(FirstChar + r.Next(26));
                data0[i] = data1[i] = new string(chars);
            }

            Stopwatch time = new Stopwatch();

            //test QSort
            time.Start();
            Array.Sort(data0);
            Console.WriteLine("QSort'ed in "+time.ElapsedMilliseconds+" ms");
            time.Reset();

            //test RadixSort
            time.Start();
            RadixSort(data1);
            Console.WriteLine("RadixSort'ed in " + time.ElapsedMilliseconds + " ms");
            time.Reset();

            if (Console.ReadKey().Key != ConsoleKey.Spacebar) return;
            Console.WriteLine("Press spacebar to print the sorted data or any other key to quit");
            foreach (string s in data1)
                Console.WriteLine(s);
            Console.ReadKey();
        }

        /// <param name="data">
        /// All the strings need to be the same length, otherwise RadixSort is much slower than QSort
        /// due to length condition checking
        /// </param>
        static void RadixSort(string[] data)
        {
            List<string>[] lists = new List<string>[AlphabetSize];
            for (int i = 0; i < lists.Length; ++i)
                lists[i] = new List<string>();
            int m = data[0].Length - 1;
            for (int position = m; position >= 0; position--)
            {
                foreach (List<string> list in lists)
                    list.Clear();

                foreach (string t in data)
                    lists[t[position]-FirstChar].Add(t);

                int pos = 0;
                foreach (List<string> list in lists)
                    foreach (string s in list)
                        data[pos++] = s;
            }
        }
    }
}

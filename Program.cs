using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace RadixSort
{
    class Program
    {
        private const int N = 1000;
        private const int digits = 6;

        static void Main(string[] args)
        {
            Random r = new Random();
            long[] data = new long[N];
            for (int i = 0; i < data.Length; ++i)
                data[i] = r.Next((int)Pow10[digits]);
            RadixSort(data, 0, data.Length, digits-1);
            foreach (long t in data)
                Console.WriteLine(t);
            Console.ReadKey();
        }

        static void RadixSort(long[] data, int from, int count, int digit)
        {
            if (digit == -1)
                return;
          
            List<long>[] currentRankItems = new List<long>[10];
            for (int i = 0; i < currentRankItems.Length; ++i)
                currentRankItems[i] = new List<long>();
            for (int i = from; i < count; ++i)
                currentRankItems[data[i] % Pow10[digit + 1] / Pow10[digit]].Add(data[i]);
            for (int i = 0, pos = 0; i < currentRankItems.Length; ++i)
                foreach (long l in currentRankItems[i])
                    data[from + pos++] = l;
            for (int i = 0, pos = 0; i < currentRankItems.Length; ++i)
            {
                RadixSort(data, pos, currentRankItems[i].Count, digit - 1);
                pos += currentRankItems[i].Count;
            }
        }

        private static readonly long[] Pow10 = new long[15];

        static Program()
        {
            //some pre-calculation to avoid multiplication during runtime
            long p = 1;
            Pow10[0] = 1;
            for (int i = 1; i < Pow10.Length; ++i)
                Pow10[i] = p *= 10;
        }
    }
}

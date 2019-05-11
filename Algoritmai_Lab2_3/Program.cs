using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmai_Lab2_3
{
    class Program
    {
        static Random random;
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            random = new Random(seed);

            
            int[] amount = { 100000, 150000, 200000, 250000, 300000, 400000, 500000, 750000, 1000000, 1250000 };
            int repeat = 10;

            var stopWatch = new Stopwatch();

            long sequentialTotal;
            long parallelTotal;

            foreach (int n in amount)
            {
               // Console.WriteLine($"n = {n}");
                sequentialTotal = 0;
                parallelTotal = 0;
                for (int i = 0; i < repeat; i++)
                {
                    List<string> nameList = new List<string>();
                    HashTable hashTable = new HashTable((int)((float)n / 0.75f), 0.8f);
                    for (int j = 0; j < n; j++)
                    {
                        string s = RandomName(10);
                        nameList.Add(s);
                        hashTable.Put(s, s);
                    }
                    GC.Collect();
                    stopWatch.Reset();
                    stopWatch.Start();
                    int count = SequentialLoop(nameList, hashTable);
                    stopWatch.Stop();
                    sequentialTotal += stopWatch.ElapsedMilliseconds;
                    //Console.WriteLine("Time in milliseconds for sequential loop: {0,6:N0} ", stopWatch.ElapsedMilliseconds);
                    //Console.WriteLine("Contains: {0,6:N0} Total: {1,6:N0}", count, nameList.Count);

                    stopWatch.Reset();
                    stopWatch.Start();
                    count = ParallelTaskLoop(nameList, hashTable);
                    stopWatch.Stop();
                    parallelTotal += stopWatch.ElapsedMilliseconds;
                    //Console.WriteLine("Time in milliseconds for parallel loop: {0,6:N0} ", stopWatch.ElapsedMilliseconds);
                    //Console.WriteLine("Contains: {0,6:N0} Total: {1,6:N0}", count, nameList.Count);

                }
                Console.Write($"{n};{sequentialTotal / repeat:N0};{parallelTotal / repeat:N0}\n");
            }
        }

        static int SequentialLoop(List<string> names, HashTable hash)
        {
            int count = 0;
            for (int i = 0; i < names.Count; i++)
                if (hash.Get(names[i]) != null)
                   count++;
            return count;
        }

        static int ParallelTaskLoop(List<string> names, HashTable hash)
        {
            int countCPU = 8;
            Task<int>[] tasks = new Task<int>[countCPU];
            for (int j = 0; j < countCPU; j++)
            {
                tasks[j] = Task<int>.Factory.StartNew(
                    (object p) =>
                    {
                        int count = 0;
                        for (int i = (int)p; i < names.Count; i += countCPU)
                            if (hash.Get(names[i]) != null)
                                count++;
                        return count;
                    }, j);
            }
            int total = 0;
            for (int i = 0; i < countCPU; i++)
                total += tasks[i].Result;
            return total;
        }

        static string RandomName(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 *
            random.NextDouble() + 65)));
            builder.Append(ch);
            for (int i = 1; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 *
                random.NextDouble() + 97)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}

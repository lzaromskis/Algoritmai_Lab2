using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmai_Lab2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> set = new List<int>(new int [] { 3, 34, 4, 12, 5, 2, 54, 34, 15, 41, 84, 51, 24, 35, 14, 17, 35, 47, 12, 11, 11, 14, 17, 15, 25, 34, 25, 4, 9, 8, 10, 15, 14, 12, 14, 16, 19, 17, 12, 24, 26, 5, 1, 27, 44, 21, 14, 15, 18, 19, 17, 13, 15, 17, 3, 34, 4, 12, 5, 2, 54, 34, 15, 41, 84, 51, 24, 35, 14, 17, 35, 47, 12, 11, 11, 14, 17, 15, 25, 34, 25, 4, 9, 8, 10, 15, 14, 12, 14, 16, 19, 17, 12, 24, 26, 5, 1, 27, 44, 21, 14, 15, 18, 19, 17, 13, 15, 17, 3, 34, 4, 12, 5, 2, 54, 34, 15, 41, 84, 51, 24, 35, 14, 17, 35, 47, 12, 11, 11, 14, 17, 15, 25, 34, 25, 4, 9, 8, 10, 15, 14, 12, 14, 16, 19, 17, 12, 24, 26, 5, 1, 27, 44, 21, 14, 15, 18, 19, 17, 13, 15, 17, 3, 34, 4, 12, 5, 2, 54, 34, 15, 41, 84, 51, 24, 35, 14, 17, 35, 47, 12, 11, 11, 14, 17, 15, 25, 34, 25, 4, 9, 8, 10, 15, 14, 12, 14, 16, 19, 17 } );
            Console.WriteLine($"set.Lenght={set.Count}");
            int sum = 4216;
            int n = set.Count;
            //  if (IsSubsetSum(set, n, sum) == true)
            //      Console.WriteLine("Found a subset with given sum");
            //  else
            //      Console.WriteLine("No subset with given sum");
            var arr = set.ToArray();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            if (IsSubsetSumIter(arr, n, sum) == true)
                Console.WriteLine("Found a subset with given sum");
            else
                Console.WriteLine("No subset with given sum");
            stopWatch.Stop();
            Console.WriteLine(stopWatch.ElapsedTicks);
            stopWatch.Reset();
            stopWatch.Start();
            var list = SubsetSum(arr, n, sum);
            stopWatch.Stop();
            Console.WriteLine(stopWatch.ElapsedTicks);
            foreach (var item in list)
            {
                Console.Write($"{item}; ");
            }
            Console.Write("\n");
            Random r = new Random();
            for (int i = 0; i < 200; i++)
            {
                long timeTotal = 0;
                var array = set.ToArray();
                for (int j = 0; j < 5; j++)
                {
                    stopWatch.Reset();
                    stopWatch.Start();
                    SubsetSum(array, array.Length, sum);
                    stopWatch.Stop();
                    timeTotal += stopWatch.ElapsedTicks;
                }
                Console.Write($"{set.Count};{timeTotal / 50}\n");
                set.Add(r.Next(1, 50));
            }
        }

        static bool IsSubsetSum(int[] set, int n, int sum)
        {
            if (sum == 0)
                return true;
            if (n == 0 && sum != 0)
                return false;
            if (set[n - 1] <= sum)
                return IsSubsetSum(set, n - 1, sum) || IsSubsetSum(set, n - 1, sum - set[n - 1]);
            return IsSubsetSum(set, n - 1, sum);
        }

        static bool IsSubsetSumIter(int[] set, int n, int sum)
        {
            bool[,] subset = new bool[sum + 1, n + 1];

            for (int i = 0; i <= n; i++)
                subset[0, i] = true;

            for (int i = 1; i <= sum; i++)
                subset[i, 0] = false;

            for (int i = 1; i <= sum; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    subset[i, j] = subset[i, j - 1];
                    if (i >= set[j - 1])
                        subset[i, j] = subset[i, j] || subset[i - set[j - 1], j - 1];

                }
            }
            return subset[sum, n];
        }

        static List<int> SubsetSum(int[] set, int n, int sum)
        {
            List<int> list = new List<int>();
            int[,] subset = new int[sum + 1, n + 1];
            for (int i = 0; i <= n; i++)
                subset[0, i] = 0;
            for (int i = 1; i <= sum; i++)
                subset[i, 0] = 0;
            for (int i = 1; i <= sum; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    subset[i, j] = subset[i, j - 1];
                    if (i >= set[j - 1])
                        subset[i, j] = Max(subset[i, j], set[j - 1] + subset[i - set[j - 1], j - 1]);
                }
            }
            if (subset[sum, n] != sum)
                return list;
            for (int i = n; i > 0; i--)
            {
                int diff = subset[sum, i] - subset[sum, i - 1];
                if (diff > 0)
                    list.Add(diff);
            }
            return list;
        }

        static void Print2DArray(bool[,] arr, int l, int h)
        {
            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    char tf = arr[i, j] ? 't' : 'f';
                    Console.Write($"{tf} ");
                }
                Console.Write("\n");
            }
        }

        static void Print2DArray(int[,] arr, int l, int h)
        {
            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < h; j++)
                    Console.Write($"{arr[i, j],2} ");
                Console.Write("\n");
            }
        }

        static int Max(int a, int b)
        {
            return a > b ? a : b;
        }
    }
}

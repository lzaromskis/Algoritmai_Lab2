using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmai_Lab2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] v = new int[] { 120, 150, 80, 100 };
            int[] s = new int[] { 10, 20, 30, 20 };
            int D = 50;
            int i = v.Length;

            //Console.WriteLine(KnapsackRec(6, new int[] { 1, 3, 4, 3, 2, 10 }, new int[] { 5, 1, 1, 3, 2, 0 }, 6));

            //Console.WriteLine(KnapsackIter(new int[] { 1, 3, 4, 3, 2, 10 }, new int[] { 5, 1, 1, 3, 2, 0 }, 6));

            v = new int[] {360, 83, 59, 130, 431, 67, 230, 52, 93,
            125, 670, 892, 600, 38, 48, 147, 78, 256,
            63, 17, 120, 164, 432, 35, 92, 110, 22,
            42, 50, 323, 514, 28, 87, 73, 78, 15,
            26, 78, 210, 36, 85, 189, 274, 43, 33,
            10, 19, 389, 276, 312 };
            s = new int[] {7, 0, 30, 22, 80, 94, 11, 81, 70,
            64, 59, 18, 0, 36, 3, 8, 15, 42,
            9, 0, 42, 47, 52, 32, 26, 48, 55,
            6, 29, 84, 2, 4, 18, 56, 7, 29,
            93, 44, 71, 3, 86, 66, 31, 65, 0,
            79, 20, 65, 52, 13 };
            D = 300;
            i = v.Length;

            List<int> vertes = new List<int>(new int[] { 360, 83, 59, 130, 431, 67, 230, 52, 93,
            125, 670, 892, 600, 38, 48, 147, 78, 256});
            List<int> svoriai = new List<int>(new int[] { 7, 0, 30, 22, 80, 94, 11, 81, 70,
            64, 59, 18, 0, 36, 3, 8, 15, 42});

            long sequentialTotal;
            long parallelTotal;

            if (vertes.Count != svoriai.Count)
                throw new ArgumentException();
            var stopWatch = new Stopwatch();
            for (int k = 0; k < 24; k++)
            {
                int[] vv = vertes.ToArray();
                int[] ss = svoriai.ToArray();
                stopWatch.Reset();
                stopWatch.Start();
                //Console.WriteLine(KnapsackRec(vv.Length, vv, ss, D));
                KnapsackRec(vv.Length, vv, ss, D);
                stopWatch.Stop();
                sequentialTotal = stopWatch.ElapsedTicks;// / TimeSpan.TicksPerMillisecond;
                stopWatch.Reset();
                stopWatch.Start();
               // Console.WriteLine(KnapsackIter(vv, ss, D));
                KnapsackIter(vv, ss, D);
                stopWatch.Stop();
                parallelTotal = stopWatch.ElapsedTicks;// / TimeSpan.TicksPerMillisecond;
                Console.Write($"{vv.Length};{sequentialTotal};{parallelTotal}\n");
                vertes.Add(v[vv.Length]);
                svoriai.Add(s[vv.Length]);
            }
            var value = TimeSpan.TicksPerMillisecond;
        }
       
        static int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        static int KnapsackRec(int i, int[] v, int[] s, int D)
        {
            if (i == 0 || D == 0)
                return 0;
            if (s[i-1] <= D)
                return Max(v[i - 1] + KnapsackRec(i - 1, v, s, D - s[i-1]), KnapsackRec(i - 1, v, s, D));
            return KnapsackRec(i - 1, v, s, D);
        }

        static int KnapsackIter(int[] v, int[] s, int D)
        {
            int t = v.Length + 1;
            int B = D + 1;
            int[,] OPT = new int[t, B];

            for (int i = 0; i < B; i++)
                OPT[0, i] = 0;
            for (int i = 1; i < t; i++)
            {
                for (int d = 0; d <= D; d++)
                    if (s[i-1] <= d)
                    {
                        OPT[i, d] = Max(v[i - 1] + OPT[i - 1, d - s[i - 1]], OPT[i - 1, d]);
                    }
                    else
                    {
                        OPT[i, d] = OPT[i - 1, d];
                    }
            }
            return OPT[t - 1, B - 1];
        }

        static void Print2DArray(int[,] arr, int l, int h)
        {
            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < h; j++)
                    Console.Write($"{arr[i,j],2} ");
                Console.Write("\n");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmai_Lab2_4
{
    class Program
    {
        class CustomData
        {
            public int[] v;
            public int[] s;
            public int D;
            public int i;

            public int result;

            public CustomData(int [] v, int [] s, int D)
            {
                this.v = v;
                this.s = s;
                this.D = D;
                i = v.Length;
                result = 0;
            }

            public CustomData(int i, int[] v, int[] s, int D)
            {
                this.v = v;
                this.s = s;
                this.D = D;
                this.i = i;
                result = 0;
            }
        }


        static void Main(string[] args)
        {
            int[] v = new int[] { 120, 150, 80, 100 };
            int[] s = new int[] { 10, 20, 30, 20 };
            int D = 50;
            int i = v.Length;

            Console.WriteLine(KnapsackRec(i, v, s, D));
            Console.WriteLine(ParallelKnapsac(i, v, s, D));
            Console.WriteLine(KnapsackRecP(i, v, s, D));
            Console.WriteLine();
            Console.WriteLine(KnapsackRec(6, new int[] { 1, 3, 4, 3, 2, 10 }, new int[] { 5, 1, 1, 3, 2, 0 }, 6));
            Console.WriteLine(ParallelKnapsac(6, new int[] { 1, 3, 4, 3, 2, 10 }, new int[] { 5, 1, 1, 3, 2, 0 }, 6));
            Console.WriteLine(KnapsackRecP(6, new int[] { 1, 3, 4, 3, 2, 10 }, new int[] { 5, 1, 1, 3, 2, 0 }, 6));
            Console.WriteLine();
            v = new int[] { 92, 57, 49, 68, 60, 43, 67, 84, 87, 72 };
            s = new int[] { 23, 31, 29, 44, 53, 38, 63, 85, 59, 82 };
            D = 165;
            i = v.Length;
            Console.WriteLine(KnapsackRec(i, v, s, D));
            Console.WriteLine(ParallelKnapsac(i, v, s, D));
            Console.WriteLine(KnapsackRecP(i, v, s, D));
            Console.WriteLine();
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

            List<int> vertes = new List<int>(new int [] { 360, 83, 59, 130, 431, 67, 230, 52, 93,
            125, 670, 892, 600, 38, 48, 147, 78, 256,
            63, 17, 120, 164, 432, 35, 92, 110, 22 });
            List<int> svoriai = new List<int>(new int[] { 7, 0, 30, 22, 80, 94, 11, 81, 70,
            64, 59, 18, 0, 36, 3, 8, 15, 42,
            9, 0, 42, 47, 52, 32, 26, 48, 55 });

            long sequentialTotal;
            long parallelTotal;

            if (vertes.Count != svoriai.Count)
                throw new ArgumentException();
            var stopWatch = new Stopwatch();
            for (int k = 0; k < 12; k++)
            {
                int[] vv = vertes.ToArray();
                int[] ss = svoriai.ToArray();
                stopWatch.Reset();
                stopWatch.Start();
                Console.WriteLine(KnapsackRec(vv.Length, vv, ss, D));
                KnapsackRec(vv.Length, vv, ss, D);
                stopWatch.Stop();
                sequentialTotal = stopWatch.ElapsedMilliseconds;
                stopWatch.Reset();
                stopWatch.Start();
                Console.WriteLine(KnapsackRecPar(vv.Length, vv, ss, D));
                KnapsackRecPar(vv.Length, vv, ss, D);
                stopWatch.Stop();
                parallelTotal = stopWatch.ElapsedMilliseconds;
                Console.Write($"{vv.Length};{sequentialTotal};{parallelTotal}\n");
                vertes.Add(v[vv.Length]);
                svoriai.Add(s[vv.Length]);
            }
            //stopWatch.Reset();
            //stopWatch.Start();
            //Console.WriteLine(KnapsackRec(i, v, s, D));
            //stopWatch.Stop();
            //Console.WriteLine("Time in milliseconds for sequential: {0,6:N0} ", stopWatch.ElapsedMilliseconds);
            //stopWatch.Reset();
            //stopWatch.Start();
            //Console.WriteLine(ParallelKnapsac(i, v, s, D));
            //stopWatch.Stop();
            //Console.WriteLine("Time in milliseconds for parallel: {0,6:N0} ", stopWatch.ElapsedMilliseconds);
            //stopWatch.Reset();
            //stopWatch.Start();
            //Console.WriteLine(KnapsackRecPar(i, v, s, D));
            //stopWatch.Stop();
            //Console.WriteLine("Time in milliseconds for parallel dumb2: {0,6:N0} ", stopWatch.ElapsedMilliseconds);
        }

        static int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        static int KnapsackRec(int i, int[] v, int[] s, int D)
        {
            if (i == 0 || D == 0)
                return 0;

            if (s[i - 1] <= D)
                return Max(v[i - 1] + KnapsackRec(i - 1, v, s, D - s[i - 1]), KnapsackRec(i - 1, v, s, D));

            return KnapsackRec(i - 1, v, s, D);
        }

        static int KnapsackRecP(int i, int[] v, int[] s, int D)
        {
            if (i == 0 || D == 0)
                return 0;
            if (s[i - 1] <= D)
            {
                Task[] tasks = new Task[2];
                tasks[0] = Task.Factory.StartNew(
                    (object p) =>
                    {
                        CustomData data = (CustomData)p;
                        data.result = KnapsackRecP(data.i, data.v, data.s, data.D);
                    }, new CustomData(i - 1, v, s, D - s[i - 1]));
                tasks[1] = Task.Factory.StartNew(
                    (object p) =>
                    {
                        CustomData data = (CustomData)p;
                        data.result = KnapsackRecP(data.i, data.v, data.s, data.D);
                    }, new CustomData(i - 1, v, s, D));
                Task.WaitAll(tasks);
                return Max(v[i - 1] + (tasks[0].AsyncState as CustomData).result, (tasks[1].AsyncState as CustomData).result);
            }
            return KnapsackRecP(i - 1, v, s, D);
        }

        static int KnapsackRecPar(int i, int[] v, int[] s, int D)
        {
            if (i == 0 || D == 0)
                return 0;
            Task[] tasks = new Task[2];
            tasks[0] = Task.Factory.StartNew(
                (object p) =>
                {
                    CustomData data = (CustomData)p;
                    data.result = KnapsackRec(data.i, data.v, data.s, data.D);
                }, new CustomData(i - 1, v, s, D - s[i - 1]));
            tasks[1] = Task.Factory.StartNew(
                (object p) =>
                {
                    CustomData data = (CustomData)p;
                    data.result = KnapsackRec(data.i, data.v, data.s, data.D);
                }, new CustomData(i - 1, v, s, D));
            //tasks[2] = Task.Factory.StartNew(
            //    (object p) =>
            //    {
            //        CustomData data = (CustomData)p;
            //        data.result = KnapsackRec(data.i, data.v, data.s, data.D);
            //    }, new CustomData(i - 1, v, s, D));
            Task.WaitAll(tasks);
            if (s[i - 1] <= D)
                return Max(v[i - 1] + (tasks[0].AsyncState as CustomData).result, (tasks[1].AsyncState as CustomData).result);
            return (tasks[1].AsyncState as CustomData).result;
        }

        static int ParallelKnapsac(int i, int[] v, int [] s, int D)
        {
            if (i < 6)
                return KnapsackRec(i, v, s, D);

            int value = 0;
            int CPUcount = 4;
            Task[] tasks = new Task[CPUcount];

            int qD = D / 4;
            int qi = i / 4;

            int lastCPU = CPUcount - 1;

            for (int j = 0; j < CPUcount; j++)
            {
                int arrLen = j == lastCPU ? v.Length - 3 * qi : qi;
                int sIndex = j * qi;
                int dVal = j == lastCPU ? D - 3 * qD : qD;

                int[] vv = new int[arrLen];
                Array.Copy(v, sIndex, vv, 0, arrLen);
                int[] ss = new int[arrLen];
                Array.Copy(s, sIndex, ss, 0, arrLen);
                tasks[j] = Task.Factory.StartNew(
                    (object p) =>
                    {
                        CustomData data = (CustomData)p;
                        data.result = KnapsackRec(data.i, data.v, data.s, data.D);
                    },new CustomData(vv, ss, dVal));
                //new CustomData(v, s, D)
            }
            Task.WaitAll(tasks);
            value = (tasks[0].AsyncState as CustomData).result
                  + (tasks[1].AsyncState as CustomData).result
                  + (tasks[2].AsyncState as CustomData).result
                  + (tasks[3].AsyncState as CustomData).result;

            return value;
        }

        static void Work(Object o)
        {
            CustomData data = (CustomData)o;
            data.result = KnapsackRec(data.i, data.v, data.s, data.D);
        }
    }
}

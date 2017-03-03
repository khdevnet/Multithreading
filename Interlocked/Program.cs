using System;
using System.Threading;

namespace InterlockedExample
{
    public class Program
    {
        private const int NumThreadIterations = 5;
        private const int NumThreads = 10;

        //0 for false, 1 for true.
        private static int usingResource;

        public static void Main()
        {
            Thread myThread;
            var rnd = new Random();

            for (var i = 0; i < NumThreads; i++)
            {
                myThread = new Thread(MyThreadProc)
                {
                    Name = $"Thread{i + 1}"
                };

                //Wait a random amount of time before starting next thread.
                Thread.Sleep(rnd.Next(0, 1000));
                myThread.Start();
            }

            Console.ReadLine();
        }

        private static void MyThreadProc()
        {
            for (var i = 0; i < NumThreadIterations; i++)
            {
                UseResource();

                //Wait 1 second before next attempt.
                Thread.Sleep(1000);
            }
        }

        //A simple method that denies reentrancy.
        private static void UseResource()
        {
            //0 indicates that the method is not in use.

            while (Interlocked.Exchange(ref usingResource, 1) == 1)
            {
                Console.WriteLine("   {0} waiting", Thread.CurrentThread.Name);
            }

            if (Interlocked.Exchange(ref usingResource, 1) == 0)
            {
                Console.WriteLine("{0} acquired the lock", Thread.CurrentThread.Name);

                //Code to access a resource that is not thread safe would go here.

                //Simulate some work
                Thread.Sleep(500);

                Console.WriteLine("{0} exiting lock", Thread.CurrentThread.Name);

                //Release the lock
                Interlocked.Exchange(ref usingResource, 0);
            }

            Console.WriteLine("   {0} was denied the lock", Thread.CurrentThread.Name);
        }
    }
}
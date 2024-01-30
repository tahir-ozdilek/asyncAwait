using System.Diagnostics;

namespace asyncAwait
{
    internal class Program
    {
        static void functionForThread() 
        { 
            for(int i = 0; i<1000; i++)
            {
                Console.WriteLine("Thread No: " + Thread.CurrentThread.ManagedThreadId + "    Output Number: " +i);
            }
        }
        
        static async Task Main(string[] args)
        {
            List<Thread> threads = new List<Thread>();
            //Thread example
            for (int i = 0; i<5; i++) 
            {
                Thread t = new Thread(functionForThread);
                t.Priority = (ThreadPriority)i;
                t.Start();
                threads.Add(t);
            }

            foreach(Thread thread in threads)
            {
                thread.Join();
            }

            //Task example
            Egg eg = new();
            Bacon bac = new();
            Pat pat = new();

            Stopwatch w = new();
            w.Start();
            Task<Egg> eggsTask = eg.FryEggsAsync();
            Task<Bacon> baconTask = bac.FryBaconAsync();
            Task<Pat> patTask = pat.FryPatAsync();

            Egg eggs = await eggsTask;
            Console.WriteLine("Eggs are ready");
            Bacon bacon = await baconTask;
            Console.WriteLine("Bacon is ready");
            Pat pato = await patTask;
            Console.WriteLine("Pat is ready");
            w.Stop();
            Console.WriteLine(w.Elapsed.ToString());


            Stopwatch w2 = new();
            w2.Start();
            Egg eg2 = await eg.FryEggsAsync();
            Console.WriteLine("Eggs are ready");
            Bacon bac2 = await bac.FryBaconAsync();
            Console.WriteLine("Bacon is ready");
            Pat pat2 = await pat.FryPatAsync();
            Console.WriteLine("Pat is ready");
            w2.Stop();
            Console.WriteLine(w2.Elapsed.ToString());
        }
    }

    public class Egg
    {
        public async Task<Egg> FryEggsAsync()
        {
            Console.WriteLine("Egg is being prepared.");
            await Task.Delay(3000);

            return new Egg();
        }
    }

    public class Bacon
    {
        public async Task<Bacon> FryBaconAsync()
        {
            Console.WriteLine("Bacon are being prepared.");
            await Task.Delay(3000);
            return new Bacon();
        }
    }

    public class Pat
    {
        public async Task<Pat> FryPatAsync()
        {
            Console.WriteLine("Pat is being Pat.");
            await Task.Delay(3000);
            return new Pat();
        }
    }
}
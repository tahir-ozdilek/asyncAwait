﻿using System.Diagnostics;

namespace asyncAwait
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
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
            await Task.Delay(5000);

            return new Egg();
        }
    }

    public class Bacon
    {
        public async Task<Bacon> FryBaconAsync()
        {
            await Task.Delay(5000);
            return new Bacon();
        }
    }
    public class Pat
    {
        public async Task<Pat> FryPatAsync()
        {
            await Task.Delay(5000);
            return new Pat();
        }
    }
}
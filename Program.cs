using System;

namespace TOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till Tjuv och Polis!");
            Console.WriteLine("Tryck på valfri knapp för att fortsätta");
            Console.ReadKey();

            Map map = new Map();
            map.PopulateCity(100, 25, 25, 15, 15);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace RoboInteligente
{
    class Program
    {
        private static int[,] grid = new int[5, 5];

        static void Main(string[] args)
        {
            RoboColetor roboColetor = new RoboColetor();

            while (roboColetor.ColetarLixo())
            {

            }

            Console.ReadKey();
        }
    }
}

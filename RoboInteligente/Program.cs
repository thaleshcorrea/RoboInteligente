using System;
using System.Collections.Generic;

namespace RoboInteligente
{
    class Program
    {
        private static int[,] grid = new int[5, 5];

        static void Main(string[] args)
        {
            RoboColetor roboColetor = new RoboColetor();

            while (roboColetor.ColetarLixo() > 0 || roboColetor.armazenamentoRobo > 0)
            {
                List<Expandir> filaPrioridade = roboColetor.FuncaoAvaliacao();
                string acao = roboColetor.MoverRobo(filaPrioridade);
                Console.WriteLine(acao);
            }

            Console.WriteLine($"{Environment.NewLine}Carga: {roboColetor.cargaAtualRobo}/20");

            Console.ReadKey();
        }
    }
}

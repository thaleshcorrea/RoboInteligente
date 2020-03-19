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

            while ((roboColetor.ColetarLixo() > 0 || roboColetor.armazenamentoRobo > 0) && roboColetor.cargaAtualRobo > 0)
            {
                List<Expandir> filaPrioridade = roboColetor.FuncaoAvaliacao();
                string acao = roboColetor.IrParaObjetivo(filaPrioridade);
                Console.WriteLine(acao);
            }

            Console.WriteLine($"{Environment.NewLine}ROBÔ:");
            Console.WriteLine($"POSICÃO ATUAL: ({roboColetor.linhaRobo}/{roboColetor.colunaRobo})");
            Console.WriteLine($"CARGA ATUAL: {roboColetor.cargaAtualRobo}/20");

            Console.ReadKey();
        }
    }
}

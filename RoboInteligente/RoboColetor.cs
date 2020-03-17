using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboInteligente
{
    public class RoboColetor
    {
        private const int COLUNAS = 5;
        private const int LINHAS = 5;
        private const int ARMAZENAMENTO_MAX_ROBO = 2;

        private int linhaRobo = 0;
        private int colunaRobo = 0;
        private int cargaAtualRobo = 20;
        private int armazenamentoRobo = 0;

        private int linhaLixo1 = 1;
        private int linhaLixo2 = 2;
        private int linhaLixo3 = 3;
        private int colunaLixo1 = 3;
        private int colunaLixo2 = 4;
        private int colunaLixo3 = 1;

        private int linhaColeta1 = 0;
        private int linhaColeta2 = 4;
        private int linhaColeta3 = 4;
        private int colunaColeta1 = 4;
        private int colunaColeta2 = 4;
        private int colunaColeta3 = 0;

        public int[,] grid;

        List<expandir> filaPrioridade;

        public RoboColetor()
        {
            CriarGridInicial();
        }

        public void CriarGridInicial()
        {
            grid = new int[LINHAS, COLUNAS];

            //Setar VAZIO para toda a matriz

            for (int i = 0; i < LINHAS; i++)
            {
                for (int j = 0; j < COLUNAS; j++)
                {
                    grid[i, j] = (int)EEstados.VAZIO;
                }
            }

            InserirRobo();
            InserirPostosDeColeta();
            InserirLixos();
        }

        private void InserirRobo()
        {
            grid[linhaRobo, colunaRobo] = (int)EEstados.ROBO;
        }

        private void InserirPostosDeColeta()
        {
            grid[linhaColeta1, colunaColeta1] = (int)EEstados.COLETA;
            grid[linhaColeta2, colunaColeta2] = (int)EEstados.COLETA;
            grid[linhaColeta3, colunaColeta3] = (int)EEstados.COLETA;
        }

        private void InserirLixos()
        {
            grid[linhaLixo1, colunaLixo1] = (int)EEstados.LIXO;
            grid[linhaLixo2, colunaLixo2] = (int)EEstados.LIXO;
            grid[linhaLixo3, colunaLixo3] = (int)EEstados.LIXO;
        }

        /// <summary>
        /// Verificar se ainda possui lixo para coletar
        /// </summary>
        public bool ColetarLixo()
        {
            for (int i = 0; i < LINHAS; i++)
            {
                for (int j = 0; j < COLUNAS; j++)
                {
                    if (grid[i, j] == (int)EEstados.LIXO)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Verificar o próximo nó a ser expandido
        /// </summary>
        public void FuncaoAvaliacao()
        {
            // Verificar se robo está cheio
            // Se não (Verfica qual o próximo lixo para expandir)
            // Se sim (Verifica qual o ponto de coleta para expandir)
            filaPrioridade = new List<expandir>();

            if (armazenamentoRobo < ARMAZENAMENTO_MAX_ROBO)
            {
                for (int i = 0; i < LINHAS; i++)
                {
                    for (int j = 0; j < COLUNAS; j++)
                    {

                    }
                }
            }
            else
            {

            }
        }

        public void CalcularCusto(int linha, int coluna)
        {

        }

        public void ReeordenarFila()
        {

        }

        public void MoverRobo()
        {

        }
    }
}

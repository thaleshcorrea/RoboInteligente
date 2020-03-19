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

        public int linhaRobo = 0;
        public int colunaRobo = 0;
        public int cargaAtualRobo = 20;
        public int armazenamentoRobo = 0;

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

        public int[,] matriz;
        public int[,] matrizHeuristica;

        List<Expandir> fila = new List<Expandir>();

        public RoboColetor()
        {
            CriarGridInicial();
        }

        /// <summary>
        /// Setar valores iniciais (matrizes e posição dos elementos)
        /// </summary>
        public void CriarGridInicial()
        {
            matriz = new int[LINHAS, COLUNAS];
            matrizHeuristica = new int[LINHAS, COLUNAS];

            //Setar VAZIO para toda a matriz

            for (int i = 0; i < LINHAS; i++)
            {
                for (int j = 0; j < COLUNAS; j++)
                {
                    matriz[i, j] = (int)EEstados.VAZIO;
                }
            }

            InserirRobo();
            InserirPostosDeColeta();
            InserirLixos();
        }

        /// <summary>
        /// Atribuir o valor do robo na matriz
        /// </summary>
        private void InserirRobo()
        {
            matriz[linhaRobo, colunaRobo] = (int)EEstados.ROBO;
            matrizHeuristica[linhaRobo, colunaRobo] = (int)EEstados.ROBO;
        }

        /// <summary>
        /// Atribuir os postos de coleta na matriz
        /// </summary>
        private void InserirPostosDeColeta()
        {
            matriz[linhaColeta1, colunaColeta1] = (int)EEstados.COLETA;
            matriz[linhaColeta2, colunaColeta2] = (int)EEstados.COLETA;
            matriz[linhaColeta3, colunaColeta3] = (int)EEstados.COLETA;

            matrizHeuristica[linhaColeta1, colunaColeta1] = (int)EEstados.COLETA;
            matrizHeuristica[linhaColeta2, colunaColeta2] = (int)EEstados.COLETA;
            matrizHeuristica[linhaColeta3, colunaColeta3] = (int)EEstados.COLETA;
        }

        /// <summary>
        /// Atribuir os materiais(lixos) na matriz
        /// </summary>
        private void InserirLixos()
        {
            matriz[linhaLixo1, colunaLixo1] = (int)EEstados.LIXO;
            matriz[linhaLixo2, colunaLixo2] = (int)EEstados.LIXO;
            matriz[linhaLixo3, colunaLixo3] = (int)EEstados.LIXO;

            matrizHeuristica[linhaLixo1, colunaLixo1] = (int)EEstados.LIXO;
            matrizHeuristica[linhaLixo2, colunaLixo2] = (int)EEstados.LIXO;
            matrizHeuristica[linhaLixo3, colunaLixo3] = (int)EEstados.LIXO;
        }

        /// <summary>
        /// Verificar se ainda possui lixo para coletar
        /// </summary>
        public int ColetarLixo()
        {
            int contador = 0;

            for (int i = 0; i < LINHAS; i++)
            {
                for (int j = 0; j < COLUNAS; j++)
                {
                    if (matriz[i, j] == (int)EEstados.LIXO)
                    {
                        contador++;
                    }
                }
            }
            return contador;
        }

        /// <summary>
        /// Verificar o próximo nó a ser expandido
        /// Todos as condições ele retorna um custo que é adiciona a uma fila
        /// Para pegar o menor caminho até o destino
        /// </summary>
        public List<Expandir> FuncaoAvaliacao()
        {
            int lixo1 = matrizHeuristica[linhaLixo1, colunaLixo1];
            int lixo2 = matrizHeuristica[linhaLixo2, colunaLixo2];
            int lixo3 = matrizHeuristica[linhaLixo3, colunaLixo3];

            List<Expandir> filaPrioridade = new List<Expandir>();
            int lixosRestantes = ColetarLixo();

            // IF Verificar se robo está cheio
            // ELSE IF quando o armazenamento está cheio e está faltando lixos para serem coletados ainda
            // ELSE executa quando não tem nenhum lixo para ser coletado, e o possui lixos no armazenamento do robô
            if (armazenamentoRobo < ARMAZENAMENTO_MAX_ROBO)
            {
                bool lixoColetado1 = (lixo1 != (int)EEstados.LIXO); // verificar se lixo ja foi coletado
                bool lixoColetado2 = (lixo2 != (int)EEstados.LIXO); // verificar se lixo ja foi coletado
                bool lixoColetado3 = (lixo3 != (int)EEstados.LIXO); // verificar se lixo ja foi coletado


                // se o robo não tiver carregando nada
                // senão, verifica se o armazenamento do robô é 1 e possui lixos restantes
                // senão, não possui mais lixos para serem coletados e armazenamento não está vazio
                if (armazenamentoRobo == 0)
                {
                    // se tiver mais de um lixo para coletar
                    if (lixosRestantes > 1)
                    {
                        if (!lixoColetado1) //IF para ver se item não foi coletado
                        {
                            filaPrioridade.AddRange(RotasLixoComArmazenamentoZerado(linhaLixo1, colunaLixo1));
                        }
                        if (!lixoColetado2) //IF para ver se item não foi coletado
                        {
                            filaPrioridade.AddRange(RotasLixoComArmazenamentoZerado(linhaLixo2, colunaLixo2));
                        }
                        if (!lixoColetado3) //IF para ver se item não foi coletado
                        {
                            filaPrioridade.AddRange(RotasLixoComArmazenamentoZerado(linhaLixo3, colunaLixo3));
                        }
                    }
                    else
                    {
                        if (!lixoColetado1) //IF para ver se item não foi coletado
                        {
                            filaPrioridade.AddRange(RotasPostosDeColetaComArmazenamentoQuaseCheio(linhaLixo1, colunaLixo1));
                        }
                        if (!lixoColetado2) //IF para ver se item não foi coletado
                        {
                            filaPrioridade.AddRange(RotasPostosDeColetaComArmazenamentoQuaseCheio(linhaLixo2, colunaLixo2));
                        }
                        if (!lixoColetado3) //IF para ver se item não foi coletado
                        {
                            filaPrioridade.AddRange(RotasPostosDeColetaComArmazenamentoQuaseCheio(linhaLixo3, colunaLixo3));
                        }
                    }
                }
                else if (armazenamentoRobo == 1 && lixosRestantes > 0)
                {
                    if (!lixoColetado1) //IF para ver se item não foi coletado
                    {
                        filaPrioridade.AddRange(RotasPostosDeColetaComArmazenamentoQuaseCheio(linhaLixo1, colunaLixo1));
                    }
                    if (!lixoColetado2) //IF para ver se item não foi coletado
                    {
                        filaPrioridade.AddRange(RotasPostosDeColetaComArmazenamentoQuaseCheio(linhaLixo2, colunaLixo2));
                    }
                    if (!lixoColetado3) //IF para ver se item não foi coletado
                    {
                        filaPrioridade.AddRange(RotasPostosDeColetaComArmazenamentoQuaseCheio(linhaLixo3, colunaLixo3));
                    }
                }
                else 
                {
                    filaPrioridade.AddRange(RotasPostosDeColeta(linhaRobo, colunaRobo));
                }
            }
            else if (lixosRestantes > 0)
            {
                filaPrioridade.AddRange(RotasPostosDeColetaComLixoRestante(linhaColeta1, colunaColeta1));
                filaPrioridade.AddRange(RotasPostosDeColetaComLixoRestante(linhaColeta2, colunaColeta2));
                filaPrioridade.AddRange(RotasPostosDeColetaComLixoRestante(linhaColeta3, colunaColeta3));
            }
            else
            {
                filaPrioridade.AddRange(RotasPostosDeColeta(linhaRobo, colunaRobo));
            }

            filaPrioridade = filaPrioridade.OrderBy(x => x.custo).ToList(); // Ordenar a fila pelo menor custo
            return filaPrioridade;
        }

        /// <summary>
        /// Mover o robô para 
        /// </summary>
        public string MoverRobo(List<Expandir> filaPrioridade)
        {
            Expandir expandir = filaPrioridade.FirstOrDefault();
            expandir.custo = CalcularCusto(linhaRobo, colunaRobo, expandir.linha, expandir.coluna);

            linhaRobo = expandir.linha;
            colunaRobo = expandir.coluna;

            if(cargaAtualRobo < expandir.custo)
            {

            }

            if (expandir.acao == EAcoes.PegarLixo)
            {
                armazenamentoRobo++;
                matrizHeuristica[expandir.linha, expandir.coluna] = (int)EEstados.ROBO;
            }
            else if (expandir.acao == EAcoes.FazerColeta)
            {
                armazenamentoRobo = 0;
            }

            matriz = matrizHeuristica;

            cargaAtualRobo -= expandir.custo;

            return $"Carga(-{expandir.custo}) (Mover robo para linha({expandir.linha}) coluna({expandir.coluna}): {expandir.acao})";
        }

        /// <summary>
        /// (Só executar quando está faltando um material para encher o armazenamento do robô)
        /// 
        /// Calcular o custo até o lixo objetivo
        /// Depois varre a lista procurando os postos de coletas mais perto dele
        /// </summary>
        private List<Expandir> RotasPostosDeColetaComArmazenamentoQuaseCheio(int linha, int coluna)
        {
            List<Expandir> filaPrioridade = new List<Expandir>();

            int custoTotal = CalcularCusto(linhaRobo, colunaRobo, linha, coluna);

            for (int i = 0; i < LINHAS; i++)
            {
                for (int j = 0; j < COLUNAS; j++)
                {
                    if (matrizHeuristica[i, j] == (int)EEstados.COLETA)
                    {
                        Expandir _expandir = new Expandir { linha = linha, coluna = coluna, custo = custoTotal, acao = EAcoes.PegarLixo };
                        _expandir.custo += CalcularCusto(linha, coluna, i, j);

                        filaPrioridade.Add(_expandir);
                    }
                }
            }

            ResetarHeuristica();
            return filaPrioridade;
        }

        /// <summary>
        /// Verifica qual o ponto de coleta mais próximo do robô para a coleta (Quando não tem nenhum lixo restante)
        /// </summary>
        private List<Expandir> RotasPostosDeColeta(int linha, int coluna)
        {
            List<Expandir> filaPrioridade = new List<Expandir>();

            for (int i = 0; i < LINHAS; i++)
            {
                for (int j = 0; j < COLUNAS; j++)
                {
                    if (matrizHeuristica[i, j] == (int)EEstados.COLETA)
                    {
                        int custoTotal = CalcularCusto(linha, coluna, i, j);
                        Expandir _expandir = new Expandir { linha = i, coluna = j, custo = custoTotal, acao = EAcoes.FazerColeta };

                        filaPrioridade.Add(_expandir);
                    }
                }
            }

            ResetarHeuristica();
            return filaPrioridade;
        }

        /// <summary>
        /// Calcular o custo até o posto de coleta mais próximo
        /// Depois varre a matriz calculando o custo do objetivo até os próximos lixos a serem coletados
        /// Somando os 2 para ver qual a melhor opção
        /// </summary>
        private List<Expandir> RotasPostosDeColetaComLixoRestante(int linha, int coluna)
        {
            List<Expandir> filaPrioridade = new List<Expandir>();

            int custoObjetivo = CalcularCusto(linhaRobo, colunaRobo, linha, coluna);

            for (int i = 0; i < LINHAS; i++)
            {
                for (int j = 0; j < COLUNAS; j++)
                {
                    if (matrizHeuristica[i, j] == (int)EEstados.LIXO)
                    {
                        int custoTotal = CalcularCusto(linha, coluna, i, j) + custoObjetivo;
                        Expandir _expandir = new Expandir { linha = linha, coluna = coluna, custo = custoTotal, acao = EAcoes.FazerColeta };

                        filaPrioridade.Add(_expandir);
                    }
                }
            }

            ResetarHeuristica();
            return filaPrioridade;
        }

        /// <summary>
        /// Calcular o custo até o lixo objetivo
        /// Depois varre a matriz calculando o custo do objetivo até os próximos lixos a serem coletados
        /// Somando os dois e retonando o custo
        /// </summary>
        private List<Expandir> RotasLixoComArmazenamentoZerado(int linha, int coluna)
        {
            List<Expandir> filaPrioridade = new List<Expandir>();

            int custoObjetivo = CalcularCusto(linhaRobo, colunaRobo, linha, coluna);
            matrizHeuristica[linha, coluna] = (int)EEstados.VAZIO;

            for (int i = 0; i < LINHAS; i++)
            {
                for (int j = 0; j < COLUNAS; j++)
                {
                    if (i == linha && j == coluna)
                    {
                        continue;
                    }
                    if (matrizHeuristica[i, j] == (int)EEstados.LIXO)
                    {
                        Expandir _expandir = new Expandir { linha = linha, coluna = coluna, acao = EAcoes.PegarLixo };
                        _expandir.custo = CalcularCusto(linha, coluna, i, j) + custoObjetivo;
                        matrizHeuristica[i, j] = (int)EEstados.VAZIO;

                        filaPrioridade.Add(_expandir);
                    }
                }
            }

            ResetarHeuristica();
            return filaPrioridade;
        }



        /// <summary>
        /// Calcular qual o gasto de energia para chegar até o ponto objetivo informado
        /// </summary>
        public int CalcularCusto(int linhaAtual, int colunaAtual, int linhaObjetivo, int colunaObjetivo)
        {
            int custoGasto = 0;

            custoGasto += CustoLinha(linhaAtual, linhaObjetivo, linhaAtual, colunaAtual);
            custoGasto += CustoColuna(colunaAtual, colunaObjetivo, colunaAtual, linhaObjetivo);

            if (matrizHeuristica[linhaObjetivo, colunaObjetivo] == (int)EEstados.LIXO)
            {
                custoGasto += (int)ECustos.PegarItem;
            }
            return custoGasto;
        }

        /// <summary>
        /// Percorre as linhas da matriz pegando os valores contidos em cada casa para calcular o quando de energia foi gastado 
        /// </summary>
        private int CustoLinha(int linhaInicio, int linhaObjetivo, int linhaAtual, int coluna)
        {
            if (linhaInicio < linhaObjetivo)
            {
                linhaAtual = linhaAtual + 1;
            }
            else
            {
                linhaAtual = linhaAtual - 1;
            }

            if (linhaInicio == linhaObjetivo)
            {
                return 0;
            }
            if (linhaAtual == linhaObjetivo)
            {
                if (matrizHeuristica[linhaAtual, coluna] == (int)EEstados.VAZIO)
                {
                    return +(int)ECustos.MoverVazio;
                }
                else
                {
                    return +(int)ECustos.MoverCheio;
                }
            }
            else
            {
                if (matrizHeuristica[linhaAtual, coluna] == (int)EEstados.VAZIO)
                {
                    return CustoLinha(linhaInicio, linhaObjetivo, linhaAtual, coluna) + (int)ECustos.MoverVazio;
                }
                else
                {
                    return CustoLinha(linhaInicio, linhaObjetivo, linhaAtual, coluna) + (int)ECustos.MoverCheio;
                }
            }
        }

        /// <summary>
        /// Percorre as colunas da matriz pegando os valores contidos em cada casa para calcular o quando de energia foi gastado 
        /// </summary>
        private int CustoColuna(int colunaInicio, int colunaObjetivo, int colunaAtual, int linha)
        {
            if (colunaInicio < colunaObjetivo)
            {
                colunaAtual = colunaAtual + 1;
            }
            else
            {
                colunaAtual = colunaAtual - 1;
            }
            if (colunaInicio == colunaObjetivo)
            {
                return 0;
            }
            if (colunaAtual == colunaObjetivo)
            {
                if (matrizHeuristica[linha, colunaAtual] == (int)EEstados.VAZIO)
                {
                    return +(int)ECustos.MoverVazio;
                }
                else
                {
                    return +(int)ECustos.MoverCheio;
                }
            }
            else
            {
                if (matrizHeuristica[linha, colunaAtual] == (int)EEstados.VAZIO)
                {
                    return CustoColuna(colunaInicio, colunaObjetivo, colunaAtual, linha) + (int)ECustos.MoverVazio;
                }
                else
                {
                    return CustoColuna(colunaInicio, colunaObjetivo, colunaAtual, linha) + (int)ECustos.MoverCheio;
                }
            }
        }

        /// <summary>
        /// Voltar matriz para o estado original dela
        /// </summary>
        public void ResetarHeuristica()
        {
            for (int i = 0; i < LINHAS; i++)
            {
                for (int j = 0; j < COLUNAS; j++)
                {
                    matrizHeuristica[i, j] = matriz[i, j];
                }
            }
        }
    }
}

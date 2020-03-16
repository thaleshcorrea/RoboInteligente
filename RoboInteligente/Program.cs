using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace RoboInteligente
{
    class Program
    {
        private static int[,] ambiente = new int[5, 5];

        static void Main(string[] args)
        {
            DefinirAmbiente();
            Tree tree = ConstruirArvore();

            Console.ReadKey();
        }

        static void DefinirAmbiente()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    bool posicaoRobo = (i == 0 && j == 0);
                    bool posicaoPontosColetas = (i == 0 && j == 4) || (i == 4 && j == 0) || (i == 4 && j == 4);
                    bool posicaoMateriais = (i == 1 && j == 3) || (i == 2 && j == 4) || (i == 3 && j == 2);

                    if (posicaoRobo)
                    {
                        ambiente[i, j] = (int)EEstados.ROBO;
                    }
                    else if (posicaoPontosColetas)
                    {
                        ambiente[i, j] = (int)EEstados.COLETA;
                    }
                    else if (posicaoMateriais)
                    {
                        ambiente[i, j] = (int)EEstados.MATERIAL;
                    }
                    else
                    {
                        ambiente[i, j] = (int)EEstados.VAZIO;
                    }
                }
            }
        }

        private static Tree ConstruirArvore()
        {
            Tree tree = new Tree();
            tree.ExpandirNodes(ambiente);

            return tree;
        }
    }



    public class Tree
    {
        public Node root;

        public class Node
        {
            public int valor;

            public Node cima;
            public Node baixo;
            public Node esquerda;
            public Node direita;

            public Node(int _valor)
            {
                valor = _valor;
                cima = null;
                baixo = null;
                esquerda = null;
                direita = null;
            }
        }

        public void ExpandirNodes(int[,] ambiente)
        {
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    root = new Node(ambiente[i, j]);

                    bool expandirCima = (i - 1) > 0;
                    bool expandirBaixo = (i + 1) < 5;
                    bool expandirEsquerda = (j - 1) > 0;
                    bool expandirDireita = (j + 1) < 5;

                    if (expandirCima)
                    {
                        root.cima = new Node(ambiente[(i - 1), j]);
                    }
                    if (expandirBaixo)
                    {
                        root.baixo = new Node(ambiente[(i + 1), j]);
                    }
                    if (expandirEsquerda)
                    {
                        root.esquerda = new Node(ambiente[i, (j - 1)]);
                    }
                    if (expandirDireita)
                    {
                        root.direita = new Node(ambiente[i, (j + 1)]);
                    }
                }
            }
        }
    }
}

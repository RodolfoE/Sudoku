using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Sudoku
{
    class Tabuleiro
    {
        // não faça [i, j] nas interações, faça ao contrário para ficar da mesma forma que no plano cartesiano.
        public int[,] mTabuleiro = new int[9,9];

        public Tabuleiro()
        {
            for (int i = 0; i < mTabuleiro.GetLength(1); i++)
            {
                for (int j = 0; j < mTabuleiro.GetLength(0); j++)
                {
                    mTabuleiro[j, i] = 0;
                }
            }
        }
        public void imprimirTabuleiro()
        {
            for (int i = 0; i < mTabuleiro.GetLength(1); i++)
            {
                for (int j = 0; j < mTabuleiro.GetLength(0); j++)
                {
                    Console.Write("  " + mTabuleiro[j, i] + "  |");   
                }
                Console.Write("\n\n\n");
            }

        }

        public bool inserirNoTabuleiro(int[] coordenada, int valor)
        {
            //se não existir valor nenhum na vertical e na horizontal, faça...
            if (!(existeValorNaVertical(coordenada[1], valor) || existeValorNaHorizontal(coordenada[0], valor)))
            {
                mTabuleiro[coordenada[0], coordenada[1]] = valor;
            }
            else
            {
                MessageBox.Show("Valor já existe na vertical ou na horizontal");
            }
            return true;
        }

        public bool existeValorNaVertical(int y, int valor)
        {
            for (int i = 0; i < mTabuleiro.GetLength(0); i++)
            {
                if (mTabuleiro[i, y] == valor)
                    return true;
            }
            return false;
        }
       
        public bool existeValorNaHorizontal(int x, int valor)
        {
            for (int i = 0; i < mTabuleiro.GetLength(0); i++)
            {
                if (mTabuleiro[x, i] == valor)
                    return true;
            }
            return false;
        }
    }

    //classe responsável pela lógica da aplicação. 
    class Jogo : Tabuleiro
    {
        string mCaminhoCSV;

        public Jogo(string caminhoCSV)
        {
            this.mCaminhoCSV = caminhoCSV;
            lerCSV();
        }

        public void lerCSV()
        {
            try
            {
                StreamReader leitor = new StreamReader(Directory.GetCurrentDirectory() + @"\" + mCaminhoCSV);
                String linha;
                int num_linha = 0;

                while ((linha = leitor.ReadLine()) != null)
                {
                    string[] aux = linha.Split(',');
                    for (int i = 0; i < aux.Length; i++)
                    {
                        if (aux[i] == "" || aux[i] == " ")
                            mTabuleiro[i, num_linha] = 0;
                        else
                            mTabuleiro[i, num_linha] = int.Parse(aux[i]);
                    }
                    num_linha++;
                }
                leitor.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void abrirArquivo()
        {
            System.Diagnostics.Process.Start(mCaminhoCSV);
        }
    }

    class Program
    {
        /*
         * 1º argumento - coluna; 2º argumento - linha
         */

        static Jogo mJogo;

        static void menu()
        {
            Console.WriteLine(" *****  Bem vindo ao jogo SUDOKU **** \n Digite:\n[ 1] - para começar\n[-1] - para sair");
            int opcao = int.Parse(Console.ReadLine());

            if (opcao != -1)
            {
                mJogo = new Jogo("csv.txt");
                do
                {
                    mJogo.imprimirTabuleiro();
                    opcao = int.Parse(Console.ReadLine());

                } while (opcao != -1);
            }
        }
        static void Main(string[] args)
        {
            //Jogo ai = new Jogo("csv.txt");
            menu();
        }
    }
}

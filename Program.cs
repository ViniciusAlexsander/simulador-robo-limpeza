using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_do_robo_limpador
{
    class Program
    {
        static bool PercorreuTudo(ref int[,] piso, int L, int C, ref int interacoes)//Função para verificar se o robô ja percorreu toda a matriz
        {
            int andou = 0, totalPiso = 0;
            totalPiso = L * C;
            //For que ira verificar se há um número diferente de 0 na matriz, se houver ele vai contar, e verificar se a matriz esta totalmente preenchida
            for (int l = 0; l < L; l++)
            {
                for (int c = 0; c < C; c++)
                {
                    if (piso[l, c] != 0)
                        andou++;
                }
            }
            if (andou == totalPiso)
            {
                return true;

            }
            else
                return false;
        }
        static void Main(string[] args)
        {
            int[,] piso;
            int L, C, RoboL, RoboC, indRandom, passos = 0, interacoes = 0, andarL, andarC;
            string repetir;
            bool acabar;

            Console.Write("Digite a quantidade de linhas da matriz, sendo entre 2 e 40: ");
            L = int.Parse(Console.ReadLine());
            Console.Write("Digite a quantidade de Colunas da matriz, sendo entre 2 e 20: ");
            C = int.Parse(Console.ReadLine());

            piso = new int[L, C]; //Dando o tamanho a matriz piso
            Console.Write($"Digite em qual linha o robo deve começar entre 0 e {L - 1}: ");
            RoboL = int.Parse(Console.ReadLine());
            Console.Write($"Digite em qual coluna o robo deve começar entre 0 e {C - 1}: ");
            RoboC = int.Parse(Console.ReadLine());
            piso[RoboL, RoboC] += 1; //Ja considera a posição inicial como uma a menos a ser "limpa" (Fiquei em dúvida se era pra fazer assim ou se não era pra considerar o ponto inicial)

            do
            {
                do
                {
                    //Sorteando o número a ser lido no vetor para dar o passo
                    Random aleatorio = new Random();
                    indRandom = aleatorio.Next(0, 7);

                    int[] vaiL = new int[] { -1, 0, 1, 1, 1, 0, -1, -1 };
                    int[] vaiC = new int[] { 1, 1, 1, 0, -1, -1, -1, 0 };
                    //Passando esse número sorteado no vetor para uma variavel 
                    andarL = vaiL[indRandom];
                    andarC = vaiC[indRandom];
                    //verificando se o número sorteado, somado com a posição atual do robô vai resultar em um passo legitimo ou em uma colisão com a parede
                    if ((RoboL + andarL >= 0 && RoboL + andarL <= L - 1) && (RoboC + andarC >= 0 && RoboC + andarC <= C - 1))
                    {
                        //Somando o lugar atual do robô com o número sorteado no vetor
                        RoboL += andarL;
                        RoboC += andarC;
                        //Acrescentando +1 na posição do piso para saber quantas vezes ele ja passou la 
                        piso[RoboL, RoboC] += 1;
                        repetir = "nao";
                        passos++; //Vendo quantos passos ao total o robô ja deu
                        Console.WriteLine($"O robô esta na posição: ({RoboL},{RoboC})"); //Escrevendo a posição atual do robô
                    }
                    else
                        repetir = "sim"; //Fazendo a repetição pois o número sorteado iria resultar em uma colisão

                } while (repetir == "sim");
                //Chamando a função para verificar se o robô ja percorreu todo o piso
                acabar = PercorreuTudo(ref piso, L, C, ref interacoes);
                interacoes++;

            } while (interacoes < 10000 && acabar == false);

            Console.WriteLine($"\nO robô deu {passos} passos. "); //Mostrando quantos passos ao total o robô deu 
            //Verificando se o robô concluiu o trajeto ou não, para poder informar o usuário 
            if (acabar == false)
                Console.WriteLine("\nO robô não foi em todos os lugares da matriz. ");
            else
                Console.WriteLine("\nO robô foi em todos os lugares da matriz. ");
            //Mostrando em forma de matriz quantas vezes o robô passou em cada ponto
            Console.WriteLine("\nA quantidade de vezes que ele visitou cada lugar da matriz foi: ");
            for (int l = 0; l < L; l++)
            {
                Console.WriteLine();
                for (int c = 0; c < C; c++)
                {
                    Console.Write($" {piso[l, c]} |");
                }
            }
            Console.ReadKey();
        }
    }
}

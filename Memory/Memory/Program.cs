using System;
using System.Collections.Generic;
using System.IO;

namespace Memory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wybierz poziom trudności:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Hard");

            if (int.TryParse(Console.ReadLine(), out int poziom))
            {

                List<string> words = new List<string>(File.ReadAllLines("Words.txt"));

                int n = poziom == 1 ? 4 : 8;

                string[] gameWords = generateGameWords(n, words);

                Console.WriteLine("------------------------");

                int wym1 = poziom == 1 ? 2 : 4;
                int wym2 = 4;


                string[,] board = new string[wym1,wym2];

                Random r = new Random();

                foreach (string gw in gameWords)
                {
                    int l = 0;

                    while (l < 2)
                    {
                        int w = r.Next(0, wym1);
                        int k = r.Next(0, wym2);

                        if (board[w, k] == null)
                        {
                            board[w, k] = gw;
                            l++;
                        }
                    }
                }




                string[,] displayBoard = new string[wym1, wym2];

                for (int wi = 0; wi < wym1; wi++)
                {
                    for (int ki = 0; ki < wym2; ki++)
                    {
                        displayBoard[wi, ki] = "X";
                    }
                }

                bool koniec = false;

                int traf = 0;
                int cel = poziom == 1 ? 4 : 8;
                int zycie = poziom == 1 ? 10 : 15;

                while (!koniec)
                {

                    show(displayBoard);

                    int w1 = -1;
                    int k1 = -1;
                    int w2 = -1;
                    int k2 = -1;


                    Console.WriteLine("Podaj współrzędne pola 1:");
                    string wsp1 = Console.ReadLine().ToLower();

                    char[] rows = { 'a', 'b', 'c', 'd' };

                    string s1 = null;

                    if (wsp1.Length > 1)
                    {
                        
                        int wi = Array.IndexOf(rows,wsp1[0]);


                        if (wi > -1 && int.TryParse(wsp1[1].ToString(), out int ki))
                        {
                            if (ki > 0 && ki <= wym2)
                            {
                                ki--;
                                s1 = board[wi, ki];

                                if (displayBoard[wi, ki] == "X")
                                {
                                    displayBoard[wi, ki] = board[wi, ki];

                                    w1 = wi;
                                    k1 = ki;
                                }

                                show(displayBoard);
                            }
                        }
                    }

                    if (s1 != null)
                    {
                        Console.WriteLine("Podaj współrzędne pola 2:");
                        string wsp2 = Console.ReadLine().ToLower();

                        string s2 = null;

                        if (wsp2.Length > 1)
                        {
                            int wi = Array.IndexOf(rows, wsp2[0]);

                            if (wi > -1 && int.TryParse(wsp2[1].ToString(), out int ki))
                            {
                                if (ki > 0 && ki <= wym2)
                                {
                                    ki--;

                                    if (wi == w1 && ki == k1)
                                    {
                                        Console.WriteLine("Wybrałeś te same współrzędne");
                                    }
                                    else
                                    {

                                        if (displayBoard[wi, ki] == "X")
                                        {
                                            s2 = board[wi, ki];
                                            displayBoard[wi, ki] = board[wi, ki];

                                            w2 = wi;
                                            k2 = ki;
                                        }

                                        Console.WriteLine("Życie: " + zycie);
                                        show(displayBoard);
                                        
                                    }
                                }
                            }
                        }

                        if (s1 != s2)
                        {
                            if (w1 > -1 && k1 > -1)
                            {
                                displayBoard[w1, k1] = "X";
                            }

                            if (w2 > -1 && k2 > -1)
                            {
                                displayBoard[w2, k2] = "X";
                            }

                            zycie--;
                        }
                        else
                        {
                            traf++;
                        }

                        if(zycie <= 0)
                        {
                            koniec = true;
                            Console.WriteLine("Przegrałeś!");
                        }

                        if(traf == cel)
                        {
                            koniec = true;
                            Console.WriteLine("Wygrałeś!");
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine("Niepoprawny poziom trudności");
            }

        }

        private static string[] generateGameWords(int n, List<string> words)
        {
            Random r = new Random();

            string[] gameWords = new string[n];

            for (int i = 0; i < n; i++)
            {
                int wi = r.Next(0, words.Count);
                gameWords[i] = words[wi];
                words.RemoveAt(wi);
            }

            return gameWords;
        }

        static void show(string[,] displayBoard)
        {
            Console.WriteLine("=================================");

            string[] rows = { "A\t", "B\t", "C\t", "D\t" };

            Console.Write("\t");

            for (int ki = 1; ki <= displayBoard.GetLength(1); ki++)
            {
                Console.Write(ki + "\t");
            }

            Console.WriteLine();


            for (int wi = 0; wi < displayBoard.GetLength(0); wi++)
            {
                Console.Write(rows[wi]);

                for (int ki = 0; ki < displayBoard.GetLength(1); ki++)
                {
                    Console.Write(displayBoard[wi, ki] + "\t");
                }

                Console.WriteLine();
            }

            Console.WriteLine("=================================");

            Console.WriteLine("Wciśnij enter aby kontynuować");
            Console.ReadLine();
            Console.Clear();
        }
    }
}

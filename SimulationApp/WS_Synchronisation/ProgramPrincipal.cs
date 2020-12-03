using System;

namespace WS_Synchronisation
{
    class ProgramPrincipal
    {
        private delegate void DELG(object state);
        private delegate void WORK(CL_outils outil1, CL_outils outil2);

        static void Main(string[] args)
        {
            //Les outils
            var t1 = new CL_outils("t1", "tournevis");
            var t2 = new CL_outils("t2", "tournevis");
            var c1 = new CL_outils("c1", "Clef");
            var c2 = new CL_outils("t2", "Clef");

            //Les ouvriers
            System.Threading.Thread o1;
            System.Threading.Thread o2;
            System.Threading.Thread o3;
            System.Threading.Thread o4;

            // Les compteurs de productions des ouvriers;
            int cpte_01 = 0;
            int cpte_02 = 0;
            int cpte_03 = 0;
            int cpte_04 = 0;

            //Le temps
            int temps = 0;
            DELG d1 = (state) =>
            {
                int swap_01 = 0;
                int swap_02 = 0;
                int swap_03 = 0;
                int swap_04 = 0;
                string ouvrier = (string)state;
                Console.WriteLine(ouvrier);
                WORK work = (out1, out2) =>
                {
                    out1.Utilisateur = ouvrier;
                    out2.Utilisateur = ouvrier;
                    out1.utilisation_outil();
                    out2.utilisation_outil();
                    out1.liberation_outil();
                    out2.liberation_outil();
                };
                while (true)
                {
                    if (ouvrier == "o1")
                    {
                        bool c1_t1 = false;
                        bool c1_t2 = false;

                        try
                        {
                            if ((System.Threading.Monitor.TryEnter(c1, 15000)) && ((System.Threading.Monitor.TryEnter(t1, 15000))))
                            {
                                work.Invoke(c1, t2);
                                CL_nb_piece.pp_piece();
                                c1_t1 = true;
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("Piece ok pour {0}", ouvrier);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                swap_01 += 1;
                                cpte_01 += 1;
                            }
                            else if ((System.Threading.Monitor.TryEnter(c1, 15000)) && ((System.Threading.Monitor.TryEnter(t2, 15000))))
                            {
                                work.Invoke(c1, t2);
                                CL_nb_piece.pp_piece();
                                c1_t2 = true;
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("Piece ok pour {0}", ouvrier);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                swap_01 += 1;
                                cpte_01 += 1;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("o1 bloqué");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                System.Threading.Thread.Sleep(1000);
                            }
                        }
                        finally
                        {
                            if (c1_t1 == true)
                            {
                                System.Threading.Monitor.Exit(c1);
                                System.Threading.Monitor.Exit(t1);
                            }
                            else if (c1_t2 == true)
                            {
                                System.Threading.Monitor.Exit(c1);
                                System.Threading.Monitor.Exit(t2);
                            }
                            if (swap_01 > 2)
                            {
                                swap_01 = 0;
                                Console.WriteLine("o1 en attente");
                                System.Threading.Thread.Sleep(30000);
                            }
                        }
                    }
                    else if (ouvrier == "o2")
                    {
                        bool c1_t1 = false;
                        bool c2_t1 = false;

                        try
                        {
                            if ((System.Threading.Monitor.TryEnter(c1, 15000)) && ((System.Threading.Monitor.TryEnter(t1, 15000))))
                            {
                                work.Invoke(c1, t1);
                                CL_nb_piece.pp_piece();
                                c1_t1 = true;
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("Piece ok pour {0}", ouvrier);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                swap_02 += 1;
                                cpte_02 += 1;
                            }
                            else if ((System.Threading.Monitor.TryEnter(c2, 15000)) && ((System.Threading.Monitor.TryEnter(t1, 15000))))
                            {
                                work.Invoke(c2, t1);
                                CL_nb_piece.pp_piece();
                                c2_t1 = true;
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("Piece ok pour {0}", ouvrier);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                swap_02 += 1;
                                cpte_02 += 1;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("o2 bloqué");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                System.Threading.Thread.Sleep(1000);
                            }
                        }
                        finally
                        {
                            if (c1_t1 == true)
                            {
                                System.Threading.Monitor.Exit(c1);
                                System.Threading.Monitor.Exit(t1);
                            }
                            else if (c2_t1 == true)
                            {
                                System.Threading.Monitor.Exit(c2);
                                System.Threading.Monitor.Exit(t1);
                            }
                            if (swap_02 > 2)
                            {
                                swap_02 = 0;
                                Console.WriteLine("o2 en attente");
                                System.Threading.Thread.Sleep(30000);
                            }
                        }
                    }
                    else if (ouvrier == "o3")
                    {
                        bool c2_t1 = false;
                        bool c2_t2 = false;

                        try
                        {
                            if ((System.Threading.Monitor.TryEnter(c2, 15000)) && ((System.Threading.Monitor.TryEnter(t1, 15000))))
                            {
                                work.Invoke(c2, t1);
                                CL_nb_piece.pp_piece();
                                c2_t1 = true;
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Piece ok pour {0}", ouvrier);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                swap_03 += 1;
                                cpte_03 += 1;
                            }
                            else if ((System.Threading.Monitor.TryEnter(c2, 15000)) && ((System.Threading.Monitor.TryEnter(t2, 15000))))
                            {
                                work.Invoke(c2, t2);
                                CL_nb_piece.pp_piece();
                                c2_t2 = true;
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Piece ok pour {0}", ouvrier);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                swap_03 += 1;
                                cpte_03 += 1;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("o3 bloqué");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                System.Threading.Thread.Sleep(1000);
                            }
                        }
                        finally
                        {
                            if (c2_t1 == true)
                            {
                                System.Threading.Monitor.Exit(c2);
                                System.Threading.Monitor.Exit(t1);
                            }
                            else if (c2_t2 == true)
                            {
                                System.Threading.Monitor.Exit(c2);
                                System.Threading.Monitor.Exit(t2);
                            }
                        }
                        if (swap_03 > 2)
                        {
                            Console.WriteLine("o3 en attente");
                            swap_03 = 0;
                            System.Threading.Thread.Sleep(30000);
                        }
                    }
                    else if (ouvrier == "o4")
                    {
                        bool c1_t2 = false;
                        bool c2_t2 = false;

                        try
                        {
                            if ((System.Threading.Monitor.TryEnter(c1, 15000)) && ((System.Threading.Monitor.TryEnter(t2, 15000))))
                            {
                                work.Invoke(c1, t2);
                                CL_nb_piece.pp_piece();
                                c1_t2 = true;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Piece ok pour {0}", ouvrier);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                swap_04 += 1;
                                cpte_04 += 1;
                            }
                            else if ((System.Threading.Monitor.TryEnter(c2, 15000)) && ((System.Threading.Monitor.TryEnter(t2, 15000))))
                            {
                                work.Invoke(c2, t2);
                                CL_nb_piece.pp_piece();
                                c2_t2 = true;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Piece ok pour {0}", ouvrier);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                swap_04 += 1;
                                cpte_04 += 1;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("o4 bloqué");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                System.Threading.Thread.Sleep(1000);
                            }
                        }
                        finally
                        {
                            if (c1_t2 == true)
                            {
                                System.Threading.Monitor.Exit(c1);
                                System.Threading.Monitor.Exit(t2);
                            }
                            else if (c2_t2 == true)
                            {
                                System.Threading.Monitor.Exit(c2);
                                System.Threading.Monitor.Exit(t2);
                            }
                        }
                        if (swap_04 > 2)
                        {
                            Console.WriteLine("o4 en attente");
                            swap_04 = 0;
                            System.Threading.Thread.Sleep(30000);
                        }
                    }
                }
            };

            o1 = new System.Threading.Thread(
                new System.Threading.ParameterizedThreadStart(d1.Invoke));
            o2 = new System.Threading.Thread(
                new System.Threading.ParameterizedThreadStart(d1.Invoke));
            o3 = new System.Threading.Thread(
                new System.Threading.ParameterizedThreadStart(d1.Invoke));
            o4 = new System.Threading.Thread(
                new System.Threading.ParameterizedThreadStart(d1.Invoke));
            o1.Start((object)("o1"));
            o2.Start((object)("o2"));
            o3.Start((object)("o3"));
            o4.Start((object)("o4"));
            while (true)
            {
                System.Threading.Thread.Sleep(30000);
                temps += 30;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Piece o1 = {0} -- Piece o2 = {1} -- Piece o3 = {2} -- Piece o4 = {3} --",
                    cpte_01, cpte_02, cpte_03, cpte_04);
                Console.WriteLine("Temps de production : {0}", temps);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}


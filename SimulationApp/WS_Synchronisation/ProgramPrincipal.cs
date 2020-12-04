using System;

namespace WS_Synchronisation
{
    class ProgramPrincipal
    {
        private delegate void DELG(object employeeName);
        private delegate void WORK(CL_outils outil1, CL_outils outil2);

        static void Main(string[] args)
        {
            //Les outils
            var t1 = new CL_outils("t1", ToolTypeEnum.Screwdriver);
            var t2 = new CL_outils("t2", ToolTypeEnum.Screwdriver);
            var c1 = new CL_outils("c1", ToolTypeEnum.Wrench);
            var c2 = new CL_outils("t2", ToolTypeEnum.Wrench);

            //Les ouvriers
            System.Threading.Thread employee1;
            System.Threading.Thread employee2;
            System.Threading.Thread employee3;
            System.Threading.Thread employee4;

            // Les compteurs de productions des ouvriers;
            int achievedPiecesCount1 = 0;
            int achievedPiecesCount2 = 0;
            int achievedPiecesCount3 = 0;
            int achievedPiecesCount4 = 0;

            //Le temps
            int temps = 0;
            DELG d1 = (employeeName) =>
            {
                int swap_01 = 0;
                int swap_02 = 0;
                int swap_03 = 0;
                int swap_04 = 0;
                string employeeId = (string)employeeName;

                Console.WriteLine(employeeId);

                WORK work = (out1, out2) =>
                {
                    out1.Utilisateur = employeeId;
                    out2.Utilisateur = employeeId;
                    out1.utilisation_outil();
                    out2.utilisation_outil();
                    out1.liberation_outil();
                    out2.liberation_outil();
                };

                while (true)
                {
                    switch (employeeId)
                    {
                        case "o1":
                            routine(employeeId, ref c1, ref t1, ref t2, ref work, ref swap_01, ref achievedPiecesCount1);
                            break;
                        case "o2":
                            routine(employeeId, ref t1, ref c1, ref c2, ref work, ref swap_02, ref achievedPiecesCount2);
                            break;
                        case "o3":
                            routine(employeeId, ref c2, ref t1, ref t2, ref work, ref swap_03, ref achievedPiecesCount3);
                            break;
                        case "o4":
                            routine(employeeId, ref t2, ref c1, ref c2, ref work, ref swap_04, ref achievedPiecesCount4);
                            break;
                    }
                }
            };

            employee1 = new System.Threading.Thread(
                new System.Threading.ParameterizedThreadStart(d1.Invoke));
            employee2 = new System.Threading.Thread(
                new System.Threading.ParameterizedThreadStart(d1.Invoke));
            employee3 = new System.Threading.Thread(
                new System.Threading.ParameterizedThreadStart(d1.Invoke));
            employee4 = new System.Threading.Thread(
                new System.Threading.ParameterizedThreadStart(d1.Invoke));

            employee1.Start((object)("o1"));
            employee2.Start((object)("o2"));
            employee3.Start((object)("o3"));
            employee4.Start((object)("o4"));

            while (true)
            {
                System.Threading.Thread.Sleep(30000);
                temps += 30;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Piece o1 = {0} -- Piece o2 = {1} -- Piece o3 = {2} -- Piece o4 = {3} --",
                    achievedPiecesCount1, achievedPiecesCount2, achievedPiecesCount3, achievedPiecesCount4);
                Console.WriteLine("Temps de production : {0}", temps);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private static void routine(string employeeId, ref CL_outils stableTool, ref CL_outils variableTool1, ref CL_outils variableTool2, ref WORK work,
            ref int swap, ref int achievedPiecesCount)
        {
            bool isPair1Complete = false;
            bool isPair2Complete = false;

            if (stableTool.Type == ToolTypeEnum.Wrench)
            {
                try
                {
                    if ((System.Threading.Monitor.TryEnter(stableTool, 15000)) && ((System.Threading.Monitor.TryEnter(variableTool1, 15000))))
                    {
                        work.Invoke(stableTool, variableTool1);
                        CL_nb_piece.pp_piece();
                        isPair1Complete = true;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Piece ok pour {0}", employeeId);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        swap += 1;
                        achievedPiecesCount += 1;
                    }
                    else if ((System.Threading.Monitor.TryEnter(stableTool, 15000)) && ((System.Threading.Monitor.TryEnter(variableTool2, 15000))))
                    {
                        work.Invoke(stableTool, variableTool2);
                        CL_nb_piece.pp_piece();
                        isPair2Complete = true;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Piece ok pour {0}", employeeId);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        swap += 1;
                        achievedPiecesCount += 1;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0} bloqué", employeeId);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                finally
                {
                    if (isPair1Complete == true)
                    {
                        System.Threading.Monitor.Exit(stableTool);
                        System.Threading.Monitor.Exit(variableTool1);
                    }
                    else if (isPair2Complete == true)
                    {
                        System.Threading.Monitor.Exit(stableTool);
                        System.Threading.Monitor.Exit(variableTool2);
                    }
                }
            } else
            {
                try
                {
                    if ((System.Threading.Monitor.TryEnter(variableTool1, 15000)) && ((System.Threading.Monitor.TryEnter(stableTool, 15000))))
                    {
                        work.Invoke(stableTool, variableTool1);
                        CL_nb_piece.pp_piece();
                        isPair1Complete = true;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Piece ok pour {0}", employeeId);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        swap += 1;
                        achievedPiecesCount += 1;
                    }
                    else if ((System.Threading.Monitor.TryEnter(variableTool2, 15000)) && ((System.Threading.Monitor.TryEnter(stableTool, 15000))))
                    {
                        work.Invoke(stableTool, variableTool2);
                        CL_nb_piece.pp_piece();
                        isPair2Complete = true;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Piece ok pour {0}", employeeId);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        swap += 1;
                        achievedPiecesCount += 1;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0} bloqué", employeeId);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                finally
                {
                    if (isPair1Complete == true)
                    {
                        System.Threading.Monitor.Exit(stableTool);
                        System.Threading.Monitor.Exit(variableTool1);
                    }
                    else if (isPair2Complete == true)
                    {
                        System.Threading.Monitor.Exit(stableTool);
                        System.Threading.Monitor.Exit(variableTool2);
                    }
                }
            }
            
            if (swap > 2)
            {
                Console.WriteLine("{0} en attente", employeeId);
                swap = 0;
                System.Threading.Thread.Sleep(30000);
            }
        }
    }

}


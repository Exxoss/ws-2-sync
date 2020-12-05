using System;
namespace WS_Synchronisation
{
    public static class ConsoleRending
    {
        private static object lockConsole = new object();

        public static void takeTool(Tool tool, string EmployeeId)
        {
            if (tool.type == ToolTypeEnum.Screwdriver)
            {
                lock (lockConsole)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Le tournevis {0} est utilisé par l'employé {1}", tool.id, EmployeeId);
                }
            }
            if (tool.type == ToolTypeEnum.Wrench)
            {
                lock (lockConsole)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("La clef {0} est utilisé par l'employé {1}", tool.id, EmployeeId);
                }
            }
        }
        public static void releaseTool(Tool tool, string EmployeeId)
        {
            if (tool.type == ToolTypeEnum.Screwdriver)
            {
                lock (lockConsole)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Le tournevis {0} est libéré par l'employé {1}", tool.id, EmployeeId);
                }
            }
            if (tool.type == ToolTypeEnum.Wrench)
            {
                lock (lockConsole)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("La clef {0} est libéré par l'employé {1}", tool.id, EmployeeId);
                }
            }
        }
        public static void currentPieceCount(int pieceCount)
        {
            lock (lockConsole)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Nombre de pièces terminé --› {0}", pieceCount);
            }
        }
        public static void productionStatus(Employee[] employees, int timeS)
        {
            lock (lockConsole)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("--------------Status-------------");
                foreach (Employee employee in employees)
                {
                    Console.WriteLine("-- Nombre de pièces réalisé par l'employé {0} : {1}", employee.id, employee.achievedPiecesCount);
                }
                Console.WriteLine("------Temps écoulé : {0}''-----", timeS);
            }
        }
        public static void donePiece(string employeeId)
        {
            lock (lockConsole)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Pièce OK par l'employé : {0}", employeeId);
            }
        }
        public static void employeeBlocked(string employeeId)
        {
            lock (lockConsole)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Employé : {0} bloqué !", employeeId);
            }
        }
        public static void employeeWait(string employeeId)
        {
            lock (lockConsole)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Employé : {0} en attente...", employeeId);
            }
        }
    }
}

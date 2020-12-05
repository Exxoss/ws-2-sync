using System;

namespace WS_Synchronisation
{
    class Simulation
    {
        const string ID_EMPLOYEE_1 = "o1";
        const string ID_EMPLOYEE_2 = "o2";
        const string ID_EMPLOYEE_3 = "o3";
        const string ID_EMPLOYEE_4 = "o4";

        const int STATUS_INTERVAL_MLS = 30000;
        const int PIECES_RELEASED_BEFORE_WAITING = 3;
        const int EMPLOYEES_WAITING_DELAY_MLS = 30000;
        const int EMPLOYEES_SHOP_DELAY_MLS = 15000;

        private delegate void DELG(object employeeName);
        private delegate void WORK(Tool outil1, Tool outil2);

        static void Main(string[] args)
        {
            //Les outils
            Tool t1 = new Tool("t1", ToolTypeEnum.Screwdriver);
            Tool t2 = new Tool("t2", ToolTypeEnum.Screwdriver);
            Tool c1 = new Tool("c1", ToolTypeEnum.Wrench);
            Tool c2 = new Tool("t2", ToolTypeEnum.Wrench);

            //Les ouvriers
            Employee[] employees = {
                new Employee(ID_EMPLOYEE_1),
                new Employee(ID_EMPLOYEE_2),
                new Employee(ID_EMPLOYEE_3),
                new Employee(ID_EMPLOYEE_4)
            };

            //Le temps
            int timeS = 0;

            DELG d1 = (employeeName) =>
            {
                string employeeId = (string)employeeName;

                Console.WriteLine(employeeId);

                WORK work = (tool1, tool2) =>
                {
                    tool1.User = employeeId;
                    tool2.User = employeeId;
                    tool1.useTool();
                    tool2.useTool();
                    tool1.releaseTool();
                    tool2.releaseTool();
                };

                while (true)
                {
                    switch (employeeId)
                    {
                        case ID_EMPLOYEE_1:
                            routine(employeeId, ref c1, ref t1, ref t2, ref work, ref employees[0]);
                            break;
                        case ID_EMPLOYEE_2:
                            routine(employeeId, ref t1, ref c1, ref c2, ref work, ref employees[1]);
                            break;
                        case ID_EMPLOYEE_3:
                            routine(employeeId, ref c2, ref t1, ref t2, ref work, ref employees[2]);
                            break;
                        case ID_EMPLOYEE_4:
                            routine(employeeId, ref t2, ref c1, ref c2, ref work, ref employees[3]);
                            break;
                    }
                }
            };

            foreach (Employee employee in employees)
            {
                employee.thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(d1.Invoke));
                employee.thread.Start((object)employee.id);
            }

            while (true)
            {
                System.Threading.Thread.Sleep(STATUS_INTERVAL_MLS);
                timeS += STATUS_INTERVAL_MLS/1000;
                ConsoleRending.productionStatus(employees, timeS);
            }
        }

        private static void routine(string employeeId, ref Tool stableTool, ref Tool variableTool1, ref Tool variableTool2, ref WORK work, ref Employee employee)
        {
            bool isPair1Complete = false;
            bool isPair2Complete = false;

            if (stableTool.type == ToolTypeEnum.Wrench)
            {
                try
                {
                    if ((System.Threading.Monitor.TryEnter(stableTool, EMPLOYEES_SHOP_DELAY_MLS)) && ((System.Threading.Monitor.TryEnter(variableTool1, EMPLOYEES_SHOP_DELAY_MLS))))
                    {
                        work.Invoke(stableTool, variableTool1);
                        isPair1Complete = true;
                        employee.swap++;
                        employee.increasePieceCount();
                    }
                    else if ((System.Threading.Monitor.TryEnter(stableTool, EMPLOYEES_SHOP_DELAY_MLS)) && ((System.Threading.Monitor.TryEnter(variableTool2, EMPLOYEES_SHOP_DELAY_MLS))))
                    {
                        work.Invoke(stableTool, variableTool2);
                        isPair2Complete = true;
                        employee.swap++;
                        employee.increasePieceCount();
                    }
                    else
                    {
                        ConsoleRending.employeeBlocked(employeeId);
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
                    if ((System.Threading.Monitor.TryEnter(variableTool1, EMPLOYEES_SHOP_DELAY_MLS)) && ((System.Threading.Monitor.TryEnter(stableTool, EMPLOYEES_SHOP_DELAY_MLS))))
                    {
                        work.Invoke(stableTool, variableTool1);
                        isPair1Complete = true;
                        employee.swap++;
                        employee.increasePieceCount();
                    }
                    else if ((System.Threading.Monitor.TryEnter(variableTool2, EMPLOYEES_SHOP_DELAY_MLS)) && ((System.Threading.Monitor.TryEnter(stableTool, EMPLOYEES_SHOP_DELAY_MLS))))
                    {
                        work.Invoke(stableTool, variableTool2);
                        isPair2Complete = true;
                        employee.swap++;
                        employee.increasePieceCount();
                    }
                    else
                    {
                        ConsoleRending.employeeBlocked(employeeId);
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
            
            if (employee.swap >= PIECES_RELEASED_BEFORE_WAITING)
            {
                ConsoleRending.employeeWait(employeeId);
                employee.swap = 0;
                System.Threading.Thread.Sleep(EMPLOYEES_WAITING_DELAY_MLS);
            }
        }
    }
}


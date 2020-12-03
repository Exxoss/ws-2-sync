using System;
namespace WS_Synchronisation
{
    public static class CL_nb_piece
    {
        private static int nb_piece = 0;
        private static object lck;
        static CL_nb_piece()
        {
            lck = new object();
        }
        public static void pp_piece()
        {
            lock (lck)
            {
                nb_piece += 1;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Nombre de pièces -> {0}", nb_piece);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}

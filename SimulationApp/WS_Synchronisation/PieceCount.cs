using System;
namespace WS_Synchronisation
{
    public static class PieceCount
    {
        private static int pieceCount = 0;
        private static object lck;

        static PieceCount()
        {
            lck = new object();
        }
        public static void IncreasePieceCount()
        {
            lock (lck)
            {
                pieceCount += 1;
                ConsoleRending.currentPieceCount(pieceCount);
            }
        }
    }
}

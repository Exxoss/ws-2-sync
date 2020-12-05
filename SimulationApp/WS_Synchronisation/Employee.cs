using System;
namespace WS_Synchronisation
{
    public class Employee
    {
        public System.Threading.Thread thread;
        public string id { get; }
        public int achievedPiecesCount { get; set; }
        public int swap { get; set; }

        public Employee(string id)
        {
            this.id = id;
            achievedPiecesCount = 0;
            swap = 0;
        }

        public void increasePieceCount()
        {
            achievedPiecesCount++;
            PieceCount.IncreasePieceCount();
        }
    }
}

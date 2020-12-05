using System;
namespace WS_Synchronisation
{
    public class Tool
    {
        public string id { get; }
        public ToolTypeEnum type { get; }
        private string user;

        public Tool(string id, ToolTypeEnum type)
        {
            this.id = id;
            this.type = type;
            this.user = "NC";
        }
        public string User
        {
            get { return user; }
            set
            {
                user = value;
                ConsoleRending.takeTool(this, value);
            }
        }
        public void useTool()
        {
            System.Threading.Thread.Sleep(2000);
            ConsoleRending.donePiece(user);
        }
        public void releaseTool()
        {
            ConsoleRending.releaseTool(this, user);
            user = null;
        }
    }
}

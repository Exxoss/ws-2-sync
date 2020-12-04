using System;
namespace WS_Synchronisation
{
    public class CL_outils
    {
        private string id;
        private ToolTypeEnum type;
        private string utilisateur;

        public CL_outils(string Id, ToolTypeEnum Type)
        {
            this.Id = Id;
            this.Type = Type;
            this.Utilisateur = "NC";
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public ToolTypeEnum Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Utilisateur
        {
            get { return utilisateur; }
            set
            {
                utilisateur = value;
                Console.WriteLine("L'outil " + this.id + " est utilisé par " + this.utilisateur);
            }
        }
        public void utilisation_outil()
        {
            System.Threading.Thread.Sleep(2000);
        }
        public void liberation_outil()
        {
            string msg = "L'outil " + this.id + " est libéré par " + this.utilisateur;
            Console.WriteLine(msg);
        }
    }
}

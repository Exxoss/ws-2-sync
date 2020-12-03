using System;
namespace WS_Synchronisation
{
    public class CL_outils
    {
        private string nom;
        private string type;
        private string utilisateur;

        public CL_outils(string Nom, string Type)
        {
            this.Nom = Nom;
            this.Type = Type;
            this.Utilisateur = "NC";
        }
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public string Type
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
                Console.WriteLine("L'outil " + this.nom + " est utilisé par " + this.utilisateur);
            }
        }
        /// <summary>
        /// Simulation d'un travail avec l'outil
        /// </summary>
        public void utilisation_outil()
        {
            System.Threading.Thread.Sleep(2000);
        }
        public void liberation_outil()
        {
            string msg = "L'outil " + this.nom + " est libéré par " + this.utilisateur;
            Console.WriteLine(msg);
        }
    }
}

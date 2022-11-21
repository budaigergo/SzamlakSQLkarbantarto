using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzamlakSQLkarbantarto
{
    internal class Szamla
    {
        int id;
        string tulajdonosneve;
        int egyenleg;
        DateTime nyitasdatum;

        public int Id { get => id; set => id = value; }
        public string Tulajdonosneve { get => tulajdonosneve; set => tulajdonosneve = value; }
        public int Egyenleg { get => egyenleg; set => egyenleg = value; }
        public DateTime Nyitasdatum { get => nyitasdatum; set => nyitasdatum = value; }

        public Szamla(int id, string tulajdonosneve, int egyenleg, DateTime nyitasdatum)
        {
            Id = id;
            Tulajdonosneve = tulajdonosneve;
            Egyenleg = egyenleg;
            Nyitasdatum = nyitasdatum;

        }
        public override string ToString()
        {
            return Tulajdonosneve;
        }
    }
}

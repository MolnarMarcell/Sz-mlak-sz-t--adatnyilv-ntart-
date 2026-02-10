using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Számlakészítő_adatnyilvántartó
{
    public class Termek_osztaly
    {
        public int ID { get; set; }
        public string Név { get; set; }
        public string Kategória { get; set; }
        public int Ár { get; set; }
        public int Darab { get; set; }

        public Termek_osztaly(int id, string nev, string kategoria, int ar, int darab)
        {
            ID = id;
            Név = nev;
            Kategória = kategoria;
            Ár = ar;
            Darab = darab;
        }
    }
    
}

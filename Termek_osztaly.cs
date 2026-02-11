using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Számlakészítő_adatnyilvántartó
{
    public class Termek_osztaly
    {
        public int ID;
        public string Név;
        public string Kategória;
        public int Ár;
        public int Darab;

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
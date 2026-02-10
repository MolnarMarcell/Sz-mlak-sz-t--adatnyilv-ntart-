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
        public int id;
        public string nev;
        public string kategoria;
        public int ar;
        public int db;

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
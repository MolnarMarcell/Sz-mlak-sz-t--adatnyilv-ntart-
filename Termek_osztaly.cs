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
        public string Nev;
        public string Kategoria;
        public int Ar;
        public int Darab;

        public Termek_osztaly(int id, string nev, string kategoria, int ar, int darab)
        {
            ID = id;
            Nev = nev;
            Kategoria = kategoria;
            Ar = ar;
            Darab = darab;
        }
    }
    
}
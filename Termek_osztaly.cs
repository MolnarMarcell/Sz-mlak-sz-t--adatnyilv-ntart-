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

        public Termek_osztaly(int ID, string Név, string Kategória, int Ár, int Darab)
        {
            id = ID;
            nev = Név;
            kategoria = Kategória;
            ar = Ár;
            db = Darab;
        }
    }
    
}

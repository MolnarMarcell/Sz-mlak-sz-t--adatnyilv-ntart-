using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Számlakészítő_adatnyilvántartó
{
    internal class Termek_osztaly
    {
        public int ID;
        public string Név;
        public string Kategória;
        public int Ár;
        public int Darab; 
        public Termek_osztaly(int ID, string Név, string Kategória, int Ár, int Darab)
        {
            ID = ID;
            Név = Név;
            Kategória = Kategória;
            Ár = Ár;
            Darab = Darab;
        }
    }
    
}

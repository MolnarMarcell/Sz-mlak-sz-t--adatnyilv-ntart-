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
        public int ID { get; set; }
        public string Nev { get; set; }
        public string Kategoria { get; set; }
        public int Ar { get; set; }
        public int Keszlet { get; set; }

        public Termek_osztaly(int id, string nev, string kategoria, int ar, int keszlet)
        {
            ID = id;
            Nev = nev;
            Kategoria = kategoria;
            Ar = ar;
            Keszlet = keszlet;
        }
    }
}
using System.Collections.Generic;
using System.Windows;

namespace Számlakészítő_adatnyilvántartó
{
    public partial class Tablazat : Window
    {

        public Tablazat()
        {
            InitializeComponent();
        }
        public void Betölt(List<Termek_osztaly> Termék) //a termékeket betölti a DataGrid-be, hogy táblázatban meg lehessen nézni őket
        {
            dgAdatok.ItemsSource = null; //elöző értékékeket törli
            dgAdatok.ItemsSource = Termék; // betölti a legfrissebb termékeket a DataGrid-be
        }
    }
}
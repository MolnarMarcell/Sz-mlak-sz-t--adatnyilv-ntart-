using System.Collections.Generic;
using System.Windows;

namespace Számlakészítő_adatnyilvántartó
{
    public partial class Tablazat : Window
    {
        public void Betölt(List<Termek_osztaly> Termék)
        {
            MessageBox.Show(Termék[0].ID + Termék[0].Név + Termék[0].Kategória + Termék[0].Ár + Termék[0].Darab);
            dgAdatok.ItemsSource = null;
            dgAdatok.ItemsSource = Termék;


        }

        public Tablazat()
        {
            InitializeComponent();
        }
    }
}
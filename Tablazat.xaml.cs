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

        public void Betölt(List<Termek_osztaly> Termék)
        {
            dgAdatok.Items.Clear();
            dgAdatok.ItemsSource = Termék;
        }
    }
}

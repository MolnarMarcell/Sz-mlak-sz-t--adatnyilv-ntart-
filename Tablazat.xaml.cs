using System.Collections.Generic;
using System.Windows;

namespace Számlakészítő_adatnyilvántartó
{
    public partial class Tablazat : Window
    {
        public void Betölt(List<Termek_osztaly> Termék)
        {
            dgAdatok.ItemsSource = null;
            dgAdatok.ItemsSource = Termék;
        }
        public Tablazat()
        {
            InitializeComponent();
        }
    }
}
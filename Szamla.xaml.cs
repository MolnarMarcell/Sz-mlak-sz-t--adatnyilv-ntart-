using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Számlakészítő_adatnyilvántartó
{
    /// <summary>
    /// Interaction logic for Szamla.xaml
    /// </summary>
    public partial class Szamla : Window
    {
        List<Termek_osztaly> Termekek;

        public Szamla(List<Termek_osztaly> termekek)
        {
            InitializeComponent();
            Termekek = termekek;
            SzamlaDataGrid.ItemsSource = Termekek;
            Osszesit();
        }

        private void Osszesit()
        {
            int osszesen = Termekek.Sum(t => t.Ar * t.Darab);
            Osszesen_Text.Text = osszesen + " Ft";
        }
    }
}
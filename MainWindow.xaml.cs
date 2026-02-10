using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Számlakészítő_adatnyilvántartó
{
    public partial class MainWindow : Window
    {
        List<Termek_osztaly> Termékek = new List<Termek_osztaly>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hozzáadás_gomb_click(object sender, RoutedEventArgs e)
        {
            if (Kategória_ComboBox.SelectedItem == null)
            {
                MessageBox.Show("Válassz kategóriát!");
                return;
            }

            int ID = Termékek.Count + 1;
            string Név = TermékNév_TextBox.Text;

            string Kategória =
                ((ComboBoxItem)Kategória_ComboBox.SelectedItem).Content.ToString();

            int Ár = int.Parse(Ár_TextBox.Text);
            int Darab = int.Parse(Darab_TextBox.Text);

            Termékek.Add(new Termek_osztaly(ID, Név, Kategória, Ár, Darab));

            MessageBox.Show("Hozzáadva. Lista elemszám: " + Termékek.Count);
        }


        private void OpenTable_Click(object sender, RoutedEventArgs e)
        {
            Tablazat tabla = new Tablazat();
            tabla.Betölt(Termékek);
            tabla.Show();
        }
    }
}

using System.Windows;

namespace Számlakészítő_adatnyilvántartó
{
    public partial class MainWindow : Window
    {
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

            if (!int.TryParse(Ár_TextBox.Text, out int Ár) ||
                !int.TryParse(Darab_TextBox.Text, out int Darab))
            {
                MessageBox.Show("Hibás szám!");
                return;
            }

            int ID = App.Termékek.Count + 1;
            string Név = TermékNév_TextBox.Text;
            string Kategória = Kategória_ComboBox.Text;

            App.Termékek.Add(new Termek_osztaly(ID, Név, Kategória, Ár, Darab));

            MessageBox.Show("Hozzáadva. Lista elemszám: " + App.Termékek.Count);
        }

        private void OpenTable_Click(object sender, RoutedEventArgs e)
        {
            Tablazat tabla = new Tablazat();
            tabla.Betölt(App.Termékek);
            tabla.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (App.Termékek.Count == 0)
            {
                MessageBox.Show("Nincs termék a listában!");
                return;
            }

            Szamla szamlaTableau = new Szamla(App.Termékek);
            szamlaTableau.Show();
        }
    }
}

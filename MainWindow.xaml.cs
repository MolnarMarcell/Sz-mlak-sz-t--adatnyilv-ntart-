using System.Windows;
using System.Linq;

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

            string Név = TermékNév_TextBox.Text.Trim();
            string Kategória = Kategória_ComboBox.Text;

            if (string.IsNullOrWhiteSpace(Név))
            {
                MessageBox.Show("Adj meg terméknevet!");
                return;
            }

            // 🔍 Megkeressük, hogy létezik-e már ilyen nevű termék
            var letezoTermek = App.Termékek
                .FirstOrDefault(t => t.Nev.Equals(Név, StringComparison.OrdinalIgnoreCase));

            if (letezoTermek != null)
            {
                // ✔ Már létezik → készlet növelése
                letezoTermek.Keszlet += Darab;

                // (ha az ár változhat, akkor ezt is frissítheted)
                letezoTermek.Ar = Ár;

                MessageBox.Show($"A(z) {Név} már létezett, készlet növelve!\nÚj készlet: {letezoTermek.Keszlet}");
            }
            else
            {
                // ✔ Nem létezik → új termék
                int ID = App.Termékek.Count + 1;
                App.Termékek.Add(new Termek_osztaly(ID, Név, Kategória, Ár, Darab));

                MessageBox.Show($" {Név} hozzáadva új termékként.");
            }
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

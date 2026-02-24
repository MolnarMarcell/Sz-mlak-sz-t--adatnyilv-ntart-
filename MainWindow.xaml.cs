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
            if (Kategória_ComboBox.SelectedItem == null //ellenőrzi, hogy van-e kiválasztva kategória
             )
            {
                MessageBox.Show("Válassz kategóriát!"); //hiba üzenet, ha nincs kiválasztva kategória
                return;
            }

            if (!int.TryParse(Ár_TextBox.Text, out int Ár) || 
                !int.TryParse(Darab_TextBox.Text, out int Darab)) //átalakítja az ár és darab értékeket, és ellenőrzi, hogy helyes szám-e
            {
                MessageBox.Show("Hibás szám!"); //hiba üzenet, ha az ár vagy darab érték nem helyes szám
                return;
            }

            string Név = TermékNév_TextBox.Text.Trim(); //név értékének lekérése és levágása a felesleges szóközökről
            string Kategória = Kategória_ComboBox.Text; //kategória értékének lekérése a ComboBox-ból

            if (string.IsNullOrWhiteSpace(Név)) //ellenőrzi, hogy a név mező nem üres-e
            {
                MessageBox.Show("Adj meg terméknevet!"); //hiba üzenet, ha a név mező üres
                return;
            }

            //  Megkeressük, hogy létezik-e már ilyen nevű termék
            var letezoTermek = App.Termékek
                .FirstOrDefault(t => t.Nev.Equals(Név, StringComparison.OrdinalIgnoreCase));

            if (letezoTermek != null)
            {
                // Már létezik → készlet növelése
                letezoTermek.Keszlet += Darab;

                // (ha az ár változhat, akkor ezt is frissítheted)
                letezoTermek.Ar = Ár;

                MessageBox.Show($"A(z) {Név} már létezett, készlet növelve!\nÚj készlet: {letezoTermek.Keszlet}");
            }
            else
            {
                //  Nem létezik → új termék
                int ID = App.Termékek.Count + 1;
                App.Termékek.Add(new Termek_osztaly(ID, Név, Kategória, Ár, Darab));

                MessageBox.Show($" {Név} hozzáadva új termékként.");
            }
        }

        private void OpenTable_Click(object sender, RoutedEventArgs e)
        {
            Tablazat tabla = new Tablazat(); //ha megakarja nézni táblázatban a termékeket, akkor megnyitja a Tablazat ablakot és betölti a termékeket
            tabla.Betölt(App.Termékek);
            tabla.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (App.Termékek.Count == 0)//ellenőrzi, hogy van-e termék a listában
            {
                MessageBox.Show("Nincs termék a listában!"); // error üzenet, ha nincs termék
                return;
            }

            Szamla szamlaTableau = new Szamla(App.Termékek); // Számla ablak létrehozása a termékek listájával
            szamlaTableau.Show(); // Számla ablak megjelenítése
        }
    }
}

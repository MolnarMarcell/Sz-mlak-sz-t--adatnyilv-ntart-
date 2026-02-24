using System.Collections.ObjectModel;
using System.Windows;
using System.Collections.Generic;
using System.Linq;

namespace Számlakészítő_adatnyilvántartó
{
    public partial class Szamla : Window
    {
        private int mennyiseg = 0;
        private int darab = 1;
        private ObservableCollection<SzamlaTetel> szamlaTetelek;
        private List<Termek_osztaly> termekek;

        // Megrendelő + Kiállító adatok
        private string rendeloNev;
        private string rendeloEmail;
        private string kiallitoNev;
        private string kiallitoEmail;

        public class SzamlaTetel
        {
            public int ID { get; set; }
            public string Nev { get; set; }
            public string Kategoria { get; set; }
            public decimal Ar { get; set; }
            public int Darab { get; set; }
            public decimal Osszeg { get; set; }
        }

        public Szamla(List<Termek_osztaly> termekekLista)
        {
            InitializeComponent();

            termekek = termekekLista; // A konstruktorban kapja meg a termékek listáját

            szamlaTetelek = new ObservableCollection<SzamlaTetel>(); // Számla tételek gyűjteménye
            SzamlaDataGrid.ItemsSource = szamlaTetelek; // A DataGrid adatok forrása a számla tételek gyűjteménye

            Termek_ComboBox.ItemsSource = termekek; // A ComboBox adatok forrása a termékek listája
            Termek_ComboBox.DisplayMemberPath = "Nev"; // A ComboBox-ban a termék neve fog megjelenni
            Termek_ComboBox.SelectionChanged += Termek_ComboBox_SelectionChanged; // eseménykezelő a ComboBox kiválasztás változására
        }

        private void Termek_ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Termek_ComboBox.SelectedItem is Termek_osztaly termek) // ellenőrzi, hogy van-e kiválasztott termék a ComboBox-ban
            {
                Keszlet_Label.Content = termek.Keszlet.ToString(); // a készlet értékét megjeleníti a Label-ben
                Ar_Label.Content = termek.Ar + " Ft"; // az ár értékét megjeleníti a Label-ben
                mennyiseg = 0; // a mennyiséget 0-ra állítja, hogy új termék kiválasztásakor ne maradjon meg az előző termék mennyisége
                Mennyiseg_TextBox.Text = "0"; // a mennyiség TextBox értékét 0-ra állítja, hogy új termék kiválasztásakor ne maradjon meg az előző termék mennyisége
            }
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            if (Termek_ComboBox.SelectedItem is Termek_osztaly termek) // ellenőrzi, hogy van-e kiválasztott termék a ComboBox-ban
            {
                if (mennyiseg < termek.Keszlet) // ellenőrzi, hogy a mennyiség kisebb-e a készletnél, hogy ne lehessen több terméket hozzáadni, mint amennyi van készleten
                {
                    mennyiseg++; //hozzáad egyet a mennyiséghez
                    Mennyiseg_TextBox.Text = mennyiseg.ToString(); //frissíti a mennyiség TextBox értékét, hogy látszódjon a változás
                }
                else
                {
                    MessageBox.Show("Nincs elég készlet!"); //hibaüzenet, ha a mennyiség eléri a készletet, hogy ne lehessen több terméket hozzáadni, mint amennyi van készleten
                }
            }
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (mennyiseg > 0) // ellenőrzi, hogy a mennyiség nagyobb-e 0-nál, hogy ne lehessen negatív mennyiséget beállítani
            {
                mennyiseg--; // levon egyet a mennyiségből
                Mennyiseg_TextBox.Text = mennyiseg.ToString(); // frissíti a mennyiség TextBox értékét, hogy látszódjon a változás
            }
        }

        private void Hozzaadas_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Termek_ComboBox.SelectedItem is Termek_osztaly termek && mennyiseg > 0) //megnézi, hogy jó-e a termék típusa és van-e belőle elég
            {
                if (mennyiseg > termek.Keszlet)
                {
                    MessageBox.Show("Nincs elegendő készlet!");
                    return;
                }

                // Megnézzük, van-e már ilyen termék a számlán
                var letezoTetel = szamlaTetelek.FirstOrDefault(t => t.ID == termek.ID);

                if (letezoTetel != null) // Ha már van ilyen tétel, akkor csak növeljük a darabszámot és újraszámoljuk az összeget
                {
                    // Darabszám növelése
                    letezoTetel.Darab += mennyiseg; 
                    letezoTetel.Osszeg = letezoTetel.Darab * letezoTetel.Ar; // Összeg újraszámolása
                }
                else
                {
                    // Új tétel
                    var tetel = new SzamlaTetel
                    {
                        ID = termek.ID,
                        Nev = termek.Nev,
                        Kategoria = termek.Kategoria,
                        Ar = termek.Ar,
                        Darab = mennyiseg,
                        Osszeg = termek.Ar * mennyiseg
                    };

                    szamlaTetelek.Add(tetel);
                }

                
                termek.Keszlet -= mennyiseg; // Készlet csökkentése
                Keszlet_Label.Content = termek.Keszlet.ToString(); // Készlet frissítése a Label-ben

                SzamlaDataGrid.Items.Refresh(); // DataGrid frissítése, hogy látszódjon a változás
                SzamlaOsszesenSzamit(); // Végösszeg újraszámítása

                mennyiseg = 0; // Mennyiség visszaállítása
                Mennyiseg_TextBox.Text = "0"; // Mennyiség TextBox visszaállítása
            }
        }

        private void TorlesElem_Click(object sender, RoutedEventArgs e) // törli a számláról a kiválasztott tételt
        {
            if (SzamlaDataGrid.SelectedItem is SzamlaTetel tetel) // ellenőrzi, hogy van-e kiválasztott tétel a DataGrid-ben
            {
                var termek = termekek.FirstOrDefault(t => t.ID == tetel.ID); // megkeresi a termékeket, hogy vissza tudja adni a készletet
                if (termek != null)
                {
                    // Készlet visszaadása
                    termek.Keszlet += tetel.Darab;
                }

                szamlaTetelek.Remove(tetel); // eltávolítja a kiválasztott tételt a számláról
                SzamlaOsszesenSzamit(); // újraszámolja a végösszeget

                if (Termek_ComboBox.SelectedItem == termek) // ha a törölt tétel terméke éppen ki van választva a ComboBox-ban, akkor frissíti a készlet értékét
                {
                    Keszlet_Label.Content = termek.Keszlet.ToString(); // frissíti a készlet értékét a Label-ben
                }
            }
        }

        private void Torles_Button_Click(object sender, RoutedEventArgs e) // törli az elemeket ha nem akarja a felhasználó
        {
            mennyiseg = 0; //0-ra állítja a mennyiséget
            Mennyiseg_TextBox.Text = "0"; //0-ra állítja a mennyiség szövegdoboz értékét
            Termek_ComboBox.SelectedIndex = -1; //nem lesz kiválasztva semmilyen termék a ComboBox-ban
        }

        private void SzamlaOsszesenSzamit() //a végösszeg kiszámítása
        {
            decimal osszesen = 0;
            foreach (var tetel in szamlaTetelek) // tételenként hozzáadja a tételek árát a végösszeghez
                osszesen += tetel.Osszeg;        //  -||-

            Osszesen_Text.Text = osszesen.ToString("0.00") + " Ft"; //végső összeg megjelenítése forintban, két tizedesjeggyel
        }

        private void SzamlaMentes_Click(object sender, RoutedEventArgs e) //gomb megnyomása esetén TXT-be írja ki a következőket:
        {
            darab++;

            if (szamlaTetelek.Count == 0) //megnézi, van-e valamilyen tétel a számlán, ha nincs, akkor nem menti el a számlát és hibaüzenetet ír ki
            {
                MessageBox.Show("Nincs tétel a számlán!");
                return;
            }

            using (var writer = new System.IO.StreamWriter($"szamla_{darab}.txt")) //létrehozza a fájlt, amibe kiírja a számla adatait
            {
                writer.WriteLine("========== SZÁMLA ==========\n");

                writer.WriteLine("KIÁLLÍTÓ:");
                writer.WriteLine($"Név: {kiallitoNev}");
                writer.WriteLine($"Email: {kiallitoEmail}\n");

                writer.WriteLine("MEGRENDELŐ:");
                writer.WriteLine($"Név: {rendeloNev}");
                writer.WriteLine($"Email: {rendeloEmail}\n");

                writer.WriteLine("--------------------------------------------");
                writer.WriteLine("ID\tNév\tKategória\tÁr\tDb\tÖsszeg");

                foreach (var tetel in szamlaTetelek) //tételenként kiírja az adatokat a fájlba, tabulátorral elválasztva
                {
                    writer.WriteLine($"{tetel.ID}\t{tetel.Nev}\t{tetel.Kategoria}\t{tetel.Ar}\t{tetel.Darab}\t{tetel.Osszeg}");
                }

                writer.WriteLine("--------------------------------------------");
                writer.WriteLine($"VÉGÖSSZEG: {Osszesen_Text.Text}");
            }

            MessageBox.Show("Számla sikeresen mentve!"); //visszajelzés
            Close();
        }
    }
}
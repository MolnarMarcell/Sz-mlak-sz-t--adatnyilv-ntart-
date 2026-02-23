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

            termekek = termekekLista;

            szamlaTetelek = new ObservableCollection<SzamlaTetel>();
            SzamlaDataGrid.ItemsSource = szamlaTetelek;

            Termek_ComboBox.ItemsSource = termekek;
            Termek_ComboBox.DisplayMemberPath = "Nev";
            Termek_ComboBox.SelectionChanged += Termek_ComboBox_SelectionChanged;
        }

        private void Termek_ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Termek_ComboBox.SelectedItem is Termek_osztaly termek)
            {
                Keszlet_Label.Content = termek.Keszlet.ToString();
                Ar_Label.Content = termek.Ar + " Ft";
                mennyiseg = 0;
                Mennyiseg_TextBox.Text = "0";
            }
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            if (Termek_ComboBox.SelectedItem is Termek_osztaly termek)
            {
                if (mennyiseg < termek.Keszlet)
                {
                    mennyiseg++;
                    Mennyiseg_TextBox.Text = mennyiseg.ToString();
                }
                else
                {
                    MessageBox.Show("Nincs elég készlet!");
                }
            }
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (mennyiseg > 0)
            {
                mennyiseg--;
                Mennyiseg_TextBox.Text = mennyiseg.ToString();
            }
        }

        private void Hozzaadas_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Termek_ComboBox.SelectedItem is Termek_osztaly termek && mennyiseg > 0)
            {
                if (mennyiseg > termek.Keszlet)
                {
                    MessageBox.Show("Nincs elegendő készlet!");
                    return;
                }

                // Megnézzük, van-e már ilyen termék a számlán
                var letezoTetel = szamlaTetelek.FirstOrDefault(t => t.ID == termek.ID);

                if (letezoTetel != null)
                {
                    // Darabszám növelése
                    letezoTetel.Darab += mennyiseg;
                    letezoTetel.Osszeg = letezoTetel.Darab * letezoTetel.Ar;
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

                // Készlet csökkentése
                termek.Keszlet -= mennyiseg;
                Keszlet_Label.Content = termek.Keszlet.ToString();

                SzamlaDataGrid.Items.Refresh();
                SzamlaOsszesenSzamit();

                mennyiseg = 0;
                Mennyiseg_TextBox.Text = "0";
            }
        }

        private void TorlesElem_Click(object sender, RoutedEventArgs e)
        {
            if (SzamlaDataGrid.SelectedItem is SzamlaTetel tetel)
            {
                var termek = termekek.FirstOrDefault(t => t.ID == tetel.ID);
                if (termek != null)
                {
                    // Készlet visszaadása
                    termek.Keszlet += tetel.Darab;
                }

                szamlaTetelek.Remove(tetel);
                SzamlaOsszesenSzamit();

                if (Termek_ComboBox.SelectedItem == termek)
                {
                    Keszlet_Label.Content = termek.Keszlet.ToString();
                }
            }
        }

        private void Torles_Button_Click(object sender, RoutedEventArgs e)
        {
            mennyiseg = 0;
            Mennyiseg_TextBox.Text = "0";
            Termek_ComboBox.SelectedIndex = -1;
        }

        private void SzamlaOsszesenSzamit()
        {
            decimal osszesen = 0;
            foreach (var tetel in szamlaTetelek)
                osszesen += tetel.Osszeg;

            Osszesen_Text.Text = osszesen.ToString("0.00") + " Ft";
        }

        private void SzamlaMentes_Click(object sender, RoutedEventArgs e)
        {
            darab++;

            if (szamlaTetelek.Count == 0)
            {
                MessageBox.Show("Nincs tétel a számlán!");
                return;
            }

            using (var writer = new System.IO.StreamWriter($"szamla_{darab}.txt"))
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

                foreach (var tetel in szamlaTetelek)
                {
                    writer.WriteLine($"{tetel.ID}\t{tetel.Nev}\t{tetel.Kategoria}\t{tetel.Ar}\t{tetel.Darab}\t{tetel.Osszeg}");
                }

                writer.WriteLine("--------------------------------------------");
                writer.WriteLine($"VÉGÖSSZEG: {Osszesen_Text.Text}");
            }

            MessageBox.Show("Számla sikeresen mentve!");
            Close();
        }
    }
}
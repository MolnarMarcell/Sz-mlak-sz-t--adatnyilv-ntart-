using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Számlakészítő_adatnyilvántartó
{
    public partial class Szamla : Window
    {
        private int mennyiseg = 0;
        private ObservableCollection<SzamlaTetel> szamlaTetelek;
        private List<Termek_osztaly> termekek;

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

            // Termékek betöltése a ComboBox-ba
            Termek_ComboBox.ItemsSource = termekek;
            Termek_ComboBox.DisplayMemberPath = "Nev";
            Termek_ComboBox.SelectedValuePath = "ID";
            Termek_ComboBox.SelectionChanged += Termek_ComboBox_SelectionChanged;
        }

        // ComboBox kiválasztás - Készlet és Ár megjelenítése
        private void Termek_ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Termek_ComboBox.SelectedItem is Termek_osztaly termek)
            {
                Keszlet_Label.Content = termek.Termek.ToString();  // ← Így helyesen!
                Ar_Label.Content = termek.Ar.ToString() + " Ft";
                mennyiseg = 0;
                Mennyiseg_TextBox.Text = "0";
            }
        }

        // Plus gomb
        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            if (Termek_ComboBox.SelectedItem is Termek_osztaly termek)
            {
                // Csak akkor nővel, ha nincs túl készlet
                if (mennyiseg < termek.Keszlet)
                {
                    mennyiseg++;
                    Mennyiseg_TextBox.Text = mennyiseg.ToString();
                }
                else
                {
                    MessageBox.Show("Nincs elég készlet!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        // Minus gomb
        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (mennyiseg > 0)
            {
                mennyiseg--;
                Mennyiseg_TextBox.Text = mennyiseg.ToString();
            }
        }

        // Hozzáadás gomb
        private void Hozzaadas_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Termek_ComboBox.SelectedItem is Termek_osztaly termek && mennyiseg > 0)
            {
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
                SzamlaOsszesenSzamit();

                // Visszaállítás
                mennyiseg = 0;
                Mennyiseg_TextBox.Text = "0";
                Termek_ComboBox.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Válassz termeket és mennyiséget!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Törlés gomb egy tételnél
        private void TorlesElem_Click(object sender, RoutedEventArgs e)
        {
            if (SzamlaDataGrid.SelectedItem is SzamlaTetel tetel)
            {
                szamlaTetelek.Remove(tetel);
                SzamlaOsszesenSzamit();
            }
        }

        // Törlés gomb - bemenet törlése
        private void Torles_Button_Click(object sender, RoutedEventArgs e)
        {
            mennyiseg = 0;
            Mennyiseg_TextBox.Text = "0";
            Termek_ComboBox.SelectedIndex = -1;
        }

        // Összesen számítása
        private void SzamlaOsszesenSzamit()
        {
            decimal osszesen = 0;
            foreach (var tetel in szamlaTetelek)
            {
                osszesen += tetel.Osszeg;
            }
            Osszesen_Text.Text = osszesen.ToString("0.00") + " Ft";
        }

        // Számla mentése
        private void SzamlaMentes_Click(object sender, RoutedEventArgs e)
        {
            if (szamlaTetelek.Count == 0)
            {
                MessageBox.Show("Nincs tétel a számlán!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Itt mentsd el az adatokat (adatbázisba, fájlba, stb.)
            MessageBox.Show("Számla sikeresen mentve!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
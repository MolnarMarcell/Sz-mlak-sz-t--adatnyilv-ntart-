using System.Collections.ObjectModel;
using System.Windows;
using System.Collections.Generic;

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

            Termek_ComboBox.ItemsSource = termekek;
            Termek_ComboBox.DisplayMemberPath = "Nev";
            Termek_ComboBox.SelectionChanged += Termek_ComboBox_SelectionChanged;
        }

        private void Termek_ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Termek_ComboBox.SelectedItem is Termek_osztaly termek)
            {
                Keszlet_Label.Content = termek.Keszlet.ToString();
                Ar_Label.Content = termek.Ar.ToString() + " Ft";
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
                var tetel = new SzamlaTetel
                {
                    ID = termek.ID,
                    Nev = termek.Nev,
                    Kategoria = termek.Kategoria,
                    Ar = termek.Ar,
                    Darab = mennyiseg,
                    Osszeg = (decimal)termek.Ar * mennyiseg
                };

                szamlaTetelek.Add(tetel);
                SzamlaOsszesenSzamit();

                mennyiseg = 0;
                Mennyiseg_TextBox.Text = "0";
                Termek_ComboBox.SelectedIndex = -1;
            }
        }

        private void TorlesElem_Click(object sender, RoutedEventArgs e)
        {
            if (SzamlaDataGrid.SelectedItem is SzamlaTetel tetel)
            {
                szamlaTetelek.Remove(tetel);
                SzamlaOsszesenSzamit();
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
            if (szamlaTetelek.Count == 0)
            {
                MessageBox.Show("Nincs tétel a számlán!");
                return;
            }

            MessageBox.Show("Számla sikeresen mentve!");
            Close();
        }
    }
}

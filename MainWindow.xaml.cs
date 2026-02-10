using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Számlakészítő_adatnyilvántartó
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Termek_osztaly> Termékek = new List<Termek_osztaly>();



        public MainWindow()
        {
            InitializeComponent();

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
            int ID = Termékek.Count() + 1;
            string Név = TermékNév_TextBox.Text;
            string Kategória = cbKat.Text;
            int Ár = int.Parse(Ár_TextBox.Text);
            int Darab = int.Parse(Darab_TextBox.Text);
            //Termek_osztaly termék = new Termek_osztaly(ID, Név, Kategória, Ár, Darab);
            //Termékek.Add(termék);
            Termékek.Add(new Termek_osztaly(ID, Név, Kategória, Ár, Darab));


        }

        private void OpenTable_Click(object sender, RoutedEventArgs e)
        {
            Tablazat tabla = new Tablazat();
            tabla.Betölt(Termékek);
            tabla.Show();
        }

    }
}
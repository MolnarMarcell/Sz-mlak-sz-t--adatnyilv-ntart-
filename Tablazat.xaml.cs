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
    /// Interaction logic for Tablazat.xaml
    /// </summary>
    public partial class Tablazat : Window
    {
        public void Betölt(List<Termek_osztaly> Termék)
        {
            dgAdatok.Items.Clear();
            foreach (var item in Termék)
            {
                dgAdatok.Items.Add(item);
            }
        }

        public Tablazat()
        {
            InitializeComponent();
        }
    }
}

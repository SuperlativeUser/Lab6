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

namespace L6
{
    /// <summary>
    /// Interaction logic for Dificult.xaml
    /// </summary>
    public partial class Dificult : Window
    {
        public int dificult = 0;
       
        public Dificult()
        {
            InitializeComponent();
            
        }

        private void easy_Click(object sender, RoutedEventArgs e)
        {
            dificult = 0;
            this.Hide();
        }

        private void medium_Click(object sender, RoutedEventArgs e)
        {
            dificult = 1;
            this.Hide();
        }

        private void hard_Click(object sender, RoutedEventArgs e)
        {
            dificult = 2;
            this.Hide();
        }
    }
}

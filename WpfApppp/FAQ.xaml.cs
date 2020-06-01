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

namespace WpfApppp
{
    /// <summary>
    /// Interaction logic for FAQ.xaml
    /// </summary>
    public partial class FAQ : Window
    {
        public FAQ()
        {
            InitializeComponent();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)// Для перетаскивания окна за любую область.
        {
            DragMove();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}

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
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        
        MainWindow main = Application.Current.MainWindow as MainWindow;
        FAQ faq = new FAQ();

        public TaskWindow()
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

        private void CheckBoxWork(object sender, RoutedEventArgs e)
        {
            main.proverkaMusicStart = (sender as CheckBox)?.IsChecked ?? false;
        }

        private void CheckBoxEnd(object sender, RoutedEventArgs e)
        {
            main.proverkaMusicEnd = (sender as CheckBox)?.IsChecked ?? false;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            faq.Owner = this;
            if (faq.IsActive == false)
            {
                faq.Show();
            }
        }
    }
    }



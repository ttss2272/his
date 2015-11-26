using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Image
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            User.Login ulogin = new User.Login();
            this.Close();
            ulogin.Show();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Admin.Login alogin = new Admin.Login();
            this.Close();
            alogin.Show();
        }
    }
}

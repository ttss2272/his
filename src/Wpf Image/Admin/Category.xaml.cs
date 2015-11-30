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
using System.Windows.Shapes;
using WPF_ImageClassLibrary;

namespace Wpf_Image.Admin
{
    /// <summary>
    /// Interaction logic for Category.xaml
    /// </summary>
    public partial class Category : Window
    {
        
        clCategory objCategory = new clCategory();
        public Category()
        {
            InitializeComponent();
        }

        private void btnCategory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string result = objCategory.saveCategory(0, txtCategory.Text, 1, 0);

                MessageBox.Show(result, "Result Message", MessageBoxButton.OKCancel);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Message", MessageBoxButton.OKCancel);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void getCategory(int CatID)
        { 
        
        }
        #region----------------------WindowLoaded()-----------------------
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindGrid();
        }
        #endregion
        /*
         * Created By:-Sameer Shinde
         * Date:-30/11/2015
         * Purpose:-Bind data to gridview
         */
        #region---------------------BindGrid-----------------------------
        private void BindGrid()
        {
            
        }
        #endregion

    }
}

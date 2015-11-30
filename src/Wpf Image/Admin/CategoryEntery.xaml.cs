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
using System.Data;

namespace Wpf_Image.Admin
{
    /// <summary>
    /// Interaction logic for CategoryEntery.xaml
    /// </summary>
    public partial class CategoryEntery : Window
    {
        
        public CategoryEntery()
        {
            InitializeComponent();
           
        }
        #region-----------------Declare Variables GlobalVariables()----------------
        clCategory objCategory = new clCategory();
        int UpID,CatId,IsActive,IsDeleted;
        string CategoryName, UpdatedDate;
       
        #endregion
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            canvas1.Visibility = Visibility.Hidden;
            BindGridView();
            btnDelete.IsEnabled = false;
        }
        #region--------------------------------btnAdd_Click----------------------------------
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            dvCategory.Visibility = Visibility.Hidden;
            canvas1.Visibility = Visibility.Visible;
            canvas1.Margin = new Thickness(138, 93, 0, 0);
            txtSearchCatName.IsEnabled = false;
            btnSearch.IsEnabled = false;
            
        }
        #endregion
        /*
         * Created By:-Sameer A. Shinde
         * Date:-30/11/2015
         * Purpose:-Clear all feilds
         */
        #region---------------------------ClearFields()---------------------------------------
        private void ClearFields()
        {
            txtSearchCatName.Text = "";
            txtCatName.Text = "";
            canvas1.Visibility = Visibility.Hidden;
            dvCategory.Visibility = Visibility.Visible;
            BindGridView();
            txtSearchCatName.IsEnabled = true;
            btnSearch.IsEnabled = true;
            btnDelete.IsEnabled = false;
        }
        #endregion
        /*
         * Created By:-Sameer A. Shinde
         * Date:-30/11/2015
         * Purpose:-Bind Category on Gridview
         */
        #region------------------------BindGridView()-------------------------------------------
        private void BindGridView()
        {
            DataSet ds = objCategory.BindCategory(0, txtSearchCatName.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dvCategory.ItemsSource = ds.Tables[0].DefaultView;
            }
            else
            {
                dvCategory.ItemsSource = null;
                MessageBox.Show("Data Not Found", "Message");
            }
        }
        #endregion
        
        /*
         * Created By:-Sameer A. Shinde
         * Date:-30/11/2015
         * Purpose:-Save Category
         */
        #region--------------------btnSave_Click()-------------------------------------------------
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validate())
                {
                    Validate();
                    SetParameters();
                    SaveDetails();
                    BindGridView();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        
        #endregion
        /*
         * Created By:-Sameer A. Shinde
         * Date:-30/11/2015
         * Purpose:-Set Parameters for Save Category Details
         */
        #region--------------------------SetParameters()-----------------------------------------------
        private void SetParameters()
        {
            CatId = UpID;
            CategoryName = txtCatName.Text;
            IsActive = 1;
            IsDeleted = 0;
            UpdatedDate = DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss tt");

        }
        #endregion
        /*
         * Created By:-Sameer A. Shinde
         * Date:-30/11/2015
         * Purpose:-Save Category Details
         */
        #region-------------------------------SaveDetails------------------------------------
        private void SaveDetails()
        {
            try
            {
                string result = objCategory.saveCategory(CatId,CategoryName,IsActive,IsDeleted);

                MessageBox.Show(result, "Result Message", MessageBoxButton.OKCancel);
                ClearFields();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Message", MessageBoxButton.OKCancel);
            }
        }
        #endregion
        /*
         * Created By:-Sameer A. Shinde
         * Date:-30/11/2015
         * Purpose:-Validate Category Details
         */
        #region------------------Validate()------------------------------------------------
        private bool Validate()
        {
            if (txtCatName.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Category Name.", "Category Name Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            
            else
            {
                return true;
            }
        }
        #endregion
        #region---------------------RowDoubleclick------------------------------------------
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            dvCategory.Visibility = Visibility.Hidden;
            canvas1.Visibility = Visibility.Visible;
            canvas1.Margin = new Thickness(138, 93, 0, 0);
            txtSearchCatName.IsEnabled = false;
            btnSearch.IsEnabled = false;
            try
            {
                object item = dvCategory.SelectedItem;
                string CategoryName = (dvCategory.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;



                DataSet ds = objCategory.BindCategory(0, CategoryName);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        UpID = Convert.ToInt32(ds.Tables[0].Rows[0]["CategoryID"]);
                        txtCatName.Text = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                        

                        btnDelete.IsEnabled = true;
                        btnSave.Content = "Update";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion

        private void btnPanelCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }
        #region---------------------------------------btnDelete_Click----------------------------------------------------------
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validate())
                {
                    MessageBoxResult result = MessageBox.Show("Do You Want to delete?", "Delete", MessageBoxButton.YesNoCancel);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        SetParameters();
                        DeleteCategory();
                        BindGridView();
                    }
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteCategory()
        {
            if (UpID != 0)
            {
                CatId = UpID;

                string Result = objCategory.DeleteCategory(CatId,UpdatedDate);
                if (Result == "Category Deleted Sucessfully!!!")
                {
                    MessageBox.Show(Result, "Category Delete Sucessfully", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show(Result, "Error To Delete", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please Select Category Name ", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }
        #endregion
        #region--------------------------btnSearch_Click()-------------------------------------------------------------
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchCatName.Text.Trim()))
                {
                    DataSet ds = objCategory.BindCategory(0, txtSearchCatName.Text);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dvCategory.ItemsSource = ds.Tables[0].DefaultView;
                        //grdvSubject.DataContext = ds.Tables[0].DefaultView;
                        //grdvSubject.Columns[0].Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        dvCategory.ItemsSource = null;
                        MessageBox.Show("No Data Available");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Category Name", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtSearchCatName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion
        
    }
}

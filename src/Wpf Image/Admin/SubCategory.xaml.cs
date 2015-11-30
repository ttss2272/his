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
    /// Interaction logic for SubCategory.xaml
    /// </summary>
    public partial class SubCategory : Window
    {
        public SubCategory()
        {
            InitializeComponent();
        }
        #region-----------------Declare Variables GlobalVariables()----------------
        clCategory objCategory = new clCategory();
        clSubCategory objSubCategory = new clSubCategory();
        int UpID, SubCatId, IsActive, IsDeleted,CategoryID;
        string SubCategoryName, UpdatedDate;

        #endregion
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            canvas1.Visibility = Visibility.Hidden;
            BindGridView();
            btnDelete.IsEnabled = false;
            BindCategory();
            cmbSearchCatName.SelectedIndex = 0;
        }
        #region--------------------------------BindCategory()---------------------------------
        private void BindCategory()
        {
            try
            {
                DataSet ds = objCategory.BindCategoryName();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbSearchCatName.DataContext = ds.Tables[0].DefaultView;
                    cmbSearchCatName.DisplayMemberPath = ds.Tables[0].Columns["CategoryName"].ToString();
                    cmbSearchCatName.SelectedValuePath = ds.Tables[0].Columns["CategoryName"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion
        
        #region--------------------------------btnAdd_Click----------------------------------
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            dvCategory.Visibility = Visibility.Hidden;
            canvas1.Visibility = Visibility.Visible;
            canvas1.Margin = new Thickness(150, 125, 0, 0);
            txtSearchSubCatName.IsEnabled = false;
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

            txtSearchSubCatName.Text = "";
            txtSubCatName.Text = "";
            canvas1.Visibility = Visibility.Hidden;
            dvCategory.Visibility = Visibility.Visible;
            BindGridView();
            txtSearchSubCatName.IsEnabled = true;
            btnSearch.IsEnabled = true;
            btnDelete.IsEnabled = false;
            cmbSearchCatName.SelectedIndex = 0;
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
            DataSet ds = objCategory.BindCategory(0, txtSearchSubCatName.Text);
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
            SubCatId = UpID;
            CategoryID = Convert.ToInt32(cmbSaveSubCatName.SelectedValue);
            SubCategoryName = txtSubCatName.Text;
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
                string result = objSubCategory.SaveSubCategory(SubCatId, CategoryID,SubCategoryName, IsActive, IsDeleted);

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
            if (txtSearchSubCatName.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Sub Category Name.", "Sub Category Name Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (cmbSearchCatName.Text == "")
            {
                MessageBox.Show("Please Select Category Name.", "Category Name Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            canvas1.Margin = new Thickness(160, 84, 0, 0);
            txtSearchSubCatName.IsEnabled = false;
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

                        UpID = Convert.ToInt32(ds.Tables[0].Rows[0]["SubCategoryID"]);
                        txtSubCatName.Text = ds.Tables[0].Rows[0]["SubCategoryName"].ToString();


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
                SubCatId = UpID;

                string Result = objCategory.DeleteCategory(SubCatId, UpdatedDate);
                if (Result == "Sub Category Deleted Sucessfully!!!")
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
                if (!string.IsNullOrEmpty(txtSearchSubCatName.Text.Trim()))
                {
                    DataSet ds = objCategory.BindCategory(0, txtSearchSubCatName.Text);
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
                    MessageBox.Show("Please Enter Sub Category Name", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtSearchSubCatName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion

        private void dvCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
    }
}

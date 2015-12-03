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
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
namespace Wpf_Image.Admin
{
    /// <summary>
    /// Interaction logic for NewProduct.xaml
    /// </summary>
    public partial class NewProduct : Window
    {
        public NewProduct()
        {
            InitializeComponent();
        }
        #region-----------------Declare Variables GlobalVariables()----------------
        clCategory objCategory = new clCategory();
        clSubCategory objSubCategory = new clSubCategory();
        clProduct objProduct = new clProduct();
        int UpID, ProductId,SubCategoryID, IsActive, IsDeleted, CategoryID;
        string ProductName, ImagePath, UpdatedDate, filepath;

        #endregion
        #region------------------------------Window_Loaded()-----------------------
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            canvas1.Visibility = Visibility.Hidden;
            BindGridView();
            btnDelete.IsEnabled = false;
            BindCategory();
            cmbSearchCatName.SelectedIndex = 0;
            txtAddImagePath.IsEnabled = true;
        }
        #endregion
        #region--------------------------------BindCategory()---------------------------------
        private void BindCategory()
        {
            try
            {
                DataSet ds = objCategory.BindCategoryName();
                if (ds.Tables[0].Rows.Count > 0)
                {

                    cmbSearchCatName.DisplayMemberPath = ds.Tables[0].Columns["CategoryName"].ToString();
                    cmbSearchCatName.SelectedValuePath = ds.Tables[0].Columns["CategoryID"].ToString();
                    cmbSearchCatName.DataContext = ds.Tables[0].DefaultView;
                    cmbSearchCatName.SelectedValue = "-1";
                    cmbAddCatName.DisplayMemberPath = ds.Tables[0].Columns["CategoryName"].ToString();
                    cmbAddCatName.SelectedValuePath = ds.Tables[0].Columns["CategoryID"].ToString();
                    cmbAddCatName.DataContext = ds.Tables[0].DefaultView;
                    cmbAddCatName.SelectedValue = "-1";
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
         * Date:-01/12/2015
         * Purpose:-Add Click
         */
        #region------------------------btnAdd_Click()------------------------------------------------
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            btnSave.Content = "Save";
            cmbAddCatName.SelectedIndex = 0;
            ClearFields();
            txtAddImagePath.IsEnabled = false;
            dgvProduct.Visibility = Visibility.Hidden;
            canvas1.Visibility = Visibility.Visible;
            canvas1.Margin = new Thickness(150, 155, 0, 0);
            cmbSearchCatName.IsEnabled = false;
            cmbSubCatName.IsEnabled = false;
            txtProductName.IsEnabled = false;
            btnSearch.IsEnabled = false;
            cmbSearchCatName.IsEnabled = false;
        }
        #endregion
        /*
         * Created By:-Sameer A. Shinde
         * Date:-01/12/2015
         * Purpose:-Clear all feilds
         */
        #region---------------------------ClearFields()---------------------------------------
        private void ClearFields()
        {
            BindCategory();
            cmbSearchCatName.SelectedIndex = 0;
            cmbSubCatName.SelectedIndex = 0;
            txtProductName.Text = "";
            canvas1.Visibility = Visibility.Hidden;
            dgvProduct.Visibility = Visibility.Visible;
            BindGridView();
            cmbSearchCatName.IsEnabled = true;
            cmbSubCatName.IsEnabled = true;
            txtProductName.IsEnabled = true;
            btnSearch.IsEnabled = true;
            btnDelete.IsEnabled = false;
            cmbSearchCatName.IsEnabled = true;
            cmbSearchCatName.SelectedIndex = 0;
            txtAddProductName.Text = "";
            txtAddImagePath.Text = "";
            image1.Source = null;
        }
        #endregion
        /*
         * Created By:-Sameer A. Shinde
         * Date:-01/12/2015
         * Purpose:-Bind Product Details on Gridview
         */
        #region------------------------BindGridView()-------------------------------------------
        private void BindGridView()
        {
            DataSet ds = objProduct.BindProductToGrid(0, Convert.ToInt32(cmbSearchCatName.SelectedValue), Convert.ToInt32(cmbSubCatName.SelectedValue), txtProductName.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dgvProduct.ItemsSource = ds.Tables[0].DefaultView;
                string imaPath = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                string name = null;
                //name = System.IO.Path.GetFileName(filepath);
                string destinationPath = GetDestinationPath(name, "\\Images\\Admin\\Image");
                System.Windows.Media.Imaging.BitmapImage logo = new System.Windows.Media.Imaging.BitmapImage();
                logo.BeginInit();
                BitmapImage btm = new BitmapImage(new Uri(destinationPath, UriKind.Relative));
                logo.UriSource = new Uri(destinationPath + imaPath);
                logo.EndInit();
               // dgvProduct.ItemsSource = ds.Tables[0].Columns["logo"].ToString();
            }
            else
            {
                dgvProduct.ItemsSource = null;
                MessageBox.Show("Data Not Found", "Message");
            }  
        }
        #endregion
        /*
         * Created By:-Sameer A. Shinde
         * Date:-01/12/2015
         * Purpose:-Save  Product Details 
         */
        #region------------------------btnSave_Click()-------------------------------------------
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validate())
                {

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
         * Date:-01/12/2015
         * Purpose:-Save Product Details
         */
        #region-------------------------------SaveDetails------------------------------------
        private void SaveDetails()
        {
            try
            {
                string result = objProduct.SaveProduct(ProductId, CategoryID, SubCategoryID,ProductName,ImagePath,IsActive, IsDeleted);
                string name = System.IO.Path.GetFileName(filepath);
                string destinationPath = GetDestinationPath(name, "\\Images\\Admin\\Image");

                File.Copy(filepath, destinationPath, true);
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
         * Date:-01/12/2015
         * Purpose:-Copy Image to file folder
         */
        #region----------------------GetDestinationPath()----------------------------------------------
        private string GetDestinationPath(string filename, string FolderName)
        {
           String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            

            appStartPath = String.Format(appStartPath + "\\{0}\\" + filename, FolderName);
            return appStartPath;
        }
        #endregion
        /*
         * Created By:-Sameer A. Shinde
         * Date:-01/12/2015
         * Purpose:-Validate Category Details
         */
        #region------------------Validate()------------------------------------------------
        private bool Validate()
        {
            if (cmbAddCatName.SelectedValue == "-1")
            {
                MessageBox.Show("Please Select Catregory.", "Category Name Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbAddCatName.Focus();
                return false;
            }

            else if (cmbAddSubCatName.SelectedValue == "-1")
            {
                MessageBox.Show("Please Select Sub Category Name.", "Sub Category Name Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbAddSubCatName.Focus();
                return false;
            }

            else if (txtAddProductName.Text == "")
            {
                MessageBox.Show("Please Enter Product Name or Description.", "Product Name Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }
            else if (txtAddImagePath.Text == "")
            {
                MessageBox.Show("Please Browse Image.", "Browse Image Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        /*
         * Created By:-Sameer A. Shinde
         * Date:-01/12/2015
         * Purpose:-Set Parameters for Save Product Details
         */
        #region--------------------------SetParameters()-----------------------------------------------
        private void SetParameters()
        {
            ProductId = UpID;
            CategoryID = Convert.ToInt32(cmbAddCatName.SelectedValue);
            SubCategoryID = Convert.ToInt32(cmbAddSubCatName.SelectedValue);
            ProductName = txtAddProductName.Text;
            ImagePath = txtAddImagePath.Text;
            IsActive = 1;
            IsDeleted = 0;
            UpdatedDate = DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss tt");

        }
        #endregion
        /*
         * Created By:-Sameer A. Shinde
         * Date:-01/12/2015
         * Purpose:-Browse Image for Save
         */
        #region--------------------------SetParameters()-----------------------------------------------
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Multiselect = false;
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                bool? result = open.ShowDialog();

                if (result == true)
                {
                    filepath = open.FileName; // Stores Original Path in Textbox D:\SVN_SchoolTimeTable\src\SchoolManagement\Logo\   
                    ImageSource imgsource = new BitmapImage(new Uri(filepath)); // Just show The File In Image when we browse It
                    image1.Source = imgsource;

                }

                string name = System.IO.Path.GetFileName(filepath);
                txtAddImagePath.Text = name;
               
                //string destinationPath = GetDestinationPath(name, "Images\\Admin\\Image");
                

                //File.Copy(filepath, destinationPath, true);

          }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private string GetTargetPath(string targetPath)
        {
            String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);


            appStartPath = String.Format(appStartPath  + targetPath);
            return appStartPath;
        }
        #endregion
        #region------------------------Close_click()----------------------------------------------------
        private void btnPanelClose_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }
        #endregion
        /*
         * Created By:-Sameer A. Shinde
         * Date:-01/12/2015
         * Purpose:-Bind Sub Category Name to dropdown
         */
        #region--------------------------cmbSearchCatName_SelectionChanged()-----------------------------------------------
        private void cmbSearchCatName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindSubCatName();
        }
        #endregion
        /*
         * Created By:-Sameer A. Shinde
         * Date:-01/12/2015
         * Purpose:-Bind Sub Category Name to dropdown
         */
        #region--------------------------BindSubCatName()-----------------------------------------------
        private void BindSubCatName()
        {
            try
            {
                DataSet ds = objSubCategory.BindSubCategoryName(Convert.ToInt32(cmbSearchCatName.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {

                    cmbSubCatName.DisplayMemberPath = ds.Tables[0].Columns["SubCategoryName"].ToString();
                    cmbSubCatName.SelectedValuePath = ds.Tables[0].Columns["SubCategoryID"].ToString();
                    cmbSubCatName.DataContext = ds.Tables[0].DefaultView;
                    cmbSubCatName.SelectedValue = "-1";
                    
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
         * Date:-01/12/2015
         * Purpose:-Bind Sub Category Name to dropdown For add
         */
        #region--------------------------BindSubCatNameforAdd()-----------------------------------------------
        private void cmbAddCatName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindSubCatNameForAdd();
        }
        #endregion
        /*
         * Created By:-Sameer A. Shinde
         * Date:-01/12/2015
         * Purpose:-Bind Sub Category Name to dropdown for Add
         */
        #region--------------------------BindSubCatNameForAdd()-----------------------------------------------
        private void BindSubCatNameForAdd()
        {
            try
            {
                DataSet ds = objSubCategory.BindSubCategoryName(Convert.ToInt32(cmbAddCatName.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {

                    cmbAddSubCatName.DisplayMemberPath = ds.Tables[0].Columns["SubCategoryName"].ToString();
                    cmbAddSubCatName.SelectedValuePath = ds.Tables[0].Columns["SubCategoryID"].ToString();
                    cmbAddSubCatName.DataContext = ds.Tables[0].DefaultView;
                    cmbAddSubCatName.SelectedValue = "-1";

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
         * Date:-02/12/2015
         * Purpose:-Search Product
         * */
        #region--------------------------btnSearch_Click()-----------------------------------------------
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (!string.IsNullOrEmpty(txtProductName.Text.Trim()))
                {
                    DataSet ds = objProduct.BindProductToGrid(0, Convert.ToInt32(cmbSearchCatName.SelectedValue), Convert.ToInt32(cmbSubCatName.SelectedValue), txtProductName.Text);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dgvProduct.ItemsSource = ds.Tables[0].DefaultView;

                    }
                    else
                    {
                        dgvProduct.ItemsSource = null;
                        MessageBox.Show("No Data Available");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Product Name", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtProductName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion
        #region---------------------RowDoubleclick------------------------------------------
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //dvCategory.Visibility = Visibility.Hidden;
            //canvas1.Visibility = Visibility.Visible;
            //canvas1.Margin = new Thickness(150, 125, 0, 0);
            //cmbSearchCatName.IsEnabled = false;
            //txtSearchSubCatName.IsEnabled = false;
            //btnSearch.IsEnabled = false;
            //try
            //{
            //    object item = dvCategory.SelectedItem;
            //    string CategoryName = (dvCategory.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            //    string SubCategoryName = (dvCategory.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;



            //    DataSet ds = objSubCategory.BindCategoryInSub(0, CategoryID, SubCategoryName);
            //    if (ds.Tables.Count > 0)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {

            //            UpID = Convert.ToInt32(ds.Tables[0].Rows[0]["SubCategoryID"]);
            //            BindCategoryforAdd();
            //            cmbSaveSubCatName.Text = ds.Tables[0].Rows[0]["CategoryName"].ToString();
            //            txtSubCatName.Text = ds.Tables[0].Rows[0]["SubCategoryName"].ToString();


            //            btnDelete.IsEnabled = true;
            //            btnSave.Content = "Update";
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }
        #endregion
    }
}

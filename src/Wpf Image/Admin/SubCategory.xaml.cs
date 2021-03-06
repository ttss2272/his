﻿using System;
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
        int UpID, SubCatId, IsActive, IsDeleted, CategoryID;
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
                    
                    cmbSearchCatName.DisplayMemberPath = ds.Tables[0].Columns["CategoryName"].ToString();
                    cmbSearchCatName.SelectedValuePath = ds.Tables[0].Columns["CategoryID"].ToString();
                    cmbSearchCatName.DataContext = ds.Tables[0].DefaultView;
                    cmbSearchCatName.SelectedValue = "-1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion
        #region--------------------------------BindCategoryforAdd()---------------------------------
        private void BindCategoryforAdd()
        {
            try
            {
                DataSet ds = objCategory.BindCategoryName();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    cmbSaveSubCatName.DisplayMemberPath = ds.Tables[0].Columns["CategoryName"].ToString();
                    cmbSaveSubCatName.SelectedValuePath = ds.Tables[0].Columns["CategoryID"].ToString();
                    cmbSaveSubCatName.DataContext = ds.Tables[0].DefaultView;
                    cmbSaveSubCatName.SelectedValue = "-1";
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
            btnSave.Content = "Save";
            cmbSaveSubCatName.SelectedIndex = 0;
            ClearFields();
            BindCategoryforAdd();
            dvCategory.Visibility = Visibility.Hidden;
            canvas1.Visibility = Visibility.Visible;
            canvas1.Margin = new Thickness(150, 125, 0, 0);
            txtSearchSubCatName.IsEnabled = false;
            btnSearch.IsEnabled = false;
            cmbSearchCatName.IsEnabled = false;
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
            BindCategory();
            BindCategoryforAdd();
            txtSearchSubCatName.Text = "";
            txtSubCatName.Text = "";
            canvas1.Visibility = Visibility.Hidden;
            dvCategory.Visibility = Visibility.Visible;
            BindGridView();
            txtSearchSubCatName.IsEnabled = true;
            btnSearch.IsEnabled = true;
            btnDelete.IsEnabled = false;
            cmbSearchCatName.IsEnabled = true;
            cmbSearchCatName.SelectedIndex = 0;
            cmbSaveSubCatName.SelectedIndex = 0;

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
            DataSet ds = objSubCategory.BindCategoryInSub(0,CategoryID, txtSearchSubCatName.Text);
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
            CategoryID =Convert.ToInt32(cmbSaveSubCatName.SelectedValue);
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
            if (cmbSaveSubCatName.SelectedValue == "-1")
            {
                MessageBox.Show("Please Select Catregory.", "Category Name Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbSaveSubCatName.Focus();
                    return false;
            }
            else if (txtSubCatName.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Sub Category Name.", "Sub Category Name Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtSubCatName.Focus();
                return false;
            }

            else if (cmbSaveSubCatName.Text == "")
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
            canvas1.Margin = new Thickness(150, 125, 0, 0);
            cmbSearchCatName.IsEnabled = false;
            txtSearchSubCatName.IsEnabled = false;
            btnSearch.IsEnabled = false;
            try
            {
                object item = dvCategory.SelectedItem;
                string CategoryName = (dvCategory.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                string SubCategoryName = (dvCategory.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;



                DataSet ds = objSubCategory.BindCategoryInSub(0,CategoryID,SubCategoryName);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        UpID = Convert.ToInt32(ds.Tables[0].Rows[0]["SubCategoryID"]);
                        BindCategoryforAdd();
                        cmbSaveSubCatName.Text = ds.Tables[0].Rows[0]["CategoryName"].ToString();
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

                string Result = objSubCategory.DeleteSubCategory(SubCatId, UpdatedDate);
                if (Result == "Sub Category Deleted Sucessfully!!!")
                {
                    MessageBox.Show(Result, "Sub Category Delete Sucessfully", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    DataSet ds = objSubCategory.BindCategoryInSub(0,CategoryID,txtSearchSubCatName.Text);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dvCategory.ItemsSource = ds.Tables[0].DefaultView;
                      
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartInventory
{
    /// <summary>
    /// Interaction logic for InvetoryWindow.xaml
    /// </summary>
    public partial class InvetoryWindow : Window
    {
        public InvetoryWindow()
        {
            InitializeComponent();
            LoadGrid();
        }

        SqlConnection con = new SqlConnection("Data Source=Frotek;Initial Catalog=Products; Integrated Security=True");
        public void LoadGrid()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from prodList", con);
                DataTable dt = new DataTable();
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
                datagrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void clearData()
        {
            txtTitle.Clear();
            txtCategory.Clear();
            txtWarranty.Clear();
            txtPrice.Clear();
            txtSearch.Clear();
        }

        public bool isValid()
        {
            if (txtTitle.Text == string.Empty)
            {
                MessageBox.Show("Title is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtCategory.Text == string.Empty)
            {
                MessageBox.Show("Category is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (prodDate.Text == string.Empty)
            {
                MessageBox.Show("Production date is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtWarranty.Text == string.Empty)
            {
                MessageBox.Show("Warranty is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtPrice.Text == string.Empty)
            {
                MessageBox.Show("Price is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update prodList set title= '" + txtTitle.Text + "', category= '" + txtCategory.Text + "', prodDate= '" + prodDate.SelectedDate + "', warranty= '" + txtWarranty.Text + "', price= '" + txtPrice.Text + "' WHERE ID= '" + txtSearch.Text + "' ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been successfully updated","Updated",MessageBoxButton.OK,MessageBoxImage.Information);

            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                clearData();
                LoadGrid();
            }
        }

        private void btnDelete_Click(object sender,RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from prodList where ID=" +txtSearch.Text+" ",con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been deleted","Deleted",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                con.Close();
                clearData();
                LoadGrid();
                con.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Item not deleted",ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    int nWarranty = Convert.ToInt32(txtWarranty.Text);
                    int nPrice = Convert.ToInt32(txtPrice.Text);
                    SqlCommand cmd = new SqlCommand("INSERT INTO prodList VALUES(@title,@category,@prodDate,@warranty,@price)");
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@category", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@prodDate", prodDate.SelectedDate);
                    cmd.Parameters.AddWithValue("@warranty",nWarranty);
                    cmd.Parameters.AddWithValue("@price", nPrice);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Successfuly added", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
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
    /// Interaction logic for SupplierWindow.xaml
    /// </summary>
    public partial class SupplierWindow : Window
    {
        public SupplierWindow()
        {
            InitializeComponent();
            LoadGrid();
        }


        SqlConnection con = new SqlConnection("Data Source=Frotek;Initial Catalog=Products; Integrated Security=True");

        public void LoadGrid()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from supplierList", con);
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
            txtName.Clear();
            txtContact.Clear();
            txtAdress.Clear();
            txtPhone.Clear();
            suptxtSearch.Clear();
        }

        public bool isValid()
        {
            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Supplier Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtContact.Text == string.Empty)
            {
                MessageBox.Show("Contact name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtAdress.Text == string.Empty)
            {
                MessageBox.Show("Adress is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtPhone.Text == string.Empty)
            {
                MessageBox.Show("Phone number is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void supBtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update supplierList set supplierName= '" + txtName.Text + "', contactName= '" + txtContact.Text + "', supplierAdress= '" + txtAdress.Text + "', supplierNumber= '" + txtPhone.Text + "', price= '" + suptxtSearch.Text + "' ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been successfully updated", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (SqlException ex)
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

        private void supBtnDelete_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from supplierList where ID=" + suptxtSearch.Text + " ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                con.Close();
                clearData();
                LoadGrid();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Item not deleted", ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Item not deleted", ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void supBtnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO supplierList VALUES(@supplierName,@contactName,@supplierAdress,@supplierNumber)");
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@supplierName", txtName.Text);
                    cmd.Parameters.AddWithValue("@contactName", txtContact.Text);
                    cmd.Parameters.AddWithValue("@supplierAdress", txtAdress.Text);
                    cmd.Parameters.AddWithValue("@supplierNumber", txtPhone.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Successfuly added", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}

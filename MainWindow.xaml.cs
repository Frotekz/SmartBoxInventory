using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartInventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            fullName();

        }


        private void fullName()
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=FROTEK;Initial Catalog=LoginDB; Integrated Security=True;");

            try
            {
                sqlCon.Open();

                string sql = @" SELECT FullName FROM userTable where UserName=@UserName";

                SqlCommand comm = new SqlCommand(sql, sqlCon);

                comm.Parameters.AddWithValue("@UserName", "admin");

                using SqlDataReader reader = comm.ExecuteReader();
                if (!reader.Read())
                    throw new Exception("Something is very wrong");

                int fullname = reader.GetOrdinal("FullName");

                FullName.Text = reader.GetString(fullname);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            InvetoryWindow invetoryWindow = new InvetoryWindow();
            invetoryWindow.Show();
        }

        private void userInfoButton_Click(object sender, RoutedEventArgs e)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.Show();
        }

        private void btnSupply_Click(object sender, RoutedEventArgs e)
        {
            SupplierWindow supplierWindow = new SupplierWindow();
            supplierWindow.Show();
        }
    }
}

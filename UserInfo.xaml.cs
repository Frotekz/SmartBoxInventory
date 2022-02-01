using System;
using System.Collections.Generic;
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
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class UserInfo : Window
    {
        public UserInfo()
        {
            InitializeComponent();
            getUserInfo();
        }

        private void getUserInfo()
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=FROTEK;Initial Catalog=LoginDB; Integrated Security=True;");

            try
            {
                sqlCon.Open();

                string sql = @" SELECT UserName,FullName, Position,Adress,number FROM userTable where UserName=@UserName";

                SqlCommand comm = new SqlCommand(sql, sqlCon);

                comm.Parameters.AddWithValue("@UserName", "admin");

                using SqlDataReader reader = comm.ExecuteReader();
                if (!reader.Read())
                    throw new Exception("Something is very wrong");

                int userName = reader.GetOrdinal("UserName");
                int fullname = reader.GetOrdinal("FullName");
                int position = reader.GetOrdinal("Position");
                int Adress = reader.GetOrdinal("Adress");
                int number = reader.GetOrdinal("number");

                infoUser.Text = reader.GetString(userName);
                infoFull.Text = reader.GetString(fullname);
                infoPosition.Text = reader.GetString(position);
                infoAdress.Text = reader.GetString(Adress);
                infoNumber.Text = reader.GetString(number);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

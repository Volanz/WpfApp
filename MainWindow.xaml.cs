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
using System.Data;
using System.Data.SqlClient;
////
    //Записи должны храниться в БД в таблице Invoice( поля уникальный идентификатор, дата, клиент, сумма счета)
    //В пользовательском интерфейсе реализовать отображение записей из БД. 
//-Должна быть возможность добавлять, изменять, удалять записи.
//-Выводить сумму всех счетов.
//-Поиск по ФИО клиента.


namespace WPFParallelCodes

{
    public partial class MainWindow : Window
    {
      
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnFill_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                String connectionString = "Data Source=WORK7879\\SQLEXPRESS;Initial Catalog=Clients;User ID=sa;Password=123";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from Users", con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        myDataGrid.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("подключение не удалось");
                return;
            }
        }


        private  void Button_Click(object sender, RoutedEventArgs e)
        {
                try 
                {
                    String connectionString = "Data Source=WORK7879\\SQLEXPRESS;Initial Catalog=Clients;User ID=sa;Password=123";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        String query = "INSERT INTO dbo.Users (Name,Summa,Date) VALUES (@Name, @Summa, @Date)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Name", Name.Text);
                            command.Parameters.AddWithValue("@Summa", Summa.Text);
                            command.Parameters.AddWithValue("@Date", Convert.ToDateTime(Date.Text));
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                MessageBox.Show("подключение не удалось");
                    return;
                }

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {

            try
            {
                String connectionString = "Data Source=WORK7879\\SQLEXPRESS;Initial Catalog=Clients;User ID=sa;Password=123";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    String query = "UPDATE [dbo].[Users] SET [Name] = @Name, [Summa] = @Summa, [Date] = @Date WHERE [ID] = @Id ";

                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {            
                        command.Parameters.AddWithValue("@Id", Id1.Text);
                        command.Parameters.AddWithValue("@Name", Name_Copy.Text);
                        command.Parameters.AddWithValue("@Summa", Summa_Copy.Text);
                        command.Parameters.AddWithValue("@Date", Convert.ToDateTime(Date_Copy.Text));
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("подключение не удалось");
                return;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            try
            {
                String connectionString = "Data Source=WORK7879\\SQLEXPRESS;Initial Catalog=Clients;User ID=sa;Password=123;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Users] WHERE [ID] = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", ID.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("подключение не удалось");
                return;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                String connectionString = "Data Source=WORK7879\\SQLEXPRESS;Initial Catalog=Clients;User ID=sa;Password=123";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT SUM(Summa) FROM [dbo].[Users]", con))
                    {
                        object result = cmd.ExecuteScalar();
                        summa.Text = Convert.ToString(result);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("подключение не удалось");
                return;
            }
        }
    }
}

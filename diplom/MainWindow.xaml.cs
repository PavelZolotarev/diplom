using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
using MySql.Data.MySqlClient;

namespace diplom
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
          

        }

        private void textLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtLogin.Focus();
        }
        private void txtLogin_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLogin.Text) && txtLogin.Text.Length > 0)
            {
                textLogin.Visibility = Visibility.Collapsed;
            }
            else
            {
                textLogin.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Text) && txtPassword.Text.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

      /*  private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }
      */
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

      



        private void Button_Login(object sender, RoutedEventArgs e) // Войти
         {

             String loginUser = txtLogin.Text;
             String passUser = txtPassword.Text;

             DataBase db = new DataBase();

             DataTable table = new DataTable();  

             MySqlDataAdapter adapter = new MySqlDataAdapter();

             MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `pass` = @uP", db.GetConnection()); // @ul @uP заглушка для безопасности!!!!
             command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
             command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;

             adapter.SelectCommand = command;
             adapter.Fill(table);

             if(table.Rows.Count > 0)
             {
                 MessageBox.Show("Пользователь авторизован!");
               // HomePage HomePage = new HomePage(loginUser);
                 mainForm newMainForm = new mainForm(loginUser);
                newMainForm.MainFrame.Navigate(new HomePage(loginUser));
                newMainForm.Show();
                 this.Close();
             }
             else
             {
                 MessageBox.Show("нЕТ ПОЛЬЗОВАТЕЛЯ");
             }

         }
        

        private void Button_Registration(object sender, RoutedEventArgs e) // Регистрация
        {
            
            registration newReg = new registration();
            newReg.Show();
            this.Close();

        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;


namespace diplom
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Window
    {
        public registration()
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

        private void againTextPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            againTxtPassword.Focus();
        }

        private void againTxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(againTxtPassword.Text) && againTxtPassword.Text.Length > 0)
            {
                againTextPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                againTextPassword.Visibility = Visibility.Visible;
            }
        }


        private void textFullName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFullName.Focus();
        }
        private void txtFullName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFullName.Text) && txtFullName.Text.Length > 0)
            {
                textFullName.Visibility = Visibility.Collapsed;
            }
            else
            {
                textFullName.Visibility = Visibility.Visible;
            }
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }
        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }

        // Обработчик для клика по метке (фокус на ComboBox)
        private void textPosition_MouseDown(object sender, MouseButtonEventArgs e)
        {
            comboPosition.Focus(); // Фокусируемся на ComboBox
        }

        // Обработчик для изменения выбранного значения в ComboBox
        private void comboPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboPosition.SelectedIndex != -1)
            {
                textPosition.Visibility = Visibility.Collapsed; // Скрыть подсказку, если выбран элемент
            }
            else
            {
                textPosition.Visibility = Visibility.Visible; // Показываем подсказку, если ничего не выбрано
            }
        }

        private void textDepartment_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtDepartment.Focus();
        }
        private void txtDepartment_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDepartment.Text) && txtDepartment.Text.Length > 0)
            {
                textDepartment.Visibility = Visibility.Collapsed;
            }
            else
            {
                textDepartment.Visibility = Visibility.Visible;
            }
        }


        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void Button_Reg(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text == "" || txtPassword.Text == "" || againTxtPassword.Text == "" || txtFullName.Text == "" || txtEmail.Text == "" || comboPosition.SelectedItem == null || txtDepartment.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены!");
                return;
            }

            if (isUserExist())
                return;

            string login = txtLogin.Text;
            string firstPass = txtPassword.Text;
            string secondPass = againTxtPassword.Text;
            string fullName = txtFullName.Text;
            string email = txtEmail.Text;
            string jobTitle = ((ComboBoxItem)comboPosition.SelectedItem).Content.ToString();
            string department = txtDepartment.Text;

            if (firstPass == secondPass)
            {

                DataBase db = new DataBase();
                MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`id`, `login`, `pass`, `full_name`, `email`, `position`, `department`) VALUES (NULL, @login, @password, @fullName, @email, @comboPosition, @department)", db.GetConnection());
                command.Parameters.Add("@login", MySqlDbType.VarChar).Value = txtLogin.Text;
                command.Parameters.Add("@password", MySqlDbType.VarChar).Value = txtPassword.Text;
                command.Parameters.Add("@fullName", MySqlDbType.VarChar).Value = fullName;
                command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
                command.Parameters.Add("@comboPosition", MySqlDbType.VarChar).Value = jobTitle;
                command.Parameters.Add("@department", MySqlDbType.VarChar).Value = department;

                db.openConnection();

                if (command.ExecuteNonQuery() == 1) // все обработалось корректно
                    MessageBox.Show("Вы успешно зарегистрировлись!");
                else
                    MessageBox.Show("Ошибка в создании аккаунта!");
                db.closeConnection();



            }
            else
            {
                MessageBox.Show("Пароли не совпадают!");
            }
        }
        public Boolean isUserExist()
        {
            DataBase db = new DataBase();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", db.GetConnection()); // @ul @uP заглушка для безопасности!!!!
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = txtLogin.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
                return true;
            }
            else
            {
                // MessageBox.Show("Вы успешно зарегистрированы!");
                return false;
            }

        }

    }

}


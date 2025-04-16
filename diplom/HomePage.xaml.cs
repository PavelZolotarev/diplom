using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

namespace diplom
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private string currentUserLogin;

        public HomePage()
        {
           // InitializeComponent();
            //currentUserLogin = userLogin; // Инициализируем логин текущего пользователя
           LoadUserData(); // Загружаем данные
        }
       public HomePage(string userLogin)
        {
            InitializeComponent();
            currentUserLogin = userLogin; // Устанавливаем значение для переменной
            LoadUserData(); // Загружаем данные пользователя
        }

        private void LoadUserData()
        {
            // Получаем данные из базы данных для текущего пользователя
            DataBase db = new DataBase();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @login", db.GetConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = currentUserLogin;
            db.openConnection();

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
              
                textBlockFullName.Text = reader["full_name"].ToString();
                textBlockEmail.Text = reader["email"].ToString();
                textBlockJobTitle.Text = reader["position"].ToString();
                textBlockRegistation.Text = reader["registration_date"].ToString();
                textBlockDepartment.Text = reader["department"].ToString();
            }
            db.closeConnection();
        }
    }
}
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
using System.Windows.Shapes;

namespace diplom
{
    /// <summary>
    /// Логика взаимодействия для NewPublicationWindow.xaml
    /// </summary>
    public partial class NewPublicationWindow : Window
    {
        private int userId;
        public NewPublicationWindow()
        {
            InitializeComponent();
           userId = currentUserId;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleBox.Text.Trim();
            string content = ContentBox.Text.Trim();
            string type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content) || string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!");
                return;
            }

            DataBase db = new DataBase();
            db.openConnection();

            string query = "INSERT INTO publications (user_id, title, publication_type, content) VALUES (@user_id, @title, @type, @content)";
            MySqlCommand command = new MySqlCommand(query, db.GetConnection());

            command.Parameters.AddWithValue("@user_id", userId);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@content", content);

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Публикация добавлена!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления: " + ex.Message);
            }

            db.closeConnection();
        }
    }
}


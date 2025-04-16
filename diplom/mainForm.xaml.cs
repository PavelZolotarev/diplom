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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace diplom
{
    /// <summary>
    /// Логика взаимодействия для mainForm.xaml
    /// </summary>
    public partial class mainForm : Window
    {

        private string currentUserLogin;
        /* public mainForm()
         {
             InitializeComponent();
             MainFrame.Navigate(new Uri("HomePage.xaml", UriKind.Relative));   
         }*/
        public mainForm(string login)
        {
            InitializeComponent();
            currentUserLogin = login;
            NavigateToHome(); // Перенаправляем на домашнюю страницу
        }
        private void NavigateToHome()
        {
            MainFrame.Navigate(new HomePage(currentUserLogin));
        }

        // Анимация 
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            DoubleAnimation fadeAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
            MainFrame.BeginAnimation(Frame.OpacityProperty, fadeAnimation);
        }


        private void Navigate_Home(object sender, RoutedEventArgs e)
        {
            NavigateToHome();
            // MainFrame.Navigate(new HomePage(currentUserLogin));
            //MainFrame.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }
        private void Navigate_Teachers(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("TeachersPage.xaml", UriKind.Relative));
        }
        private void Navigate_Publications(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("PublicationsPage.xaml", UriKind.Relative));
        }

        private void Navigate_Conferences(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("ConferencesPage.xaml", UriKind.Relative));
        }

        private void Navigate_Reports(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("ReportsPage.xaml", UriKind.Relative));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

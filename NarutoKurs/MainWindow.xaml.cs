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

namespace NarutoKurs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TechniquesPage techniquesPageInstance;
        private Login loginPage;
        private TechniquesPage techniquesPage;
        private bool IsUserRegistered = false;

        public MainWindow()
        {

            InitializeComponent();
            techniquesPage = new TechniquesPage(MainFrame, loginPage);
            loginPage = new Login(this, techniquesPage);

            WindowState = WindowState.Maximized;

            HideButtons(); // Скрыть кнопки при инициализации MainWindow

            if (MainFrame != null)
            {
                MainFrame.Navigate(loginPage);
            }
        }
    
        public void SetTechniquesPageInstance(TechniquesPage techniquesPage)
        {
            techniquesPageInstance = techniquesPage;
        }
        public void SetIsUserRegistered(bool value)
        {
            IsUserRegistered = value;
            CheckUserRegistration(); // Вызов метода для проверки статуса регистрации пользователя
        }

        public bool GetIsUserRegistered()
        {
            return IsUserRegistered;
        }

        private void CheckUserRegistration()
        {
            HideButtons(); // Скрываем кнопки по умолчанию
            if (IsUserRegistered)
            {
                ShowButtons();
            }
        }

        public void ShowButtons()
        {
            // Показать кнопки, установив их видимость в true
            CharactersButton.Visibility = Visibility.Visible;
            TechniquesButton.Visibility = Visibility.Visible;
            TailedBeastsButton.Visibility = Visibility.Visible;
        }

        public void HideButtons()
        {
            // Скрыть кнопки, установив их видимость в false
            CharactersButton.Visibility = Visibility.Collapsed;
            TechniquesButton.Visibility = Visibility.Collapsed;
            TailedBeastsButton.Visibility = Visibility.Collapsed;
        }


        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void CharactersButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Characters());
        }

        private void TechniquesButton_Click(object sender, RoutedEventArgs e)
        {

            MainFrame.Navigate(new TechniquesPage(MainFrame, loginPage));
        }

        private void TailedBeastsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CaudatePage());
        }
    }
}

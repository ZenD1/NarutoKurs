using System;
using System.Collections.Generic;
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

namespace NarutoKurs
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {

        private SqlConnectionManager sqlConnectionManager = new SqlConnectionManager();
        private MainWindow mainWindow;
        private TechniquesPage techniquesPageInstance;
        public int roleId;
        private Login login; // Объявление переменной login

        public Login(MainWindow mainWindow, TechniquesPage techniquesPage)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.techniquesPageInstance = techniquesPage;

        }
        private void ManageMenuBasedOnRole(int roleId)
        {
            if (roleId == 1)
            {
                // Показать меню
                techniquesPageInstance.ShowAdminMenu();
            }
            else if (roleId == 2)
            {
                // Скрыть меню
                techniquesPageInstance.HideAdminMenu();
            }
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (AuthenticateUser(username, password))
            {
                MessageBox.Show("Успешный вход!");
                mainWindow.SetIsUserRegistered(true);
                MainGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Ошибка входа. Проверьте логин и пароль.");
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null)
            {
                NavigationService.Navigate(new Registr());
            }
        }
        private bool AuthenticateUser(string username, string password)
        {
            SqlConnection connection = null;
            try
            {
                connection = sqlConnectionManager.GetSqlConnection();

                if (connection != null)
                {
                    string query = "SELECT id, role FROM [dbo].[User] WHERE username = @Username AND passwordHash = @Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            roleId = reader.GetInt32(1); // Сохраняем значение roleId
                            mainWindow.SetIsUserRegistered(true);
                            SetUserRole(roleId); // Используем roleId для установки роли пользователя
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка входа: " + ex.Message);
                return false;
            }
            finally
            {
                sqlConnectionManager.CloseSqlConnection(connection);
            }
        }

        public void SetUserRole(int roleId)
        {
            if (techniquesPageInstance != null && login != null)
            {
                if (roleId == 1)
                {
                    // Показать меню
                    techniquesPageInstance.ShowAdminMenu();
                }
                else if (roleId == 2)
                {
                    // Скрыть меню
                    techniquesPageInstance.HideAdminMenu();
                }
            }
        }
    }
}
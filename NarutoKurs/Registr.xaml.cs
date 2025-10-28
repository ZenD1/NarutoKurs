using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Registr.xaml
    /// </summary>
    public partial class Registr : Page

    {
        private Login loginPage;
        private SqlConnectionManager sqlConnectionManager; // Объявление объекта SqlConnectionManager

        private Frame mainFrame;
        public Registr()
        {
            InitializeComponent();
            sqlConnectionManager = new SqlConnectionManager(); // Инициализация объекта SqlConnectionManager
        }
        private bool IsEmailValid(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string username = tbtName.Text;
            string email = tbtDr.Text;
            string password = tbtTel.Text;

            if (password.Length < 6 || password.Length > 12)
            {
                MessageBox.Show("Пароль должен содержать от 6 до 12 символов.");
                return;
            }

            if (!IsEmailValid(email))
            {
                MessageBox.Show("Неправильный формат email.");
                return;
            }

            if (!CheckPasswordRequirements(password))
            {
                MessageBox.Show("Пароль должен содержать от 6 до 12 символов, включая минимум одну заглавную букву и одну цифру.");
                return;
            }

            bool registrationResult = AddUserToDatabase(username, email, password);

            if (registrationResult)
            {
                MessageBox.Show("Регистрация успешна!");
                NavigationService?.Navigate(loginPage);
            }
            else
            {
                MessageBox.Show("Ошибка при регистрации. Пожалуйста, проверьте данные.");
            }
        }

        private bool CheckPasswordRequirements(string password)
        {
            if (password.Length < 6 || password.Length > 12)
            {
                return false;
            }

            if (!Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*\d).+$"))
            {
                return false;
            }

            return true;
        }

        private bool AddUserToDatabase(string username, string email, string password)
        {
            try
            {
                using (SqlConnection connection = sqlConnectionManager.GetSqlConnection())
                {
                    if (connection != null)
                    {
                        string query = "INSERT INTO [User] (username, email, password) VALUES (@Username, @Email, @Password)";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при регистрации пользователя: " + ex.Message);
                return false;
            }
        }

        private void btnBack_Click()
        {

        }
    }
}
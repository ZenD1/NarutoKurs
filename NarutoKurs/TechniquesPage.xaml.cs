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
using System.Data;
using NarutoKurs.clases;
using System.Collections.ObjectModel;

namespace NarutoKurs
{
    /// <summary>
    /// Логика взаимодействия для TechniquesPage.xaml
    /// </summary>
    public partial class TechniquesPage : Page
    {
        private Login login;
        string connectionString = SqlConnectionManager.ConnectionString;
        SqlConnectionManager sqlConnectionManager = new SqlConnectionManager();
        private Frame MainFrame { get; set; }

        public ObservableCollection<TechniquesList> TechniquesList { get; set; }

        public TechniquesPage(Frame mainFrame, Login login)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            TechniquesList = new ObservableCollection<TechniquesList>();
            this.login = login; 
            if (this.login != null) 
            {
                this.login.SetUserRole(this.login.roleId);
            }
            LoadData();
        }

        public void ShowAdminMenu()
        {
            if (AdminMenu != null)
            {
                AdminMenu.Visibility = Visibility.Visible;
            }
        }

        // Метод для скрытия меню для других ролей, кроме администратора
        public void HideAdminMenu()
        {
            if (AdminMenu != null)
            {
                AdminMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void ComboBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void TextBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }
       

        // Метод для проверки роли пользователя и отображения меню

        private void ApplyFilters()
        {
            var filteredList = TechniquesList.ToList();

            // Фильтрация по типу техники
            string selectedType = (comboBoxTypeFilter.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (!string.IsNullOrEmpty(selectedType) && selectedType != "Не имеется техники")
            {
                filteredList = filteredList.Where(item => item.TypeName == selectedType).ToList();
            }

            // Фильтрация по подтипу техники
            string selectedSubtype = (comboBoxSubtypeFilter.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (!string.IsNullOrEmpty(selectedSubtype) && selectedSubtype != "Не имеется техники")
            {
                filteredList = filteredList.Where(item => item.SubtypeName == selectedSubtype).ToList();
            }

            // Фильтрация по рангу техники
            string selectedRang = (comboBoxRangFilter.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (!string.IsNullOrEmpty(selectedRang) && selectedRang != "Не имеется ранга")
            {
                filteredList = filteredList.Where(item => item.RangTechTier == selectedRang).ToList();
            }

            // Поиск по текстовому полю
            if (!string.IsNullOrEmpty(textBoxSearch.Text))
            {
                string searchQuery = textBoxSearch.Text.ToLower();
                filteredList = filteredList.Where(item =>
                    item.TechniqueName.ToLower().Contains(searchQuery) ||
                    item.TypeName.ToLower().Contains(searchQuery) ||
                    item.SubtypeName.ToLower().Contains(searchQuery) ||
                    item.RangTechTier.ToLower().Contains(searchQuery)).ToList();
            }

            techniquesDataGrid.ItemsSource = filteredList;
        }
        public void Add_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame != null)
            {
                MainFrame.Navigate(new AddTechniquePage(MainFrame, this));
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (techniquesDataGrid.SelectedItem != null)
            {
                // Получение выбранного элемента
                TechniquesList selectedTechnique = (TechniquesList)techniquesDataGrid.SelectedItem;

                // Удаление элемента из коллекции и базы данных
                if (DeleteTechnique(selectedTechnique.TechniqueID))
                {
                    MessageBox.Show("Техника успешно удалена!");
                    // Перезагрузка данных после удаления
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Ошибка при удалении техники.");
                }
            }
            else
            {
                MessageBox.Show("Выберите технику для удаления.");
            }
        }
       


        private bool DeleteTechnique(int techniqueID)
        {
            try
            {

                using (SqlConnection connection = sqlConnectionManager.GetSqlConnection())
                {
                    if (connection != null)
                    {
                        string query = @"DELETE FROM Techniques WHERE id = @TechniqueID";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@TechniqueID", techniqueID);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении техники: " + ex.Message);
                return false;
            }
        }
       

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранного объекта TechniquesList из DataGrid
            TechniquesList selectedTechnique = techniquesDataGrid.SelectedItem as TechniquesList;

            if (selectedTechnique != null)
            {
                // Выполните необходимые изменения в объекте TechniquesList (например, изменение его свойств)

                // Обновление источника данных DataGrid
                techniquesDataGrid.ItemsSource = null;
                techniquesDataGrid.ItemsSource = TechniquesList;
            }
        }
        public void SetUserRole(int roleId)
        {
            if (roleId == 1)
            {
                // Показать меню
                ShowAdminMenu();
            }
            else if (roleId == 2)
            {
                // Скрыть меню
                HideAdminMenu();
            }
        }
        public void LoadData()
        {
            string connectionString = SqlConnectionManager.ConnectionString;
            string query = @"SELECT 
            Techniques.id AS TechniqueID,
            Techniques.name AS TechniqueName,
            RangTech.tier AS RangTechTier,
            SubTypes_Tech.name_subtype AS SubtypeName,
            Types_Tech.name_type AS TypeName
            FROM
            [dbo].[Techniques] AS Techniques
            INNER JOIN
            [dbo].[RangTech] AS RangTech ON Techniques.rangtech = RangTech.id
            INNER JOIN
            [dbo].[SubTypes_Tech] AS SubTypes_Tech ON Techniques.subtypes = SubTypes_Tech.id
            INNER JOIN
            [dbo].[Types_Tech] AS Types_Tech ON Techniques.types = Types_Tech.id; ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                TechniquesList = new ObservableCollection<TechniquesList>();

                foreach (DataRow row in dataTable.Rows)
                {
                    TechniquesList technique = new TechniquesList(
                        Convert.ToInt32(row["TechniqueID"]),
                        row["TechniqueName"].ToString(),
                        row["RangTechTier"].ToString(),
                        row["SubtypeName"].ToString(),
                        row["TypeName"].ToString()
                    );

                    TechniquesList.Add(technique);
                }

                techniquesDataGrid.ItemsSource = TechniquesList;
            }
        }
    }
}
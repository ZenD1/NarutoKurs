using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using NarutoKurs.clases;

namespace NarutoKurs
{
    /// <summary>
    /// Логика взаимодействия для AddTechniquePage.xaml
    /// </summary>
    public partial class AddTechniquePage : Page
    {
        private TechniquesPage techniquesPage;
        private Frame MainFrame { get; set; }
        public ObservableCollection<TechniquesList> TechniquesList { get; set; }
        private SqlConnectionManager sqlConnectionManager;
        public AddTechniquePage(Frame mainFrame, TechniquesPage techniquesPage)
        {
            InitializeComponent();
            sqlConnectionManager = new SqlConnectionManager();
            this.techniquesPage = techniquesPage;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = sqlConnectionManager.GetSqlConnection())
                {
                    string techniqueName = TechniqueNameTextBox.Text;
                    string rangTechTier = (RangTechTierComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                    string subTypeName = (SubtypeNameComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                    string typeName = (TypeNameComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                    // Добавление данных в базу данных
                    if (AddTechniqueToDatabase(techniqueName, rangTechTier, subTypeName, typeName))
                    {
                        MessageBox.Show("Техника успешно добавлена в базу данных!");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении техники в базу данных.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            techniquesPage.LoadData();
            NavigationService?.GoBack();

        }


        private bool AddTechniqueToDatabase(string techniqueName, string rangTechTier, string subTypeName, string typeName)
        {
            try
            {
                using (SqlConnection connection = sqlConnectionManager.GetSqlConnection())
                {
                    if (connection != null)
                    {
                        string query = @"
                    INSERT INTO Techniques ([name], [types], [subtypes], [rangtech])
                    SELECT 
                        @Name, Types_Tech.id, SubTypes_Tech.id, RangTech.id
                    FROM 
                        [dbo].[Types_Tech] AS Types_Tech
                    INNER JOIN 
                        [dbo].[SubTypes_Tech] AS SubTypes_Tech ON SubTypes_Tech.name_subtype = @SubType
                    INNER JOIN 
                        [dbo].[RangTech] AS RangTech ON RangTech.tier = @RangTech
                    WHERE Types_Tech.name_type = @Type; -- Фильтрация по выбранному типу техники
                ";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Name", techniqueName);
                        command.Parameters.AddWithValue("@RangTech", rangTechTier);
                        command.Parameters.AddWithValue("@SubType", subTypeName);
                        command.Parameters.AddWithValue("@Type", typeName);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении техники: " + ex.Message);
                return false;
            }
        }
    }
}
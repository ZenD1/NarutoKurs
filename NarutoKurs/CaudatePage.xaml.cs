using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using NarutoKurs.clases;

namespace NarutoKurs
{
    /// <summary>
    /// Логика взаимодействия для CaudatePage.xaml
    /// </summary>
    public partial class CaudatePage : Page
    {
        private List<Hvost> caudateList = new List<Hvost>();

        public CaudatePage()
        {
            InitializeComponent();
            LoadCaudateData();
        }

        private void LoadCaudateData()
        {
            string connectionString = SqlConnectionManager.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT id, caudatename, owner, information FROM Caudate";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Hvost caudate = new Hvost(
                            reader.GetInt32(0), // ID
                            reader.GetString(1), // CaudateName
                            reader.IsDBNull(2) ? string.Empty : reader.GetString(2), // Description из БД
                            reader.IsDBNull(3) ? string.Empty : reader.GetString(3) // Abilities из БД
                        );
                        caudateList.Add(caudate);
                    }
                }
                reader.Close();
            }

            Caudate.ItemsSource = caudateList;
        }

        private void Caudate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Caudate.SelectedItem != null)
            {
                Hvost selectedHvost = (Hvost)Caudate.SelectedItem;
                // При выборе строки, выводите информацию о хвостатом персонаже в MessageBox
                string message = $"ID: {selectedHvost.ID}\nИмя: {selectedHvost.CaudateName}\n" +
                                 $"Описание: {selectedHvost.Description}\nСпособности: {selectedHvost.Abilities}";
                MessageBox.Show(message, "Информация о хвостатом персонаже");
            }
        }
    }
}
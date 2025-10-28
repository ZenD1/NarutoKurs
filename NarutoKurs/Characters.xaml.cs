using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для Characters.xaml
    /// </summary>
    public partial class Characters : Page
    {
        private ObservableCollection<clases.Character.CharacterList> charactersList;
        private readonly SqlConnectionManager connectionManager = new SqlConnectionManager();
        private SqlConnection connection; // Объявление переменной connection


        public Characters()
        {
            InitializeComponent(); 
            charactersList = new ObservableCollection<clases.Character.CharacterList>();

            LoadCharactersFromDatabase();

            charactersListBox.ItemsSource = charactersList;
        }
        private void GetInfo_Click(object sender, RoutedEventArgs e)
        {
            if (charactersListBox.SelectedItem != null)
            {
                clases.Character.CharacterList selectedCharacter = (clases.Character.CharacterList)charactersListBox.SelectedItem;

                string connectionString = SqlConnectionManager.ConnectionString;
                string query = @"
            SELECT Characters.name AS CharacterName, 
                   Rangs.name_rang AS Rang, 
                   Clan.name_clan AS Clan, 
                   Caudate.caudatename AS Caudate, 
                   Teams.name_team AS Teams, 
                   RangTech.tier AS RangTech, 
                   Techniques.name AS Techniques, 
                   Types_Tech.name_type AS TypeTech, 
                   SubTypes_Tech.name_subtype AS SubTypesTech, 
                   Vilages.name_vilage AS Vilages, 
                   Characters.age AS Age
            FROM Characters
            LEFT JOIN Rangs ON Characters.rang = Rangs.id
            LEFT JOIN Clan ON Characters.clan = Clan.id
            LEFT JOIN Caudate ON Characters.caudate = Caudate.id
            LEFT JOIN Teams ON Characters.team = Teams.id
            LEFT JOIN RangTech ON Rangs.id = RangTech.id
            LEFT JOIN Techniques ON Characters.main_techniques = Techniques.id
            LEFT JOIN Types_Tech ON Techniques.types = Types_Tech.id
            LEFT JOIN SubTypes_Tech ON Techniques.subtypes = SubTypes_Tech.id
            LEFT JOIN Vilages ON Characters.village = Vilages.id
            WHERE Characters.id = @CharacterId";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@CharacterId", selectedCharacter.Id); // Передача ID выбранного персонажа

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // Переменные для хранения данных
                        string characterName = null, rang = null, clan = null, caudate = null, teams = null,
                               rangTech = null, techniques = null, typeTech = null, subTypesTech = null, vilages = null;
                        int age = 0;

                        // Если есть результаты запроса
                        if (reader.Read())
                        {
                            characterName = reader["CharacterName"].ToString();
                            rang = reader["Rang"].ToString();
                            clan = reader["Clan"].ToString();
                            caudate = reader["Caudate"].ToString();
                            teams = reader["Teams"].ToString();
                            rangTech = reader["RangTech"].ToString();
                            techniques = reader["Techniques"].ToString();
                            typeTech = reader["TypeTech"].ToString();
                            subTypesTech = reader["SubTypesTech"].ToString();
                            vilages = reader["Vilages"].ToString();
                            age = Convert.ToInt32(reader["Age"]);
                        }

                        reader.Close();

                        // Создаем объект для хранения информации о персонаже
                        clases.CharacterInfo characterInfo = new clases.CharacterInfo(
                            characterName, rang, clan, caudate, teams, rangTech, techniques, typeTech, subTypesTech, vilages, age.ToString()
                        );

                        // Передаем выбранного персонажа в окно CharacterInfoWindow
                        CharacterInfoWindow characterInfoWindow = new CharacterInfoWindow(characterInfo);
                        characterInfoWindow.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении запроса: " + ex.Message);
                }
            }
        }

        private void GetVideo_Click(object sender, RoutedEventArgs e)
        {
            if (charactersListBox.SelectedItem != null)
            {
                clases.Character.CharacterList selectedCharacter = (clases.Character.CharacterList)charactersListBox.SelectedItem;
                GetVideo(selectedCharacter);
            }
        }

        private void GetVideo(clases.Character.CharacterList selectedCharacter)
        {
            if (selectedCharacter.Video != null)
            {
                // Создание окна проигрывателя видео и передача пути к видео
                VideoPlayerWindow videoPlayerWindow = new VideoPlayerWindow(selectedCharacter.Video);
                videoPlayerWindow.Show();
            }
            else
            {
                MessageBox.Show("Видео для этого персонажа не найдено.");
            }
        }
        private void CharacterSelected(object sender, SelectionChangedEventArgs e)
        {
            if (charactersListBox.SelectedItem != null)
            {
                clases.Character.CharacterList selectedCharacter = (clases.Character.CharacterList)charactersListBox.SelectedItem;
                GetInfo(selectedCharacter.Id);
            }
        }
       private void GetInfo(int characterId)
        {
            if (charactersListBox.SelectedItem != null)
            {
                string connectionString = SqlConnectionManager.ConnectionString;
                string query = @"
            SELECT Characters.name AS CharacterName, 
                   Rangs.name_rang AS Rang, 
                   Clan.name_clan AS Clan, 
                   Caudate.caudatename AS Caudate, 
                   Teams.name_team AS Teams, 
                   RangTech.tier AS RangTech, 
                   Techniques.name AS Techniques, 
                   Types_Tech.name_type AS TypeTech, 
                   SubTypes_Tech.name_subtype AS SubTypesTech, 
                   Vilages.name_vilage AS Vilages, 
                   Characters.age AS Age
            FROM Characters
            LEFT JOIN Rangs ON Characters.rang = Rangs.id
            LEFT JOIN Clan ON Characters.clan = Clan.id
            LEFT JOIN Caudate ON Characters.caudate = Caudate.id
            LEFT JOIN Teams ON Characters.team = Teams.id
            LEFT JOIN RangTech ON Rangs.id = RangTech.id
            LEFT JOIN Techniques ON Characters.main_techniques = Techniques.id
            LEFT JOIN Types_Tech ON Techniques.types = Types_Tech.id
            LEFT JOIN SubTypes_Tech ON Techniques.subtypes = SubTypes_Tech.id
            LEFT JOIN Vilages ON Characters.village = Vilages.id
            WHERE Characters.id = @CharacterId"; // Фильтрация по ID персонажа

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@CharacterId", characterId); // Передача ID выбранного персонажа

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении запроса: " + ex.Message);
                }
            }
        }
      
        
        private void LoadCharactersFromDatabase()
        {
            try
            {
                using (connection = connectionManager.GetSqlConnection())
                {
                    if (connection != null)
                    {
                        string query = "SELECT Id, Name, Clan, Team, Rang, Village, Caudate, Main_techniques, Age, Photo, Video FROM Characters";
                        SqlCommand command = new SqlCommand(query, connection);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                int? clanId = reader.IsDBNull(2) ? null : (int?)reader.GetInt32(2);
                                int? teamId = reader.IsDBNull(3) ? null : (int?)reader.GetInt32(3);
                                int? rangId = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4);
                                int? villageId = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5);
                                int? caudateId = reader.IsDBNull(6) ? null : (int?)reader.GetInt32(6);
                                int? mainTechniqueId = reader.IsDBNull(7) ? null : (int?)reader.GetInt32(7);
                                int? age = reader.IsDBNull(8) ? null : (int?)reader.GetInt32(8);
                                byte[] photo = reader.IsDBNull(9) ? null : (byte[])reader["Photo"];
                                byte[] video = reader.IsDBNull(10) ? null : (byte[])reader["Video"];

                                var character = new clases.Character.CharacterList(
                                    id, name, clanId, teamId, rangId, villageId, caudateId, mainTechniqueId, age, photo, video
                                );

                                charactersList.Add(character);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке персонажей из базы данных: {ex.Message}");
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            connectionManager.CloseSqlConnection(connection);
        }
    }
}
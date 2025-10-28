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
using System.Windows.Shapes;

namespace NarutoKurs
{
    /// <summary>
    /// Логика взаимодействия для CharacterInfoWindow.xaml
    /// </summary>
    public partial class CharacterInfoWindow : Window
    {
        public clases.CharacterInfo CharacterInfo { get; set; }
        public CharacterInfoWindow(clases.CharacterInfo characterInfo)
        {
            InitializeComponent();
            CharacterInfo = characterInfo;
            DataContext = CharacterInfo;
        }

        public clases.Character SelectedCharacter { get; set; }

    }
}
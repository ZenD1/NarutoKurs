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

namespace WpfZab5
{
    /// <summary>
    /// Логика взаимодействия для PageEmployee.xaml
    /// </summary>
    public partial class PageEmployee : Page
    {
        private bool isDirty = true;
        public PageEmployee()
        {
            InitializeComponent();

        }


        private void UndoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Отмена");
            isDirty = true;
            e.Handled = true;
        }

        private void UndoCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
            e.Handled = true;
        }

        private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            isDirty = !isDirty;
            e.Handled = true;
        }

        private void NewCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true; // Можете добавить свою логику проверки здесь
            e.Handled = true;
        }

        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            isDirty = !isDirty;
            e.Handled = true;
        }

        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true; // Можете добавить свою логику проверки здесь
            e.Handled = true;
        }

        private void FindCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            isDirty = !isDirty;
            e.Handled = true;
        }

        private void FindCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true; // Можете добавить свою логику проверки здесь
            e.Handled = true;
        }
        private void EditCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Обработка команды Edit
            e.Handled = true;
        }

        private void EditCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Логика проверки возможности выполнения команды Edit
            e.CanExecute = true; // Или ваша логика проверки
            e.Handled = true;
        }
        private void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Обработка команды Edit
            e.Handled = true;
        }

        private void DeleteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Логика проверки возможности выполнения команды Edit
            e.CanExecute = true; // Или ваша логика проверки
            e.Handled = true;
        }


    }
}
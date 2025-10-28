using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для VideoPlayerWindow.xaml
    /// </summary>
    public partial class VideoPlayerWindow : Window
    {

        private readonly byte[] videoBytes;
        public VideoPlayerWindow(byte[] video)
        {
            InitializeComponent();
            videoBytes = video;
            Loaded += VideoPlayerWindow_Loaded;
        }
        private void PlayVideoWithKMPlayer(string videoPath)
        {
            try
            {
                Process.Start(@"C:\KMPlayer\KMPlayer.exe", videoPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при запуске KMPlayer: {ex.Message}");
            }
        }
    


private void VideoPlayerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (videoBytes != null)
                {
                    string tempVideoPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "tempVideo.mp4");
                    System.IO.File.WriteAllBytes(tempVideoPath, videoBytes);

                    PlayVideoWithKMPlayer(tempVideoPath);
                }
                else
                {
                    MessageBox.Show("Видео для этого персонажа не найдено.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при воспроизведении видео: {ex.Message}");
            }
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                mediaElement.Play();
            }
        }

        private void MediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show($"Ошибка при воспроизведении видео: {e.ErrorException.Message}");
        }
    }
}
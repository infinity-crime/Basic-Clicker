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
using System.Media;
using Basic_Clicker.Helpers;

namespace Basic_Clicker
{
    public partial class MainWindow : Window
    {
        private MediaPlayer _buttonPlayer = new MediaPlayer(); // объект MediaPlayer для воспроизведения звука клика

        public MainWindow()
        {
            InitializeComponent();
            MusicManager.Instance.PlayMusic();
        }

        private void ButtonSound()
        {
            try
            {
                string path = @"Sounds\ButtonSoundMenu.mp3";
                if (_buttonPlayer.Source != null && _buttonPlayer.Position > TimeSpan.Zero)
                {
                    _buttonPlayer.Stop();
                    _buttonPlayer.Position = TimeSpan.Zero;
                }
                else
                    _buttonPlayer.Open(new Uri(path, UriKind.RelativeOrAbsolute)); // открытие пути всего один раз при запуске

                _buttonPlayer.Volume = 1.0;
                _buttonPlayer.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка воспроизведения: {ex.Message}");
            }
        }

        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            ButtonSound();

            MusicManager.Instance.StopMusic();

            ClickerWindow clickerWindow = new ClickerWindow();
            clickerWindow.Show();
            this.Close();
        }

        private void ButtonOptions_Click(object sender, RoutedEventArgs e)
        {
            ButtonSound();

            MusicManager.Instance.StopMusic();

            ClickerSettings clickerSettings = new ClickerSettings();
            clickerSettings.Show();
            this.Close();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e) // обработчик вызывает модальное окно с подтверждением выхода из приложения
        {
            ButtonSound();

            MessageBoxResult messageBoxResult = MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            switch (messageBoxResult) // обрабатываем кнопки, нажатые пользователем в модальном окне
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
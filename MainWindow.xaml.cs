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

namespace Basic_Clicker
{
    public partial class MainWindow : Window
    {
        private MediaPlayer _buttonPlayer = new MediaPlayer(); // объект MediaPlayer для воспроизведения звука клика
        private MediaPlayer _backroundPlayer = new MediaPlayer(); // объект MediaPlayer для воспроизведения фоновой музыки

        public MainWindow()
        {
            InitializeComponent();
            BackroundSoundMenu();
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


        private void MediaPlayer1_MediaEnded(object sender, EventArgs e) // функция для повтоного воспроизведения музыки
        {
            _backroundPlayer.Position = TimeSpan.Zero; 
            _backroundPlayer.Play(); 
        }

        private void BackroundSoundMenu()
        {
            try
            {
                string path = @"Sounds\FonSoundMenu.mp3";
                if (_backroundPlayer.Source != null && _backroundPlayer.Position > TimeSpan.Zero)  // Если музыка уже играет, не запускаем её повторно
                {
                    _buttonPlayer.Stop();
                    _buttonPlayer.Position = TimeSpan.Zero;
                }

                _backroundPlayer.Open(new Uri(path, UriKind.RelativeOrAbsolute));
                _backroundPlayer.Volume = 1.0;
                _backroundPlayer.Play();

                _backroundPlayer.MediaEnded += MediaPlayer1_MediaEnded; // воспроизводит музыку с начала после её окончания


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка воспроизведения: {ex.Message}");
            }
        }


        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            ButtonSound();
        }

        private void ButtonOptions_Click(object sender, RoutedEventArgs e)
        {
            ButtonSound();
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
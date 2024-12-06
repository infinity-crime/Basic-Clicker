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
        private MediaPlayer _mediaplayer = new MediaPlayer(); // объект MediaPlayer для воспроизведения звука клика
        private MediaPlayer _mediaplayer1 = new MediaPlayer(); // объект MediaPlayer для воспроизведения фоновой музыки
        public MainWindow()
        {
            InitializeComponent();
            FonSound();
        }

        private void ButtonSound()
        {
            try
            {
                string path = @"Sounds\ButtonSoundMenu.mp3";
                if (_mediaplayer.Source != null && _mediaplayer.Position > TimeSpan.Zero)
                {
                    _mediaplayer.Stop();
                    _mediaplayer.Position = TimeSpan.Zero;
                }
                else
                    _mediaplayer.Open(new Uri(path, UriKind.RelativeOrAbsolute)); // открытие пути всего один раз при запуске

                _mediaplayer.Volume = 1.0;
                _mediaplayer.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка воспроизведения: {ex.Message}");
            }
        }


        private void MediaPlayer1_MediaEnded(object sender, EventArgs e) // функция для повтоного воспроизведения музыки
        {
            _mediaplayer1.Position = TimeSpan.Zero; 
            _mediaplayer1.Play(); 
        }

        private void FonSound()
        {
            try
            {
                string path = @"Sounds\FonSoundMenu.mp3";
                if (_mediaplayer1.Source != null && _mediaplayer1.Position > TimeSpan.Zero)  // Если музыка уже играет, не запускаем её повторно
                {
                    _mediaplayer.Stop();
                    _mediaplayer.Position = TimeSpan.Zero;
                }

                _mediaplayer1.Open(new Uri(path, UriKind.RelativeOrAbsolute));
                _mediaplayer1.Volume = 1.0;
                _mediaplayer1.Play();

                _mediaplayer1.MediaEnded += MediaPlayer1_MediaEnded; // воспроизводит музыку с начала после её окончания


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



        /*  Функция для закрытия игры */
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Вы точно хотите выйти из игры?",
                "Выход из игры",
                MessageBoxButton.YesNo
               
             );
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown(); 
            }

        }
    }
}
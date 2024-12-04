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
        private MediaPlayer _mediaplayer = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e) // обработчик вызывает модальное окно с подтверждением выхода из приложения
        {
            ButtonSound_Click(ButtonExit, e);

            MessageBoxResult messageBoxResult = MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            switch(messageBoxResult) // обрабатываем кнопки, нажатые пользователем в модальном окне
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void ButtonSound_Click(object sender, RoutedEventArgs e)
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
                    _mediaplayer.Open(new Uri(path, UriKind.RelativeOrAbsolute));

                _mediaplayer.Volume = 1.0;
                _mediaplayer.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка воспроизведения: {ex.Message}");
            }
        }
    }
}
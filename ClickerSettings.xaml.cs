using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Basic_Clicker
{

    public partial class ClickerSettings : Window
    {
        private MediaPlayer _buttonPlayer = new MediaPlayer();

        public ClickerSettings()
        {
            InitializeComponent();
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


        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonSound();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }

}

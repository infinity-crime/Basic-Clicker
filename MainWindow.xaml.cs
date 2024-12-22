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
        FileManager fileManagerBackground = new FileManager(@"LocalSave\SettingsBackgroundSound.txt");
        FileManager fileManagerButton = new FileManager(@"LocalSave\SettingsButtonSound.txt");

        public MainWindow()
        {
            InitializeComponent();
            double volBackground = fileManagerBackground.ReadRecordDouble();
            MusicManager.Instance.SetBackgroundVolume(volBackground);
            MusicManager.Instance.PlayMusic();

            double volButton = fileManagerButton.ReadRecordDouble();
            MusicManager.Instance.SetButtonVolume(volButton);
        }

        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            MusicManager.Instance.PlayButtonSound();
            MusicManager.Instance.StopMusic();

            ClickerWindow clickerWindow = new ClickerWindow();
            clickerWindow.Show();
            this.Close();
        }

        private void ButtonOptions_Click(object sender, RoutedEventArgs e)
        {
            MusicManager.Instance.PlayButtonSound();

            ClickerSettings clickerSettings = new ClickerSettings();
            clickerSettings.Show();
            this.Close();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e) // обработчик вызывает модальное окно с подтверждением выхода из приложения
        {
            MusicManager.Instance.PlayButtonSound();

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
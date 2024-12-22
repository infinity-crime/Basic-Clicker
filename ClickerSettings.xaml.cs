using Basic_Clicker.Helpers;
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
        FileManager saveBackgroundSound = new FileManager(@"LocalSave\SettingsBackgroundSound.txt");
        FileManager saveButtonSound = new FileManager(@"LocalSave\SettingsButtonSound.txt");

        public ClickerSettings()
        {
            InitializeComponent();

            VolumeSlider.Value = saveBackgroundSound.ReadRecordDouble() * 100;
            VolumeLabel.Text = $"Music Volume: {VolumeSlider.Value}%";

            SoundSlider.Value = saveButtonSound.ReadRecordDouble() * 100;
            SoundLabel.Text = $"Button Sound: {SoundSlider.Value}%";
            UpdateButtonState();
        }

        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MusicManager.Instance.PlayButtonSound();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void UpdateButtonState()
        {
            if (MusicManager.Instance.MusicStates)
            {
                ButtonMusic.Content = "✔";
            }
            else
            {
                ButtonMusic.Content = "";
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(VolumeLabel != null)
            {
                double vol = VolumeSlider.Value / 100;
                MusicManager.Instance.SetBackgroundVolume(vol);
                VolumeLabel.Text = $"Music Volume: {VolumeSlider.Value}%";

                saveBackgroundSound.WriteRecord(vol);
            }
        }

        private void SoundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(SoundLabel != null)
            {
                double vol = SoundSlider.Value / 100;
                MusicManager.Instance.SetButtonVolume(vol);
                SoundLabel.Text = $"Button Sound: {SoundSlider.Value}%";

                saveButtonSound.WriteRecord(vol);
            }
        }


        private void Button_ClickMusic(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Content.ToString() == "✔")
            {
                button.Content = "";
                MusicManager.Instance.MusicStates = false;
            }
            else
            {
                button.Content = "✔";
                MusicManager.Instance.MusicStates = true;
            }
        }
        
    }
}

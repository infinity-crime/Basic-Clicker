using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Media;
using System.Windows;

namespace Basic_Clicker.Helpers
{
    public class MusicManager
    {
        private static MusicManager _instance;
        private MediaPlayer _mediaBackgroundPlayer;
        private MediaPlayer _mediaButtonPlayer;

     
        private MusicManager()
        {
            _mediaBackgroundPlayer = new MediaPlayer();
            _mediaButtonPlayer = new MediaPlayer();

            string path = @"Helpers\FonSoundMenu.mp3";

            _mediaBackgroundPlayer.Open(new Uri(path, UriKind.RelativeOrAbsolute));

            string path1 = @"Helpers\ButtonSoundMenu.mp3";

            _mediaButtonPlayer.Open(new Uri(path1, UriKind.RelativeOrAbsolute));

        }

        public static MusicManager Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new MusicManager();
                return _instance;
            }
        }

        public void PlayMusic()
        {
            _mediaBackgroundPlayer.Play();
        }

        public void StopMusic()
        {
            _mediaBackgroundPlayer.Stop();
            _mediaBackgroundPlayer.Position = TimeSpan.Zero;
        }

        public void SetBackgroundVolume(double volume)
        {  
            _mediaBackgroundPlayer.Volume = volume;
        }

        public void SetButtonVolume(double volume)
        {
            _mediaButtonPlayer.Volume = volume;
        }

        public void PlayButtonSound()
        {
            _mediaButtonPlayer.Position = TimeSpan.Zero;
            _mediaButtonPlayer.Play();
        }


    }
}

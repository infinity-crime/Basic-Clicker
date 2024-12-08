using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Media;

namespace Basic_Clicker.Helpers
{
    public class MusicManager
    {
        private static MusicManager _instance;
        private MediaPlayer _mediaBackgroundPlayer;

        private MusicManager()
        {
            _mediaBackgroundPlayer = new MediaPlayer();
            string path = @"Helpers\FonSoundMenu.mp3";

            _mediaBackgroundPlayer.Open(new Uri(path, UriKind.RelativeOrAbsolute));
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
    }
}

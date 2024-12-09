using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Clicker.ViewModel
{
    public class ClickerViewModel : INotifyPropertyChanged
    {
        private int _totalClicks;

        public int TotalClicks
        {
            get
            {
                return _totalClicks;
            }

            set
            {
                if (_totalClicks != value)
                {
                    _totalClicks = value;
                    OnPropertyChanged(nameof(TotalClicks));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void IncrementClick()
        {
            TotalClicks++;
        }
    }
}

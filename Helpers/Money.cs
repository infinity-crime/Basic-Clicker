using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Clicker.Helpers
{
    public class Money : INotifyPropertyChanged
    {
        private double _value;

        public event PropertyChangedEventHandler PropertyChanged;
        public double Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        public Money()
        {
            _value = 0;
        }

        //public void SetValue()
        //{
        //    _value = value;
        //}

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

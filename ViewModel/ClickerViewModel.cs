using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;
using System.Windows.Input;
using Basic_Clicker.Helpers;

namespace Basic_Clicker.ViewModel
{
    public class ClickerViewModel : INotifyPropertyChanged
    {
        private FileManager _fileManager = new FileManager(@"LocalSave\Record.txt");

        private int _totalClicks;
        private int _recordClick;

        private string _selectedTime;

        private bool _isClickingAllowed; // флаг, указывающий на нажатие кнокпи отсчета времени (если не нажато, то не засчитываем клики)
        private string _remainingTime; // строковое поле оставшегося времени
        private Timer _timer; // таймер
        private int _timeLeftInSeconds; // время в секундах, которое прошло

        public ObservableCollection<string> AvailableTimes { get; set; }

        public ICommand StartClickCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public int TotalClicks
        {
            get => _totalClicks;
            set
            {
                if (_totalClicks != value)
                {
                    _totalClicks = value;
                    OnPropertyChanged(nameof(TotalClicks));
                }
            }
        }

        public int ClickRecord
        {
            get => _recordClick;
            set
            {
                if (_recordClick != value)
                {
                    _recordClick = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedTime
        {
            get => _selectedTime;
            set
            {
                if(_selectedTime != value)
                {
                    _selectedTime = value;
                    OnPropertyChanged(nameof(SelectedTime));
                }
            }
        }

        public string RemainingTime
        {
            get => _remainingTime;
            set
            {
                if (_remainingTime != value)
                {
                    _remainingTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public ClickerViewModel()
        {
            AvailableTimes = new ObservableCollection<string>
            {
                "30 seconds",
                "1 minute",
                "1 minute 30 seconds"
            };

            SelectedTime = AvailableTimes[0];

            StartClickCommand = new RelayCommand(StartClick);

            ClickRecord = _fileManager.ReadRecord();
        }

        private int ParseTime(string selectedTime) // преобразователь времени из строки в int (секунды)
        {
            switch(selectedTime)
            {
                case "30 seconds":
                    return 30;
                case "1 minute":
                    return 60;
                case "1 minute 30 seconds":
                    return 90;
                default:
                    return 30;
            }
        }

        private void StartClick()
        {
            if(_timer != null)
            {
                if (_timeLeftInSeconds > 0) // если врем еще осталось, то сбросить нажатием кнопки нельзя
                    return;

                _timer.Stop();
                _timer.Dispose();

                TotalClicks = 0;
            }

            _timeLeftInSeconds = ParseTime(SelectedTime);
            RemainingTime = TimeSpan.FromSeconds(_timeLeftInSeconds).ToString(@"mm\:ss");

            _isClickingAllowed = true;
            TotalClicks = 0;

            _timer = new Timer(1000); // здесь 1000 это миллисекунды (1 секунда). Таймер будет тикать каждую секунду
            _timer.Elapsed += (sender, e) =>
            {
                _timeLeftInSeconds--;
                RemainingTime = TimeSpan.FromSeconds(_timeLeftInSeconds).ToString(@"mm\:ss");

                if (_timeLeftInSeconds <= 0)
                {
                    _isClickingAllowed = false;
                    _timer.Stop();
                    if(TotalClicks > ClickRecord)
                    {
                        _fileManager.WriteRecord(TotalClicks);
                        ClickRecord = TotalClicks;
                    }
                }
            };

            _timer.Start();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void IncrementClick()
        {
            if(_isClickingAllowed)
                TotalClicks++;
        }
    }

    public class RelayCommand : ICommand // шаблонная реализация класса для команд
    {
        private readonly Action _execute;

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _execute();
    }
}

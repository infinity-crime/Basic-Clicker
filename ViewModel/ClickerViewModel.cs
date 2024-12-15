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

        public Multiplier ClickMultiplier { get; set; }
        public ICommand ClickMultiplierCommand { get; }

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

            ClickMultiplier = new Multiplier();

            SelectedTime = AvailableTimes[0];

            StartClickCommand = new RelayCommand(StartClick);
            ClickMultiplierCommand = new RelayCommand<string>(multiplierValue => ChangeMultiplier(multiplierValue));

            ClickRecord = _fileManager.ReadRecord();
        }

        private void ChangeMultiplier(string multiplierValue)
        {
            if(double.TryParse(multiplierValue, out double newValue))
            {
                ClickMultiplier.Value = newValue;
            }
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
                    ClickMultiplier.Value = 1;
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
            if (_isClickingAllowed)
                TotalClicks += (int)ClickMultiplier.Value;
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => _execute();
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}

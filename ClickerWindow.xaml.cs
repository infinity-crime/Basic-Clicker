using Basic_Clicker.ViewModel;
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
using System.Windows.Shapes;

namespace Basic_Clicker
{
    public partial class ClickerWindow : Window
    {
        private ClickerViewModel _clickerViewModel;

        public ClickerWindow()
        {
            InitializeComponent();
            _clickerViewModel = new ClickerViewModel();
            this.DataContext = _clickerViewModel;
        }

        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as ClickerViewModel;
            viewModel?.IncrementClick();
        }
    }
}

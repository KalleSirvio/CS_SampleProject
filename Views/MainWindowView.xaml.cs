using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using WorldDatabase.ViewModels;

namespace WorldDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();

        }

        private void NewsArticleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                var psi = new ProcessStartInfo
                {
                    FileName = btn?.ToolTip.ToString(),
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
        }
    }
}

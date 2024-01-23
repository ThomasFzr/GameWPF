using Game.ViewModel;

namespace Game
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void ButtonPlay_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ButtonQuit_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}

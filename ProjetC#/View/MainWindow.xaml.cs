using System.Windows;

namespace Game
{
    public partial class MainWindow : Window
    {
        private InGameWindow inGameWindow;

        public MainWindow()
        {
            InitializeComponent();
            inGameWindow = new InGameWindow();
        }

        private void JouerButton_Click(object sender, RoutedEventArgs e)
        {
            inGameWindow.Show();
            Close();
        }

        private void QuitterButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Quitter button clicked!");
            Close();
        }
    }
}

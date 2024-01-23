using System.Windows;

namespace Game
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void JouerButton_Click(object sender, RoutedEventArgs e)
        {
            // Masquer l'interface 1 et afficher l'interface 2

        }

        private void QuitterButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Quitter button clicked!");
            Close(); // Fermer l'application
        }

        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            // Masquer l'interface 2 et afficher l'interface 1

        }
    }
}
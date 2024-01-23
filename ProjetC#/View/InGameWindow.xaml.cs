using System.Windows;
using System.Windows.Controls;

namespace Game
{
    public partial class InGameWindow : Window
    {
        public int SpellNbr { get; private set; }

        public InGameWindow()
        {
            InitializeComponent();
        }

        private void SpellButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse((sender as Button)?.Tag?.ToString(), out int spellNumber))
            {
                SpellNbr = spellNumber;
            }
        }
    }
}

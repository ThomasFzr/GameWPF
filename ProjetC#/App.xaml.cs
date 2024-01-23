using Game.Model;
using System.Windows;

namespace Game
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainClass game = new MainClass();
            game.Start();
        }
    }
}

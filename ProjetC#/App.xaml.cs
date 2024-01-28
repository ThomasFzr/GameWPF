using Game.Model;
using System.Windows;

namespace Game;

public partial class App : Application
{
    public App()
    {
        StartGame();
    }

    private void StartGame()
    {
        GameManager.Instance.StartGame();
        ProcessUpdateStateMachine();
    }

    private async void ProcessUpdateStateMachine()
    {
        while (GameManager.Instance.IsGameRunning)
        {
            await System.Threading.Tasks.Task.Delay(100);
            StateMachine.Instance.ProcessUpdate();
        }
    }
}

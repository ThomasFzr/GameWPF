using Game.Model;
using System;
using System.Windows;

namespace Game
{
    public partial class App : Application
    {
        public StateMachine stateMachine;

        public App()
        {
            stateMachine = new();
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
                stateMachine.ProcessUpdate();
            }
        }
    }
}

using Game.Model;
using System;
using System.Windows;

namespace Game
{
    public partial class App : Application
    {
        public StateMachine stateMachine;
        public GameManager gameManager;

        public App()
        {
            stateMachine = new();
            gameManager = new();
            StartGame();
        }

        private void StartGame()
        {
            gameManager.StartGame();
            ProcessUpdateStateMachine();
        }

        private async void ProcessUpdateStateMachine()
        {
            while (gameManager.IsGameRunning)
            {
                await System.Threading.Tasks.Task.Delay(100);
                stateMachine.ProcessUpdate();
            }
        }
    }
}

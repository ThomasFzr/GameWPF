using Game.Model;
using System;
using System.Data;
using System.Windows;
using System.Xml;

namespace Game;

public partial class App : Application
{
    StateMachine stateMachine = new();
    GameManager gameManager = new GameManager();
    public App()
    {
        ProcessUpdateStateMachine();
    }
    void ProcessUpdateStateMachine()
    {
        
        //while (true)
        //{
        //    //stateMachine.ProcessUpdate();
        //}
    }
}

using System;
using System.Media;
using System.Threading.Tasks;
using Game.View;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows;

namespace Game.Model
{
    public class MonsterState : AState
    {
        private static MonsterState instance;
        private int randomNumber;
        private Random random = new();
        private SoundPlayer soundPlayer = new SoundPlayer("C:\\Users\\sam\\source\\repos\\ProjetJeuWPF\\ProjetC#\\music\\monsterattack.wav"); // Remplacez par le chemin réel de votre fichier son d'attaque

        private MonsterState()
        {
        }

        public static MonsterState Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public override void OnEnter()
        {
            Task.Delay(2500).ContinueWith(t =>
            {
                if (!GameManager.Instance.Monster.IsDead)
                {
                    randomNumber = random.Next(0, GameManager.Instance.Monster.monsterAttackLevel);
                    GameManager.Instance.Monster.Attacks[randomNumber]?.Execute(GameManager.Instance.Monster, GameManager.Instance.Player);

                    GameManager.InGameWindowInstance?.AdvanceMonster();


                    PlayAttackSound();



                }
                else
                {
                    GameManager.Instance.Monster.IsDead = false;
                }

                App.Current.Dispatcher.Invoke(() =>
                {
                    StateMachine.Instance.HandleRequestStateChangement(PlayerState.Instance);
                });
            });
        }

        public override void OnLeave()
        {
        }

        public override void OnProcess()
        {
        }

        private void PlayAttackSound()
        {
            try
            {
                soundPlayer.Play();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erreur lors de la lecture du son d'attaque : {ex.Message}");
            }
        }
    }

}

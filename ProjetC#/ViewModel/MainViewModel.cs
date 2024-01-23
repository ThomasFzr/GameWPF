using System;
using System.ComponentModel;

namespace Game.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool _isPlaying;
        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                if (_isPlaying != value)
                {
                    _isPlaying = value;
                    OnPropertyChanged(nameof(IsPlaying));
                }
            }
        }

        private void OnPropertyChanged(string v)
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        // Other properties and methods representing your game state

        // Implement INotifyPropertyChanged (omitted for brevity)

        public void StartGame()
        {
            // Move your game logic here
            // NotifyPropertyChanged for properties that affect the UI
        }
    }
}

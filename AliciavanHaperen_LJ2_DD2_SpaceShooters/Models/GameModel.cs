using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AliciavanHaperen_LJ2_DD2_SpaceShooters.Models
{
    public class GameModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private int id;
        private int healthPoints;
        private int score;
        private  TimeSpan elapsedTime;

        public int Id 
        { 
            get { return id; } 
            set { id = value; OnPropertyChanged(); } 
        }

        public int HealthPoints
        {
            get { return healthPoints; }
            set { healthPoints = value; OnPropertyChanged(); }
        }

        public int Score
        {
            get { return score; }
            set { score = value; OnPropertyChanged(); }
        }

        public TimeSpan ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; OnPropertyChanged(); }
        }

        public PlayerModel PlayerModel { get; set; }

    }
}

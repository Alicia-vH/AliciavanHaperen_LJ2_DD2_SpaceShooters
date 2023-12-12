using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AliciavanHaperen_LJ2_DD2_SpaceShooters.Models
{
    public class PlayerModel : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private string? name;

        public string? Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private int healthPoints;

        public int HealthPoints
        {
            get { return healthPoints; }
            set { healthPoints = value; OnPropertyChanged(); }
        }

        private TimeSpan elapsedTime;

        public TimeSpan ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; OnPropertyChanged(); }
        }

        public PlayerModel(int healthPoints, string name)
        {
            HealthPoints = healthPoints;
            Name = name;
        }


    }
}

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

        protected int id;
        protected string Name;
        protected int HealthPoints;
        protected TimeSpan ElapsedTime;

        public PlayerModel(int healthPoints, string Name)
        {

        }
    }
}

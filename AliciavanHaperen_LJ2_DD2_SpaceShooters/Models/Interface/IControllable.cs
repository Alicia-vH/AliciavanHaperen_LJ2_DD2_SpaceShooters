using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliciavanHaperen_LJ2_DD2_SpaceShooters.Models.Interface
{
    public interface IControllable
    {
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void MoveDown();
        void UpdateSpeed();
    }
}

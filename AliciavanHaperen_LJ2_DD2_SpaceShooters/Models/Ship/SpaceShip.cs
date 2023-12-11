using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliciavanHaperen_LJ2_DD2_SpaceShooters.Models.Ship
{
    abstract public class SpaceShip
    {
        public float Speed { get; }

        public SpaceShip(float speed) 
        {
            Speed = speed;
        } 

    }
}

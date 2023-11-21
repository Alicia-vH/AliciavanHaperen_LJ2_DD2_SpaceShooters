using AliciavanHaperen_LJ2_DD2_SpaceShooters.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliciavanHaperen_LJ2_DD2_SpaceShooters.Models.Ship
{
    public class PlayerShip : SpaceShip, IControllable
    {
        protected float XSpeed;
        protected float YSpeed;
        protected double FireRate;

        public PlayerShip(double fireRate, float friction, float speed) 
        { 

        }

        public void MoveDown()
        {

        }

        public void MoveLeft()
        {

        }

        public void MoveRight()
        {

        }

        public void MoveUp()
        {

        }

        public void UpdateSpeed()
        {

        }
    }
}

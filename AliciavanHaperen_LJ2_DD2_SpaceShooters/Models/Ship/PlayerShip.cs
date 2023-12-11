using AliciavanHaperen_LJ2_DD2_SpaceShooters.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AliciavanHaperen_LJ2_DD2_SpaceShooters.Models.Ship
{
    public class PlayerShip : SpaceShip, IControllable
    {
        public float XSpeed { get; set; }
        public float YSpeed { get; set; }
        public double FireRate { get; }
        public float Friction { get; private set; }

        public PlayerShip(double fireRate, float friction, float speed) : base(speed)
        {
           FireRate = fireRate;
           Friction = friction;
        }

        public void MoveDown()
        {
            YSpeed += Speed;
        }

        public void MoveLeft()
        {
            XSpeed += Speed;
        }

        public void MoveRight()
        {
            XSpeed -= Speed;
        }

        public void MoveUp()
        {
            YSpeed -= Speed;
        }

        public void UpdateSpeed()
        {
            XSpeed *= Friction;
            YSpeed *= Friction;
        }
    }
}

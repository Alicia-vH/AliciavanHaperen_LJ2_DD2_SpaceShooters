﻿using AliciavanHaperen_LJ2_DD2_SpaceShooters.Models.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliciavanHaperen_LJ2_DD2_SpaceShooters.Models.Enemy
{
    public class EnemyGray : EnemyShip
    {
        public EnemyGray(int attackDamage, string spaceShipImage, float speed) : base(attackDamage, spaceShipImage, speed)
        {
        }

        public override string EnemyDescription()
        {
            return "I am enemy Gray";
        }
    }
}

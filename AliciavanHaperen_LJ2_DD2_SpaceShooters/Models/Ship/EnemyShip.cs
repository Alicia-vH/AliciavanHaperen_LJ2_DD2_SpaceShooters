using AliciavanHaperen_LJ2_DD2_SpaceShooters.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AliciavanHaperen_LJ2_DD2_SpaceShooters.Models.Ship
{
    abstract public class EnemyShip : SpaceShip, IAgressive
    {
        protected ImageBrush SpaceShipImage;

        public EnemyShip(int attackDamage, string spaceShipImage, float speed) 
        { 
            
        }

        public abstract string EnemyDescription();
    }
}

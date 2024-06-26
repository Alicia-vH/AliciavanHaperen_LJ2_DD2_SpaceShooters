﻿using AliciavanHaperen_LJ2_DD2_SpaceShooters.Models.Interface;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AliciavanHaperen_LJ2_DD2_SpaceShooters.Models.Ship
{
    abstract public class EnemyShip : SpaceShip, IAgressive
    {
        public ImageBrush SpaceShipImage { get; set; }
        public int AttackDamage { get; }

        int IAgressive.AttackDamage => 200;

        protected EnemyShip(int attackDamage, string spaceShipImage, float speed) : base(speed)
        {

            SpaceShipImage = new ImageBrush() 
            { 
                ImageSource = new BitmapImage(new Uri(spaceShipImage)) 
            };

            AttackDamage = attackDamage;

        }

        public abstract string EnemyDescription();


    }
}

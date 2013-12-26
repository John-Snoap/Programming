using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    abstract class EnemyBoss : EnemySpaceShip
    {
        // protected data
        protected TimeSpan timeSincePreviousAttack;
        protected bool attack;

        // public properties
        public bool Attack
        {
            get
            {
                return attack;
            } // end getter
        } // end property

        // constructor
        public EnemyBoss(Vector2 pos, int hitPoints) : base(pos, hitPoints)
        {
            timeSincePreviousAttack = new TimeSpan(0, 0, 0);
            attack = false;
        } // end constructor
    } // end class EnemyBoss
} // end namespace Star_Wars_Space_Invaders

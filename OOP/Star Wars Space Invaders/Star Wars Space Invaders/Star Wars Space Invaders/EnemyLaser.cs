using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    abstract class EnemyLaser : Laser
    {
        // this class is only here to ensure that we do not mix up player lasers with enemy lasers in the game

        // constructor
        public EnemyLaser(Vector2 pos) : base(pos)
        {
        } // end constructor

        // public abstract methods
        //public abstract Texture2D FindTexture();
    } // end abstract class EnemyLaser
} // end namespace Star_Wars_Space_Invaders

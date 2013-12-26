using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    abstract class EnemyFighter : EnemySpaceShip
    {
        // constructor
        public EnemyFighter(Vector2 pos, int hitPoints) : base(pos, hitPoints)
        {
        } // end constructor

        // public abstract properties
        public abstract Vector2 GunPosition { get; }

        // protected methods
        protected void move(Texture2D Texture, int moveSpeed)
        {
            if (movingRight)
            {
                position.X += moveSpeed;

                if (position.X >= (positionOfReset.X + (Texture.Width * 2)))
                {
                    moveDown(moveSpeed);
                    movingRight = false;
                } // end if
            } // end if
            else
            {
                position.X -= moveSpeed;

                if (position.X <= (positionOfReset.X - (Texture.Width * 2)))
                {
                    moveDown(moveSpeed);
                    movingRight = true;
                } // end if
            } // end else
        } // end override method Move

        // private methods
        private void moveDown(int moveSpeed)
        {
            position.Y += moveSpeed * 3;
        } // end method moveDown
    } // end abstract class EnemyFighter
} // end namespace Star_Wars_Space_Invaders

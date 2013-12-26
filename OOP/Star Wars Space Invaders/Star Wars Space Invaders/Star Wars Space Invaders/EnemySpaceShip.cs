using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    abstract class EnemySpaceShip : SpaceShip
    {
        // private static random variable for agility calculations
        private static Random R = new Random();

        // protected data
        protected int percentAgility;
        protected bool movingRight;

        // properties
        public bool KeepDrawing { get; set; }
        public bool MovingRight
        {
            get
            {
                return movingRight;
            } // end getter
        } // end property MovingRight

        // constructor
        public EnemySpaceShip(Vector2 pos, int hitPoints) : base(pos)
        {
            percentAgility = 0;
            KeepDrawing = true;
            movingRight = true;
            playerLivesOrEnemyHitPoints = hitPoints;
        } // end constructor

        // public override methods
        public override void ImHit()
        {
            if (R.Next(1, 101) > percentAgility)
            {
                base.ImHit();

                if (playerLivesOrEnemyHitPoints <= 0 && KeepDrawing)
                {
                    KeepDrawing = false;
                } // end if
            } // end if
        }

        // public abstract methods
        public abstract void Move();
        //public abstract Texture2D FindTexture();
        //public abstract Texture2DData FindTexture();
        public abstract EnemyLaser MakeNewEnemyLaser();

        // public override abstract methods
        public override abstract void Draw(SpriteBatch spriteBatch);

        // protected methods
        protected void draw(SpriteBatch spriteBatch, Texture2D Texture)
        {
            if (KeepDrawing)
            {
                spriteBatch.Draw(Texture, position, Color.White);
            } // end if
        }
    } // end class EnemySpaceShip
} // end namespace Star_Wars_Space_Invaders

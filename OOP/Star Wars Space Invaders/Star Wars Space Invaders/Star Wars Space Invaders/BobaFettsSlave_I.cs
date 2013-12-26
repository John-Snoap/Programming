using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    class BobaFettsSlave_I : EnemyBoss
    {
        // private static random variable for laser fire calculations
        private static Random R = new Random();

        // private data
        private const int HIT_POINTS = 50;
        private int moveSpeed = 10;
        private int chanceToFire;
        private bool stage2;
        private bool stage3;

        // properties
        public static Texture2D Texture { get; set; }
        public static Texture2DData TextureData { get; set; }

        // returns center top of texture (i.e., where the gun is located)
        public Vector2 GunPosition
        {
            get
            {
                return new Vector2(position.X + (Texture.Width / 2), (position.Y + ((Texture.Height) / 2)));
            } // end get
        } // end property

        // constructor
        public BobaFettsSlave_I(Vector2 pos) : base(pos, HIT_POINTS)
        {
            chanceToFire = 5;
            percentAgility = 40;
            stage2 = false;
            stage3 = false;
        } // end constructor

        // public override methods
        public override void Move()
        {
            if (playerLivesOrEnemyHitPoints <= 10 && !stage3)
            {
                stage3 = true;
                stage2 = false;
                chanceToFire = 12;
                percentAgility = 90;
                moveSpeed = 15;
            } // end if
            else if (playerLivesOrEnemyHitPoints <= 30 && !stage2 && !stage3)
            {
                stage2 = true;
                chanceToFire = 10;
                percentAgility = 60;
                moveSpeed = 12;
            } // end else if

            if (movingRight)
            {
                position.X += moveSpeed;

                if (position.X >= (positionOfReset.X + (Texture.Width * 4)))
                {
                    movingRight = false;
                    attack = true;
                } // end if
                else if (attack)
                {
                    attack = false;
                } // end else if
            } // end if
            else
            {
                position.X -= moveSpeed;

                if (position.X <= (positionOfReset.X - (Texture.Width * 4)))
                {
                    movingRight = true;
                    attack = true;
                } // end if
                else if (attack)
                {
                    attack = false;
                } // end else if
            } // end else

            if (position.X == positionOfReset.X)
            {
                attack = true;
            } // end if

            if (R.Next(1, 101) <= chanceToFire)
            {
                attack = true;
            } // end if
        } // end override method move

        public override Texture2DData FindTexture()
        {
            return TextureData;
        } // end override method FindTexture

        public override EnemyLaser MakeNewEnemyLaser()
        {
            return new Slave_I_Laser(new Vector2((GunPosition.X - Slave_I_Laser.Texture.Width / 2 + 2), GunPosition.Y));
        } // end odverride method MakeNewEnemyLaser

        public override void Draw(SpriteBatch spriteBatch)
        {
            draw(spriteBatch, Texture);
        } // end override method Draw
    } // end class BobaFettsSlave_I
} // end namespace Star_Wars_Space_Invaders

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    class ImperialClassStarDestroyer : EnemyBoss
    {
        // private data
        private const int HIT_POINTS = 500;
        private int moveSpeed = 2;

        // properties
        public static Texture2D Texture { get; set; }
        public static Texture2DData TextureData { get; set; }

        // constructor
        public ImperialClassStarDestroyer(Vector2 pos) : base(pos, HIT_POINTS)
        {
            movingRight = false;
        } // end constructor

        public override void Move()
        {
            if (movingRight)
            {
                if (!TextureData.FlippedHorizontally)
                {
                    TextureData.FlippedHorizontally = movingRight;
                } // end if

                position.X += moveSpeed;

                if (position.X >= positionOfReset.X)
                {
                    movingRight = false;
                    TextureData.FlippedHorizontally = movingRight;
                } // end if
            } // end if
            else
            {
                if (TextureData.FlippedHorizontally)
                {
                    TextureData.FlippedHorizontally = movingRight;
                } // end if

                position.X -= moveSpeed;

                if (position.X <= (positionOfReset.X - positionOfReset.X + 100 - Texture.Width))
                {
                    movingRight = true;
                    TextureData.FlippedHorizontally = movingRight;
                } // end if
            } // end else
        } // end override method Move

        public override Texture2DData FindTexture()
        {
            return TextureData;
        }

        public override EnemyLaser MakeNewEnemyLaser()
        {
            return new TieLaser(new Vector2((positionOfReset.X / 2 - TieLaser.Texture.Width / 2 + 2), positionOfReset.Y));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (KeepDrawing)
            {
                if (movingRight)
                {
                    spriteBatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                } // end if
                else
                {
                    spriteBatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                } // end else
            } // end if
        }
    } // end class ImperialClassStarDestroyer
} // end namespace Star_Wars_Space_Invaders

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    class TieBomb : EnemyLaser
    {
        // private data
        private const int ACCELERATION = 1;
        private int velocity = 3;

        // properties
        public static Texture2D Texture { get; set; }
        public static Texture2DData TextureData { get; set; }

        // constructor
        public TieBomb(Vector2 pos) : base(pos)
        {
        } // end constructor

        // public override methods
        public override Texture2DData FindTexture()
        {
            return TextureData;
        } // end override method FindTexture

        public override void Move()
        {
            position.Y += velocity;
            velocity += ACCELERATION;
        } // end override method Move

        public override void Draw(SpriteBatch spriteBatch)
        {
            draw(spriteBatch, Texture);
        } // end override method Draw
    } // end class TieBomb
} // end namespace Star_Wars_Space_Invaders

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    class TieLaser : EnemyLaser
    {
        // properties
        public static Texture2D Texture { get; set; }
        public static Texture2DData TextureData { get; set; }

        // constructor
        public TieLaser(Vector2 pos) : base(pos)
        {
        } // end constructor

        // public override methods
        public override Texture2DData FindTexture()
        {
            return TextureData;
        } // end override method FindTexture

        public override void Draw(SpriteBatch spriteBatch)
        {
            draw(spriteBatch, Texture);
        } // end override method Draw
    } // end class TieFighterLaser
} // end namespace Star_Wars_Space_Invaders

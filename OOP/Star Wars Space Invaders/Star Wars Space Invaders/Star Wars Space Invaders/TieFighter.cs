using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    class TieFighter : EnemyFighter
    {
        // private data
        private const int MOVE_SPEED = 5;
        private const int HIT_POINTS = 2;

        // properties
        public static Texture2D Texture { get; set; }
        public static Texture2DData TextureData { get; set; }

        // returns center top of texture (i.e., where the gun is located)
        public override Vector2 GunPosition
        {
            get
            {
                return new Vector2(position.X + (Texture.Width / 2), (position.Y + ((Texture.Height) / 2)));
            } // end get
        } // end property

        // constructor
        public TieFighter(Vector2 pos) : base(pos, HIT_POINTS)
        {
        } // end constructor

        // public override methods
        //public override Texture2D FindTexture()
        //{
        //    return Texture;
        //} // end override method FindTexture
        public override Texture2DData FindTexture()
        {
            return TextureData;
        } // end override method FindTexture

        public override EnemyLaser MakeNewEnemyLaser()
        {
            return new TieLaser(new Vector2((GunPosition.X - TieLaser.Texture.Width / 2), GunPosition.Y));
        } // end odverride method MakeNewEnemyLaser

        public override void Move()
        {
            move(Texture, MOVE_SPEED);
        } // end override method move

        public override void Draw(SpriteBatch spriteBatch)
        {
            draw(spriteBatch, Texture);
        } // end override method Draw
    } // end class TieFighter
} // end namespace Star_Wars_Space_Invaders

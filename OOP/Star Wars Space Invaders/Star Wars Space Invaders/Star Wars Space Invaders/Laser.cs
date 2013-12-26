using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    abstract class Laser : GameObject
    {
        // private data
        protected const int LASER_SPEED = 10;

        // protected data
        protected Vector2 position; // position of laser (upper left)

        // properties
        public bool KeepDrawing { get; set; }
        public Vector2 Position
        {
            get
            {
                return position;
            } // end getter
        } // end property Position

        // constructor
        protected Laser(Vector2 pos)
        {
            position = pos;
            KeepDrawing = true;
        } // end constructor

        // public virtual methods
        public virtual void Move()
        {
            position.Y += LASER_SPEED;
        } // end virtual method Move

        // public abstract methods
        public abstract void Draw(SpriteBatch spriteBatch);

        // protected methods
        protected void draw(SpriteBatch spriteBatch, Texture2D Texture)
        {
            if (KeepDrawing)
            {
                spriteBatch.Draw(Texture, position, Color.White);
            } // end if
        }
    } // class Laser
} // end namespace Star_Wars_Space_Invaders

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    abstract class SpaceShip : GameObject
    {
        // public enumerations
        public enum Direction { Left, Right, Up, Down };

        // protected data
        protected Vector2 position; // position of ship (upper left)
        protected Vector2 positionOfReset; // starting position of the ship
        protected int playerLivesOrEnemyHitPoints; // number of lives the player has or number of hits an enemy can take

        // properties
        public Vector2 Position
        {
            get
            {
                return position;
            } // end getter
        } // end property Position

        public Vector2 SetResetPositionAndReset
        {
            set
            {
                position = value;
                setResetPosition();
            } // end setter
        } // end property SetResetPositionAndReset

        public int PlayerLivesOrEnemyHitPoints
        {
            get
            {
                return playerLivesOrEnemyHitPoints;
            } // end getter
        } // end property PlayerLivesOrEnemyHitPoints

        // constructor
        protected SpaceShip(Vector2 pos)
        {
            position = pos;
            setResetPosition();
            playerLivesOrEnemyHitPoints = 1;
        } // end constructor

        // public methods
        public void Reset()
        {
            position = positionOfReset;
        } // end method reset

        // public virtual methods
        public virtual void ImHit()
        {
            playerLivesOrEnemyHitPoints--;
        } // end virtual method ImHit

        // public abstract methods
        //public abstract void Move(Direction dir);
        public abstract void Draw(SpriteBatch spriteBatch);

        // private methods
        private void setResetPosition()
        {
            positionOfReset = position;
        } // end method setResetPosition

        // static methods
        // collosion detection algorithm:
        // first see if the the texture rectangles intersect
        // if there is an intersection look at just the pixels in the intersection
        // if the pixels in both textures in the intersection are not transparent, then it is a collision
        public static bool CollisionBetween(Vector2 thatVector, Texture2DData thatTextureData, Vector2 theOtherVector, Texture2DData theOtherTextureData, bool calcPerPixel = true)
        {
            bool didItCollide = false;
            Rectangle that = new Rectangle((int)thatVector.X, (int)thatVector.Y, thatTextureData.Texture.Width, thatTextureData.Texture.Height);
            Rectangle theOther = new Rectangle((int)theOtherVector.X, (int)theOtherVector.Y, theOtherTextureData.Texture.Width, theOtherTextureData.Texture.Height);
            
            didItCollide = that.Intersects(theOther);

            if (calcPerPixel && didItCollide) // If simple intersection fails, don't even bother with per-pixel
            {
                didItCollide = PerPixelCollision(that, thatTextureData, theOther, theOtherTextureData);
            } // end if

            return didItCollide;
        } // end static method CollisionBetween

        private static bool PerPixelCollision(Rectangle rectangleA, Texture2DData textureAData, Rectangle rectangleB, Texture2DData textureBData)
        {
            bool returnValue = false; // no intersection found

            // find the bounds of the rectangle intersection
            Rectangle rectangleOfCollision = Rectangle.Intersect(rectangleA, rectangleB);

            

            // get the color data of just the part of the texture we are looking at
            //Texture2D cropTextureA = new Texture2D(textureAData.Texture.GraphicsDevice, rectangleOfCollision.Width, rectangleOfCollision.Height);
            //Color[] colorA = new Color[rectangleOfCollision.Width * rectangleOfCollision.Height];
            //textureA.GetData(0, rectangleOfCollision, colorA, 0, colorA.Length);
            //textureAData.Texture.GetData(0, rectangleOfCollision, colorA, 0, colorA.Length);
            //cropTextureA.SetData(colorA);

            // get the color data of just the part of the texture we are looking at
            //Texture2D cropTextureB = new Texture2D(textureBData.Texture.GraphicsDevice, rectangleOfCollision.Width, rectangleOfCollision.Height);
            //Color[] colorB = new Color[rectangleOfCollision.Width * rectangleOfCollision.Height];
            //textureB.GetData(0, rectangleOfCollision, colorB, 0, colorB.Length);
            //textureBData.Texture.GetData(colorB);
            //cropTextureB.SetData(colorB);

            //Color[] colorA = textureAData.GetTextureColors(rectangleOfCollision);
            Color[] colorA = textureAData.ColorData;
            Color[] colorB = textureBData.ColorData;

            Color cA;
            Color cB;


            // get Color data of each Texture
            //Color[] dataAA = new Color[textureA.Width * textureA.Height];
            //textureA.GetData(dataAA);
            //Color[] dataBB = new Color[textureB.Width * textureB.Height];
            //textureB.GetData(dataBB);

            // to find the color at a specific pixel
            //Color colorAA;
            //Color colorBB;

            //Color cA;
            //Color cB;

            //foreach (Color cA in colorA)
            //{

            //} // end foreach loop

            // check every point within the intersection bounds
            //for (int color = 0; color < colorA.Length && !returnValue; color++)
            //{
            //    if (colorA[color].A * colorB[color].A != 0) // if both pixels are not completely transparent
            //    {
            //        returnValue = true; // an intersection has been found
            //    } // end if
            //} // end for loop

            // check every point within the intersection bounds
            for (int y = rectangleOfCollision.Top; y < rectangleOfCollision.Bottom && !returnValue; y++)
            {
                for (int x = rectangleOfCollision.Left; x < rectangleOfCollision.Right && !returnValue; x++)
                {
                    // get the color of both pixels at this point
                    cA = colorA[(x - rectangleA.Left) + (y - rectangleA.Top) * rectangleA.Width];
                    cB = colorB[(x - rectangleB.Left) + (y - rectangleB.Top) * rectangleB.Width];

                    // if both pixels are not completely transparent,
                    if (cA.A * cB.A != 0)
                    {
                        returnValue = true; // an intersection has been found
                    } // end if
                } // end for loop
            } // end for loop

            return returnValue;
        } // end method PerPixelCollision
    } // end abstract class SpaceShip
} // end namespace Star_Wars_Space_Invaders

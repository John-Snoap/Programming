using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    class PlayerSpaceShip : SpaceShip
    {
        // private data
        private const int MOVE_SPEED = 7;
        private int numberOfLives;

        // properties
        public static Texture2D Texture { get; set; }
        public static Texture2DData TextureData { get; set; }

        // returns center top of texture (i.e., where the gun is located)
        public Vector2 GunPosition
        {
            get
            {
                return new Vector2(position.X + (Texture.Width / 2), position.Y);
            } // end get
        } // end property

        // constructor
        public PlayerSpaceShip(Vector2 pos, int playerLives) : base(pos)
        {
            numberOfLives = playerLives;
            NewLevel();
        } // end constructor

        // public methods
        public void NewLevel()
        {
            playerLivesOrEnemyHitPoints = numberOfLives;
        } // end method NewLevel

        public void Move(SpaceShip.Direction dir)
        {
            switch (dir)
            {
                case Direction.Left: position.X -= MOVE_SPEED; // Good Guy = 7, TIE = 5, Boba Fett = 15?, Imperial SS = 2, Executor SS = 1
                    break;
                case Direction.Right: position.X += MOVE_SPEED;
                    break;
            } // end switch
        } // end method move

        // public override methods
        public override Texture2DData FindTexture()
        {
            return TextureData;
        } // end override method FindTexture

        public override void ImHit()
        {
            base.ImHit();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, Color.White);
        } // end override method Draw
    } // end class PlayerSpaceShip
} // end namespace Star_Wars_Space_Invaders

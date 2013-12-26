using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Windows.Forms;          // needed so that we can add normal buttons, etc.

// This project doesn't do anything useful, but it illustrates the functions you'll need
// to implement your game.
namespace xnaDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics; // this represents the graphics device 
        private SpriteBatch spriteBatch;        // all drawing takes place via this object
        private Texture2D backGroundTexture;    // will point to a picture to use for background
        private Texture2D missileTexture;       // will point to a picture to use for the missile
        private Vector2 missilePos;             // position of the missle
        private bool isMissleInFlight;          // is the missle active?
        private int numMissilesFired = 0;       // how many times has missle been fired?
        private Rectangle viewPort;             // tells us the size of the drawable area in the window
        private Ship myShip;                    // reference to the ship controlled by the user
        private KeyboardState oldState;         // keeps previous state of keys pressed, so we can
                                                // detect when a change in state occurs

        // NOTE: we really ought to have a class to keep up with the missle information; 
        // this is left as an exercise for the reader

        const int PANEL_WIDTH = 100;    // width of a panel we'll add for adding buttons, etc.

        // Controls we'll add to form, just for sake of illustration.
        Panel gameControlPanel; // control panel
        Label lblFireCount;     // a label to show how many times we're fired

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            InitializeControlPanel();

            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            
            viewPort = GraphicsDevice.Viewport.Bounds;
            oldState = Keyboard.GetState();
            missilePos = Vector2.Zero;
            isMissleInFlight = false;

            myShip = new Ship(new Vector2((viewPort.Width - PANEL_WIDTH)/2, 
                                          viewPort.Height/2));
            base.Initialize();
        }

        // Since this is an xna project; we can't easily drag/drop controls onto the
        // main Form like we have been doing in the past. But, we can do it programatically,
        // as illustrated below.
        private void InitializeControlPanel()
        {
            // instantiate a panel and a label
            gameControlPanel = new Panel();
            lblFireCount = new Label();

            // setup the panel control. Note: this panel will overlay the viewport
            // for 100 pixels; we should adjust for this (not shown in example)
            this.gameControlPanel.Dock = DockStyle.Left;
            this.gameControlPanel.Width = PANEL_WIDTH;

            // create a button           
            Button btn = new Button();
            btn.Location = new System.Drawing.Point(10, 10);
            btn.Text = "Testing";
            btn.Click += new EventHandler(btn_Click);

            // add the button to the panel 
            this.gameControlPanel.Controls.Add(btn);

            // setup the label control and add it to the panel             
            this.lblFireCount.Text = "";
            this.lblFireCount.Location = new System.Drawing.Point(10, btn.Top + btn.Height + 10);
            this.lblFireCount.AutoSize = true;
            this.gameControlPanel.Controls.Add(this.lblFireCount);

            // get a reference to game window  
            // note that the 'as Form' is the same as a type cast
            Form form = Control.FromHandle(this.Window.Handle) as Form;

            // add the panel to the game window form
            form.Controls.Add(gameControlPanel);
        }

        // demo button event handler
        void btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked a button!");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load textures into memory once during LoadContent()
            backGroundTexture = Content.Load<Texture2D>("Stars");
            
            // Good Guy
            //missileTexture = Content.Load<Texture2D>("Double Red Laser");
            //Ship.Texture = Content.Load<Texture2D>("Millennium Falcon");

            // TIE Fighters
            //missileTexture = Content.Load<Texture2D>("Double Green Laser");
            //Ship.Texture = Content.Load<Texture2D>("TIE Fighter");
            //Ship.Texture = Content.Load<Texture2D>("TIE Bomber");
            //Ship.Texture = Content.Load<Texture2D>("TIE Interceptor");
            //Ship.Texture = Content.Load<Texture2D>("Darth Vader's TIE Advanced x1");

            // Boba Fett
            missileTexture = Content.Load<Texture2D>("Double Red Laser");
            Ship.Texture = Content.Load<Texture2D>("Slave I");

            // Star Destoyers
            //Ship.Texture = Content.Load<Texture2D>("Imperial Class Star Destroyer");
            //Ship.Texture = Content.Load<Texture2D>("Executor Class Star Destroyer");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// This gets called ~60 times per second
        protected override void Update(GameTime gameTime)
        {

            moveShip();     // if a arrow key is held down, move the ship

            fireMissle();   // create a missle if the space bar was hit

            moveMissle();  // if the missle is alive, update its position

            base.Update(gameTime);
        }

        // Check to see if an arrow key is currently pressed. Take appropriate
        // action to move ship (note, no boundary checking is done to ensure
        // ship doesn't move off-screen; this should be fixed in your program).
        private void moveShip()
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                myShip.move(Ship.Direction.Left);  
            }
            else if ( state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right ))
            {
                myShip.move(Ship.Direction.Right);
            }
            else if ( state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up ))
            {
                 myShip.move(Ship.Direction.Up);
            }
            else if ( state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                myShip.move(Ship.Direction.Down);
            }

        }

        // Check to see if the space key is down. If so, set flag and position so
        // that the Draw will Draw the missle. Note: this is a little different than
        // the ship, because we only want to fire the missle on a button click
        private void fireMissle()
        {
            KeyboardState newState = Keyboard.GetState();

            // Is the SPACE key down?
            if (newState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            {
               
                // If it was not down the previous time we checked, then this is a 
                // new key press. If the key was down in the old state, they're just
                // holding it down, so we ignore this state.
                if (!oldState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    // I'm making a rule that you can only have one missle at a time
                    if (!isMissleInFlight)
                    {
                        isMissleInFlight = true;
                        missilePos = myShip.GunPosition;
                        missilePos.X = missilePos.X - missileTexture.Width / 2 + 2;
                        numMissilesFired++;
                        lblFireCount.Text = numMissilesFired.ToString();
                    }
                }

            }


            // Update saved state.
            oldState = newState;
        }

        // If the missle is alive, move it up a few clicks until it goes off screen
        public void moveMissle()
        {
            if (isMissleInFlight)
            {
                //missilePos.Y -= 25;
                missilePos.Y += 10; // 10 is a good laser speed
                //if (missilePos.Y <= 0)
                if (missilePos.Y >= viewPort.Height)
                {
                    isMissleInFlight = false;
                    missilePos = Vector2.Zero;
                } 
            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// This is called in the main loop after Update to render the current state of the game 
        /// on the screen. 
        protected override void Draw(GameTime gameTime)
        {
            // spriteBatch is an object that allows us to draw everything
            // on screen (it contains the Draw functions).
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            
            // draw the background
            spriteBatch.Draw(backGroundTexture, viewPort, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            
            // draw the ship over background
            myShip.Draw(spriteBatch);

            // draw the missle
            if (isMissleInFlight)
                spriteBatch.Draw(missileTexture, missilePos, Color.White);
           
            // This tells the graphics pipeline that we're done drawing this frame,
            // and asks it to push the pixels to the graphics frame buffer.
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

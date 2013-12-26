// Joshua Mullen and John Snoap
// https://drive.google.com/a/oc.edu/folderview?id=0B0Smr0ENLWFRcUZ6MURBX0xndVU&usp=sharing

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
using System.Windows.Forms; // needed so that we can add normal buttons, etc.
using System.ComponentModel;
using System.Data;
using System.Text;
using System.IO;


namespace Star_Wars_Space_Invaders
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class StarWarsSpaceInvaders : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;         // this represents the graphics device
        private SpriteBatch spriteBatch;                // all drawing takes place via this object
        private Texture2D backGroundTexture;            // will point to a picture to use for background
        private Rectangle viewPort;                     // tells us the size of the drawable area in the window
        private PlayerSpaceShip milleniumFalcon;        // reference to the ship controlled by the user
        private List<PlayerLaser> playerLasers;         // list of all the lasers that the player fires
        private List<EnemyLaser> enemyLasers;           // list of all the lasers that the enemies fire
        private List<EnemyFighter> enemyFighters;       // list of all the enemy fighters in the current level
        private List<EnemyBoss> enemyBosses;            // list of all the enemy bosses in the current level
        private Level[] levels;                         // array of all the levels in the game
        private int currentLevel;                       // the current level we are on
        private KeyboardState oldKeyboardState;         // keeps previous state of keys pressed, so we can detect when a change in state occurs
        private GamePadState oldGamePadState;           // keeps previous state of game pad buttons pressed, so we can detect when a change in state occurs
        private TimeSpan oldGameTime;                   // keeps previous time of the game, so we can determine how long it has been since last enemy fired
        private int randomMilliSecondsWaitTime;         // keeps track of how long we are going to wait this time
        private int minWaitMilliSeconds;                // keeps track of the minimum wait time in seconds between laser fires
        private int maxWaitMilliSeconds;                // keeps track of the maximum wait time in seconds between laser fires
        private Random rnd;                             // allows us to make random numbers
        private bool autoFire;                          // whether the user has to tell the game to shoot or not
        private bool invincibleShielding;               // if the user is effected by bullets
        private bool gameOver;                          // tells us if the game is over
        private bool gameWon;                           // tells us if the game is won
        private Form1 form1;                            // this actuall does work!
        private int difficulty;                         // used to determine the difficulty from the text file
        private int playerLives;                        // player lives is based on difficulty
        private int playerLivesLeft;                    // number of lives the player has left
        private int enemiesDestroyed;                   // number of enemies the player has destroyed
        private int enemiesDestroyedAtLvlBegin;         // number of enemies destroyed at the beginning of the level
        private bool cheat;                             // tells whether or not the player is cheating
        private bool gameStarted;                       // lets us know if the user has started the game
        private bool beginningOfNewGame;                // lets us know if it is the beginning of a new game
        private bool gamePaused;                        // lets us know if the user has paused the game
        private bool gameQuit;                          // lets us know if the user has quit the game
        private bool settingsHaveChanged;               // lets us know if the settings have been changed
        private bool settingsAreBeingLookedAt;          // lets us know if the settings are being looked at by the user

        private const int PANEL_WIDTH = 100;            // width of a panel we'll add for adding buttons, etc.
        private const int NUMBER_OF_LEVELS = 16;         // number of levels

        Panel gameControlPanel;                         // control panel

        public StarWarsSpaceInvaders()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = false;
            InitializeControlPanel();

            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1380;
            graphics.PreferredBackBufferHeight = 720;
        }

        // Since this is an xna project; we can't easily drag/drop controls onto the
        // main Form like we have been doing in the past. But, we can do it programatically,
        // as illustrated below.
        private void InitializeControlPanel()
        {
            // instantiate a panel and a form1
            
            form1 = new Form1();

            gameControlPanel = form1.pnlGameOptions;

            // setup the panel control.
            this.gameControlPanel.Dock = DockStyle.Left;
            this.gameControlPanel.Width = PANEL_WIDTH;

            // get a reference to game window  
            // note that the 'as Form' is the same as a type cast
            Form form = Control.FromHandle(this.Window.Handle) as Form;

            // add the panel to the game window form
            form.Controls.Add(gameControlPanel);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            viewPort = GraphicsDevice.Viewport.Bounds;
            levels = new Level[NUMBER_OF_LEVELS] { new Level1(viewPort, PANEL_WIDTH), new Level2(viewPort, PANEL_WIDTH), new Level3(viewPort, PANEL_WIDTH), new BossLevelDarthVader(viewPort, PANEL_WIDTH), new Level4(viewPort, PANEL_WIDTH), new Level5(viewPort, PANEL_WIDTH), new Level6(viewPort, PANEL_WIDTH), new BossLevelBobaFett(viewPort, PANEL_WIDTH), new Level7(viewPort, PANEL_WIDTH), new Level8(viewPort, PANEL_WIDTH), new Level9(viewPort, PANEL_WIDTH), new BossLevelSS(viewPort, PANEL_WIDTH), new Level10(viewPort, PANEL_WIDTH), new Level11(viewPort, PANEL_WIDTH), new Level12(viewPort, PANEL_WIDTH), new BossLevelSSS(viewPort, PANEL_WIDTH) }; 
            rnd = new Random();
            
            base.Initialize();
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

            // load background
            backGroundTexture = Content.Load<Texture2D>("Stars");

            // load good guy
            PlayerSpaceShip.Texture = Content.Load<Texture2D>("Millennium Falcon");
            PlayerSpaceShip.TextureData = new Texture2DData(PlayerSpaceShip.Texture);
            PlayerLaser.Texture = Content.Load<Texture2D>("Single Red Laser");
            PlayerLaser.TextureData = new Texture2DData(PlayerLaser.Texture);

            // load tie fighter textures
            TieFighter.Texture = Content.Load<Texture2D>("TIE Fighter");
            TieFighter.TextureData = new Texture2DData(TieFighter.Texture);
            TieInterceptor.Texture = Content.Load<Texture2D>("TIE Interceptor");
            TieInterceptor.TextureData = new Texture2DData(TieInterceptor.Texture);
            TieLaser.Texture = Content.Load<Texture2D>("Double Green Laser");
            TieLaser.TextureData = new Texture2DData(TieLaser.Texture);
            TieBomber.Texture = Content.Load<Texture2D>("TIE Bomber");
            TieBomber.TextureData = new Texture2DData(TieBomber.Texture);
            TieBomb.Texture = Content.Load<Texture2D>("TIE Bomb");
            TieBomb.TextureData = new Texture2DData(TieBomb.Texture);

            // load darth vader boss texture
            DarthVadersTieAdvancedx1.Texture = Content.Load<Texture2D>("Darth Vader's TIE Advanced x1");
            DarthVadersTieAdvancedx1.TextureData = new Texture2DData(DarthVadersTieAdvancedx1.Texture);

            // load boba fett boss texture
            BobaFettsSlave_I.Texture = Content.Load<Texture2D>("Slave I");
            BobaFettsSlave_I.TextureData = new Texture2DData(BobaFettsSlave_I.Texture);
            Slave_I_Laser.Texture = Content.Load<Texture2D>("Double Red Laser");
            Slave_I_Laser.TextureData = new Texture2DData(Slave_I_Laser.Texture);

            // load imperial class star destroyer
            ImperialClassStarDestroyer.Texture = Content.Load<Texture2D>("Imperial Class Star Destroyer");
            ImperialClassStarDestroyer.TextureData = new Texture2DData(ImperialClassStarDestroyer.Texture);

            // load executor class star destroyer
            ExecutorClassStarDestroyer.Texture = Content.Load<Texture2D>("Executor Class Star Destroyer");
            ExecutorClassStarDestroyer.TextureData = new Texture2DData(ExecutorClassStarDestroyer.Texture);
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
        protected override void Update(GameTime gameTime)
        {
            gameStarted = form1.GameStarted;
            gamePaused = form1.GamePaused;
            gameQuit = form1.GameQuit;
            settingsHaveChanged = form1.SettingsHaveChanged;
            settingsAreBeingLookedAt = form1.SettingsAreBeingLookedAt;

            // change what some labels say in the panel
            form1.lblLivesLeft.Text = playerLivesLeft.ToString();
            form1.lblShipsDestroyed.Text = enemiesDestroyed.ToString();

            if (gameQuit)
            {
                prepareToStartGame();
                form1.GameQuit = gameQuit;
            } // end if

            if (settingsHaveChanged)
            {
                loadCurrentSettings();
                settingsHaveChanged = false;
                form1.SettingsHaveChanged = settingsHaveChanged;
            } // end if

            if (gameStarted && beginningOfNewGame)
            {
                prepareToStartGame();
                beginningOfNewGame = false;
            } // end if

            if (!gameOver && !gamePaused && !gameQuit && !settingsAreBeingLookedAt)
            {
                movePlayerSpaceShip(); // if an arrow key is held down, move the ship
                fireLaser(); // if the space bar is pressed, fire a laser
                moveLasers(); // move the lasers if they exist

                if (gameStarted)
                {
                    updateLevel(); // update the level as appropriate; i.e. advance level, reset level if player is shot, etc.
                    moveEnemySpaceShips(); // move enemy space ships
                    fireEnemyLaser(gameTime); // fire enemy lasers
                    detectCollisions();
                    removeSpaceDebris();
                } // end if
            } // end if
            else if (gameOver && !gameWon)
            {
                // losing sign
                MessageBox.Show("Apology accepted.\n\nDo not click OK.\nDo not click on the big X.\nPress Enter.", "You Have Lost", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                KeyboardState state = Keyboard.GetState();
                if (state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    form1.resetGame();
                    prepareToStartGame();
                } // end if
                
            } // end else if
            else if (gameOver && gameWon)
            {
                // winning sign
                MessageBox.Show("You have won!\n\nDo not click OK.\nDo not click on the big X.\nPress Enter.", "Winner!", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
                KeyboardState kState = Keyboard.GetState();
                if (kState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    form1.resetGame();
                    prepareToStartGame();
                } // end if
            } // end else if

            base.Update(gameTime);
        } // end override method Update

        private void updateLevel()
        {
            if (enemyBosses.Count <=0 && enemyFighters.Count <= 0 && enemyLasers.Count <= 0 && currentLevel < NUMBER_OF_LEVELS)
            {
                currentLevel++;
                enemiesDestroyedAtLvlBegin = enemiesDestroyed;
                enemyFighters = levels[currentLevel - 1].StartLevel(out enemyBosses);
                minWaitMilliSeconds = levels[currentLevel - 1].MinTimeInMilliSecondsBetweenLaserFires;
                maxWaitMilliSeconds = levels[currentLevel - 1].MaxTimeInMilliSecondsBetweenLaserFires;
                milleniumFalcon.NewLevel(); // this makes the numbers of lives go back up
                playerLivesLeft = milleniumFalcon.PlayerLivesOrEnemyHitPoints;
            } // end if
            else if (enemyBosses.Count > 0 && enemyFighters.Count <= 0)
            {
                enemyFighters = levels[currentLevel - 1].ResetFighters(); // reset fighters if they are all gone and there is a boss
            } // end else if
            else if (enemyBosses.Count <= 0 && enemyFighters.Count <= 0 && enemyLasers.Count <= 0 && currentLevel >= NUMBER_OF_LEVELS && milleniumFalcon.PlayerLivesOrEnemyHitPoints > 0)
            {
                gameOver = true; // the game is over...
                gameWon = true; // and the game is won!!!
            } // end else if
        } // end method updateLevel

        // Check to see if an arrow key is currently pressed. Take appropriate action to move ship
        private void movePlayerSpaceShip()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes);

            if ((keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left) || gamePadState.IsButtonDown(Buttons.DPadLeft) || gamePadState.ThumbSticks.Left.X < 0 || gamePadState.ThumbSticks.Right.X < 0) && !(keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right) || gamePadState.IsButtonDown(Buttons.DPadRight) || gamePadState.ThumbSticks.Left.X > 0 || gamePadState.ThumbSticks.Right.X > 0) && milleniumFalcon.Position.X > (PANEL_WIDTH + 10))
            {
                milleniumFalcon.Move(SpaceShip.Direction.Left);
            } // end if
            else if ((keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right) || gamePadState.IsButtonDown(Buttons.DPadRight) || gamePadState.ThumbSticks.Left.X > 0 || gamePadState.ThumbSticks.Right.X > 0) && !(keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left) || gamePadState.IsButtonDown(Buttons.DPadLeft) || gamePadState.ThumbSticks.Left.X < 0 || gamePadState.ThumbSticks.Right.X < 0) && milleniumFalcon.Position.X < (GraphicsDevice.Viewport.Bounds.Width - PlayerSpaceShip.Texture.Width - 10))
            {
                milleniumFalcon.Move(SpaceShip.Direction.Right);
            } // end else if
        } // end method movePlayerSpaceShip

        private void moveEnemySpaceShips()
        {
            foreach (EnemyFighter enemyFighter in enemyFighters)
            {
                enemyFighter.Move();
            } // end foreach loop

            foreach (EnemyBoss enemyBoss in enemyBosses)
            {
                enemyBoss.Move();
            } // end foreach loop
        } // end method moveEnemySpaceShips

        // Check to see if the space key is down. If so, set flag and position so that the Draw will Draw the missle
        private void fireLaser()
        {
            KeyboardState newKeyboardState = Keyboard.GetState();
            GamePadState newGamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes);

            // was the space key just pressed and not held down?
            if ((newKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) || newGamePadState.IsButtonDown(Buttons.A) || newGamePadState.IsButtonDown(Buttons.B) || newGamePadState.IsButtonDown(Buttons.X) || newGamePadState.IsButtonDown(Buttons.Y) || newGamePadState.IsButtonDown(Buttons.LeftShoulder) || newGamePadState.IsButtonDown(Buttons.LeftTrigger) || newGamePadState.IsButtonDown(Buttons.RightShoulder) || newGamePadState.IsButtonDown(Buttons.RightTrigger)) && !(oldKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) || oldGamePadState.IsButtonDown(Buttons.A) || oldGamePadState.IsButtonDown(Buttons.B) || oldGamePadState.IsButtonDown(Buttons.X) || oldGamePadState.IsButtonDown(Buttons.Y) || oldGamePadState.IsButtonDown(Buttons.LeftShoulder) || oldGamePadState.IsButtonDown(Buttons.LeftTrigger) || oldGamePadState.IsButtonDown(Buttons.RightShoulder) || oldGamePadState.IsButtonDown(Buttons.RightTrigger)) && !autoFire)
            {
                playerLasers.Add(new PlayerLaser(new Vector2((milleniumFalcon.GunPosition.X - PlayerSpaceShip.Texture.Width / 7), milleniumFalcon.GunPosition.Y)));
                playerLasers.Add(new PlayerLaser(new Vector2((milleniumFalcon.GunPosition.X + PlayerSpaceShip.Texture.Width / 14), milleniumFalcon.GunPosition.Y)));
            } // end if
            else if (autoFire)
            {
                playerLasers.Add(new PlayerLaser(new Vector2((milleniumFalcon.GunPosition.X - PlayerSpaceShip.Texture.Width / 7), milleniumFalcon.GunPosition.Y)));
                playerLasers.Add(new PlayerLaser(new Vector2((milleniumFalcon.GunPosition.X + PlayerSpaceShip.Texture.Width / 14), milleniumFalcon.GunPosition.Y)));
            } // end else if

            oldKeyboardState = newKeyboardState; // update saved state.
            oldGamePadState = newGamePadState; // update saved state.
        } // end method fireLaser

        private void fireEnemyLaser(GameTime gameTime)
        {
            TimeSpan randomWaitTime = new TimeSpan(0, 0, 0, 0, randomMilliSecondsWaitTime);

            if ((gameTime.TotalGameTime - oldGameTime) >= randomWaitTime)
            {
                oldGameTime = gameTime.TotalGameTime;
                Random randomEnemySelector = new Random();
                int randomEnemy = randomEnemySelector.Next(0, enemyFighters.Count);

                if (enemyFighters.Count > 0) // make sure there really are enemies there
                {
                    enemyLasers.Add(enemyFighters[randomEnemy].MakeNewEnemyLaser());
                    randomMilliSecondsWaitTime = rnd.Next(minWaitMilliSeconds, maxWaitMilliSeconds + 1);
                } // end if
            } // end if

            foreach (EnemyBoss enemyBoss in enemyBosses)
            {
                if (enemyBoss.Attack)
                {
                    enemyLasers.Add(enemyBoss.MakeNewEnemyLaser());
                } // end if
            } // end foreach loop
        } // end method fireEnemyLaser

        // If the missle is alive, move it up a few clicks until it goes off screen
        private void moveLasers()
        {
            foreach (PlayerLaser playerLaser in playerLasers) // check to see is player lasers went past the end of the screen
            {
                if (playerLaser.Position.Y >= (0 - PlayerLaser.Texture.Height) && playerLaser.KeepDrawing)
                {
                    playerLaser.Move();
                } // end if
                else
                {
                    playerLaser.KeepDrawing = false; // if they did, mark them for removal
                } // end else
            } // end foreach loop

            foreach (EnemyLaser enemyLaser in enemyLasers) // check to see if enemy lasers went past the end of the screen
            {
                if (enemyLaser.Position.Y <= (viewPort.Height + TieLaser.Texture.Height) && enemyLaser.KeepDrawing)
                {
                    enemyLaser.Move();
                } // end if
                else
                {
                    enemyLaser.KeepDrawing = false; // if they did, mark them for removal
                } // end else
            } // end foreach loop
        } // end method moveMissle

        private void detectCollisions()
        {
            bool enemiesInvaded = false;

            foreach (PlayerLaser playerLaser in playerLasers) // check to see if any enemies were hit by a player laser
            {
                foreach (EnemyFighter enemyFighter in enemyFighters) // check to see if any enemy fighters were hit by a player laser
                {
                    if (SpaceShip.CollisionBetween(enemyFighter.Position, enemyFighter.FindTexture(), playerLaser.Position, PlayerLaser.TextureData))
                    {
                        enemyFighter.ImHit();
                        playerLaser.KeepDrawing = false;
                    } // end if
                } // end foreach loop

                foreach (EnemyBoss enemyBoss in enemyBosses) // check to see if any enemy bosses were hit by a player laser
                {
                    if (SpaceShip.CollisionBetween(enemyBoss.Position, enemyBoss.FindTexture(), playerLaser.Position, PlayerLaser.TextureData))
                    {
                        enemyBoss.ImHit();
                        playerLaser.KeepDrawing = false;
                    } // end if
                } // end foreach loop
            } // end foreach loop

            foreach (EnemyLaser enemyLaser in enemyLasers) // check to see if the player was hit by an enemy laser
            {
                if (SpaceShip.CollisionBetween(milleniumFalcon.Position, PlayerSpaceShip.TextureData, enemyLaser.Position, enemyLaser.FindTexture()))
                {
                    if (!invincibleShielding) // if the user does not have invincible shielding turned on
                    {
                        resetLevel();
                    } // end if
                    else
                    {
                        enemyLaser.KeepDrawing = false;
                    } // end else
                    //resetLevel();
                } // end if
            } // for foreach loop

            foreach (EnemyFighter enemyFighter in enemyFighters) // check to see if the enemies invaded
            {
                if (((enemyFighter.Position.Y + enemyFighter.FindTexture().Texture.Height) >= (viewPort.Height - PlayerSpaceShip.Texture.Height) || SpaceShip.CollisionBetween(milleniumFalcon.Position, PlayerSpaceShip.TextureData, enemyFighter.Position, enemyFighter.FindTexture())) && !enemiesInvaded)
                {
                    enemiesInvaded = true;
                } // end if
            } // end foreach loop

            if (enemiesInvaded) // this way if 10 enemies invade at once you do not loose 10 lives, you just loose one life because the enemies invaded only once
            {
                resetLevel();
            } // end if
        } // end method detectCollisions

        private void resetLevel()
        {
            milleniumFalcon.ImHit();
            enemiesDestroyed = enemiesDestroyedAtLvlBegin;
            playerLivesLeft = milleniumFalcon.PlayerLivesOrEnemyHitPoints;

            if (milleniumFalcon.PlayerLivesOrEnemyHitPoints <= 0)
            {
                gameOver = true; // the game is over...
            } // end if
            else
            {
                //enemyFighters = levels[currentLevel - 1].StartLevel(out enemyBosses); // reset all enemies in the level
                enemyFighters = levels[currentLevel - 1].ResetFighters(); // reset all enemies except the boss

                foreach (EnemyLaser enemyLaser in enemyLasers) // stop drawing all enemy lasers
                {
                    enemyLaser.KeepDrawing = false;
                } // end foreach loop

                foreach (PlayerLaser playerLaser in playerLasers) // stop drawing all player lasers
                {
                    playerLaser.KeepDrawing = false;
                } // end foreach loop

                milleniumFalcon.Reset(); // reset the player
            } // end else
        } // end mothod resetLevel

        private void removeSpaceDebris()
        {
            int previousCount;
            playerLasers.RemoveAll(playerLaser => !playerLaser.KeepDrawing); // this should remove all of the lasers that are either past the screen or collided
            previousCount = enemyFighters.Count;
            enemyFighters.RemoveAll(enemyFighter => !enemyFighter.KeepDrawing); // this should remove all of the enemy fighters that are blown away
            enemiesDestroyed += previousCount - enemyFighters.Count; // update number of enemies destroyed
            previousCount = enemyBosses.Count;
            enemyBosses.RemoveAll(enemyBoss => !enemyBoss.KeepDrawing); // this should remove all of the enemy bosses that are blown away
            enemiesDestroyed += previousCount - enemyBosses.Count; // update number of enemies destroyed
            enemyLasers.RemoveAll(enemyLaser => !enemyLaser.KeepDrawing); // this should remove all of the enemy lasers that are either past the screen or collided
        } // end method deleteUnusedLasers

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // spriteBatch is an object that allows us to draw everything
            // on screen (it contains the Draw functions).
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            // draw the background
            spriteBatch.Draw(backGroundTexture, viewPort, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);

            // draw the ship over background
            milleniumFalcon.Draw(spriteBatch);

            // draw the demo enemy ship over background
            //tieFighter.Draw(spriteBatch);
            foreach (EnemyFighter enemyFighter in enemyFighters)
            {
                enemyFighter.Draw(spriteBatch);
            } // end foreach loop

            foreach (EnemyBoss enemyBoss in enemyBosses)
            {
                enemyBoss.Draw(spriteBatch);
            } // end foreach loop

            // draw the lasers
            foreach (PlayerLaser playerLaser in playerLasers)
            {
                playerLaser.Draw(spriteBatch);
            } // end foreach loop

            foreach (EnemyLaser enemyLaser in enemyLasers)
            {
                enemyLaser.Draw(spriteBatch);
            } // end foreach loop

            // This tells the graphics pipeline that we're done drawing this frame,
            // and asks it to push the pixels to the graphics frame buffer.
            spriteBatch.End();

            base.Draw(gameTime);
        } // end override method Draw

        private void loadCurrentSettings()
        {
            // try to load settings from a text file
            string fileName = "CurrentSettings.txt";
            try
            {
                // create FileStream to obtain read access to file
                FileStream input = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                //set file from where data is read
                StreamReader fileReader = new StreamReader(input);

                // goo to the beginning of the file
                input.Seek(0, SeekOrigin.Begin);

                string[] inputFields; // stores individual pieces of data

                string inputString = fileReader.ReadLine(); // just read one line

                inputFields = inputString.Split(','); // parse input

                difficulty = Convert.ToInt32(inputFields[0]); // get the difficulty
                cheat = Convert.ToBoolean(inputFields[1]); // get whether or not cheating is enabled
                autoFire = Convert.ToBoolean(inputFields[2]); // get whether or not auto fire is enabled
                invincibleShielding = Convert.ToBoolean(inputFields[3]); // get whether or not invincible shielding is enabled

                fileReader.Close(); // close the stream reader
                input.Close(); // close the file stream

                switch (difficulty) // determine how many lives the player has based on difficulty
                {
                    case 0:
                        playerLives = 10;
                        break;
                    case 1:
                        playerLives = 6;
                        break;
                    case 2:
                        playerLives = 3;
                        break;
                    case 3:
                        playerLives = 1;
                        break;
                    default:
                        playerLives = 10;
                        break;
                } // end switch

                playerLivesLeft = playerLives;
            } // end try
            catch (Exception)
            { // do nothing if read from file fails, just silently ignore it
            } //end catch
        } // end method loadCurrentSettings

        private void prepareToStartGame()
        {
            gameOver = false;
            gameWon = false;
            playerLives = 10;
            autoFire = false;
            invincibleShielding = false;

            loadCurrentSettings(); // load saved settings if they exist

            playerLivesLeft = playerLives;
            gameStarted = false;
            beginningOfNewGame = true;
            gamePaused = false;
            gameQuit = false;
            enemiesDestroyed = 0;
            enemiesDestroyedAtLvlBegin = 0;

            currentLevel = 0;
            enemyFighters = new List<EnemyFighter>();
            enemyBosses = new List<EnemyBoss>();
            enemyLasers = new List<EnemyLaser>();

            oldGameTime = new TimeSpan(0, 0, 0);
            oldKeyboardState = Keyboard.GetState();

            milleniumFalcon = new PlayerSpaceShip(new Vector2(((viewPort.Width - PANEL_WIDTH) / 2) + PANEL_WIDTH, viewPort.Height - 10), playerLives);
            milleniumFalcon.SetResetPositionAndReset = new Vector2((milleniumFalcon.Position.X - PlayerSpaceShip.Texture.Width / 2), (milleniumFalcon.Position.Y - PlayerSpaceShip.Texture.Height));
            playerLasers = new List<PlayerLaser>();
        } // end method prepareToStartGame
    } // end class StarWarsSpaceInvaders
} // end namesapce Star_Wars_Space_Invaders

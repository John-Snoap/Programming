using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Start_Settings
{
    public partial class Form1 : Form
    {
        // private data
        private int level;
        private bool cheat;
        private bool autoFire;
        private bool invincibleShielding;
        private bool gamePaused;

        public Form1()
        {
            InitializeComponent();

            level = 0;
            cheat = false;
            autoFire = false;
            invincibleShielding = false;
            gamePaused = false;
            lblLivesLeft.Text = "one";
            lblShipsDestroyed.Text = "Fifty-Two";


            resetGame();
        }


        //*******************
        // Event Handlers   *
        //*******************

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Start game


            // Enable Pause and Quit Buttons
            // Disable Start and Settings Buttons
            btnStart.Enabled = false;
            btnStart.Visible = false;

            btnPause.Enabled = true;
            btnPause.Visible = true;

            btnQuit.Enabled = true;
            btnQuit.Visible = true;

            btnSettings.Enabled = false;
            btnSettings.Visible = false;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            loadCurrentSettings();

            FormSettings settingsBox;
            settingsBox = new FormSettings(level, cheat, autoFire, invincibleShielding);

            if (settingsBox.ShowDialog() == DialogResult.OK)
            {
                // get everything so it is saved
	            level = settingsBox.cmbLevel.SelectedIndex;
                cheat = settingsBox.radBtnCheating.Checked;
                autoFire = settingsBox.chckBxAutoFire.Checked;
                invincibleShielding = settingsBox.chckBxInvincibleShielding.Checked;

                saveCurrentSettings();

                // Used for checking
            }

        }


        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!gamePaused)
            {
                btnPause.Text = "Resume";
                gamePaused = true;
            }
            else
            {
                btnPause.Text = "Pause";
                gamePaused = false;
            }

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            resetGame();
        }


        //*******************
        // Private Methods  *
        //*******************

        //***************************************
        // Method for loading current settings  *
        //***************************************

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

                level = Convert.ToInt32(inputFields[0]); // get the level
                cheat = Convert.ToBoolean(inputFields[1]); // get whether or not cheating is enabled
                autoFire = Convert.ToBoolean(inputFields[2]); // get whether or not auto fire is enabled
                invincibleShielding = Convert.ToBoolean(inputFields[3]); // get whether or not invincible shielding is enabled

                fileReader.Close(); // close the stream reader
                input.Close(); // close the file stream
            } // end try
            catch (Exception)
            { // do nothing if read from file fails, just silently ignore it
            } //end catch
        } // end method loadCurrentSettings


        //**************************************
        // Method for saving current settings  *
        //**************************************
        private void saveCurrentSettings()
        {
            string fileName = "CurrentSettings.txt";
            string settings = level.ToString() + ", " + cheat.ToString() + ", " + autoFire.ToString() + ", " + invincibleShielding.ToString();

            try
            {
                // open file with write access
                FileStream output = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);

                // sets file to where data is written
                StreamWriter fileWriter = new StreamWriter(output);

                fileWriter.WriteLine(settings); // write to the file

                fileWriter.Flush(); // force the program to actually write the data
                fileWriter.Close(); // close the stream writer
                output.Close(); // close the file stream
            } // end try
            catch
            { // do nothing, just silently ignore it
            } // end catch
        } // end method saveCurrentSettings


        private void resetGame()
        {
            // Enable and Disable appropriate buttons
            btnStart.Enabled = true;
            btnStart.Visible = true;

            btnPause.Enabled = false;
            btnPause.Visible = false;
            btnPause.Text = "Pause";

            btnQuit.Enabled = false;
            btnQuit.Visible = false;

            btnSettings.Enabled = true;
            btnSettings.Visible = true;

            //Call method to start new game


        }


    } // end class Form1
} // end namespace Start_Settings

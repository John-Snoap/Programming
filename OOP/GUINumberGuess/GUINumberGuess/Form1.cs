/* John Snoap
 * Assignment 4
 * GUI Number Guess
 * Object Oriented Programming
 * September 23, 2013
 * URL:  https://drive.google.com/a/oc.edu/folderview?id=0B2KfHJWwUz_sTUFMdEV5eHRLYVU&usp=sharing
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NumberGame;

namespace GUINumberGuess
{
    public partial class Form1 : Form
    {
        private Game trialGame = new Game(); // create a new instance of the number game
        private int distanceFromCorrectNumber; // a variable to hold how far away the player is from the correct number
        private int previousDistanceFromCorrectNumber; // a variable to hold the previous distance from the correct number

        public Form1()
        {
            InitializeComponent(); // auto generated code
            startOfGameMessage(); // start the game
        } // end method Form1

        private void startOfGameMessage()
        {
            lblFirstHint.Text = "I am thinking of an integer between 1 and 10,000.\nCan you guess my number?";
            lblSecondHint.Text = "";
            this.BackColor = Color.FromArgb(246, 234, 219); // This should make an off-white color
            btnPlayAgain.Enabled = false; // disable play again button
            btnEnterGuess.Enabled = true; // enable enter guess button
            mskTxtUserGuess.Enabled = true; // enable txt input
            this.AcceptButton = btnEnterGuess; // enable the user to press enter if they do not want to click the button
            mskTxtUserGuess.Text = "";
            focusOnUserGuess();
        } // end method startOfGameMessage

        private void focusOnUserGuess()
        {
            mskTxtUserGuess.Focus(); // make the cursor focus on the text box
            if (mskTxtUserGuess.Text.Length > 0)
            {
                mskTxtUserGuess.SelectionStart = 0;
                mskTxtUserGuess.SelectionLength = mskTxtUserGuess.TextLength;
            }
        } // end method focusOnUserGuess

        private void btnEnterGuess_Click(object sender, EventArgs e)
        {
            lblFirstHint.Text = "";
            lblSecondHint.Text = "";
            if (mskTxtUserGuess.Text.Length > 0) // make sure the user enters a number
            {
                distanceFromCorrectNumber = trialGame.GuessMyNumber(Convert.ToInt32(mskTxtUserGuess.Text));

                if (distanceFromCorrectNumber != 0) // the user did not win yet
                {
                    if (trialGame.GetGuessCount == 1) // it is the first guess
                    {
                        lblFirstHint.Text = "You just entered your first guess!";
                        previousDistanceFromCorrectNumber = distanceFromCorrectNumber;
                    }
                    else // it is greater than the first guess
                    {
                        if (Math.Abs(distanceFromCorrectNumber) > Math.Abs(previousDistanceFromCorrectNumber))
                        {
                            lblFirstHint.Text = "You're getting colder!";
                            this.BackColor = Color.FromArgb(135, 206, 250); // This should make a light sky blue color
                        } // end if
                        else if (Math.Abs(distanceFromCorrectNumber) < Math.Abs(previousDistanceFromCorrectNumber))
                        {
                            lblFirstHint.Text = "You're getting warmer!";
                            this.BackColor = Color.FromArgb(255, 199, 206); // This should make a pinkish color
                        } // end else if
                        else
                        {
                            // this CAN be a VERY useful hint
                            lblFirstHint.Text = "You are not closer nor are you farther away\nthan you were before!";
                            this.BackColor = Color.FromArgb(255, 235, 156); // This should make a neutral color
                        } // end else
                    } // end else

                    if (distanceFromCorrectNumber > 0)
                        lblSecondHint.Text = "Your guess was too high!  Guess again.";
                    else
                        lblSecondHint.Text = "Your guess was too low!  Guess again.";

                    previousDistanceFromCorrectNumber = distanceFromCorrectNumber; // move distance to previous distance
                    focusOnUserGuess();
                } // end if
                else // the user won!
                {
                    lblFirstHint.Text = "Congratulations!!!\nYou guessed my number in " + trialGame.GetGuessCount + " tries!!!\nYou Win!!!";
                    this.BackColor = Color.FromArgb(246, 234, 219); // This should make an off-white color
                    btnEnterGuess.Enabled = false; // disable enter guess button
                    mskTxtUserGuess.Enabled = false; // disable txt input field
                    btnPlayAgain.Enabled = true; // enable play again button
                    this.AcceptButton = btnPlayAgain; // enable the user to press enter if they do not want to click the button
                } // end else
            } // end if
            else
            {
                lblFirstHint.Text = "You have to enter a number, silly!";
                this.BackColor = Color.FromArgb(246, 234, 219); // This should make an off-white color
                mskTxtUserGuess.Text = "";
                focusOnUserGuess();
            }
        } // end method btnEnterGuess_Click

        private void btnPlayAgain_Click(object sender, EventArgs e)
        {
            trialGame.NumberGameReset(); // reset the game
            startOfGameMessage(); // start the game again
        } // end method btnPlayAgain_Click
    } // end partial class Form1
} // end namespace GUINumberGuess

/* John Snoap
 * Joshua Mullen
 * Assignment 7
 * Connect Four
 * Graphical User Interface
 * Object Oriented Programming
 * November 20, 2013
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using ConnectFour;
using Painter;

namespace ConnectFourGUI
{
    public partial class CnnctFourFrm : Form
    {
        // private data
        private bool redsTurn = true;
        private ConnectFourGameState connectFourGameState = new ConnectFourGameState();
        private ConnectFourGameState.GameState gameState; // enum to keep track of win, draw, keepPlaying
        private const float SIZE = 50; // size of the paint, float so the center of paint can be more precise
        private List<Circle> circles = new List<Circle>(); // the list of paint circles
        private float columnWidth;
        private float rowHeight;

        // constructor
        public CnnctFourFrm()
        {
            InitializeComponent();

            // Red goes first so turn off blkLbl's message
            // and display message that it is Red's Turn
            blkLbl.Text = "";
            redLbl.Text = "Red's Turn";

            // set some size variables
            columnWidth = (gmeBrdPnl.Size.Width / 7.0f);
            rowHeight = (gmeBrdPnl.Size.Height / 6.0f);
        } // end constructor

        // event handler for clicking on the game board and placing a checker in the gird
        private void GmeBrdPnl_MouseDown(object sender, MouseEventArgs e)
        {
            float xCoord;
            float yCoord;
            int column;
            int row;

            if (gameState == ConnectFourGameState.GameState.notWon)
            {
                // size of panel = 476, 396
                // 68 = (476 / 7)
                // 34 = (68 / 2)
                column = (int)(e.X / columnWidth); // we want this to be an integer!
                xCoord = (column * (columnWidth) + (columnWidth / 2.0f));

                gameState = connectFourGameState.playTurn(redsTurn, column, out row);

                if (row < 7)
                {
                    // size of panel = 476, 396
                    // 66 = (396 / 6)
                    // 33 = (66 / 2)
                    rowHeight = (gmeBrdPnl.Size.Height / 6.0f);
                    yCoord = (gmeBrdPnl.Size.Height - (row * rowHeight + (rowHeight / 2.0f)));

                    if (redsTurn)
                    {
                        // since it is red's turn the checker will be red
                        drawGamePiece(xCoord, yCoord, Color.Red);
                    }
                    else
                    {
                        // since it is not red's turn the checker will be black
                        drawGamePiece(xCoord, yCoord, Color.Black);
                    }

                    displayGameState();
                } // end if
            } // end if
        } // end event handler GmeBrdPnl_MouseDown

        private void displayGameState()
        {
            if (gameState == ConnectFourGameState.GameState.won && redsTurn)
            {
                blkLbl.Text = "";
                redLbl.Text = "Red Player Won!";
            } // end if
            else if (gameState == ConnectFourGameState.GameState.won && !redsTurn)
            {
                redLbl.Text = "";
                blkLbl.Text = "Black Player Won!";
            } // end else if
            else if (gameState == ConnectFourGameState.GameState.draw)
            {
                redLbl.Text = "";
                blkLbl.Text = "It's a draw!";
            } // end else if

            if (redsTurn && gameState == ConnectFourGameState.GameState.notWon)
            {
                // Change it to black's turn after red player clicks
                redsTurn = false;

                // Display message indicating that it is black's turn
                // and turn off rdLbl's message
                redLbl.Text = "";
                blkLbl.Text = "Black's Turn";
            } // end if
            else if (!redsTurn && gameState == ConnectFourGameState.GameState.notWon)
            {
                // Change it to red's turn after black player clicks
                redsTurn = true;

                // Display message indicating that it is red's turn
                // and turn off blkLbl's message
                blkLbl.Text = "";
                redLbl.Text = "Red's Turn";
            } // end elseif
        } // end method displayGameState

        private void drawGamePiece(float xCoord, float yCoord, Color colorOfChecker)
        {
            // first, create a new circle to draw, since redsTurn is true make the checker's color red
            Circle circle = new Circle(SIZE, (xCoord - (SIZE / 2.0f)), (yCoord - (SIZE / 2.0f)), colorOfChecker);

            // then, add the new circle to the list for saving
            circles.Add(circle);

            // finally, draw the new circle on the screen
            using (Graphics graphics = gmeBrdPnl.CreateGraphics())
            {
                circle.draw(graphics);
            } // end using; calls graphics.Dispose()
        } // end method drawGraphics

        // redraws all of the circles
        private void redrawCircles(Graphics graphics)
        {
            foreach (Circle circle in circles)
            {
                circle.draw(graphics);
            } // end foreach loop
        } // end method redrawCircles

        // should redraw all the circles so the screen can be minimized/moved around
        private void gmeBrdPnl_Paint(object sender, PaintEventArgs e)
        {
            redrawCircles(e.Graphics);
        }

        private void saveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create and show dialog box enabling user to save file
            DialogResult result; // result of SaveFileDialog
            string fileName; // name of file containing data

            using (SaveFileDialog fileChooser = new SaveFileDialog())
            {
                fileChooser.CheckFileExists = false; // let user create file
                fileChooser.FileName = ".txt";
                fileChooser.Filter = "Text File | *.txt";
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; // name of file to save data
            } // end using

            // ensure that user clicked "OK"
            if (result == DialogResult.OK)
            {
                // show error if user specified invalid file
                if (fileName == string.Empty)
                {
                    MessageBox.Show("Invalid File Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } // end if
                else
                {
                    // save file via FileStream if user specified valid file
                    try
                    {
                        // open file with write access
                        FileStream output = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);

                        // sets file to where data is written
                        StreamWriter fileWriter = new StreamWriter(output);

                        foreach (Circle circle in circles)
                        {
                            fileWriter.WriteLine(circle.ToString());
                        } // end foreach loop

                        fileWriter.Flush(); // force the program to actually write the data
                        fileWriter.Close(); // force the program to close the writer becaue we are done with it now
                    } // end try

                    // handle exception if there is a problem opening the file
                    catch (IOException)
                    {
                        // notify user if file does not exist
                        MessageBox.Show("Error opening file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } // end catch
                } // end else
            } // end if
        } // end event handler saveasToolStripMenuItem_Click

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create and show dialog box enabling user to open file
            DialogResult result; // result of OpenFileDialog
            string fileName; // name of File containing data

            using (OpenFileDialog fileChooser = new OpenFileDialog())
            {
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; // get specified name
            } // end using

            // ensure that suer clicked "OK"
            if (result == DialogResult.OK)
            {
                //ClearTextBox();

                // show error if user specified invalid file
                if (fileName == string.Empty)
                {
                    MessageBox.Show("Invalid File Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } // end if
                else
                {
                    try
                    {
                        // create temp connect four game state
                        ConnectFourGameState tempCF = new ConnectFourGameState();
                        ConnectFourGameState.GameState tempGS = ConnectFourGameState.GameState.notWon;
                        redsTurn = false; // just in case there is nothing in the file

                        // create temp cirles
                        List<Circle> tempCircles = new List<Circle>(); // the list of temporary paint circles

                        // create FileStream to obtain read acess to file
                        FileStream input = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                        // set file from where data is read
                        StreamReader fileReader = new StreamReader(input);

                        // go back to the beginning of the file
                        input.Seek(0, SeekOrigin.Begin);

                        // traverse file until end of file
                        while (!fileReader.EndOfStream)
                        {
                            string[] inputFields; // stores individual pieces of data
                            float size; // store each circle's size
                            float x; // store each circle's x position
                            float y; // store each circle's y position
                            int color; // store each circle's colorARGB
                            int column; // store the column of the game piece
                            int row; // store the row of the game piece

                            // get next circle available in file
                            string inputString = fileReader.ReadLine();

                            inputFields = inputString.Split(','); // parse input

                            size = Convert.ToInt32(inputFields[0]); // get the size
                            x = Convert.ToInt32(inputFields[1]); // get the x coord
                            y = Convert.ToInt32(inputFields[2]); // get the y coord
                            color = Convert.ToInt32(inputFields[3]); // get the color

                            column = (int)((x + SIZE/2.0f - columnWidth/2.0f) / columnWidth); // get the correct column for the game

                            if (Color.Equals(Color.FromArgb(color), Color.FromArgb(Color.Red.ToArgb())))
                            {
                                redsTurn = true;
                                tempGS = tempCF.playTurn(redsTurn, column, out row);
                            } // end if
                            else if (Color.Equals(Color.FromArgb(color), Color.FromArgb(Color.Black.ToArgb())))
                            {
                                redsTurn = false;
                                tempGS = tempCF.playTurn(redsTurn, column, out row);
                            } // end else if

                            // create circle from input and add it to the temporary list
                            tempCircles.Add(new Circle(size, x, y, color));// = new Circle(size, x, y, color);

                        } // end EOF loop

                        // forget about any previous game
                        connectFourGameState.resetGame();
                        connectFourGameState = tempCF;
                        gameState = tempGS;

                        // forget about any circles already in the painting panel
                        circles.Clear(); // forget about old circles
                        circles = tempCircles;
                        gmeBrdPnl.Refresh(); // clear the game board panel

                        // display who's turn it is
                        displayGameState();
                    } // end try
                    catch (IOException)
                    {
                        MessageBox.Show("Error reading from file", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } // end catch
                    catch (FormatException)
                    {
                        MessageBox.Show("You opened the wrong type of file!", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } //end catch
                    catch (IndexOutOfRangeException)
                    {
                        MessageBox.Show("Your file has been corrupted!", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } //end catch
                    catch (Exception)
                    {
                        MessageBox.Show("Some other error has occured!", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } //end catch
                } // end else
            } // end if
        } // end event handler openToolStripMenuItem_Click

        private void StartOvrBtn_Click(object sender, EventArgs e)
        {
            DialogResult result;

            // Display a message to ask the user if
            // they really want to start over or not
            result = MessageBox.Show("Are you sure you want to start a new game?", "Start Over",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If player clicked OK then clear
            // the graphics on the game board
            if (result == DialogResult.Yes)
            {
               connectFourGameState.resetGame();
               circles.Clear(); // forget about old circles
               gmeBrdPnl.Refresh(); // clear the game board panel
               redsTurn = false; // this will make displayGameState have red begin
               gameState = ConnectFourGameState.GameState.notWon; // make the game start at not won

               // display who's turn it is
               displayGameState();
            }
        } // end event handler StartOvrBtn_Click
    } // end class CnnctFourFrm
} // end namespace ConnectFourGameGUI

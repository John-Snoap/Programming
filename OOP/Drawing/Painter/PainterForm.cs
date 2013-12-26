/* John Snoap
 * Daniel Griffin
 * Joshua Mullen
 * Assignment 6
 * try catch practice
 * Drawing
 * Object Oriented Programming
 * October 23, 2013
 * URL:  https://drive.google.com/a/oc.edu/folderview?id=0B2KfHJWwUz_sRHRiTHRIZTIxQkk&usp=sharing
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Painter
{
    // creates a Form that is a drawing surface
    public partial class PainterForm : Form
    {
        // private data
        private bool shouldPaint = false; // determines whether to paint
        private Color color; // color of the paint
        private float size; // size of the paint, float so the center of paint can be more precise
        private List<Circle> circles = new List<Circle>(); // the list of paint circles

        // default constructor
        public PainterForm()
        {
            InitializeComponent();

            radioButtonBlack.Checked = true;
            radioButtonSmall.Checked = true;
        } // end constructor

        // draw circle
        private void drawCircle(object sender, MouseEventArgs e)
        {
            // first, create a new circle to draw
            Circle circle = new Circle(size, (e.X - (size / 2.0f)), (e.Y - (size / 2.0f)), color);

            // then, add the new circle to the list for saving
            circles.Add(circle);

            // finally, draw the new circle on the screen
            using (Graphics graphics = painterPanel.CreateGraphics())
            {
                circle.draw(graphics);
            } // end using; calls graphics.Dispose()
        } // end method drawCircle

        // redraws all of the circles
        private void redrawCircles(Graphics graphics)
        {
            foreach (Circle circle in circles)
            {
                //using (Graphics graphics = painterPanel.CreateGraphics())
                //{
                    circle.draw(graphics);
                //} // end using; calls graphics.Dispose()
            } // end foreach loop
        } // end method redrawCircles

        // should redraw all the circles so the screen can be minimized
        private void painterPanel_Paint(object sender, PaintEventArgs e)
        {
            redrawCircles(e.Graphics);
        } // end event handler painterPanel_Paint

        // should paint when mouse button is pressed down
        private void painterPanel_MouseDown(object sender, MouseEventArgs e)
        {
            shouldPaint = true; // indicate that user is dragging the mouse
            drawCircle(sender, e); // draw a circle
        } // end event handler painterPanel_MouseDown

        // stop painting when mouse button is released
        private void painterPanel_MouseUp(object sender, MouseEventArgs e)
        {
            shouldPaint = false; // indicate that user released the mouse button
        } // end event handler painterPanel_MouseUp

        // draw circle whenever mouse moves with its button held down
        private void painterPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (shouldPaint) // check if mouse button is being pressed
            {
                drawCircle(sender, e); // draw a circle where the mouse pointer is present
            } // end if
        } // end event handler painterPanel_MouseMove

        // change the color of a new circle to black
        private void radioButtonBlack_CheckedChanged(object sender, EventArgs e)
        {
            color = Color.Black;
        } // end event handler radioButtonBlack_CheckedChanged

        // change the color of a new circle to red
        private void radioButtonRed_CheckedChanged(object sender, EventArgs e)
        {
            color = Color.Red;
        } // end event handler radioButtonRed_CheckedChanged

        // change the color of a new circle to blue
        private void radioButtonBlue_CheckedChanged(object sender, EventArgs e)
        {
            color = Color.Blue;
        } // end event handler radioButtonBlue_CheckedChanged

        // change the color of a new circle to green
        private void radioButtonGreen_CheckedChanged(object sender, EventArgs e)
        {
            color = Color.Green;
        } // end event handler radioButtonGreen_CheckedChanged

        // change the size of a new circle to small
        private void radioButtonSmall_CheckedChanged(object sender, EventArgs e)
        {
            size = 4.0f;
        } // end event handler radioButtonSmall_CheckedChanged

        // change the size of a new circle to medium
        private void radioButtonMedium_CheckedChanged(object sender, EventArgs e)
        {
            size = 8.0f;
        } // end event handler radioButtonMedium_CheckedChanged

        // change the size of a new circle to large
        private void radioButtonLarge_CheckedChanged(object sender, EventArgs e)
        {
            size = 16.0f;
        } // end event handler radioButtonLarge_CheckedChanged

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
                        fileWriter.Close(); // close the stream writer
                    } // end try

                    // handle exception if there is a problem saving the file
                    catch (IOException)
                    {
                        // notify user if file does not exist
                        MessageBox.Show("Error saving file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // ensure that user clicked "OK"
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
                        // create temp cirles
                        List<Circle> tempCircles = new List<Circle>(); // the list of temporary paint circles

                        // create FileStream to obtain read acess to file
                        FileStream input = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                        // set file from where data is read
                        StreamReader fileReader = new StreamReader(input);

                        // go back to the beginning of the file
                        input.Seek(0, SeekOrigin.Begin);

                        // forget about any circles already in the painting panel
                        //circles.Clear(); // forget about old circles
                        //painterPanel.Refresh(); // clear the painter panel

                        // traverse file until end of file
                        while (!fileReader.EndOfStream)
                        {
                            string[] inputFields; // stores individual pieces of data
                            //Circle circle; // store each circle as file is read
                            float size; // store each circle's size
                            float x; // store each circle's x position
                            float y; // store each circle's y position
                            int color; // store each circle's colorARGB

                            // get next circle available in file
                            string inputString = fileReader.ReadLine();

                            inputFields = inputString.Split(','); // parse input

                            size = Convert.ToInt32(inputFields[0]); // get the size
                            x = Convert.ToInt32(inputFields[1]); // get the x coord
                            y = Convert.ToInt32(inputFields[2]); // get the y coord
                            color = Convert.ToInt32(inputFields[3]); // get the color



                            // create circle from input and add it to the temporary list
                            tempCircles.Add(new Circle(size, x, y, color));// = new Circle(size, x, y, color);

                            // add circle to the linked list
                            //circles.Add(circle);
                        } // end EOF loop

                        // draw all the circles on the screen for the user to see
                        //redrawCircles();
                        // forget about any circles already in the painting panel
                        circles.Clear(); // forget about old circles
                        circles = tempCircles;
                        painterPanel.Refresh(); // clear the painter panel

                        //painterPanel.Invalidate();
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
        } // end event halder openToolStripMenuItem_Click
    } // end class PainterForm
} // end namespace Painter

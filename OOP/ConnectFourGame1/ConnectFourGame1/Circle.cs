/* John Snoap
 * Joshua Mullen
 * Assignment 7
 * Connect Four
 * Circle (Game Piece)
 * Object Oriented Programming
 * November 20, 2013
 */

using System;
using System.Drawing;
using System.Collections.Generic;

namespace Painter
{
    class Circle
    {
        // private data
        private float circleSize;
        private float xCoord;
        private float yCoord;
        private Color circleColor;

        // default constructor
        public Circle(float size, float x, float y, Color color)
        {
            circleSize = size;
            xCoord = x;
            yCoord = y;
            circleColor = color;
        } // end default constructor

        // file load constructor
        public Circle(float size, float x, float y, int colorARGB)
        {
            circleSize = size;
            xCoord = x;
            yCoord = y;
            circleColor = Color.FromArgb(colorARGB);
        } // end file load constructor

        // public methods
        public void draw(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(circleColor), xCoord, yCoord, circleSize, circleSize);
        } // end method draw

        // override method ToString
        public override string ToString()
        {
            return (circleSize.ToString() + ", " + xCoord.ToString() + ", " + yCoord.ToString() + ", " + circleColor.ToArgb());
        } // end override method ToString
    } // end class Circle
} // end namespace Painter

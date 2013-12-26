using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectFourGUI
{
   class GamePiece
   {

      // Attributes that a circle needs to know. Make them
      // private because the class is the only one that needs
      // to access them
      private int radius;
      private int xLocation;
      private int yLocation;
      private Color color;
    

      //***********************************************************************
      // GamePiece constructor                                                *
      // Initialize the four components required for a circle to draw itself  *
      //***********************************************************************

      public GamePiece(int xLocation, int yLocation, Color color)
      {

         // Make sure to use keyword "this" to differentiate between same variable names
         radius = 50;
         this.xLocation = xLocation;
         this.yLocation = yLocation;
         this.color = color;

      } //End GamePiece()


      //**************************************************
      // This method takes a circle instance's size,     *
      // location, and color information and returns     *
      // a string that can be used later for saving the  *
      // progress of a specific drawing session          *
      //**************************************************

      public override string ToString()
      {

         // String that holds a circle instance's size, location, and color
         string circleInstanceData;

         // Data will be stored in this order: size, xLocation,
         // yLocation, color

         // Convert all data to strings
         circleInstanceData = radius.ToString() + "," + xLocation.ToString() + "," +
            yLocation.ToString() + "," + color.ToArgb();

         // Return the string so that it can be used by the client code
         return circleInstanceData;

      } // End ToString()


      //*******************************************************************
      // This method draws a circle(impressive right?). Pass              *
      // DrawCircle() a graphics object so that it can draw itself        *  
      //*******************************************************************

      public void DrawCircle( Graphics circleGraphics )
      {

         // Draw a circle in DrwPnl1 wherever the user clicked
         circleGraphics.FillEllipse(new SolidBrush(color), xLocation - (radius / 2),
            yLocation - (radius / 2), radius, radius);

      } // End DrawCircle()







   }
}

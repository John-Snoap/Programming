using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    class Texture2DData
    {
        // private data
        private Color[] colorData;
        private Color[] colorDataFlippedHorizontally;
        private bool flippedHorizontally;
        private Texture2D texture;

        // properties
        public Texture2D Texture
        {
            get
            {
                return texture;
            } // end getter
            set
            {
                texture = value;
                saveColorData();
            } // end setter
        } // end property Texture

        public bool FlippedHorizontally
        {
            get
            {
                return flippedHorizontally;
            } // end getter
            set
            {
                flippedHorizontally = value;
            } // end setter
        } // end property FlippedHorizontally

        public Color[] ColorData
        {
            get
            {
                Color[] whatToReturn;

                //Color[] c = new Color[colorData.Length];

                //for (int i = 0; i < colorData.Length; i++)
                //{
                //    c[i] = colorData[i];
                //} // end for loop

                //return c;

                if (!flippedHorizontally)
                {
                    whatToReturn = colorData;//.Copy();
                } // end if
                else
                {
                    whatToReturn = colorDataFlippedHorizontally;//.Copy();
                } // end else

                return whatToReturn;
            } // end getter
        } // end property ColorData

        // constructor
        public Texture2DData(Texture2D texture2D)
        {
            texture = texture2D;
            flippedHorizontally = false;
            saveColorData();
        } // end constructor

        // public methods THIS METHOD DOES NOT WORK!!!
        public Color[] GetTextureColors(Rectangle boundingRectangle)
        {
            Color[] color = new Color[boundingRectangle.Width * boundingRectangle.Height];
            for (int x = 0; x < boundingRectangle.Width; x++)
                for (int y = 0; y < boundingRectangle.Height; y++)
                    color[x + y * boundingRectangle.Width] = colorData[x + boundingRectangle.X + (y + boundingRectangle.Y) * texture.Width];
            return color;
        } // end method GetTextureColors

        // private methods
        private void saveColorData()
        {
            colorData = new Color[texture.Width * texture.Height];
            colorDataFlippedHorizontally = new Color[colorData.Length];
            texture.GetData<Color>(colorData);

            for (int y = 0; y < texture.Height; y++)
                for (int x = 0; x < texture.Width; x++)
                    colorDataFlippedHorizontally[y * texture.Width + (texture.Width - 1 - x)] = colorData[y * texture.Width + x];
        } // end method saveColorData
    } // end class TextureData
} // end namespace Star_Wars_Space_Invaders

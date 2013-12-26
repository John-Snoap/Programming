using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    abstract class GameObject
    {
        public abstract Texture2DData FindTexture();
    } // end abstract class GameObject
} // end namespace Star_Wars_Space_Invaders

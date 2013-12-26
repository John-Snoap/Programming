using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    class BossLevelBobaFett : Level
    {
        // constructor
        public BossLevelBobaFett(Rectangle viewPort, int PANEL_WIDTH) : base()
        {
            enemyBosses.Add(new BobaFettsSlave_I(new Vector2(((viewPort.Width - PANEL_WIDTH) * 7 / 14) + 14, 10)));
        } // end constructor
    } // end class BossLevelBobaFett
} // end namespace Star_Wars_Space_Invaders

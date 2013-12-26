using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    class BossLevelDarthVader : Level
    {
        // constructor
        public BossLevelDarthVader(Rectangle viewPort, int PANEL_WIDTH) : base()
        {
            enemyFighters.Add(new TieFighter(new Vector2(((viewPort.Width - PANEL_WIDTH) * 6 / 14) + 14, 100)));
            enemyFighters.Add(new TieFighter(new Vector2(((viewPort.Width - PANEL_WIDTH) * 8 / 14) + 14, 100)));

            enemyBosses.Add(new DarthVadersTieAdvancedx1(new Vector2(((viewPort.Width - PANEL_WIDTH) * 7 / 14) + 14, 10)));

            minTimeInMilliSecondsBetweenLaserFires = 0250; // 500 milliseconds = 0.50 seconds
            maxTimeInMilliSecondsBetweenLaserFires = 0500; // 750 milliseconds = 0.75 seconds
        } // end constructor
    } // end class BossLevelDarthVader
} // end namespace Star_Wars_Space_Invaders

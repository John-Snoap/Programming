using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    class BossLevelSSS : Level
    {
        // constructor
        public BossLevelSSS(Rectangle viewPort, int PANEL_WIDTH) : base()
        {
            // the boss
            enemyBosses.Add(new ExecutorClassStarDestroyer(new Vector2(viewPort.Width, 0)));

            // the fighters
            enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 4 / 14) + 14, 250)));
            enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 6 / 14) + 14, 250)));
            enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 8 / 14) + 14, 250)));
            enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 10 / 14) + 14, 250)));

            minTimeInMilliSecondsBetweenLaserFires = 1500; // 1500 milliseconds = 1.5 seconds
            maxTimeInMilliSecondsBetweenLaserFires = 2000; // 500 milliseconds = 2 seconds
        } // end constructor
    } // end class BossLevelSSS
} // namespace Star_Wars_Space_Invaders

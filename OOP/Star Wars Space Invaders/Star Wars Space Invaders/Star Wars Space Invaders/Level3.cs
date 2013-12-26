using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    class Level3 : Level
    {
        // constructor
        public Level3(Rectangle viewPort, int PANEL_WIDTH) : base()
        {
            enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 3 / 14) + 14, 10)));
            enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 4 / 14) + 14, 10)));
            enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 5 / 14) + 14, 10)));
            enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 6 / 14) + 14, 10)));
            enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 7 / 14) + 14, 10)));
            enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 8 / 14) + 14, 10)));
            enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 9 / 14) + 14, 10)));
            enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 10 / 14) + 14, 10)));
            enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 11 / 14) + 14, 10)));
            enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 12 / 14) + 14, 10)));

            minTimeInMilliSecondsBetweenLaserFires = 0500; // 500 milliseconds = 0.50 seconds
            maxTimeInMilliSecondsBetweenLaserFires = 0750; // 750 milliseconds = 0.75 seconds
        } // end constructor
    } // end class Level3
} // end namespace Star_Wars_Space_Invaders

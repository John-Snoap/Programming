using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    class Level8 : Level
    {
        // constructor
        public Level8(Rectangle viewPort, int PANEL_WIDTH) : base()
        {
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 4 / 15) + 11, 10)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 6 / 15) + 11, 10)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 8 / 15) + 11, 10)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 10 / 15) + 11, 10)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 12 / 15) + 11, 10)));

            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 4 / 15) + 11, 100)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 6 / 15) + 11, 100)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 8 / 15) + 11, 100)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 10 / 15) + 11, 100)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 12 / 15) + 11, 100)));

            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 4 / 15) + 11, 200)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 6 / 15) + 11, 200)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 8 / 15) + 11, 200)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 10 / 15) + 11, 200)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 12 / 15) + 11, 200)));

            minTimeInMilliSecondsBetweenLaserFires = 0250; // 250 milliseconds = 0.25 seconds
            maxTimeInMilliSecondsBetweenLaserFires = 0500; // 500 milliseconds = 0.50 seconds
        } // end constructor
    } // end class Level8
} // end namespace Star_Wars_Space_Invaders

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    class Level2 : Level
    {
        // constructor
        public Level2(Rectangle viewPort, int PANEL_WIDTH) : base()
        {
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 4 / 15) + 11, 10)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 6 / 15) + 11, 10)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 8 / 15) + 11, 10)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 10 / 15) + 11, 10)));
            enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 12 / 15) + 11, 10)));

            //enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 4 / 15) + 11, 100)));
            //enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 6 / 15) + 11, 100)));
            //enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 8 / 15) + 11, 100)));
            //enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 10 / 15) + 11, 100)));
            //enemyFighters.Add(new TieBomber(new Vector2(((viewPort.Width - PANEL_WIDTH) * 12 / 15) + 11, 100)));

            minTimeInMilliSecondsBetweenLaserFires = 0750; // 700 milliseconds = 0.75 seconds
            maxTimeInMilliSecondsBetweenLaserFires = 1000; // 1000 milliseconds = 1 second
        } // end constructor
    } // end class Level2
} // end namespace Star_Wars_Space_Invaders

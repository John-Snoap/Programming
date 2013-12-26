using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    class Level10 : Level
    {
        // constructor
        public Level10(Rectangle viewPort, int PANEL_WIDTH) : base()
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

            //enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 3 / 14) + 14, 200)));
            //enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 4 / 14) + 14, 200)));
            //enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 5 / 14) + 14, 200)));
            //enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 6 / 14) + 14, 200)));
            //enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 7 / 14) + 14, 200)));
            //enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 8 / 14) + 14, 200)));
            //enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 9 / 14) + 14, 200)));
            //enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 10 / 14) + 14, 200)));
            //enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 11 / 14) + 14, 200)));
            //enemyFighters.Add(new TieInterceptor(new Vector2(((viewPort.Width - PANEL_WIDTH) * 12 / 14) + 14, 200)));

            enemyFighters.Add(new TieFighter(new Vector2(((viewPort.Width - PANEL_WIDTH) * 3 / 14) + 14, 200)));
            enemyFighters.Add(new TieFighter(new Vector2(((viewPort.Width - PANEL_WIDTH) * 4 / 14) + 14, 200)));
            enemyFighters.Add(new TieFighter(new Vector2(((viewPort.Width - PANEL_WIDTH) * 5 / 14) + 14, 200)));
            enemyFighters.Add(new TieFighter(new Vector2(((viewPort.Width - PANEL_WIDTH) * 6 / 14) + 14, 200)));
            enemyFighters.Add(new TieFighter(new Vector2(((viewPort.Width - PANEL_WIDTH) * 7 / 14) + 14, 200)));
            enemyFighters.Add(new TieFighter(new Vector2(((viewPort.Width - PANEL_WIDTH) * 8 / 14) + 14, 200)));
            enemyFighters.Add(new TieFighter(new Vector2(((viewPort.Width - PANEL_WIDTH) * 9 / 14) + 14, 200)));
            enemyFighters.Add(new TieFighter(new Vector2(((viewPort.Width - PANEL_WIDTH) * 10 / 14) + 14, 200)));
            enemyFighters.Add(new TieFighter(new Vector2(((viewPort.Width - PANEL_WIDTH) * 11 / 14) + 14, 200)));
            enemyFighters.Add(new TieFighter(new Vector2(((viewPort.Width - PANEL_WIDTH) * 12 / 14) + 14, 200)));

            minTimeInMilliSecondsBetweenLaserFires = 0250; // 250 milliseconds = 0.25 seconds
            maxTimeInMilliSecondsBetweenLaserFires = 0500; // 500 milliseconds = 0.50 seconds
        } // end constructor
    } // end class Level10
} // namespace Star_Wars_Space_Invaders

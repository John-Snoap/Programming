using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Space_Invaders
{
    abstract class Level
    {
        // protected data
        protected List<EnemyFighter> enemyFighters; // list of all the enemy space ships in the level
        protected List<EnemyBoss> enemyBosses; // list of all the enemy bosses in the level
        protected int minTimeInMilliSecondsBetweenLaserFires;
        protected int maxTimeInMilliSecondsBetweenLaserFires;

        // properties
        public int MinTimeInMilliSecondsBetweenLaserFires
        {
            get
            {
                return minTimeInMilliSecondsBetweenLaserFires;
            } // end get
        } // end property MinTimeInMilliSecondsBetweenLaserFires

        public int MaxTimeInMilliSecondsBetweenLaserFires
        {
            get
            {
                return maxTimeInMilliSecondsBetweenLaserFires;
            } // end get
        } // end property MaxTimeInMilliSecondsBetweenLaserFires

        // constructor
        public Level()
        {
            enemyFighters = new List<EnemyFighter>();
            enemyBosses = new List<EnemyBoss>();
            minTimeInMilliSecondsBetweenLaserFires = 1000; // 1000 milliseconds = 1 second
            maxTimeInMilliSecondsBetweenLaserFires = 1250; // 1250 milliseconds = 1.25 seconds
        } // end constructor

        // public methods
        public List<EnemyFighter> StartLevel(out List<EnemyBoss> enemyBosses)
        {
            enemyBosses = this.enemyBosses.Copy(); // give the game a copy so we do not lose how the level starts out
            return enemyFighters.Copy(); // the copy method is in the ObjectExtensions.cs file
        } // end method StartLevel

        public List<EnemyFighter> ResetFighters()
        {
            return enemyFighters.Copy(); // give the game a copy so we do not loose how the level starts out
        } // end method ResetFighters
    } // end abstract class Level
} // end namespace Star_Wars_Space_Invaders

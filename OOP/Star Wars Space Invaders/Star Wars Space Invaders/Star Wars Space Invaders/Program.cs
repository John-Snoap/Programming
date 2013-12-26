using System;

namespace Star_Wars_Space_Invaders
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (StarWarsSpaceInvaders game = new StarWarsSpaceInvaders())
            {
                game.Run();
            }
        }
    }
#endif
}


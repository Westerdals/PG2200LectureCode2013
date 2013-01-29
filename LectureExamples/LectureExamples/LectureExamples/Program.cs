using System;

namespace Lecture4Examples
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CollisionAndInputGame game = new CollisionAndInputGame())
            {
                game.Run();
            }
        }
    }
#endif
}


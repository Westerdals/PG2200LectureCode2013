using System;

namespace SolutionLecture5
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SolutionLecture5Game game = new SolutionLecture5Game())
            {
                game.Run();
            }
        }
    }
#endif
}


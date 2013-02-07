using System;

namespace LectureExamples5
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ObjectOrientedGame game = new ObjectOrientedGame())
            {
                game.Run();
            }
        }
    }
#endif
}


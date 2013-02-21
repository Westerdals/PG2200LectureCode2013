using System;

namespace Lecture6Examples
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Lecture6ExampleGame game = new Lecture6ExampleGame())
            {
                game.Run();
            }
        }
    }
#endif
}


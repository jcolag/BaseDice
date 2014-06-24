// <copyright file="MainClass.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace BaseDice
{
        using System;

        /// <summary>
        /// Main class.
        /// </summary>
        public static class MainClass
        {
                /// <summary>
                /// The entry point of the program, where the program control starts and ends.
                /// </summary>
                private static void Main()
                {
                        Game g = new Game();
                        string s = string.Empty;
                        while (!g.Done())
                        {
                                s = g.TakeTurn();
                                if (!string.IsNullOrWhiteSpace(s))
                                {
                                        Console.WriteLine(s);
                                }
                        }
                        
                        Console.WriteLine(g.FinalTally());
                }
        }
}

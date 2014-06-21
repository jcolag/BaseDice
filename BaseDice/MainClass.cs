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
        public class MainClass
        {
                /// <summary>
                /// The entry point of the program, where the program control starts and ends.
                /// </summary>
                /// <param name="args">The command-line arguments.</param>
                public static void Main(string[] args)
                {
                        Game g = new Game();
                        string s = string.Empty;
                        while (g.Done())
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

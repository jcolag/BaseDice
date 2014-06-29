// <copyright file="GameTest.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace BaseDice
{
        using System;
        using NUnit.Framework;

        /// <summary>
        /// Game test.
        /// </summary>
        [TestFixture]
        public static class GameTest
        {
                /// <summary>
                /// Test the Done() method.
                /// </summary>
                [Test]
                public static void Done()
                {
                        var g = new Game();

                        Assert.AreEqual(g.Done(), false);
                        for (int i = 0; i < 25; i++)
                        {
                                g.TakeTurn();
                                Assert.AreEqual(g.Done(), false);
                        }

                        for (int i = 0; i < 125; i++)
                        {
                                g.TakeTurn();
                        }

                        Assert.AreEqual(g.Done(), true);
                }

                /// <summary>
                /// Test of LastRoll() method.
                /// </summary>
                [Test]
                public static void LastRoll()
                {
                        var g = new Game();
                        System.Collections.ObjectModel.Collection<int> dice = g.LastRoll();
                        Assert.AreEqual(dice.Count, 0);
                        g.TakeTurn();
                        dice = g.LastRoll();
                        Assert.IsNotNull(dice);
                        Assert.AreNotEqual(dice.Count, 0);
                        foreach (int i in dice)
                        {
                                Assert.GreaterOrEqual(i, 1);
                                Assert.LessOrEqual(i, 6);
                        }
                }

                /// <summary>
                /// Test of Diamond() method.
                /// </summary>
                [Test]
                public static void Diamond()
                {
                        var g = new Game();
                        string s = g.Diamond();
                        Assert.AreEqual(s, "0000");
                        for (int i = 0; i < 100; i++)
                        {
                                g.TakeTurn();
                                s = g.Diamond();
                                Assert.AreEqual(s.Length, 4);
                                s = s.Replace("0", string.Empty).Replace("1", string.Empty);
                                Assert.AreEqual(s, string.Empty);
                        }
                }

                /// <summary>
                /// Verifies the final tally.
                /// </summary>
                [Test]
                public static void FinalTally()
                {
                        var g = new Game();
                        string s;
                        int countedRuns = 0;
                        int runs;
                        int temp;
                        char[] delims = { ' ' };

                        for (int i = 0; i < 100; i++)
                        {
                                s = g.FinalTally();
                                Assert.IsNotNullOrEmpty(s);
                                int.TryParse(s.Split(delims)[0], out runs);
                                Assert.AreEqual(runs, countedRuns);
                                s = g.TakeTurn();
                                Assert.IsNotNullOrEmpty(s);
                                if (s.Contains(" Home Plate"))
                                {
                                        int.TryParse(s.Split(delims)[0], out temp);
                                        countedRuns += temp;
                                }
                        }

                        s = g.FinalTally();
                        Assert.IsNotNullOrEmpty(s);
                }
        }
}
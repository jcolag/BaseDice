// <copyright file="DieTest.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace BaseDice
{
        using System;
        using NUnit.Framework;

        /// <summary>
        /// Die test.
        /// </summary>
        [TestFixture]
        public static class DieTest
        {
                /// <summary>
                /// Tests the Die class.
                /// </summary>
                [Test]
                public static void TestCase()
                {
                        var r = new Random();
                        var d = new Die(r);
                        for (int i = 0; i < 100; i++)
                        {
                                d.Roll();
                                Assert.GreaterOrEqual(d.Value, 1);
                                Assert.LessOrEqual(d.Value, 6);
                        }
                }
        }
}
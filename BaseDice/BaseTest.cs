// <copyright file="BaseTest.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace BaseDice
{
        using System;
        using NUnit.Framework;

        /// <summary>
        /// Tests for the Base class.
        /// </summary>
        [TestFixture]
        public static class BaseTest
        {
                /// <summary>
                /// Tests the creation of a base.
                /// </summary>
                [Test]
                public static void TestCreation()
                {
                        var b = new Base(null);
                        var a = new Base(b);

                        Assert.AreEqual(a.HasRunner, false);
                        Assert.AreEqual(b.HasRunner, false);
                        Assert.AreEqual(a.Advance(), false);
                        Assert.AreEqual(b.Advance(), false);
                        Assert.AreEqual(a.Out(), false);
                        Assert.AreEqual(b.Out(), false);
                }

                /// <summary>
                /// Tests the runners.
                /// </summary>
                [Test]
                public static void TestRunners()
                {
                        var b = new Base(null);
                        var a = new Base(b);

                        a.Land();
                        Assert.AreEqual(a.HasRunner, true);
                        Assert.AreEqual(b.HasRunner, false);

                        Assert.AreEqual(b.Advance(), false);
                        Assert.AreEqual(a.Advance(), true);
                        Assert.AreEqual(a.HasRunner, false);
                        Assert.AreEqual(b.HasRunner, true);

                        Assert.AreEqual(a.Advance(), false);
                        Assert.AreEqual(b.Advance(), true);
                        Assert.AreEqual(a.HasRunner, false);
                        Assert.AreEqual(b.HasRunner, false);

                        a.Error();
                        Assert.AreEqual(a.HasRunner, true);
                        Assert.AreEqual(b.HasRunner, false);

                        Assert.AreEqual(a.Out(), true);
                        Assert.AreEqual(a.Out(), false);
                        a.Land();
                        Assert.AreEqual(a.Out(), true);
                        Assert.AreEqual(a.Out(), false);

                        a.Land();
                        Assert.AreEqual(a.HasRunner, true);
                        Assert.AreEqual(b.HasRunner, false);
                        a.Clear();
                        Assert.AreEqual(a.HasRunner, false);
                        Assert.AreEqual(b.HasRunner, false);
                }

                /// <summary>
                /// Tests landing on home plate.
                /// </summary>
                [Test]
                public static void TestHome()
                {
                        bool success = false;
                        var b = new Base(null);
                        var a = new Base(b);

                        b.SetRun(() => success = true);
                        Assert.AreEqual(success, false);

                        b.Land();
                        Assert.AreEqual(success, true);
                        Assert.AreEqual(b.HasRunner, false);

                        success = false;
                        a.Land();
                        Assert.AreEqual(success, false);
                        a.Advance();
                        Assert.AreEqual(success, true);
                }
        }
}
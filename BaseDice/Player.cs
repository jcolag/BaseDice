// <copyright file="Player.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace BaseDice
{
        using System;
        using System.Collections;

        /// <summary>
        /// BaseDice Player.
        /// </summary>
        public class Player
        {
                /// <summary>
                /// The point.
                /// </summary>
                private int point;

                /// <summary>
                /// The number of outs.
                /// </summary>
                private int outs;

                /// <summary>
                /// The previous value of outs.
                /// </summary>
                private int lastouts = 0;

                /// <summary>
                /// The player's score.
                /// </summary>
                private int runs;

                /// <summary>
                /// The score up through the last inning.
                /// </summary>
                private Hashtable inningRuns = new Hashtable();

                /// <summary>
                /// The number of hits.
                /// </summary>
                private int hits;

                /// <summary>
                /// The number of errors.
                /// </summary>
                private int errors;

                /// <summary>
                /// The number of runners to cross home that haven't otherwise been processed.
                /// </summary>
                private int home;

                /// <summary>
                /// Initializes a new instance of the <see cref="BaseDice.Player"/> class.
                /// </summary>
                public Player()
                {
                }

                /// <summary>
                /// Gets the point.
                /// </summary>
                /// <value>The point.</value>
                public int Point
                {
                        get
                        {
                                return this.point;
                        }
                }

                /// <summary>
                /// Determines whether this instance is point set.
                /// </summary>
                /// <returns><c>true</c> if this instance's point is set; otherwise, <c>false</c>.</returns>
                public bool IsPointSet()
                {
                        return this.point != 0;
                }

                /// <summary>
                /// Determines whether the supplied roll made the point.
                /// </summary>
                /// <returns><c>true</c>, if <c>candidate</c> matches the point, <c>false</c> otherwise.</returns>
                /// <param name="candidate">Candidate to test.</param>
                public bool PointMade(int candidate)
                {
                        return candidate == this.point;
                }

                /// <summary>
                /// Sets the point.
                /// </summary>
                /// <param name="p">The point.</param>
                public void SetPoint(int p)
                {
                        this.point = p;
                }

                /// <summary>
                /// Unsets the point.
                /// </summary>
                public void UnsetPoint()
                {
                        this.point = 0;
                }

                /// <summary>
                /// Player has been caught out.
                /// </summary>
                public void Out()
                {
                        ++this.outs;
                }

                /// <summary>
                /// Record the player's hit.
                /// </summary>
                public void Hit()
                {
                        ++this.hits;
                }

                /// <summary>
                /// Player scores a run.
                /// </summary>
                /// <returns>Current score.</returns>
                public int Run()
                {
                        return ++this.runs;
                }
        }
}
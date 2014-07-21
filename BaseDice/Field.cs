// <copyright file="Field.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace BaseDice
{
        using System;

        /// <summary>
        /// Playing field.
        /// </summary>
        public class Field
        {
                /// <summary>
                /// The bases.
                /// </summary>
                private Base[] bases = new Base[4];

                /// <summary>
                /// Initializes a new instance of the <see cref="BaseDice.Field"/> class.
                /// </summary>
                /// <param name="homeCallback">Home plate callback.</param>
                public Field(Action homeCallback)
                {
                        this.bases[3] = new Base(null, "Home Plate");
                        this.bases[2] = new Base(this.bases[3], "Third Base");
                        this.bases[1] = new Base(this.bases[2], "Second Base");
                        this.bases[0] = new Base(this.bases[1], "First Base");
                        this.bases[3].SetRun(homeCallback);
                }

                /// <summary>
                /// Clear the bases.
                /// </summary>
                public void Clear()
                {
                        foreach (Base b in this.bases)
                        {
                                b.Clear();
                        }
                }

                /// <summary>
                /// Adds a runner to the field.
                /// </summary>
                public void AddRunner()
                {
                        this.bases[0].Land();
                }

                /// <summary>
                /// Steal a base.
                /// </summary>
                /// <returns>Stolen base.</returns>
                public int Steal()
                {
                        int which = -1;

                        for (int i = this.bases.Length - 1; i >= 0; i--)
                        {
                                if (this.bases[i].HasRunner)
                                {
                                        this.bases[i].Advance();
                                        which = i + 1;
                                        break;
                                }
                        }

                        return which;
                }

                /// <summary>
                /// Report the state of the bases.
                /// </summary>
                /// <returns>String where each character is 1 if a runner is on-base.</returns>
                public string Diamond()
                {
                        string coll = string.Empty;
                        foreach (Base b in this.bases)
                        {
                                coll = (b.HasRunner ? "1" : "0") + coll;
                        }

                        return coll;
                }

                /// <summary>
                /// Advances runners on all bases.
                /// </summary>
                public void AdvanceAll()
                {
                        this.Advance(2);
                        this.Advance(1);
                        this.Advance(0);
                }

                /// <summary>
                /// Advance the runner on the specified base.
                /// </summary>
                /// <param name="which">Which base.</param>
                /// <returns>Player advanced.</returns>
                public bool Advance(int which)
                {
                        if (this.bases.GetLowerBound(0) > which || this.bases.GetUpperBound(0) < which)
                        {
                                return false;
                        }

                        return this.bases[which].Advance();
                }

                /// <summary>
                /// Tag the player out on the specified base.
                /// </summary>
                /// <param name="which">Which base.</param>
                /// <returns>Player was tagged.</returns>
                public bool Out(int which)
                {
                        if (this.bases.GetLowerBound(0) > which || this.bases.GetUpperBound(0) < which)
                        {
                                return false;
                        }

                        return this.bases[which].Out();
                }

                /// <summary>
                /// Return the specified base's name.
                /// </summary>
                /// <param name="which">Which base.</param>
                /// <returns>The name.</returns>
                public string Name(int which)
                {
                        if (this.bases.GetLowerBound(0) > which || this.bases.GetUpperBound(0) < which)
                        {
                                return string.Empty;
                        }

                        return this.bases[which].Name;
                }

                /// <summary>
                /// Error play.
                /// </summary>
                public void Error()
                {
                        this.bases[0].Error();
                }

                /// <summary>
                /// Score a single.
                /// </summary>
                public void Single()
                {
                        this.AddRunner();
                }

                /// <summary>
                /// Score a double.
                /// </summary>
                public void Double()
                {
                        this.AddRunner();
                        this.Advance(0);
                }

                /// <summary>
                /// Score a triple.
                /// </summary>
                public void Triple()
                {
                        this.AddRunner();
                        this.Advance(0);
                        this.Advance(1);
                }

                /// <summary>
                /// Score a home run.
                /// </summary>
                public void HomeRun()
                {
                        this.AddRunner();
                        this.Advance(0);
                        this.Advance(1);
                        this.Advance(2);
                }
        }
}
// <copyright file="Base.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace BaseDice
{
        using System;

        /// <summary>
        /// Base on the field.
        /// </summary>
        public class Base
        {
                /// <summary>
                /// The next base.
                /// </summary>
                private Base nextBase = null;

                /// <summary>
                /// The base is occupied?
                /// </summary>
                private bool occupied = false;

                /// <summary>
                /// The run callback.
                /// </summary>
                private CallbackFunction run = null;

                /// <summary>
                /// Initializes a new instance of the <see cref="BaseDice.Base"/> class.
                /// </summary>
                /// <param name="next">The next base.</param>
                public Base(Base next)
                {
                        this.nextBase = next;
                }

                /// <summary>
                /// Callback function.
                /// </summary>
                public delegate void CallbackFunction();

                /// <summary>
                /// Advance the player to the next base.
                /// </summary>
                /// <returns>Runner was on base.</returns>
                public bool Advance()
                {
                        if (!this.occupied)
                        {
                                return false;
                        }

                        if (this.nextBase != null)
                        {
                                this.nextBase.Land();
                        }

                        this.occupied = false;
                        return true;
                }

                /// <summary>
                /// Land on this base.
                /// </summary>
                public void Land()
                {
                        if (this.occupied)
                        {
                                this.Advance();
                        }

                        this.occupied = true;

                        if (this.run != null)
                        {
                                this.occupied = false;
                                this.run();
                        }
                }

                /// <summary>
                /// Land on this base if vacant.
                /// </summary>
                public void Error()
                {
                        if (this.occupied)
                        {
                                return;
                        }

                        this.Land();
                }

                /// <summary>
                /// Player is out.
                /// </summary>
                /// <returns>Whether a player was caught out.</returns>
                public bool Out()
                {
                        bool occ = this.occupied;
                        this.occupied = false;
                        return occ;
                }

                /// <summary>
                /// Sets the callback for landing on the base.
                /// </summary>
                /// <param name="callback">The callback function.</param>
                public void SetRun(CallbackFunction callback)
                {
                        this.run = callback;
                }
        }
}
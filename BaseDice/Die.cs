// <copyright file="Die.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace BaseDice
{
        using System;

        /// <summary>
        /// A single die.
        /// </summary>
        public class Die
        {
                /// <summary>
                /// The number of sides on the dice.
                /// </summary>
                private const int Sides = 6;

                /// <summary>
                /// The random number generator.
                /// </summary>
                private Random rand = null;

                /// <summary>
                /// The value.
                /// </summary>
                private int value = 0;

                /// <summary>
                /// Initializes a new instance of the <see cref="BaseDice.Die"/> class.
                /// </summary>
                /// <param name="r">The random number generator.</param>
                public Die(Random r)
                {
                        this.rand = r;
                }
                
                /// <summary>
                /// Gets the value.
                /// </summary>
                /// <value>The value.</value>
                public int Value
                {
                        get { return this.value; }
                }

                /// <summary>
                /// Roll this die.
                /// </summary>
                public void Roll()
                {
                        this.value = this.rand.Next(1, Die.Sides);
                }
        }
}
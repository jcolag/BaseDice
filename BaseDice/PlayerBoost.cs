// <copyright file="PlayerBoost.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace BaseDice
{
        /// <summary>
        /// Possible player bonuses.
        /// </summary>
        [System.Serializable]
        public enum PlayerBoost
        {
                /// <summary>
                /// Steal a base.
                /// </summary>
                StealBase,

                /// <summary>
                /// Walk the batter.
                /// </summary>
                Walk,

                /// <summary>
                /// Do nothing.
                /// </summary>
                Nothing
        }
}
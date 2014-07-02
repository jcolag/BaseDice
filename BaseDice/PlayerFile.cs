// <copyright file="PlayerFile.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace BaseDice
{
        using System;
        using System.Collections.Generic;
        using System.Collections.ObjectModel;
        using System.Xml;

        /// <summary>
        /// Player file.
        /// </summary>
        public class PlayerFile
        {
                /// <summary>
                /// The player's bonuses.
                /// </summary>
                private List<PlayerBoost> bonuses = new List<PlayerBoost>();

                /// <summary>
                /// Initializes a new instance of the <see cref="BaseDice.PlayerFile"/> class.
                /// </summary>
                /// <param name="xml">XML Document describing player.</param>
                public PlayerFile(System.Xml.XPath.IXPathNavigable xml)
                {
                        if (xml == null)
                        {
                                return;
                        }

                        var doc = (XmlDocument)xml;
                        XmlNodeList bonusList = doc.GetElementsByTagName("Bonus");
                        foreach (XmlNode bonus in bonusList)
                        {
                                XmlAttribute attr = bonus.Attributes["Name"];
                                string name = attr.InnerText;
                                PlayerBoost b = PlayerBoost.Nothing;
                                try
                                {
                                        b = (PlayerBoost)Enum.Parse(typeof(PlayerBoost), name);
                                }
                                catch (ArgumentNullException)
                                {
                                }
                                catch (ArgumentException)
                                {
                                }
                                catch (OverflowException)
                                {
                                }

                                if (b == PlayerBoost.Nothing)
                                {
                                        continue;
                                }

                                this.Add(b);
                        }
                }

                /// <summary>
                /// Gets the bonuses.
                /// </summary>
                /// <value>The bonuses.</value>
                public Collection<PlayerBoost> Bonuses
                {
                        get
                        {
                                return new Collection<PlayerBoost>(this.bonuses);
                        }
                }

                /// <summary>
                /// Add the specified boost to the bonus list.
                /// </summary>
                /// <param name="b">The bonus.</param>
                public void Add(PlayerBoost b)
                {
                        this.bonuses.Add(b);
                }

                /// <summary>
                /// Remove the specified bonus.
                /// </summary>
                /// <returns>True on success.</returns>
                /// <param name="b">The bonus.</param>
                public bool Remove(PlayerBoost b)
                {
                        return this.bonuses.Remove(b);
                }
        }
}
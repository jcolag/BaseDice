// <copyright file="Game.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace BaseDice
{
        using System;
        using System.Collections.Generic;

        /// <summary>
        /// One game of BaseDice.
        /// </summary>
        public class Game
        {
                /// <summary>
                /// The random number generator.
                /// </summary>
                private Random rand = new Random();

                /// <summary>
                /// The dice.
                /// </summary>
                private Die[] dice = new Die[2];

                /// <summary>
                /// The bases.
                /// </summary>
                private Base[] bases = new Base[4];

                /// <summary>
                /// The point.
                /// </summary>
                private int point = 0;

                /// <summary>
                /// The number of outs.
                /// </summary>
                private int outs = 0;

                /// <summary>
                /// The player's score.
                /// </summary>
                private int runs = 0;

                /// <summary>
                /// The number of hits.
                /// </summary>
                private int hits = 0;

                /// <summary>
                /// The number of errors.
                /// </summary>
                private int errors = 0;

                /// <summary>
                /// The number of runners to cross home that haven't otherwise been processed.
                /// </summary>
                private int home = 0;

                /// <summary>
                /// Initializes a new instance of the <see cref="BaseDice.Game"/> class.
                /// </summary>
                public Game()
                {
                        for (int die = 0; die < this.dice.Length; die++)
                        {
                                this.dice[die] = new Die(this.rand);
                        }

                        this.bases[3] = new Base(null);
                        this.bases[2] = new Base(this.bases[3]);
                        this.bases[1] = new Base(this.bases[2]);
                        this.bases[0] = new Base(this.bases[1]);
                        this.bases[3].SetRun(this.Run);
                }

                /// <summary>
                /// Takes the turn.
                /// </summary>
                /// <returns>A report of the at-bat results.</returns>
                public string TakeTurn()
                {
                        int rollTotal = 0;
                        List<int> roll = this.Roll();
                        string report = string.Empty;

                        foreach (int val in roll)
                        {
                                rollTotal += val;
                        }

                        if (this.point == 0)
                        {
                                report = this.TurnNoPoint(roll);

                                this.point = rollTotal;
                                report = "the point is " + this.point.ToString() + ", " + report;
                        }
                        else
                        {
                                if (rollTotal == this.point)
                                {
                                        report = this.TurnPointMatched(roll);
                                        this.point = 0;
                                }

                                string report2 = this.TurnWithPoint(roll);
                                if (!string.IsNullOrWhiteSpace(report))
                                {
                                        report += "\n" + report2;
                                }
                        }

                        string runners = string.Empty;
                        if (this.home > 0)
                        {
                                runners = this.home.ToString() + " runner" +
                                        (this.home == 1 ? string.Empty : "s") +
                                        " cross Home Plate.\n";
                                this.home = 0;
                        }

                        return runners +
                                "Rolled " + DiceTalk(rollTotal, roll[0] == roll[1]) + ", " + report;
                }

                /// <summary>
                /// Describes whether the game is over.
                /// </summary>
                /// <returns>True if the game still has outs remaining.</returns>
                public bool Done()
                {
                        return this.outs < 27;
                }

                /// <summary>
                /// Produces the final tally.
                /// </summary>
                /// <returns>The tally.</returns>
                public string FinalTally()
                {
                        return this.runs.ToString() + " runs.  " +
                                this.hits.ToString() + " hits.  " +
                                        this.errors.ToString() + " errors.";
                }

                /// <summary>
                /// Translates the roll to crap-ese.
                /// </summary>
                /// <returns>The talk.</returns>
                /// <param name="roll">The roll.</param>
                /// <param name="equal">The dice show the same face.</param>
                private static string DiceTalk(int roll, bool equal)
                {
                        string res = string.Empty;
                        switch (roll)
                        {
                                case 2:
                                res = "Snake Eyes";
                                break;
                                case 3:
                                res = "Ace Deuce";
                                break;
                                case 4:
                                res = equal ? "Hard" : "Soft" + " Four";
                                break;
                                case 5:
                                res = "Fever";
                                break;
                                case 6:
                                res = equal ? "Hard" : "Soft" + " Six";
                                break;
                                case 7:
                                res = "Natural";
                                break;
                                case 8:
                                res = equal ? "Hard" : "Soft" + " Eight";
                                break;
                                case 9:
                                res = "Nina";
                                break;
                                case 10:
                                res = equal ? "Hard" : "Soft" + " Ten";
                                break;
                                case 11:
                                res = "Yo";
                                break;
                                case 12:
                                res = "Boxcars";
                                break;
                        }

                        return res;
                }

                /// <summary>
                /// Takes the turns with the point not set.
                /// </summary>
                /// <returns>The resulting report.</returns>
                /// <param name="roll">The roll.</param>
                private string TurnNoPoint(List<int> roll)
                {
                        int rollTotal = 0;
                        string report = string.Empty;

                        foreach (int val in roll)
                        {
                                rollTotal += val;
                        }

                        if (roll[0] == roll[1])
                        {
                                this.bases[2].Advance();
                                this.bases[1].Advance();
                                this.bases[0].Advance();
                                this.bases[0].Land();
                                ++this.hits;
                                report = "Advance all bases";
                        }
                        else if (rollTotal == 3)
                        {
                                this.bases[0].Land();
                                if (this.bases[2].Advance())
                                {
                                        report = "Advance to home";
                                }
                                else
                                {
                                        report = "Single";
                                }
                        }
                        else if (new List<int>() { 4, 5, 9, 10 }.Contains(rollTotal))
                        {
                                if (this.bases[0].Advance())
                                {
                                        report = "Advance to second";
                                }
                                else
                                {
                                        report = "Single";
                                }

                                this.bases[0].Land();
                        }
                        else if (rollTotal == 6 || rollTotal == 8)
                        {
                                if (this.bases[1].Advance())
                                {
                                        report = "Advance to third";
                                }
                                else
                                {
                                        report = "Single";
                                }

                                this.bases[0].Land();
                        }
                        else if (rollTotal == 7)
                        {
                                ++this.outs;
                                report = "Out";
                        }
                        else if (rollTotal == 11)
                        {
                                ++this.outs;
                                report = "Non-at-bat out";
                        }
                        else
                        {
                                throw new Exception("Missed a roll - point off! " + rollTotal.ToString());
                        }

                        return report;
                }

                /// <summary>
                /// Take the turn when the point has been matched.
                /// </summary>
                /// <returns>The report.</returns>
                /// <param name="roll">The roll.</param>
                private string TurnPointMatched(List<int> roll)
                {
                        int rollTotal = 0;
                        string report = string.Empty;

                        foreach (int val in roll)
                        {
                                rollTotal += val;
                        }

                        if (new List<int>() { 2, 3, 7, 11, 12 }.Contains(rollTotal))
                        {
                                // Do nothing
                        }
                        else if (roll[0] == 5 && roll[1] == 5)
                        {
                                this.bases[0].Land();
                                this.bases[0].Advance();
                                this.bases[1].Advance();
                                ++this.hits;
                                report = "Triple";
                        }
                        else if (roll[0] == 3 && roll[1] == 3)
                        {
                                this.bases[0].Error();
                                ++this.errors;
                                report = "Error";
                        }
                        else if (roll[0] == roll[1])
                        {
                                this.bases[0].Land();
                                ++this.errors;
                                report = "Error";
                        }
                        else if (rollTotal == 5)
                        {
                                this.bases[0].Land();
                                this.bases[0].Advance();
                                ++this.hits;
                                report = "Double (standard)";
                        }
                        else if (new List<int>() { 4, 6, 10 }.Contains(rollTotal))
                        {
                                this.bases[0].Land();
                                ++this.hits;
                                report = "Single (standard)";
                        }
                        else if (rollTotal == 8)
                        {
                                this.bases[0].Land();
                                this.bases[0].Advance();
                                this.bases[1].Advance();
                                this.bases[2].Advance();
                                ++this.hits;
                                report = "Home Run";
                        }
                        else if (rollTotal == 9)
                        {
                                this.bases[0].Land();
                                this.bases[0].Advance();
                                ++this.hits;
                                report = "Double (strong)";
                        }
                        else
                        {
                                throw new Exception("Missed a roll - made the point! " + rollTotal.ToString());
                        }

                        return report;
                }

                /// <summary>
                /// Takes the turn with a point to meet.
                /// </summary>
                /// <returns>The report.</returns>
                /// <param name="roll">The roll.</param>
                private string TurnWithPoint(List<int> roll)
                {
                        int rollTotal = 0;
                        string report = string.Empty;

                        foreach (int val in roll)
                        {
                                rollTotal += val;
                        }

                        if (rollTotal == 2)
                        {
                                if (this.bases[1].Out())
                                {
                                        ++this.outs;
                                        report = "Eliminate player on second";
                                }
                                else
                                {
                                        report = "Thrown to second";
                                }
                        }
                        else if (rollTotal == 3)
                        {
                                if (this.bases[2].Out())
                                {
                                        ++this.outs;
                                        report = "Eliminate player on third";
                                }
                                else
                                {
                                        report = "Thrown to third";
                                }
                        }
                        else if (new List<int>() { 4, 5, 6, 8, 9, 10 }.Contains(rollTotal))
                        {
                                ++this.outs;
                                report = "Out";
                        }
                        else if (rollTotal == 7)
                        {
                                report = "Roll again";
                        }
                        else if (rollTotal == 11)
                        {
                                this.bases[0].Land();
                                report = "Walk";
                        }
                        else if (rollTotal == 12)
                        {
                                if (this.bases[0].Out())
                                {
                                        report = "Eliminate man first";
                                        ++this.outs;
                                }
                                else
                                {
                                        report = "Thrown to first";
                                }
                        }
                        else if (roll[0] == roll[1])
                        {
                                this.bases[0].Land();
                                ++this.hits;
                                report = "Single (strong)";
                        }
                        else
                        {
                                throw new Exception("Missed a roll - point on! " + rollTotal.ToString());
                        }

                        return report;
                }

                /// <summary>
                /// Roll this instance.
                /// </summary>
                /// <returns>List of die values</returns>
                private List<int> Roll()
                {
                        List<int> result = new List<int>();
                        int total = 0;

                        for (int die = 0; die < this.dice.Length; die++)
                        {
                                this.dice[die].Roll();
                                result.Add(this.dice[die].Value);
                                total += this.dice[die].Value;
                        }

                        return result;
                }
                
                /// <summary>
                /// Add a run; use as callback only.
                /// </summary>
                private void Run()
                {
                        ++this.home;
                        ++this.runs;
                }
        }
}
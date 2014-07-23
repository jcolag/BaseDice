// <copyright file="Game.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace BaseDice
{
        using System;
        using System.Collections.Generic;
        using System.Collections.ObjectModel;

        /// <summary>
        /// One game of BaseDice.
        /// </summary>
        public class Game
        {
                /// <summary>
                /// The number of outs per inning.
                /// </summary>
                private const int Inning = 3;

                /// <summary>
                /// The number of innings per game.
                /// </summary>
                private const int Length = 9;

                /// <summary>
                /// The playing field.
                /// </summary>
                private Field field;

                /// <summary>
                /// The player.
                /// </summary>
                private Player player;

                /// <summary>
                /// The random number generator.
                /// </summary>
                private Random rand = new Random();

                /// <summary>
                /// The dice.
                /// </summary>
                private Die[] dice = new Die[2];

                /// <summary>
                /// The roll of the dice.
                /// </summary>
                private List<int> roll;

                /// <summary>
                /// Initializes a new instance of the <see cref="BaseDice.Game"/> class.
                /// </summary>
                public Game()
                {
                        for (int die = 0; die < this.dice.Length; die++)
                        {
                                this.dice[die] = new Die(this.rand);
                        }

                        this.player = new Player();
                        this.field = new Field(this.player.Run);
                }

                /// <summary>
                /// Return this instance's diamond code.
                /// </summary>
                /// <returns>Diamond code.</returns>
                public string Diamond()
                {
                        return this.field.Diamond();
                }

                /// <summary>
                /// Takes the turn.
                /// </summary>
                /// <returns>A report of the at-bat results.</returns>
                public string TakeTurn()
                {
                        return this.TakeTurn(PlayerBoost.Nothing);
                }

                /// <summary>
                /// Takes the turn.
                /// </summary>
                /// <returns>>A report of the at-bat results.</returns>
                /// <param name="bonus">Bonus the player would like to use.</param>
                public string TakeTurn(PlayerBoost bonus)
                {
                        int rollTotal = 0;
                        int oldHits = this.player.Hits;
                        int curr = this.player.CurrentInning(Game.Inning);
                        string report = string.Empty;
                        string nl = Environment.NewLine;
                        bool inning = false;

                        this.roll = this.Roll();
                        foreach (int val in this.roll)
                        {
                                rollTotal += val;
                        }

                        if (this.player.Point == 0)
                        {
                                report = this.TurnNoPoint(this.roll);
                                if (rollTotal == 12)
                                {
                                        report += " (Crapped out, bets pushed) ";
                                }
                                else if (new List<int>() { 2, 3 }.Contains(rollTotal))
                                {
                                        report += " (Crapped out) ";
                                }
                                else if (new List<int>() { 7, 11 }.Contains(rollTotal))
                                {
                                        report += " (Natural) ";
                                }
                                else
                                {
                                        this.player.SetPoint(rollTotal);
                                        report = "the point is " + this.player.Point.ToString() + ", " + report;
                                        report += " (On) ";
                                }
                        }
                        else
                        {
                                if (rollTotal == this.player.Point)
                                {
                                        report = this.TurnPointMatched(this.roll);
                                        report += " (Hit the point) ";
                                        this.player.UnsetPoint();
                                }
                                else if (rollTotal == 7)
                                {
                                        report += " (Seven-out) ";
                                        this.player.UnsetPoint();
                                }

                                string report2 = this.TurnWithPoint(this.roll);
                                if (!string.IsNullOrWhiteSpace(report) && !string.IsNullOrWhiteSpace(report2))
                                {
                                        report += nl + report2;
                                }
                                else if (!string.IsNullOrWhiteSpace(report2))
                                {
                                        report = report2;
                                }
                        }

                        string runners = this.player.ReportRuns(nl);

                        if (this.player.IsNewInning(Game.Inning))
                        {
                                int which = this.player.CurrentInning(Game.Inning) - 1;
                                report += nl + " * " + which.ToString();
                                report += Ordinal(which);

                                report += " inning over! *";
                                inning = true;
                                this.field.Clear();
                        }

                        if (!inning)
                        {
                                switch (bonus)
                                {
                                case PlayerBoost.Walk:
                                        if (oldHits == this.player.Hits)
                                        {
                                                this.field.AddRunner();
                                                report += nl + "Batter walks." + nl;
                                        }

                                        break;
                                case PlayerBoost.StealBase:
                                        // Advance the furthest runner along.
                                        int whichBase = this.field.Steal();
                                        if (whichBase > 0)
                                        {
                                                report += nl + "Runner on " + whichBase.ToString() +
                                                        Ordinal(whichBase) + " steals " +
                                                        this.field.Name(whichBase) + "!" + nl;
                                        }

                                        break;
                                case PlayerBoost.Nothing:
                                        // Do nothing
                                        break;
                                default:
                                        throw new ArgumentOutOfRangeException(
                                                "bonus",
                                                "Impossible bonus passed");
                                }
                        }

                        this.player.AddInning(curr);
                        return runners +
                                "Rolled " + DiceTalk(rollTotal, this.roll[0] == this.roll[1]) + ", " + report;
                }

                /// <summary>
                /// Describes whether the game is over.
                /// </summary>
                /// <returns>True if the game still has outs remaining.</returns>
                public bool Done()
                {
                        return this.player.IsGameOver(Game.Inning, Game.Length);
                }

                /// <summary>
                /// Returns the current inning number.
                /// </summary>
                /// <returns>The inning.</returns>
                public int WhatInning()
                {
                        return this.player.CurrentInning(Game.Inning);
                }

                /// <summary>
                /// Returns the inning's score.
                /// </summary>
                /// <returns>The score.</returns>
                public int InningScore()
                {
                        return this.InningScore(this.player.CurrentInning(Game.Inning));
                }

                /// <summary>
                /// Separates the inning's score from the total.
                /// </summary>
                /// <returns>The score.</returns>
                /// <param name="inn">The current inning.</param>
                public int InningScore(int inn)
                {
                        return this.player.InningScore(inn);
                }

                /// <summary>
                /// Return the last roll.
                /// </summary>
                /// <returns>The roll.</returns>
                public Collection<int> LastRoll()
                {
                        return this.roll == null ? new Collection<int>() : new Collection<int>(this.roll);
                }

                /// <summary>
                /// Produces the final tally.
                /// </summary>
                /// <returns>The tally.</returns>
                public string FinalTally()
                {
                        return this.player.FinalTally();
                }

                /// <summary>
                /// Get the Ordinal suffix of the specified number.
                /// </summary>
                /// <returns>The suffix.</returns>
                /// <param name="which">Which number.</param>
                private static string Ordinal(int which)
                {
                        string suffix;

                        switch (which)
                        {
                        case 1:
                                suffix = "st";
                                break;
                        case 2:
                                suffix = "nd";
                                break;
                        case 3:
                                suffix = "rd";
                                break;
                        default:
                                suffix = "th";
                                break;
                        }

                        return suffix;
                }

                /// <summary>
                /// Translates the roll to crap-ese.
                /// </summary>
                /// <returns>The talk.</returns>
                /// <param name="number">The roll.</param>
                /// <param name="equal">The dice show the same face.</param>
                private static string DiceTalk(int number, bool equal)
                {
                        string res = string.Empty;
                        switch (number)
                        {
                                case 2:
                                res = "Snake Eyes";
                                break;
                                case 3:
                                res = "Ace Deuce";
                                break;
                                case 4:
                                res = (equal ? "Hard" : "Soft") + " Four";
                                break;
                                case 5:
                                res = "Fever";
                                break;
                                case 6:
                                res = (equal ? "Hard" : "Soft") + " Six";
                                break;
                                case 7:
                                res = "Natural";
                                break;
                                case 8:
                                res = (equal ? "Hard" : "Soft") + " Eight";
                                break;
                                case 9:
                                res = "Nina";
                                break;
                                case 10:
                                res = (equal ? "Hard" : "Soft") + " Ten";
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
                /// <param name="dieRolls">The roll.</param>
                private string TurnNoPoint(List<int> dieRolls)
                {
                        int rollTotal = 0;
                        string report = string.Empty;

                        foreach (int val in dieRolls)
                        {
                                rollTotal += val;
                        }

                        if (dieRolls[0] == dieRolls[1])
                        {
                                this.field.AdvanceAll();
                                this.field.Single();
                                this.player.Hit();
                                report = "Advance all bases";
                        }
                        else if (rollTotal == 3)
                        {
                                this.field.Single();
                                if (this.field.Advance(2))
                                {
                                        report = "Advance to home";
                                }
                                else
                                {
                                        report = "Single";
                                }

                                this.player.Hit();
                        }
                        else if (new List<int>() { 4, 5, 9, 10 }.Contains(rollTotal))
                        {
                                if (this.field.Advance(0))
                                {
                                        report = "Advance to second";
                                }
                                else
                                {
                                        report = "Single";
                                }

                                this.field.Single();
                                this.player.Hit();
                        }
                        else if (rollTotal == 6 || rollTotal == 8)
                        {
                                if (this.field.Advance(1))
                                {
                                        report = "Advance to third";
                                }
                                else
                                {
                                        report = "Single";
                                }

                                this.field.Single();
                                this.player.Hit();
                        }
                        else if (rollTotal == 7)
                        {
                                this.player.Out();
                                report = "Out";
                        }
                        else if (rollTotal == 11)
                        {
                                this.player.Out();
                                report = "Non-at-bat out";
                        }
                        else
                        {
                                throw new ArgumentOutOfRangeException(
                                        "dieRolls",
                                        "Missed a roll - point off! " + rollTotal.ToString());
                        }

                        return report;
                }

                /// <summary>
                /// Take the turn when the point has been matched.
                /// </summary>
                /// <returns>The report.</returns>
                /// <param name="dieRolls">The roll.</param>
                private string TurnPointMatched(List<int> dieRolls)
                {
                        int rollTotal = 0;
                        string report = string.Empty;

                        foreach (int val in dieRolls)
                        {
                                rollTotal += val;
                        }

                        if (new List<int>() { 2, 3, 7, 11, 12 }.Contains(rollTotal))
                        {
                                // Do nothing
                                report = "Matched the point";
                        }
                        else if (dieRolls[0] == 5 && dieRolls[1] == 5)
                        {
                                this.field.Triple();
                                this.player.Hit();
                                report = "Triple";
                        }
                        else if (dieRolls[0] == 3 && dieRolls[1] == 3)
                        {
                                this.field.Error();
                                this.player.Error();
                                report = "Error";
                        }
                        else if (dieRolls[0] == dieRolls[1])
                        {
                                this.field.Single();
                                this.player.Error();
                                report = "Error";
                        }
                        else if (rollTotal == 5)
                        {
                                this.field.Double();
                                this.player.Hit();
                                report = "Double (standard)";
                        }
                        else if (new List<int>() { 4, 6, 10 }.Contains(rollTotal))
                        {
                                this.field.Single();
                                this.player.Hit();
                                report = "Single (standard)";
                        }
                        else if (rollTotal == 8)
                        {
                                this.field.HomeRun();
                                this.player.Hit();
                                report = "Home Run";
                        }
                        else if (rollTotal == 9)
                        {
                                this.field.Double();
                                this.player.Hit();
                                report = "Double (strong)";
                        }
                        else
                        {
                                throw new ArgumentOutOfRangeException(
                                        "dieRolls",
                                        "Missed a roll - made the point! " + rollTotal.ToString());
                        }

                        return report;
                }

                /// <summary>
                /// Takes the turn with a point to meet.
                /// </summary>
                /// <returns>The report.</returns>
                /// <param name="dieRolls">The roll.</param>
                private string TurnWithPoint(List<int> dieRolls)
                {
                        int rollTotal = 0;
                        string report = string.Empty;

                        foreach (int val in dieRolls)
                        {
                                rollTotal += val;
                        }

                        if (rollTotal == 2)
                        {
                                if (this.field.Out(1))
                                {
                                        this.player.Out();
                                        report = "Eliminate player on second";
                                }
                                else
                                {
                                        report = "Thrown to second";
                                }
                        }
                        else if (rollTotal == 3)
                        {
                                if (this.field.Out(2))
                                {
                                        this.player.Out();
                                        report = "Eliminate player on third";
                                }
                                else
                                {
                                        report = "Thrown to third";
                                }
                        }
                        else if (new List<int>() { 4, 5, 6, 8, 9, 10 }.Contains(rollTotal))
                        {
                                this.player.Out();
                                report = "Out";
                        }
                        else if (rollTotal == 7)
                        {
                                report = "Roll again";
                        }
                        else if (rollTotal == 11)
                        {
                                this.field.AddRunner();
                                report = "Walk";
                        }
                        else if (rollTotal == 12)
                        {
                                if (this.field.Out(0))
                                {
                                        report = "Eliminate man on first";
                                        this.player.Out();
                                }
                                else
                                {
                                        report = "Thrown to first";
                                }
                        }
                        else if (dieRolls[0] == dieRolls[1])
                        {
                                this.field.Single();
                                this.player.Hit();
                                report = "Single (strong)";
                        }
                        else
                        {
                                throw new ArgumentOutOfRangeException(
                                        "dieRolls",
                                        "Missed a roll - point on! " + rollTotal.ToString());
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
        }
}
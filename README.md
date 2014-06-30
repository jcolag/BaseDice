BaseDice
========

Dynamic narrative crossing sports (baseball) and gambling (craps).

Note
----

I can't take credit for the idea, but I don't know _who_ to credit.  I recently stumbled across a file on an old hard drive outlining the rules without any indication of where they came from or what to do with them.

I _do_ know two important things:

 - I never entered into any kind of business deal to bring this to market.

 - Rules of games can only be protected through patents, and there is no patent covering these rules.

In other words, I believe this is a safe project to publish, legally speaking.

It would still be nice to give whoever came up with the concept credit, though.

Usage
-----

 1. Build.

 2. Run.

 3. Play.

The "game" engine runs for 27 outs (broken somewhat carelessly into nine innings), reporting what has happened and a final score.

I should point out that I don't follow baseball and don't gamble, so if any part of this looks peculiar--as if I don't know what the output should look like--it's because I'm just implementing the idea directly.

Build
-----

It definitely works in MonoDevelop. It almost certainly works in SharpDevelop. It probably works in Visual Studio, maybe with some fudging.

Obviously, that needs testing.

Future
------

Among other things...

 - [ ] Right now, this is only a single-player story, by design.  It shouldn't be too hard, though output that makes sense could be.  Arguably, given the format, more than two players could even work.  Massively multiplayer online baseball...?

 - [X] Separating the output into innings might be nice, though scores seem very low as it is.  (_After fixing the craps point system, scores are no longer low._)

 - [ ] Interactivity.  Of some sort.

 - [X] Web interface.

 - [ ] Some way of distinguishing good teams/players from bad, possibly related to interactivity, somehow.  For example, a player could choose to employ some (earned) tactic at each at-bat that alters the probabilities.

 - [ ] Change the rules to better fit the Craps paradigm.  As mentioned, I don't know much about either game, but I'm still uncomfortable with crapping out advancing all the runners on bases.

 - [ ] In fact, the rules should be factored out into configuration.  Hardcoding them is seriously ugly, though since many are logic connections, that may prove difficult.

Credits
-------

As mentioned, someone else created the game, whose name I've somehow managed to lose, over the years.

The shortcut icon is a combination of pianoBrad's [Baseball](https://openclipart.org/detail/75919/baseball-by-pianobrad) and the pips from pierro72's [dice](https://openclipart.org/detail/181176/dice-by-pierro72-181176).  The main background image is jcoop585's [Baseball Sports America](http://pixabay.com/en/baseball-sports-america-192400/).

The dynamic work is handled through [jQuery](https://jquery.com/), but don't blame them for anything weird I have done.

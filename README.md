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

At this time, I have not been able to get a viable, modern ASP.NET environment under Apache on Ubuntu.  IIS would probably be better, but is not on hand.

Future
------

Among other things...

 - [ ] Right now, this is only a single-player story, by design.  It shouldn't be too hard, though output that makes sense could be.  Arguably, given the format, more than two players could even work.  Massively multiplayer online baseball...?

 - [X] Separating the output into innings might be nice, though scores seem very low as it is.  (_After fixing the craps point system, scores are no longer low._)

 - [X] Interactivity.  Of some sort.

 - [X] Web interface.

 - [X] Some way of distinguishing good teams/players from bad, possibly related to interactivity, somehow.  For example, a player could choose to employ some (earned) tactic at each at-bat that alters the probabilities.  (The bonuses can add strategy and be affixed to the player rather than simply allowing them.)

 - [ ] Change the rules to better fit the Craps paradigm.  As mentioned, I don't know much about either game, but I'm still uncomfortable with crapping out advancing all the runners on bases.

 - [ ] In fact, the rules should be factored out into configuration.  Hardcoding them is seriously ugly, though since many are logic connections, that may prove difficult.

 - [ ] Trying to add music to the game via SoundCloud's API turned out to be non-trivial due to [CORS](https://en.wikipedia.org/wiki/Cross-Origin_Resource_Sharing) restrictions and [XSP](https://en.wikipedia.org/wiki/XSP_%28software%29)'s low-end architecture.  Trying to install an up-to-date Mono on Apache turned out to be...non-trivial, as well.  The most basic code is in-place if anybody wants to test and expand it, and I have a list of tracks that seem appropriate, but for now, this will have to wait.

- [ ] Now that we have player files, the bonuses should perhaps be encrypted with "umpires"' private keys on a per-bonus basis.

- [ ] The game might also consider awarding bonuses based on performance.

- [ ] The game has become almost entirely baseball.  A gambling mechanic that combines both the score and the craps side of the game might rebalance it.

Credits
-------

As mentioned, someone else created the game, whose name I've somehow managed to lose, over the years.

The shortcut icon is a combination of pianoBrad's [Baseball](https://openclipart.org/detail/75919/baseball-by-pianobrad) and the pips from pierro72's [dice](https://openclipart.org/detail/181176/dice-by-pierro72-181176).  The main background image is jcoop585's [Baseball Sports America](http://pixabay.com/en/baseball-sports-america-192400/).

The fonts, to the extent they're interesting, are [Playball](https://www.google.com/fonts/specimen/Playball) by TypeSETit and [Exo 2](https://www.google.com/fonts/specimen/Exo+2) by Natanael Gama.

The dynamic page updates are handled through [jQuery](https://jquery.com/), but don't blame them for anything weird I have done.

###Music (Future Credits)###

As mentioned in the to-dos, a nice touch would be background music, which appears possible through [SoundCloud](https://soundcloud.com/) assuming the CORS issue can be overcome.  On the chance anybody wants to tinker, the tracks I found with a baseball tag that seemed to be high-quality (whether or not I actually liked them) include, in no particular order:

- [Size 10 (Original Mix)](https://soundcloud.com/ottoblucker/otto-bl-cker-size-10-original)
- [Ucreate Music Sample Track](https://soundcloud.com/madebyrobot/ucreate-music-sample-track)
- [Kracker Jaxx](https://soundcloud.com/jk-harris/krackerjaxx)
- [The Cheesy](https://soundcloud.com/ob3ple/the-cheesy-1)
- [Lulzy Baseball](https://soundcloud.com/lightningdude/lulzy-baseball)
- [Imaginary Baseball](https://soundcloud.com/tnyfrrro/imaginary-baseball)
- [Baseball](https://soundcloud.com/acloudintrousers/baseball)

The other criteria was what you might consider an All-Ages rating.  Profanity disqualified quite a few.  Hopefully, none slipped into this batch.


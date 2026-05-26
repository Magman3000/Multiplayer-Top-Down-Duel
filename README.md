# Multiplayer-Top-Down-Duel
### Game Design

I made a simple top-down shooter where the players shoot at each other in an attempt to reduce the
opponent's health to 0. When a player reduces an opponent's health to 0, the opponent's health resets,
and the player gets a point. The camera shifts perspective dynamically so that the players are always
within view. There is a cooldown between shots, so players can’t spam them and instead need to make
their shots count.

### Network Features

Gameplay synced over the network. Scoreboard and Text Chat with names set before joining a match.

### Challenges

Remote Procedure Calls (RPCs) and Network Variables have changed over time, causing certain
guides and videos to be out of date. Additionally, certain bugs took time and sometimes outside help to
fix. This included an order of operations issue that disrupted the return on the ”IsOwner” bool, and and
aditional order of operations issue regarding RPCs. Beyond this, the client player kept hitting itself
with its own bullets. Ultimately, I fixed this by having a coroutine enable the hitbox slightly after the
bullet was shot.

### What I Learned

To properly use the internet as a source of information regarding the Systems of the Engine I’m using
and to properly ask for help online regarding issues I encounter.

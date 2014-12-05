Age of CAS IV: *Cassmo Strikes Back*
====================

**Find us at our website:** [Not yet Completed](https://github.com/ErisMik/Cassmo_Website_and_Wiki)

**Engine:** Unity3D + [Insert cool name here]: Node.js server by Peter Fajner 

**Genre:** MMORPGRTS Command Simulation

**Features:** It's a Divinity-Like Space Engineers related clone-ish Multiplayer Online Shooter Strategy Game in space

**Platforms:** Windows, perhaps OSX and Linux

Created and Worked on by [Eric M](https://github.com/ErisMik), [Peter F](https://github.com/PeterFajner) and [Troy H](https://github.com/TroyNH)

###**State of the Game**
*(Updated December 4, 2014)*

In short, it's not really a game yet; all of the work up to this point in time is in the Walking Test Unity project. It is, and will continue to be a collection of tests and small implementations of features until we feel confident in starting the next stage of the project, the actual game itself.

###**Features:**

####*Implemented*
- Universal gravity
  - Every rigidbody is attracted to every other rigidbody
  - Mass can be set manually except for Celestial Bodies, where it is calculated using the spheroid volume formula and the density setting
  - Artificial gravity sources implemented (insufficiently tested)
  - Universal drag system, drag increases with inverse of distance from gravity source, does not affect Celestial Bodies.
  - TODO: Hard mode, drag system affects Celestial Bodies, you get to stationkeep your moons.
  
####*In Developement*
1. Movement
  1. works *adequately*
  2. torso rotation towards the normal of the total gravity vector implemented
    1. may be changed to the normal of the strongest gravity vector
2. Camera Controls
  1. rotation implemented, but is buggy
3. Instancing
  1. most likely will involve separate planet instance and the universe instance, separated by ~~opaque~~ translucent (to get pretty surface textures bro) cloud layers
    1. Will Begin to preload scene as you approach planet, so when you hit cloud layer load times are reduced
4. Physics of the Universe (Partially Complete)
  1. gravity works perfectly
  2. drag is applied to all objects except celestial bodies based on the inverse square distance from gravity sources
    1. results in a KSP-like "athmospheric soup"
5. Title and Menu scene (1st iteration)
6. World Building
  1. Lore
    2. "I'm going to get someone to build this"
  2. Current State
    3. Planets
      4. Names
      5. Occupants
      6. Resources
      7. Features
      8. etc...
    3. Who is in the universe?
    4. Why are they where they are?
    5. What are they like?
    6. What are the currently doing?
    6. When did they get here?
    7. What are major events happening in the universe right now?
    8. etc...

####*Planned*
1. Enemies
2. Evolution of enemies 
3. Combat
4. Multiplayer Aspect
5. Market/Shops
6. Loot Drops
7. Inventory
8. Hats
9. User Interface / HUD

####**Improvements**
1. Every physics update, objects check a lot of gravity. If needed, we can set a cutoff distance in the once-per-number-of-seconds calculation. 

2. Comparing the sqrmagnitude of vectors and distances gives the same values as comparing their magnitude minus the expensive sqrt calculation.

3. A lot of the textures have extra render effects that are not needed/can be done in better ways, fixing these could improve performance

####**Suggestions or Ideas?**
Create an issue with the tag "Suggestion" and we will/may implement it!

Age of CAS III: Return of Mrs. Miller
====================

Engine: Unity3D + [Insert cool name here]: Node.js server by Peter Fajner 

Genre: MMORPG

Features: It's a Divinity-Like clone-ish RPG MO

Platforms: Windows, perhaps OSX and Linux

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
  2. Turnbased
  3. Realtime
2. Camera Controls
3. Loot Drops
4. Inventory
5. Hats?

####*Planned*
1. Enemies
2. Evolution of enemies 
3. Combat

####**Improvements**
1. Every physics update, objects check a lot of gravity. If needed, we can set a cutoff distance in the once-per-number-of-seconds calculation. 
2. Comparing the sqrmagnitude of vectors and distances gives the same values as comparing their magnitude minus the expensive sqrt calculation.


Age of CAS III: Return of Mrs. Miller
====================

Engine: Unity3D + [Insert cool name here]: Node.js server by Peter Fajner 

Genre: MMORPGRTS

Features: It's a Divinity-Like clone-ish RPG MO in space?

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
  1. works *adequately*
  2. torso rotation towards the normal of the total gravity vector implemented
    1. may be changed to the normal of the strongest gravity vector
2. Camera Controls
  1. rotation implemented, but is buggy
3. Instancing
  1. most likely will involve separate planet instance and the universe instance, separated by opaque cloud layers
4. Physics of the Universe (Partially Complete)
  1. gravity works perfectly
  2. drag is applied to all objects except celestial bodies based on the inverse square distance from gravity sources
    1. results in a KSP-like "athmospheric soup"

####*Planned*
1. Enemies
2. Evolution of enemies 
3. Combat
4. Multiplayer Aspect
5. Market/Shops
6. Loot Drops
7. Inventory
8. Hats?

####**Improvements**
1. Every physics update, objects check a lot of gravity. If needed, we can set a cutoff distance in the once-per-number-of-seconds calculation. 

2. Comparing the sqrmagnitude of vectors and distances gives the same values as comparing their magnitude minus the expensive sqrt calculation.

####**Suggestions or Ideas?**
Create an issue with the tag "Suggestion" and we will may implement it!

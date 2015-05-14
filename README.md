Age of CAS IV: *Cassmo Strikes Back*
====================

[**Find us at our website**](https://rting.github.io/Cassmo_Website_and_Wiki/)

**Engine:** Unity3D + Custom Server Software

**Genre:** MMORPGRTS Command Simulation Rogue-Lite

**Features:** It's a Divinity-Like Space Engineers related clone-ish Multiplayer Online Shooter Strategy Game in space with teamwork aspects

**Platforms:** Windows. Perhaps OSX and Linux later in development

Created and Worked on by [Eric M](https://github.com/ErisMik), [Peter F](https://github.com/PeterFajner) and [Troy H](https://github.com/TroyNH)

### State of the Game
*(Updated December 4, 2014)*

In short, it's not really a game yet; all of the work up to this point in time is in the Walking Test Unity project. It is, and will continue to be a collection of tests and small implementations of features until we feel confident in starting the next stage of the project, the actual game itself.

### The Repository
Currently the repository is in 3 parts. 2 testing and development projects (Walking Test and Testing Grounds) and the official game project.

### Features:

#### *Implemented*
- None :P

#### *Tested*
- Universal gravity
  - Every rigidbody is attracted to every other rigidbody
  - Mass can be set manually except for Celestial Bodies, where it is calculated using the spheroid volume formula and the density setting
  - Artificial gravity sources implemented (insufficiently tested)
  - Universal drag system, drag increases with inverse of distance from gravity source, does not affect Celestial Bodies.
  - TODO: Hard mode, drag system affects Celestial Bodies, you get to stationkeep your moons.
  
#### *In Developement*
1. Character Animation and Movement
2. Ships and Ship Movement
2. Camera Controls
  1. Rotation tested, but is buggy
3. Instancing
  1. Planets will be seperate levels that are loaded when the player collides with the translucent cloud layer
    1. Will Begin to preload scene as you approach planet, so when you hit cloud layer load times are as reduced as possible
4. Physics of the Universe
  1. Planets don't spin yet
  2. Ships gravity needs to be worked to create inertail like dampeners, but still have gravity affect them 
5. Title and Menu scene
6. World Building
  1. Lore
     1. Current State
      1. Planets
        1. Names
        2. Occupants
        3. Resources
        4. Features
        5. etc...
      2. Who is in the universe?
      3. Why are they where they are?
      4. What are they like?
      5. What are the currently doing?
      6. When did they get here?
      7. What are major events happening in the universe right now?
      8. etc...
    2. History of the universe

#### *Planned*
1. Enemies
2. Evolution of enemies 
3. Combat
4. Multiplayer Aspect
5. Market/Shops
6. Loot Drops
7. Inventory
8. Hats
9. User Interface / HUD
10. Storyline (Campaign)
11. Additional Content

### Improvements
1. Every physics update, objects check a lot of gravity. If needed, we can set a cutoff distance in the once-per-number-of-seconds calculation. 

2. Comparing the sqrmagnitude of vectors and distances gives the same values as comparing their magnitude minus the expensive sqrt calculation.

3. A lot of the textures have extra render effects that are not needed/can be done in better ways, fixing these could improve performance

### License
This game is under the GPLv3 License

### Suggestions or Ideas?
Create an issue with the tag "Suggestion" and we will/may implement it!
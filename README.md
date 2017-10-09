# The White Mask
Artists using "smart graffiti" have a responsibility to know everything they can about how their art communicates with the living world - since smart graffiti can watch, listen, and even interact with its surroundings, there's plenty of opportunity for hijacking and misusing the art, especially with the enormous money and power that the oppressive government and its supporters can bring to bear.

This is where you come in.

You're a hacker whose experience with smart graffiti lets you find all the vulnerabilities any mural has left exposed - open doors, poor security, and more are all ready to be patched up for good after you put murals through every trial you can think of.

Stay one step ahead of malicious hackers, government and otherwise, as you protect artists, their audience, and their work, sneaking a glimmer of hope into a cruel and controlled atmosphere.
## Table of Contents
1. [Anatomy of a Level](#anatomy-of-a-level)
    1. [`State` class](#state-class)
    1. [`Board` class](#board-class)
    1. Actors

## Anatomy of a Level
The levels in The White Mask (WM) are run with a central state machine as the source of truth for that level. Each level will contain one GameObject called `Game Manager`, which will have the scripts `State` and `StateBus` attached.

### `State` class
The main state machine for a level. The State is the source of truth for the level.

#### Properties
* `board` - Instance of the current Board. Created from the Board's default Snapshot.

### `Board` class
Class that holds all information about a given game board, including paths to art, different board configurations, animations, board layout, etc.

#### Properties

#### Methods
* `CreateSnapshot()` - Returns a Snapshot, a string that represents the Board in its current state
* `LoadFromSnapshot(string snapshot)` - Creates a Board from a Snapshot.

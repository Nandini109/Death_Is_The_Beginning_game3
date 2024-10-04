# 2D SideScroller Read Me

## Player Data
You are encouraged to make a new Player Data Scriptable Object for each iteration of your player, rather than modifying the pre-existing one. This will help you ave time in development.

There are numerous parameters in this player data controller:
### Player Movement Parameters
* MovementSpeed: this controls how fast your character can move (max speed)
* MaxAcceleration: this is the acceleration amount for your character
* MaxDeceleration: this is how fast your character slows down
* MaxTurnSpeed: this is the turn speed for your character. Usually you want a character to turn faster than they would normally accelerate

### Player Jump Parameters
* JumpHeight: this is the overall height your character can jump to
* TimeToApex: this is how quickly your character will take to jump to the JumpHeight
* UpMovementMultiplier: this is what you multiply gravity by when going upwards
* DownMovementMultiplier: this is what you multiply gravity by when going downards
* GravityJumpcutOff: once releasing jump button, before descending, this is what to set gravity to
* VariableJumpHeight: whether you want to be able to hold jump to go to max height, or whether you will cut off the jump when releasing

### Air Movement Parameters
* AirMaxAcceleration: this is your acceleration for movement while in the air
* AirMaxDeceleration: this is your deceleration for movement while in the air
* AirMaxTurnSpeed: this is your turn speed while in the air

### Jumping Modifiers
* CoyoteTime: how long after you leave a ledge that you can jump
* JumpBufferTime: if you press within the jump buffer time, in the air, and land, then you will jump up immediately
* DefaultGravityScale: the default gravity in your game
* TerminalVelocity: How fast your character can fall


### Player Event Parameters
Whenever a player enters a new state, that state is sent out, as well as the player, so that this can be hooked up to various events in your game. You can then use these to trigger things like audio, effects, things in your scene, etc.
* PlayerSpawns: Event happens when player is created
* PlayerIdles, PlayerRuns, PlayerInAir, PlayerJumps: handles these respective state enters respectively
* PlayerDeath: Happens whenever the player dies
* PlayerUpdate: Happens on every update





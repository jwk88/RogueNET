# RogueNET

**RogueNET** is an experimental roguelike gameplay framework written in
**C# / .NET 10**.

The project explores a **fully modular interaction system** where
gameplay behavior is composed from small, decoupled components. The
architecture follows **SOLID principles** and uses **dependency
injection**, allowing entities to interact through capabilities rather
than hard‑coded relationships.

Instead of scripting outcomes directly, entities expose behaviors such
as *Openable*, *Lockable*, *Lootable*, and *Usable*. The player can
repeatedly combine interactions and items, and the system resolves the
result through the interfaces available on the entities involved.

The goal is a system where **gameplay emerges from logical composition
rather than rigid scripting.**

------------------------------------------------------------------------

# Example Scenario
Legend

  Symbol   Meaning
  -------- --------------
  `@`      Player
  `C`      Wooden Chest
  `D`      Steel Door
  `.`      Empty floor

The player starts in the middle of the room.\
A **Wooden Chest** is located on the left side and a **Steel Door** is
on the right side.

------------------------------------------------------------------------

# Test Code

The following code drives the interaction sequence.

``` csharp
// Place the entitis:
entityManager.Setup();
entityManager.Draw();
        
// Player steps to the right seven times, collides with 'Door'
entityManager.Player.StepRight(7);

// Stands now left of the door, interracts to the right, but the door is locked
entityManager.Player.Interract(1, 0);
entityManager.Draw();

// Player steps to the left seven times, collides with 'Chest'
entityManager.Player.StepLeft(7);

// Player tries to loot to the left, but the chest is closed
entityManager.Player.Loot(-1, 0);

// Player interracts to the left, opening the chest
entityManager.Player.Interract(-1, 0);

// Player tries to loot again to the left, and as chest is now open, gets the contents
entityManager.Player.Loot(-1, 0);
entityManager.Draw();

// Player steps seven times to the right, colliding with the door
entityManager.Player.StepRight(7);

// player uses first item in inventory, to the right
// a key -> lock check is made, unlocking the handle on the door
entityManager.Player.UseItem(index: 0, 1, 0);

// player interracts to the right, opening the door (not locked anymore)
entityManager.Player.Interract(1, 0);
entityManager.Draw();
```

------------------------------------------------------------------------

# Runtime Output

```
-----------------------------------------
[Info] 'John Doe' has been spawned at {5:4}
[Info] 'Door' has been spawned at {9:4}
[Info] 'Container' has been spawned at {2:4}
 
*-------*
|.......|
|.......|
|C..@...D
|.......|
|.......|
*-------*

-----------------------------------------
[Info] 'John Doe' is moving from {5:4} to {6:4}
[Info] 'John Doe' is moving from {6:4} to {7:4}
[Info] 'John Doe' is moving from {7:4} to {8:4}
[Info] 'John Doe' {8:4} path was blocked by 'Steel Door' {9:4}
[Info] 'John Doe' {8:4} interracts with 'Steel Door' {9:4}
[Info] 'Steel Door' {9:4} is locked
 
*-------*
|.......|
|.......|
|C.....@D
|.......|
|.......|
*-------*

-----------------------------------------
[Info] 'John Doe' is moving from {8:4} to {7:4}
[Info] 'John Doe' is moving from {7:4} to {6:4}
[Info] 'John Doe' is moving from {6:4} to {5:4}
[Info] 'John Doe' is moving from {5:4} to {4:4}
[Info] 'John Doe' is moving from {4:4} to {3:4}
[Info] 'John Doe' {3:4} path was blocked by 'Wooden Chest' {2:4}
[Info] 'John Doe' {3:4} tries to loot 'Wooden Chest' {2:4}
[Info] 'Wooden Chest' {2:4} is closed!
[Info] 'John Doe' {3:4} interracts with 'Wooden Chest' {2:4}
[Info] 'Wooden Chest' {2:4} is now open
[Info] 'John Doe' {3:4} tries to loot 'Wooden Chest' {2:4}
[Info] 'John Doe' {3:4} placed 'Silver Key' {-,-} in their inventory
 
*-------*
|.......|
|.......|
|C@.....D
|.......|
|.......|
*-------*

-----------------------------------------
[Info] 'John Doe' is moving from {3:4} to {4:4}
[Info] 'John Doe' is moving from {4:4} to {5:4}
[Info] 'John Doe' is moving from {5:4} to {6:4}
[Info] 'John Doe' is moving from {6:4} to {7:4}
[Info] 'John Doe' is moving from {7:4} to {8:4}
[Info] 'John Doe' {8:4} path was blocked by 'Steel Door' {9:4}
[Info] 'John Doe' {8:4} uses 'Silver Key' {-,-} on 'Steel Door' {9:4}
[Info] 'Steel Door' {9:4} was owned by 'Steel Door Handle' {-,-}
[Info] 'John Doe' {8:4} used 'Silver Key' {-,-} to open 'Steel Door Handle' {-,-} on 'Steel Door' {9:4}
[Info] 'John Doe' {8:4} interracts with 'Steel Door' {9:4}
[Info] 'Steel Door' {9:4} is now open
 
*-------*
|.......|
|.......|
|C.....@D
|.......|
|.......|
*-------*
```

------------------------------------------------------------------------

# Interaction Flow

The chain of gameplay emerges from modular behaviors:

1.  Player attempts to open a **locked door**
2.  Player encounters a **closed chest**
3.  Chest must first be **opened**
4.  Player **loots a key**
5.  Key is used on the **door lock**
6.  The **door handle unlocks**
7.  The **door can now be opened**

No direct scripting exists between the chest, key, or door.\
Each interaction resolves through the interfaces exposed by the entities
involved.

------------------------------------------------------------------------

# Project Status

RogueNET is currently **early stage** and primarily serves as an
exploration of modular roguelike architecture.

Current focus areas:

-   Modular entity composition
-   Interface‑driven gameplay interactions
-   Decoupled action resolution
-   Dependency injection for gameplay systems
-   Logical interaction chains instead of scripted events

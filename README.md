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

Initial world layout:

    ***********
    *.........*
    *C...@....D
    *.........*
    ***********

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
// place the entities
entityManager.Setup();

// Player starts at the middle
// Steel Door to the right
// Wooden Chest to the left

// move player to the right four times
for (int i = 0; i < 4; i++)
{
    entityManager.Player.Move(1, 0);
}

// stands left of the door, tries to interact
// but the door is locked
entityManager.Player.Interract(1, 0);

// move player left until reaching the chest
for (int i = 0; i < 9; i++)
{
    entityManager.Player.Move(-1, 0);
}

// attempt to loot the chest (fails because closed)
entityManager.Player.Loot(-1, 0);

// interact with the chest to open it
entityManager.Player.Interract(-1, 0);

// loot again (now succeeds)
entityManager.Player.Loot(-1, 0);

// walk back to the door
for (int i = 0; i < 9; i++)
{
    entityManager.Player.Move(1, 0);
}

// use the first item in inventory on the door
// the key unlocks the door handle
entityManager.Player.UseItem(0, 1, 0);

// interact with the door again
// now it opens
entityManager.Player.Interract(1, 0);
```

------------------------------------------------------------------------

# Runtime Output

    [Info] 'John Doe' is moving from {64:91} to {65:91}
    [Info] 'John Doe' is moving from {65:91} to {66:91}
    [Info] 'John Doe' is moving from {66:91} to {67:91}
    [Info] 'John Doe' is moving from {67:91} to {68:91}
    [Info] 'John Doe' {68:91} interracts with 'Steel Door' {69:91}
    [Info] 'Steel Door' {69:91} is locked
    [Info] 'John Doe' is moving from {68:91} to {67:91}
    [Info] 'John Doe' is moving from {67:91} to {66:91}
    [Info] 'John Doe' is moving from {66:91} to {65:91}
    [Info] 'John Doe' is moving from {65:91} to {64:91}
    [Info] 'John Doe' is moving from {64:91} to {63:91}
    [Info] 'John Doe' is moving from {63:91} to {62:91}
    [Info] 'John Doe' is moving from {62:91} to {61:91}
    [Info] 'John Doe' is moving from {61:91} to {60:91}
    [Info] 'John Doe' {60:91} path was blocked by 'Wooden Chest' {59:91}
    [Info] 'John Doe' {60:91} tries to loot 'Wooden Chest' {59:91}
    [Info] 'Wooden Chest' {59:91} is closed!
    [Info] 'John Doe' {60:91} interracts with 'Wooden Chest' {59:91}
    [Info] 'Wooden Chest' {59:91} is now open
    [Info] 'John Doe' {60:91} tries to loot 'Wooden Chest' {59:91}
    [Info] 'John Doe' {60:91} placed 'Silver Key' {-,-} in their inventory
    [Info] 'John Doe' is moving from {60:91} to {61:91}
    [Info] 'John Doe' is moving from {61:91} to {62:91}
    [Info] 'John Doe' is moving from {62:91} to {63:91}
    [Info] 'John Doe' is moving from {63:91} to {64:91}
    [Info] 'John Doe' is moving from {64:91} to {65:91}
    [Info] 'John Doe' is moving from {65:91} to {66:91}
    [Info] 'John Doe' is moving from {66:91} to {67:91}
    [Info] 'John Doe' is moving from {67:91} to {68:91}
    [Info] 'John Doe' {68:91} path was blocked by 'Steel Door' {69:91}
    [Info] 'John Doe' {68:91} uses 'Silver Key' {-,-} on 'Steel Door' {69:91}
    [Info] 'Steel Door' {69:91} was owned by 'Steel Door Handle' {-,-}
    [Info] 'John Doe' {68:91} used 'Silver Key' {-,-} to open 'Steel Door Handle' {-,-} on 'Steel Door' {69:91}
    [Info] 'John Doe' {68:91} interracts with 'Steel Door' {69:91}
    [Info] 'Steel Door' {69:91} is now open

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

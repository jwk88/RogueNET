# RogueNET

**RogueNET** is an experimental roguelike gameplay framework written in **C# / .NET 10**.

The project explores a **fully modular interaction system** where gameplay behavior is composed from small, decoupled components. The architecture follows **SOLID principles** and uses **dependency injection**, allowing entities to interact through capabilities rather than hard-coded relationships.

Instead of scripting outcomes directly, entities expose behaviors such as *Openable*, *Lockable*, *Lootable*, and *Usable*. The player can repeatedly combine interactions and items, and the system resolves the result through the interfaces available on the entities involved.

The goal is a system where **gameplay emerges from logical composition rather than rigid scripting.**

---

# Dungeon Generation (early)

![preview](preview.gif)

# Example Gameplay Scenarios

### Legend

| Symbol | Meaning      |
| ------ | ------------ |
| `@`    | Player       |
| `C`    | Wooden Chest |
| `D`    | Steel Door   |
| `r`    | Rat          |

The player starts in the middle of the room.
A **Wooden Chest** is located on the left side and a **Steel Door** is on the right side.

---

# Test Code

The automated tests implement the interaction sequences.

* [`Test1.Run()`](src/test/Test1.cs#L40-L58)
* [`Test1b.Run()`](src/test/Test1b.cs#L8-L23)

---

# Automated Test Outputs

The project includes automated tests that validate the interaction system.
The outputs are embedded below for quick viewing.

<details>
<summary>Test1 Output</summary>

```text
              
 *-----------*
 |       r   |
 |           |
 |C    @     D
 |           |
 |           |
 *-----------*

              
 *-----------*
 |       r   |
 |           |
 |C     @    D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {7:4} to {8:4}
              
 *-----------*
 |       r   |
 |           |
 |C      @   D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {8:4} to {9:4}
              
 *-----------*
 |       r   |
 |           |
 |C       @  D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {9:4} to {10:4}
              
 *-----------*
 |       r   |
 |           |
 |C        @ D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {10:4} to {11:4}
              
 *-----------*
 |       r   |
 |           |
 |C         @D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {11:4} to {12:4}
              
 *-----------*
 |       r   |
 |           |
 |C         @D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' {12:4} path was blocked by 'Steel Door' {13:4}
              
 *-----------*
 |       r   |
 |           |
 |C         @D
 |           |
 |           |
 *-----------*

[ACTION] INTERRACT
[Info] 'John Doe' {12:4} interracts with 'Steel Door' {13:4}
[Info] 'Steel Door' {13:4} is locked
              
 *-----------*
 |       r   |
 |           |
 |C         @D
 |           |
 |           |
 *-----------*

              
 *-----------*
 |       r   |
 |           |
 |C        @ D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {12:4} to {11:4}
              
 *-----------*
 |       r   |
 |           |
 |C       @  D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {11:4} to {10:4}
              
 *-----------*
 |       r   |
 |           |
 |C      @   D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {10:4} to {9:4}
              
 *-----------*
 |       r   |
 |           |
 |C     @    D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {9:4} to {8:4}
              
 *-----------*
 |       r   |
 |           |
 |C    @     D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {8:4} to {7:4}
              
 *-----------*
 |       r   |
 |           |
 |C   @      D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {7:4} to {6:4}
              
 *-----------*
 |       r   |
 |           |
 |C  @       D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {6:4} to {5:4}
              
 *-----------*
 |       r   |
 |           |
 |C @        D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {5:4} to {4:4}
              
 *-----------*
 |       r   |
 |           |
 |C@         D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {4:4} to {3:4}
              
 *-----------*
 |       r   |
 |           |
 |C@         D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' {3:4} path was blocked by 'Wooden Chest' {2:4}
              
 *-----------*
 |       r   |
 |           |
 |C@         D
 |           |
 |           |
 *-----------*

[ACTION] LOOT
[Info] 'John Doe' {3:4} tries to loot 'Wooden Chest' {2:4}
[Info] 'Wooden Chest' {2:4} is closed!
              
 *-----------*
 |       r   |
 |           |
 |C@         D
 |           |
 |           |
 *-----------*

[ACTION] INTERRACT
[Info] 'John Doe' {3:4} interracts with 'Wooden Chest' {2:4}
[Info] 'Wooden Chest' {2:4} is now open
              
 *-----------*
 |       r   |
 |           |
 |C@         D
 |           |
 |           |
 *-----------*

[ACTION] LOOT
[Info] 'John Doe' {3:4} tries to loot 'Wooden Chest' {2:4}
[Info] 'John Doe' {3:4} placed 'Silver Key' {-,-} in their inventory
              
 *-----------*
 |       r   |
 |           |
 |C@         D
 |           |
 |           |
 *-----------*

              
 *-----------*
 |       r   |
 |           |
 |C @        D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {3:4} to {4:4}
              
 *-----------*
 |       r   |
 |           |
 |C  @       D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {4:4} to {5:4}
              
 *-----------*
 |       r   |
 |           |
 |C   @      D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {5:4} to {6:4}
              
 *-----------*
 |       r   |
 |           |
 |C    @     D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {6:4} to {7:4}
              
 *-----------*
 |       r   |
 |           |
 |C     @    D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {7:4} to {8:4}
              
 *-----------*
 |       r   |
 |           |
 |C      @   D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {8:4} to {9:4}
              
 *-----------*
 |       r   |
 |           |
 |C       @  D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {9:4} to {10:4}
              
 *-----------*
 |       r   |
 |           |
 |C        @ D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {10:4} to {11:4}
              
 *-----------*
 |       r   |
 |           |
 |C         @D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {11:4} to {12:4}
              
 *-----------*
 |       r   |
 |           |
 |C         @D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' {12:4} path was blocked by 'Steel Door' {13:4}
              
 *-----------*
 |       r   |
 |           |
 |C         @D
 |           |
 |           |
 *-----------*

[ACTION] USE ITEM
[Info] 'John Doe' {12:4} uses 'Silver Key' {-,-} on 'Steel Door' {13:4}
[Info] 'Steel Door' {13:4} was owned by 'Steel Door Handle' {-,-}
[Info] 'John Doe' {12:4} used 'Silver Key' {-,-} to open 'Steel Door Handle' {-,-} on 'Steel Door' {13:4}
              
 *-----------*
 |       r   |
 |           |
 |C         @D
 |           |
 |           |
 *-----------*

[ACTION] INTERRACT
[Info] 'John Doe' {12:4} interracts with 'Steel Door' {13:4}
[Info] 'Steel Door' {13:4} is now open
```

</details>

<details>
<summary>Test1b Output</summary>

```text
              
 *-----------*
 |       r   |
 |           |
 |C    @     D
 |           |
 |           |
 *-----------*

              
 *-----------*
 |       r   |
 |           |
 |C   @      D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {7:4} to {6:4}
              
 *-----------*
 |       r   |
 |           |
 |C  @       D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {6:4} to {5:4}
              
 *-----------*
 |       r   |
 |           |
 |C @        D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {5:4} to {4:4}
              
 *-----------*
 |       r   |
 |           |
 |C@         D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {4:4} to {3:4}
              
 *-----------*
 |       r   |
 |           |
 |C@         D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' {3:4} path was blocked by 'Wooden Chest' {2:4}
              
 *-----------*
 |       r   |
 |           |
 | @         D
 |           |
 |           |
 *-----------*

[ACTION] PICK UP
[Info] 'John Doe' {3:4} picked up 'Wooden Chest' {2:4}
              
 *-----------*
 |       r   |
 |           |
 | @         D
 |           |
 |           |
 *-----------*

              
 *-----------*
 |       r   |
 |           |
 |  @        D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {3:4} to {4:4}
              
 *-----------*
 |       r   |
 |           |
 |   @       D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {4:4} to {5:4}
              
 *-----------*
 |       r   |
 |           |
 |    @      D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {5:4} to {6:4}
              
 *-----------*
 |       r   |
 |           |
 |     @     D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {6:4} to {7:4}
              
 *-----------*
 |       r   |
 |           |
 |      @    D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {7:4} to {8:4}
              
 *-----------*
 |       r   |
 |           |
 |       @   D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {8:4} to {9:4}
              
 *-----------*
 |       r   |
 |       @   |
 |           D
 |           |
 |           |
 *-----------*

[Info] 'John Doe' is moving from {9:4} to {9:3}
              
 *-----------*
 |       C   |
 |       @   |
 |           D
 |           |
 |           |
 *-----------*

[ACTION] PUT DOWN
[Info] 'John Doe' {9:3} puts down the 'Wooden Chest' {2:4} he was carrying
[Info] 'Rat' {9:2} (Female) died
              
 *-----------*
 |           |
 |       @   |
 |           D
 |           |
 |           |
 *-----------*

[ACTION] PICK UP
[Info] 'John Doe' {9:3} picked up 'Wooden Chest' {2:4}
```

</details>

---

# Interaction Flow

The chain of gameplay emerges from modular behaviors:

1. Player attempts to open a **locked door**
2. Player encounters a **closed chest**
3. Chest must first be **opened**
4. Player **loots a key**
5. Key is used on the **door lock**
6. The **door handle unlocks**
7. The **door can now be opened**

No direct scripting exists between the chest, key, or door.
Each interaction resolves through the interfaces exposed by the entities involved.

---

# Project Status

RogueNET is currently **early stage** and primarily serves as an exploration of modular roguelike architecture.

Current focus areas:

* Modular entity composition
* Interface-driven gameplay interactions
* Decoupled action resolution
* Dependency injection for gameplay systems
* Logical interaction chains instead of scripted events

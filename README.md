# RogueNET

**RogueNET** is an experimental roguelike gameplay framework written in **C# / .NET 10**.

The project explores a **fully modular interaction system** where gameplay behavior is composed from small, decoupled components. The architecture follows **SOLID principles** and uses **dependency injection**, allowing entities to interact through capabilities rather than hard-coded relationships.

Instead of scripting outcomes directly, entities expose behaviors such as *Openable*, *Lockable*, *Lootable*, and *Usable*. The player can repeatedly combine interactions and items, and the system resolves the result through the interfaces available on the entities involved.

The goal is a system where **gameplay emerges from logical composition rather than rigid scripting.**

---

# Example Scenario

### Legend

| Symbol | Meaning      |
| ------ | ------------ |
| `@`    | Player       |
| `C`    | Wooden Chest |
| `D`    | Steel Door   |

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
(paste contents of Test1.txt here)
```

</details>

<details>
<summary>Test1b Output</summary>

```text
(paste contents of Test1b.txt here)
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

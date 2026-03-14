using System;

public class RogueNET
{
    EntityManager entityManager;

    public RogueNET()
    {
        entityManager = new EntityManager();
    }

    public void Run()
    {
        Console.Clear();

        // place the entitis:
        entityManager.Setup();
        entityManager.Draw();
        
        // move player to the right four times
        entityManager.Player.Move(4, 0);

        // stands now left of the door, interracts to the right, but the door is locked
        entityManager.Player.Interract(1, 0);
        entityManager.Draw();

        // player moves to the left seven times, collides with the chest
        entityManager.Player.Move(-7, 0);

        // player tries to loot to the left, but the chest is closed
        entityManager.Player.Loot(-1, 0);

        // player interracts to the left, opening the chest
        entityManager.Player.Interract(-1, 0);

        // player tries to loot again to the left, and as chest is now open, gets the contents
        entityManager.Player.Loot(-1, 0);
        entityManager.Draw();

        // player moves seven times to the right, colliding with the door
        entityManager.Player.Move(7, 0);

        // player uses first item in inventory, to the right
        // a key -> lock check is made, unlocking the handle on the door
        entityManager.Player.UseItem(0, 1, 0);

        // player interracts to the right, opening the door (not locked anymore)
        entityManager.Player.Interract(1, 0);
        entityManager.Draw();
    }
}

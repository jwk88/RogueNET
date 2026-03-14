using System;

public class RogueNET
{
    EntityManager entityManager;

    static int Seed = 1;

    public RogueNET()
    {
        entityManager = new EntityManager();
    }

    public void Run()
    {
        Console.Clear();

        // Place the entitis:
        entityManager.Setup(Seed);
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
    }
}

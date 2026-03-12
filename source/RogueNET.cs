public class RogueNET
{
    EntityManager entityManager;

    public RogueNET()
    {
        entityManager = new EntityManager();
    }

    public void Run()
    {
        entityManager.Setup();

        for (int i = 0; i < 4; i++)
        {
            entityManager.Player.Move(1, 0);
        }

        entityManager.Player.Interract(1, 0);

        for (int i = 0; i < 9; i++)
        {
            entityManager.Player.Move(-1, 0);
        }

        entityManager.Player.Loot(-1, 0);
        entityManager.Player.Interract(-1, 0);
        entityManager.Player.Loot(-1, 0);

        for (int i = 0; i < 9; i++)
        {
            entityManager.Player.Move(1, 0);
        }

        entityManager.Player.Interract(1, 0);
    }
}

using System;
using System.Collections.Generic;

public class EntityManager
{
    Layers layers;
    Player player;
    public Player Player => player;

    int frameCount;
    int frameOffset;

    public EntityManager()
    {
        layers = new Layers(Config.width, Config.depth, Config.layerHeight);
    }

    // TODO: Clean this, really unreadable
    public void Setup()
    {
        new RoomBuilder(layers, layers[0].Origin, seed: 1);

        player  = new Builder<Player>(layers[1], layers[1].Origin).Build();
        var door    = new Builder<Door>(layers[1], layers[1].RightOf(layers[1].Origin, 4)).Build();
        var chest   = new Builder<Container>(layers[1], layers[1].LeftOf(layers[1].Origin, -3)).Build();

        var key = new Key();
        key.SetName("Silver Key");

        var handle1 = new Handle();
        handle1.SetName("Steel Door Handle");

        var handle2 = new Handle();
        handle1.LockWithKey(key);
        handle2.SetName("Chest Lid");

        door.SetName("Steel Door");
        chest.SetName("Wooden Chest");

        door.Inject(handle1);
        chest.Inject(handle2);
        chest.SetContents(key);
    }

    public void Draw()
    {
        Console.WriteLine();
        Console.WriteLine("-----------------------------------------");

        while (Log.logs.Count > 0)
        {
            Console.WriteLine(Log.logs.Dequeue());
            frameOffset++;
        }

        int depth = layers[0].Depth;
        int width = layers[0].Width;

        for (int y = 0; y < depth; y++)
        {
            char[] line = new char[width];

            for (int x = 0; x < width; x++)
            {
                char symbol = ' ';
                for (int i = 0; i < layers.Height; i++)
                {
                    var owner = layers[i][x, y].Owner;
                    if (owner != null)
                    {
                        symbol = owner.Symbol;
                    }
                }

                line[x] = symbol;
            }

            Console.WriteLine(line);
        }

        frameCount++;
        frameOffset = 0;
        Console.ReadKey();
    }
}
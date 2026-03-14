using System;

public class EntityManager
{
    static World world;
    public Player Player { get; private set; }

    public static int GroundLayer = 0;
    public static int FloorLayer = 1;
    public static int RoofLayer = Config.worldHeight - 1;
    public static int RoomHeight = Config.worldHeight - 2;
    public static Grid Ground => world[GroundLayer];
    public static Grid Floor => world[FloorLayer];
    public static Grid Roof => world[RoofLayer];

    public EntityManager()
    {
        world = new World(Config.width, Config.depth, Config.worldHeight);
    }

    public void Setup(int seed)
    {
        new RoomBuilder(world, Floor.Origin, new Random(seed));

        var spawn = Floor.Origin;
        var playerBuild = new EntityBuilder<Player>(world, FloorLayer, spawn);
        var doorBuild   = new EntityBuilder<Door>(world, FloorLayer, spawn + (Point.Right * 4));
        var chestBuild  = new EntityBuilder<Container>(world, FloorLayer, spawn + (Point.Left * 3));

        doorBuild.SetCustomHeight(RoomHeight);

        var player = playerBuild.Build();
        var door = doorBuild.Build(overwrite: true);
        var chest = chestBuild.Build();

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

        Player = player;
    }

    public void Draw()
    {
        Console.WriteLine();
        Console.WriteLine("-----------------------------------------");

        while (Log.logs.Count > 0)
        {
            Console.WriteLine(Log.logs.Dequeue());
        }

        int depth = Ground.Depth;
        int width = Ground.Width;

        for (int y = 0; y < depth; y++)
        {
            char[] line = new char[width];

            for (int x = 0; x < width; x++)
            {
                char symbol = ' ';
                for (int i = 0; i < world.Height - 1; i++)
                {
                    var owner = world[i][x, y].Owner;
                    if (owner != null)
                    {
                        symbol = owner.Symbol;
                    }
                }

                line[x] = symbol;
            }

            Console.WriteLine(line);
        }

        Console.ReadKey();
    }
}
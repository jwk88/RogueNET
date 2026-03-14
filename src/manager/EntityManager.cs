using System;
using System.Collections.Generic;

public class EntityManager
{
    Grid grid;
    Point origin;
    Dictionary<Type, Entity> entities;

    public EntityManager()
    {
        grid = new Grid(Config.width, Config.height);
        var originX = Config.width / 2;
        var originY = Config.height / 2;

        origin = grid[originX, originY].Point;
        entities = new Dictionary<Type, Entity>();
    }

    public void Setup()
    {
        // TODO: generate the positions and layout procedurally later

        var DungeonBuilder = new DungeonBuilder();
        
        var builders = new List<Builder>();
        builders.Add(new Builder(typeof(Player), grid, origin));
        builders.Add(new Builder(typeof(Door), grid, grid.RightOf(origin, 5)));
        builders.Add(new Builder(typeof(Container), grid, grid.LeftOf(origin, -5)));
        builders.ForEach(x => x.Build((entity, type) => entities.Add(type, entity)));

        var key = new Key();
        key.SetName("Silver Key");

        var handle1 = new Handle();
        handle1.SetName("Steel Door Handle");

        var handle2 = new Handle();
        handle1.LockWithKey(key);
        handle2.SetName("Chest Lid");

        foreach (var pair in entities)
        {
            // TODO: create player character generation and set name etc for player
            if (pair.Key == typeof(Door))
            {
                var door = pair.Value as Door;
                door.Inject(handle1);
                door.SetName("Steel Door");
            }
            if (pair.Key == typeof(Container))
            {
                var chest = pair.Value as Container;
                chest.Inject(handle2);
                chest.SetContents(key);
                chest.SetName("Wooden Chest");
            }
            if (pair.Key == typeof(Player))
            {
                pair.Value.SetName("John Doe");
            }
        }
    }

    public Player Player => (Player)entities[typeof(Player)];
}
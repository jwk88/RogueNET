using System;

public class Test1 : Test
{
    public Player player { get; }

    public Test1()
    {
        grid = new Grid(10, 8);
        new RoomBuilder(grid, grid.Origin, new Random(1));

        var spawn = grid.Origin;
        var playerBuild = new EntityBuilder<Player>(grid, spawn);
        var doorBuild   = new EntityBuilder<Door>(grid, spawn + (Point.Right * 4));
        var chestBuild  = new EntityBuilder<Container>(grid, spawn + (Point.Left * 3));

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

        this.player = player;
    }

    public override bool Run(bool initialize = false)
    {   
        player.StepRight(7);
        player.Interract(1, 0);
        console.Draw(grid);

        player.StepLeft(7);
        player.Loot(-1, 0);
        player.Interract(-1, 0);
        player.Loot(-1, 0);
        console.Draw(grid);

        player.StepRight(7);
        player.UseItem(index: 0, 1, 0);
        player.Interract(1, 0);
        console.Draw(grid);

        var output = console.GetOutput();
        return Validate(output, initialize);
    }
}

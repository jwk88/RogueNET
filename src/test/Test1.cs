using System;

public class Test1 : Test
{
    public Player player { get; }

    public Test1()
    {
        grid = new Grid();
        new RoomBuilder(grid, grid.Origin);

        var spawn = grid.Origin;
        var playerBuild = new EntityBuilder<Player>(grid, spawn);
        var doorBuild   = new EntityBuilder<Door>(grid, spawn + (Point.Right * 6));
        var chestBuild  = new EntityBuilder<Container>(grid, spawn + (Point.Left * 5));
        var ratBuild = new EntityBuilder<Rat>(grid, spawn + (Point.Up * 2) + (Point.Right * 2));

        var player = playerBuild.Build();
        var door = doorBuild.Build(overwrite: true);
        var chest = chestBuild.Build();
        var rat = ratBuild.Build();

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
        console.Title = "Test 1";
    }

    public override bool Run(bool initialize = false)
    {
        player.StepRight(7, () => { console.Draw(grid); });
        console.Draw(grid);
        player.Interract(1, 0);
        console.Draw(grid);

        player.StepLeft(10, () => { console.Draw(grid); });
        console.Draw(grid);
        player.Loot(-1, 0);
        console.Draw(grid);
        player.Interract(-1, 0);
        console.Draw(grid);
        player.Loot(-1, 0);
        console.Draw(grid);

        player.StepRight(12, () => { console.Draw(grid); });
        console.Draw(grid);
        player.UseItem(index: 0, 1, 0);
        console.Draw(grid);
        player.Interract(1, 0);
        console.Draw(grid);

        var output = console.GetOutput();
        return Validate(output, initialize);
    }
}

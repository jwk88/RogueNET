public class Test1b : Test1
{
    public Test1b()
    {
        console.Title = "Test 1B";
    }

    public override bool Run(bool initialize = false)
    {
        player.StepLeft(9, () => { console.Draw(grid); });
        console.Draw(grid);

        player.Pickup(-1, 0);
        console.Draw(grid);

        player.StepRight(6, () => { console.Draw(grid); });
        player.StepUp(1, () => { console.Draw(grid); });
        console.Draw(grid);

        player.Putdown(0, -1);
        console.Draw(grid);

        player.Pickup(0, -1);
        console.Draw(grid);

        var output = console.GetFullOutput();
        return Validate(output, initialize);
    }
}

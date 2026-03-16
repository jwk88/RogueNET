public class Test1b : Test1
{
    public Test1b()
    {
        
    }

    public override bool Run(bool initialize = false)
    {
        player.StepLeft(7);
        console.Draw(grid);

        player.Pickup(-1, 0);
        console.Draw(grid);

        player.StepRight(3);
        console.Draw(grid);

        player.Putdown(0, -1);
        console.Draw(grid);

        var output = console.GetOutput();
        return Validate(output, initialize);
    }
}

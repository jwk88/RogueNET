using System;
using System.Collections.Generic;

public class RoomBuilder
{
    Layers layers;
    Random rng;
    Dictionary<Point, Entity> entities = new Dictionary<Point, Entity>();

    public RoomBuilder(Layers layers, Point origin, int seed)
    {
        this.layers = layers;

        // TOOD: ran out of time so just use the bottom floor layer for the room for now.
        // Later, implement so that it fills 'Floor' entity on layer[0], then 'Wall's up per layer, 'Roof' on last

        Fill(new Builder<Wall>(layers[0], new Point(0,0))); // remember layer 0 
        rng = new Random(seed);
        
        var rWidth = rng.Next(Config.roomMinWidth, Config.roomMaxWidth);
        var rHeight = rng.Next(Config.roomMinDepth, Config.roomMaxDepth);

        FormatRoom(origin, rWidth, rHeight);
    }

    public Dictionary<Point, Entity> Entities => entities;

    void Fill<T>(Builder<T> builder) where T : Entity
    {
        for (int y = 0; y < layers[0].Depth; y++) // remember layer 0 
        {
            for (int x = 0; x < layers[0].Width; x++) // remember layer 0 
            {
                builder.Retarget(x, y);
                var filler = builder.Build();
                entities.Add(filler.Node.Point, filler);
            }
        }
    }

    void FormatRoom(Point point, int width, int height)
    {
        var xMin = point.X - (width / 2) + 1;
        var yMin = point.Y - (height / 2) + 1;
        var xMax = point.X + (width / 2) - 1;
        var yMax = point.Y + (height / 2) - 1;

        foreach (var node in layers[0]) // remember layer 0 
        {
            var p = node.Point; 
            var owner = layers[0][p.X, p.Y].Owner; // remember layer 0 

            if (p.X < xMin || p.Y < yMin || p.X > xMax || p.Y > yMax)
            {
                entities.Remove(p);
                owner = null;
                continue;
            }

            if ((p.X == xMin || p.X == xMax) && (p.Y == yMin || p.Y == yMax))
                owner.SetSymbol('*');
            else if (p.X == xMin || p.X == xMax)
                owner.SetSymbol('|');
            else if (p.Y == yMin || p.Y == yMax)
                owner.SetSymbol('-');
            else
                owner.SetSymbol('.');
        }
    }
}
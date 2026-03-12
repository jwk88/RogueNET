using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Container : Door
{
    List<Entity> contents;

    public Container()
    {
        contents = new List<Entity>();
    }

    public virtual void SetContents(params Entity[] contents)
    {
        this.contents.AddRange(contents);
    }

    public void PickupContents(Actor actor)
    {
        Log.Info($"{actor} tries to loot {this}");
        if (handle.IsOpen)
        {
            if (contents.Count == 0)
            {
                Log.Info($"{this} is empty!");
                return;
            }

            foreach (var content in contents)
            {
                actor.Pickup(content);
            }

            contents.Clear();
        }
        else
        {
            Log.Info($"{this} is closed!");
        }
    }
}
using System.Collections.Generic;

public class Container : Door, ILootable
{
    List<Entity> contents;

    public Container()
    {
        contents = new List<Entity>();

        SetSymbol('C');
        SetName("Container");

        stats = new Stats();
        stats.SetWeight(50);
    }

    public virtual void SetContents(params Entity[] contents)
    {
        this.contents.AddRange(contents);
    }

    public void LootFor(Actor actor)
    {
        Log.Info($"{actor} tries to loot {this}");
        if (openable.IsOpen)
        {
            if (contents.Count == 0)
            {
                Log.Info($"{this} is empty!");
                return;
            }

            foreach (var content in contents)
            {
                if (content is IPickupable)
                {
                    var pickup = content as IPickupable;
                    pickup.PickedUpBy(actor);
                }
            }

            contents.Clear();
        }
        else
        {
            Log.Info($"{this} is closed!");
        }
    }
}
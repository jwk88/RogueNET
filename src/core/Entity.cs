namespace RogueNET
{
    using System;

    public abstract class Entity : EntityBase
    {
        protected Grid grid;
        protected Point point;
        protected Stats stats;
        protected Node Node => grid[point];

        Entity carry;
        bool isPlaced;
        Point direction;
    
        public Stats Stats => stats;
        public Entity Carry => carry;
        public Point Direction => direction;
        public Point Point => point;

        public void SetStats(Stats stats)
        {
            this.stats = stats;
        }

        public virtual void SetGrid(Grid grid)
        {
            this.grid = grid;
        }

        public virtual void SetCarry(Entity entity)
        {
            carry = entity;
        }

        public void Step(Point direction)
        {
            SetPosition(Node.Point + direction);
        }

        public void StepUp(int count, Action onStep = null)
        {
            for (int i = 0; i < count; i++)
            {
                onStep?.Invoke();
                if (!SetPosition(point.X, point.Y - 1))
                {
                    break;
                }
            }
        }

        public void StepDown(int count, Action onStep = null)
        {
            for (int i = 0; i < count; i++)
            {
                onStep?.Invoke();
                if (!SetPosition(point.X, point.Y + 1))
                {
                    break;
                }
            }
        }

        public void StepRight(int count, Action onStep = null)
        {
            for (int i = 0; i < count; i++)
            {   
                onStep?.Invoke();
                if (!SetPosition(point.X + 1, point.Y))
                {
                    break;
                }
            }
        }

        public void StepLeft(int count, Action onStep = null)
        {
            for (int i = 0; i < count; i++)
            {
                onStep?.Invoke();
                if (!SetPosition(point.X - 1, point.Y))
                {
                    break;
                }
            }
        }

        public bool SetPosition(int x, int y, bool overwrite = false) => SetPosition(new Point(x, y), overwrite);
        public bool SetPosition(Point point, bool overwrite = false)
        {
            var next = grid[point];

            if (next.Occupied && !overwrite)
            {
                Log.Info($"{this} path was blocked by {next.Owner}");
                return false;
            }

            Point prevPoint = Point.Zero;
            if (Node != null)
            {
                prevPoint = Node.Point;
                Node.SetOwner(null);
            }

            if (isPlaced)
            {
                Log.Info($"'{Name}' is moving from {this.point} to {point}");    
            }

            this.point = point;
            direction = point - prevPoint;
            Node.SetOwner(this);
            isPlaced = true;
        
            return true;
        }

        public override string ToString()
        {
            if (!isPlaced)
            {
                return base.ToString() + " {-,-}";
            }
            else
            {
                return $"{base.ToString()} {point}";
            }
        }
    }
}
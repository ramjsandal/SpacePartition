namespace SpacePartition;

public class SpacePartition
{
    public enum Direction
    {
        TOP = 0,
        LEFT,
        BOTTOM,
        RIGHT,
    };

    public enum Quadrant
    {
        TOPRIGHT,
        TOPLEFT,
        BOTTOMRIGHT,
        BOTTOMLEFT
    };

    // Assuming top left origin
    public int xOrigin;
    public int yOrigin;
    public int width;
    public int height;
    public bool traversible;

    // Only going to have TOPLEFT, TOPRIGHT, BOTTOMLEFT, BOTTOMRIGHT here
    public Dictionary<Quadrant, SpacePartition> partitions;
    private SpacePartition parent;
    private Random rand;
    public SpacePartition(int xOrigin, int yOrigin, int width, int height, Random random, SpacePartition parent)
    {
        this.xOrigin = xOrigin;
        this.yOrigin = yOrigin;
        this.width = width;
        this.height = height;
        rand = random;
        this.partitions = new Dictionary<Quadrant, SpacePartition>();
        this.parent = parent;
    }

    public bool ContainsCoordinate(int x, int y)
    {
        return (x >= this.xOrigin && x < this.xOrigin + this.width) && (y >= this.yOrigin && y < this.yOrigin + this.height);
    }

    void MakeLeafTraversable(SpacePartition leaf)
    {
        if (!ContainsCell(leaf))
        {
            return;
        }
        else if (this == leaf)
        {
            this.traversible = true;
        }
        else
        {
            // recur on our child that has this cell
            SpacePartition child = this.partitions.Where(a => a.Value.ContainsCell(leaf)).First().Value;
            this.traversible = true;
            child.MakeLeafTraversable(leaf);
        }
    }

    public SpacePartition GetRoot()
    {
        SpacePartition root = this;
        while (root.parent != null)
        {
            root = root.parent;
        }

        return root;
    }
    SpacePartition? GetGlobalLeafAt(int x, int y)
    {
        // Get root node
        SpacePartition root = this;
        while (root.parent != null)
        {
            root = root.parent;
        }

        return root.GetLeafAt(x, y);
    }
    SpacePartition? GetLeafAt(int x, int y)
    {
        if (!ContainsCoordinate(x, y))
        {
            return null;
        }
        else if (this.partitions.Count == 0)
        {
            return this;
        }
        else
        {
            // recur on our child that has this cell
            SpacePartition child = this.partitions.Where(a => a.Value.ContainsCoordinate(x, y)).First().Value;
            return child.GetLeafAt(x, y);
        }
    }

    bool ContainsCell(SpacePartition sp)
    {
        bool ret = (this.Equals(sp));
        foreach (var partition in partitions.Values)
        {
            ret = ret || partition.ContainsCell(sp);
        }
        return ret;
    }

    public List<SpacePartition> GetAllSubpartitionsOnEdge(Direction direction)
    {

        // Base case, if were a leaf return us
        if (this.partitions.Count == 0)
        {
            return new List<SpacePartition>() { this };
        }

        // Get partitions we care about
        List<SpacePartition> recur = new List<SpacePartition>();
        switch (direction)
        {
            case Direction.TOP:
                recur.Add(this.partitions[Quadrant.TOPRIGHT]);
                recur.Add(this.partitions[Quadrant.TOPLEFT]);
                break;
            case Direction.BOTTOM:
                recur.Add(this.partitions[Quadrant.BOTTOMLEFT]);
                recur.Add(this.partitions[Quadrant.BOTTOMRIGHT]);
                break;
            case Direction.RIGHT:
                recur.Add(this.partitions[Quadrant.TOPRIGHT]);
                recur.Add(this.partitions[Quadrant.BOTTOMRIGHT]);
                break;
            case Direction.LEFT:
                recur.Add(this.partitions[Quadrant.BOTTOMLEFT]);
                recur.Add(this.partitions[Quadrant.TOPLEFT]);
                break;
        }

        // recur on each of these partitions and return result
        List<SpacePartition> ret = new List<SpacePartition>();
        foreach (SpacePartition partition in recur)
        {
            ret.Concat(partition.GetAllSubpartitionsOnEdge(direction));
        }

        return ret;
    }

    // Called on our root node, we subdivide ourselves
    // down to leaves
    public void Subdivide(int depth, int minWidth, int minHeight)
    {

        // if were too small such that our children would
        // be below the min width and min height thresholds
        // we should be a leaf
        if (width / 2 < minWidth || height / 2 < minHeight)
        {
            return;
        }

        // Give us the chance to become a leaf, but only if 
        // we have already subdivded twice
        if (depth > 1)
        {
            bool becomeLeaf = rand.Next(0, 4) < 1;

            if (becomeLeaf)
            {
                return;
            }
        }

        // Lets subdivide ourself into 4 quadrants
        // and recur on each of them
        int bigWidth = width % 2 == 0 ? width / 2 : width / 2 + 1;
        int lilWidth = width / 2;

        int bigHeight = height % 2 == 0 ? height / 2 : height / 2 + 1;
        int lilHeight = height / 2;

        // Top Left
        SpacePartition tl = new SpacePartition(xOrigin, yOrigin, width / 2, height / 2, rand, this);
        tl.Subdivide(depth + 1, minWidth, minHeight);
        partitions.Add(Quadrant.TOPLEFT, tl);

        // Top Right
        SpacePartition tr = new SpacePartition(xOrigin + (width / 2), yOrigin, bigWidth, height / 2, rand, this);
        tr.Subdivide(depth + 1, minWidth, minHeight);
        partitions.Add(Quadrant.TOPRIGHT, tr);

        // Bottom Left
        SpacePartition bl = new SpacePartition(xOrigin, yOrigin + (height / 2), width / 2, bigHeight, rand, this);
        bl.Subdivide(depth + 1, minWidth, minHeight);
        partitions.Add(Quadrant.BOTTOMLEFT, bl);

        // Bottom Right
        SpacePartition br = new SpacePartition(xOrigin + (width / 2), yOrigin + (height / 2), bigWidth, bigHeight, rand, this);
        br.Subdivide(depth + 1, minWidth, minHeight);
        partitions.Add(Quadrant.BOTTOMRIGHT, br);

    }

    public void AssignLeafTraversability()
    {
        // Base Case
        // if were a leaf we randomly choose
        // if we are traversible
        if (this.partitions.Count == 0)
        {
            traversible = rand.Next(0, 4) == 0;
            return;
        }

        // Split / Recur
        foreach (var partition in partitions)
        {
            // since were not a leaf,
            // recur on our children and 
            // assign their traversability
            partition.Value.AssignLeafTraversability();
        }

    }

    // Assuming we have divided our tree, assign traversability
    // such that we maintain overall traversability
    public void AssignTraversability()
    {

        // Base Case
        // if were a leaf we stop
        if (this.partitions.Count == 0)
        {
            return;
        }

        // Split / Recur
        foreach (var partition in partitions)
        {
            // since were not a leaf,
            // recur on our children and if 
            // any of them are traversable, we set
            // ourselves as traversable to indicate we
            // have traversable cells
            partition.Value.AssignTraversability();
            if (partition.Value.traversible)
            {
                this.traversible = true;
            }
        }

        MergeTrees();

    }

    public void MergeTrees()
    {
        // merges our subpartitions such that each one that has traversible
        // cells is traversible to its traversible sibling neighbors
        foreach (var partition in partitions)
        {
            // if we dont have any traversable cells
            // just continue
            if (!partition.Value.traversible)
            {
                continue;
            }

            // if we have traversible cells, grab our siblings
            // who are traversible and adjacent
            // PROBLEM, somethimes we needa connect diagonal cells
            List<(Quadrant, SpacePartition)> adj = GetSiblings(partition.Key).Where(a => a.Item2.traversible).ToList();


            foreach (var part in adj)
            {
                var before = partition;
                // TUNNEL NOT DOING ITS JOB HERE
                // everything seems to get here ok though
                SpacePartition edited = partition.Value.Tunnel(part.Item2);
                var after = partition;
                this.partitions[part.Item1] = edited;
                this.partitions[partition.Key] = partition.Value;
            }

        }
    }

    // Makes every leaf between the closest traversable
    // leaf in this node and the closest traversable leaf
    // of the given node traversable
    // if they're already traversable, do nothing
    // returns the modified other space partition
    public SpacePartition Tunnel(SpacePartition other)
    {
        // for each leaf in this space partition
        // find the closest traversable leaf
        // to a traversable leaf in other
        List<SpacePartition> ourLeaves = new List<SpacePartition>();
        List<SpacePartition> otherLeaves = new List<SpacePartition>();
        GetAllLeaves(ref ourLeaves);
        other.GetAllLeaves(ref otherLeaves);
        ourLeaves = ourLeaves.Where(a => a.traversible).ToList();
        otherLeaves = otherLeaves.Where(a => a.traversible).ToList();

        int minDist = int.MaxValue;
        (SpacePartition, SpacePartition) bestPair = (null, null);

        foreach (var leaf in ourLeaves)
        {
            var min = otherLeaves.OrderByDescending(a => a.Distance(leaf)).Last();
            if (min.Distance(leaf) < minDist)
            {
                minDist = min.Distance(leaf);
                bestPair = (leaf, min);
            }
        }

        // Get a list of all the leaves between the two leaves
        // Lets do this in a right angle, will be easier.
        // Get all of the cells till we get to the x value we need
        // then get all of the cells till the y value we need

        // Lets always tunnel left to right
        SpacePartition start = bestPair.Item1.xOrigin < bestPair.Item2.xOrigin ? bestPair.Item1 : bestPair.Item2;
        SpacePartition end = start == bestPair.Item1 ? bestPair.Item2 : bestPair.Item1;
        List<SpacePartition> cellsToTunnel = new List<SpacePartition>();
        SpacePartition current = start;
        while (current != null && current.xOrigin < end.xOrigin)
        {
            SpacePartition? getLeaf = this.GetGlobalLeafAt(current.xOrigin + current.width + 1, current.yOrigin);
            current = getLeaf;
            if (getLeaf != null)
            {
                cellsToTunnel.Add(current);
            }
        }

        // Now lets tunnel down or up
        if (current != null && current.yOrigin < end.yOrigin)
        {
            while (current != null && current.yOrigin < end.yOrigin)
            {
                SpacePartition? getLeaf = this.GetGlobalLeafAt(current.xOrigin, current.yOrigin + current.height + 1);
                current = getLeaf;
                if (getLeaf != null)
                {
                    cellsToTunnel.Add(current);
                }

            }
        }
        else if (current != null && current.yOrigin > end.yOrigin)
        {
            while (current != null && current.yOrigin > end.yOrigin)
            {
                SpacePartition? getLeaf = this.GetGlobalLeafAt(current.xOrigin, current.yOrigin - 1);
                current = getLeaf;
                if (getLeaf != null)
                {
                    cellsToTunnel.Add(current);
                }


            }
        }

        // Ok now we should have all of the cells that we want to make sure are
        // traversible inside the list, now we need to go change them to traversible
        // in their parents because i forgot to make this program pass by reference everywhere
        // (like an idiot!!!)
        // will fix later if im not lazy 
        // (i am !!!!)
        SpacePartition root = GetRoot();
        foreach (var cell in cellsToTunnel)
        {
            root.MakeLeafTraversable(cell);

        }

        return other;

    }

    // Center to center distance
    int Distance(SpacePartition other)
    {
        int xDistance = (xOrigin + (width / 2)) - (other.xOrigin + (other.width / 2));
        int yDistance = (yOrigin + (height / 2)) - (other.yOrigin + (other.height / 2));

        return (int)Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
    }

    private List<(Quadrant, SpacePartition)> GetAdjacentSiblings(Quadrant quad)
    {
        List<(Quadrant, SpacePartition)> result = new List<(Quadrant, SpacePartition)>();

        switch (quad)
        {
            case Quadrant.TOPRIGHT:
                result.Add((Quadrant.TOPLEFT, partitions[Quadrant.TOPLEFT]));
                result.Add((Quadrant.BOTTOMRIGHT, partitions[Quadrant.BOTTOMRIGHT]));
                break;
            case Quadrant.TOPLEFT:
                result.Add((Quadrant.TOPRIGHT, partitions[Quadrant.TOPRIGHT]));
                result.Add((Quadrant.BOTTOMLEFT, partitions[Quadrant.BOTTOMLEFT]));
                break;
            case Quadrant.BOTTOMLEFT:
                result.Add((Quadrant.TOPLEFT, partitions[Quadrant.TOPLEFT]));
                result.Add((Quadrant.BOTTOMRIGHT, partitions[Quadrant.BOTTOMRIGHT]));
                break;
            case Quadrant.BOTTOMRIGHT:
                result.Add((Quadrant.TOPRIGHT, partitions[Quadrant.TOPRIGHT]));
                result.Add((Quadrant.BOTTOMLEFT, partitions[Quadrant.BOTTOMLEFT]));
                break;
        }
        return result;
    }

    private List<(Quadrant, SpacePartition)> GetSiblings(Quadrant quad)
    {
        List<(Quadrant, SpacePartition)> result = new List<(Quadrant, SpacePartition)>();

        switch (quad)
        {
            case Quadrant.TOPRIGHT:
                result.Add((Quadrant.TOPLEFT, partitions[Quadrant.TOPLEFT]));
                result.Add((Quadrant.BOTTOMRIGHT, partitions[Quadrant.BOTTOMRIGHT]));
                result.Add((Quadrant.BOTTOMLEFT, partitions[Quadrant.BOTTOMLEFT]));
                break;
            case Quadrant.TOPLEFT:
                result.Add((Quadrant.TOPRIGHT, partitions[Quadrant.TOPRIGHT]));
                result.Add((Quadrant.BOTTOMLEFT, partitions[Quadrant.BOTTOMLEFT]));
                result.Add((Quadrant.BOTTOMRIGHT, partitions[Quadrant.BOTTOMRIGHT]));
                break;
            case Quadrant.BOTTOMLEFT:
                result.Add((Quadrant.TOPLEFT, partitions[Quadrant.TOPLEFT]));
                result.Add((Quadrant.TOPRIGHT, partitions[Quadrant.TOPRIGHT]));
                result.Add((Quadrant.BOTTOMRIGHT, partitions[Quadrant.BOTTOMRIGHT]));
                break;
            case Quadrant.BOTTOMRIGHT:
                result.Add((Quadrant.TOPRIGHT, partitions[Quadrant.TOPRIGHT]));
                result.Add((Quadrant.TOPLEFT, partitions[Quadrant.TOPLEFT]));
                result.Add((Quadrant.BOTTOMLEFT, partitions[Quadrant.BOTTOMLEFT]));
                break;
        }
        return result;
    }



    public void GetAllLeaves(ref List<SpacePartition> prevList)
    {
        if (this.partitions.Count == 0)
        {
            prevList.Add(this);
        }
        else
        {
            foreach (var partition in this.partitions)
            {
                partition.Value.GetAllLeaves(ref prevList);
            }
        }
    }

    public void PrintLeaves()
    {

        List<SpacePartition> leaves = new List<SpacePartition>();
        GetAllLeaves(ref leaves);

        foreach (var partition in leaves)
        {
            Console.WriteLine("X Coords: " + partition.xOrigin + ", " + (partition.xOrigin + partition.width));
            Console.WriteLine("Y Coords: " + partition.yOrigin + ", " + (partition.yOrigin + partition.height));
            Console.WriteLine("Traversable: " + partition.traversible);
        }

    }

    public override bool Equals(object? obj)
    {
        var item = obj as SpacePartition;
        if (item == null)
        { return false; }

        return xOrigin == item.xOrigin && yOrigin == item.yOrigin && width == item.width && height == item.height;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

namespace SpacePartition.Tests;

using System.Diagnostics;
using Xunit;

public class UnitTest1
{
    public SpacePartition CreatePartition()
    {
        Random rand = new Random(1);
        SpacePartition sp = new SpacePartition(0, 0, 800, 600, rand, null);
        SpacePartition topLeft = new SpacePartition(0, 0, 400, 300, rand, sp);
        SpacePartition topRight = new SpacePartition(400, 400, 400, 300, rand, sp);
        SpacePartition bottomLeft = new SpacePartition(0, 300, 400, 300, rand, sp);
        SpacePartition bottomRight = new SpacePartition(400, 300, 400, 300, rand, sp);

        sp.partitions.Add(SpacePartition.Quadrant.TOPLEFT, topLeft);
        sp.partitions.Add(SpacePartition.Quadrant.TOPRIGHT, topRight);
        sp.partitions.Add(SpacePartition.Quadrant.BOTTOMLEFT, bottomLeft);
        sp.partitions.Add(SpacePartition.Quadrant.BOTTOMRIGHT, bottomRight);

        return sp;
    }

    public SpacePartition CreateUnlinkedComplexPartition()
    {
        SpacePartition sp = CreateComplexPartition();
        sp.traversible = true;

        // top left
        sp.partitions[SpacePartition.Quadrant.TOPLEFT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible = false;

        // top right
        sp.partitions[SpacePartition.Quadrant.TOPRIGHT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible = false;
        sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible = false;
        sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible = true;

        // bot left
        sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible = false;

        // bot right
        sp.partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible = false;

        return sp;

    }

    public SpacePartition CreateLinkedComplexPartition()
    {
        SpacePartition sp = CreateComplexPartition();
        sp.traversible = true;

        // top left
        sp.partitions[SpacePartition.Quadrant.TOPLEFT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible = false;

        // top right
        sp.partitions[SpacePartition.Quadrant.TOPRIGHT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible = false;
        sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible = true;

        // bot left
        sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible = true;
        sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible = false;

        // bot right
        sp.partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible = false;

        return sp;

    }

    public SpacePartition CreateComplexPartition()
    {
        Random rand = new Random(1);

        SpacePartition sp = new SpacePartition(0, 0, 800, 600, rand, null);
        SpacePartition topLeft = new SpacePartition(0, 0, 400, 300, rand, sp);
        SpacePartition topRight = new SpacePartition(400, 0, 400, 300, rand, sp);
        SpacePartition bottomLeft = new SpacePartition(0, 300, 400, 300, rand, sp);
        SpacePartition bottomRight = new SpacePartition(400, 300, 400, 300, rand, sp);

        SpacePartition tltl = new SpacePartition(0, 0, 200, 150, rand, topLeft);
        SpacePartition tltr = new SpacePartition(200, 0, 200, 150, rand, topLeft);
        SpacePartition tlbl = new SpacePartition(0, 150, 200, 150, rand, topLeft);
        SpacePartition tlbr = new SpacePartition(200, 150, 200, 150, rand, topLeft);
        topLeft.partitions.Add(SpacePartition.Quadrant.TOPLEFT, tltl);
        topLeft.partitions.Add(SpacePartition.Quadrant.TOPRIGHT, tltr);
        topLeft.partitions.Add(SpacePartition.Quadrant.BOTTOMLEFT, tlbl);
        topLeft.partitions.Add(SpacePartition.Quadrant.BOTTOMRIGHT, tlbr);

        SpacePartition trtl = new SpacePartition(400, 0, 200, 150, rand, topRight);
        SpacePartition trtr = new SpacePartition(600, 0, 200, 150, rand, topRight);
        SpacePartition trbl = new SpacePartition(400, 150, 200, 150, rand, topRight);
        SpacePartition trbr = new SpacePartition(600, 150, 200, 150, rand, topRight);
        topRight.partitions.Add(SpacePartition.Quadrant.TOPLEFT, trtl);
        topRight.partitions.Add(SpacePartition.Quadrant.TOPRIGHT, trtr);
        topRight.partitions.Add(SpacePartition.Quadrant.BOTTOMLEFT, trbl);
        topRight.partitions.Add(SpacePartition.Quadrant.BOTTOMRIGHT, trbr);

        SpacePartition bltl = new SpacePartition(0, 300, 200, 150, rand, bottomLeft);
        SpacePartition bltr = new SpacePartition(200, 300, 200, 150, rand, bottomLeft);
        SpacePartition blbl = new SpacePartition(0, 450, 200, 150, rand, bottomLeft);
        SpacePartition blbr = new SpacePartition(200, 450, 200, 150, rand, bottomLeft);
        bottomLeft.partitions.Add(SpacePartition.Quadrant.TOPLEFT, bltl);
        bottomLeft.partitions.Add(SpacePartition.Quadrant.TOPRIGHT, bltr);
        bottomLeft.partitions.Add(SpacePartition.Quadrant.BOTTOMLEFT, blbl);
        bottomLeft.partitions.Add(SpacePartition.Quadrant.BOTTOMRIGHT, blbr);

        sp.partitions.Add(SpacePartition.Quadrant.TOPLEFT, topLeft);
        sp.partitions.Add(SpacePartition.Quadrant.TOPRIGHT, topRight);
        sp.partitions.Add(SpacePartition.Quadrant.BOTTOMLEFT, bottomLeft);
        sp.partitions.Add(SpacePartition.Quadrant.BOTTOMRIGHT, bottomRight);



        return sp;
    }

    public SpacePartition CreateSubdivision()
    {
        SpacePartition root = new SpacePartition(0, 0, 800, 600, new Random(1), null);
        root.Subdivide(0, 100, 100);
        root.AssignLeafTraversability();
        root.PrintLeaves();

        Console.WriteLine("~~~~\n\n\n");
        Console.WriteLine("\n\n\n~~~~");

        /*
        // THIS DOES NOTHING
        root.AssignTraversability();
        root.PrintLeaves();

        Console.WriteLine("~~~~\n\n\n");
        Console.WriteLine("\n\n\n~~~~");

        root.MergeTrees();
        root.PrintLeaves();
        */

        return root;
    }
    [Fact]
    public void AssignLeafTravTest()
    {
        CreateSubdivision();
    }

    [Fact]
    public void CreateSpacePartitionsTest()
    {
        Random rand = new Random(1);
        SpacePartition sp = new SpacePartition(0, 0, 800, 600, rand, null);
        SpacePartition topLeft = new SpacePartition(0, 0, 400, 300, rand, sp);
        SpacePartition topRight = new SpacePartition(400, 0, 400, 300, rand, sp);
        SpacePartition bottomLeft = new SpacePartition(0, 300, 400, 300, rand, sp);
        SpacePartition bottomRight = new SpacePartition(400, 300, 400, 300, rand, sp);

        sp.partitions.Add(SpacePartition.Quadrant.TOPLEFT, topLeft);
        sp.partitions.Add(SpacePartition.Quadrant.TOPRIGHT, topRight);
        sp.partitions.Add(SpacePartition.Quadrant.BOTTOMLEFT, bottomLeft);
        sp.partitions.Add(SpacePartition.Quadrant.BOTTOMRIGHT, bottomRight);

        Assert.NotNull(sp);
        Assert.NotNull(topLeft);
        Assert.NotNull(topRight);
        Assert.NotNull(bottomRight);
        Assert.NotNull(bottomLeft);
    }

    [Fact]
    public void ContainsCoordinateTest()
    {
        SpacePartition sp = CreatePartition();
        SpacePartition topLeft = sp.partitions[SpacePartition.Quadrant.TOPLEFT];

        Assert.True(sp.ContainsCoordinate(0, 0));
        Assert.True(sp.ContainsCoordinate(400, 300));
        Assert.True(!sp.ContainsCoordinate(800, 0));
        Assert.True(!sp.ContainsCoordinate(0, 600));
        Assert.True(!sp.ContainsCoordinate(800, 600));

        Assert.True(topLeft.ContainsCoordinate(0, 0));
        Assert.True(topLeft.ContainsCoordinate(0, 50));
        Assert.True(!topLeft.ContainsCoordinate(0, 300));
        Assert.True(!topLeft.ContainsCoordinate(400, 0));
        Assert.True(!topLeft.ContainsCoordinate(400, 300));

        SpacePartition complexSP = CreateComplexPartition();
        SpacePartition compSPTL = complexSP.partitions[SpacePartition.Quadrant.TOPLEFT];
        SpacePartition compSPTLTL = compSPTL.partitions[SpacePartition.Quadrant.TOPLEFT];

        Assert.True(complexSP.ContainsCoordinate(0, 0));
        Assert.True(complexSP.ContainsCoordinate(400, 300));
        Assert.True(!complexSP.ContainsCoordinate(800, 0));
        Assert.True(!complexSP.ContainsCoordinate(0, 600));
        Assert.True(!complexSP.ContainsCoordinate(800, 600));

        Assert.True(compSPTL.ContainsCoordinate(0, 0));
        Assert.True(compSPTL.ContainsCoordinate(0, 50));
        Assert.True(!compSPTL.ContainsCoordinate(0, 300));
        Assert.True(!compSPTL.ContainsCoordinate(400, 0));
        Assert.True(!compSPTL.ContainsCoordinate(400, 300));

        Assert.True(compSPTLTL.ContainsCoordinate(0, 0));
        Assert.True(compSPTLTL.ContainsCoordinate(0, 50));
        Assert.True(compSPTLTL.ContainsCoordinate(50, 50));
        Assert.True(!compSPTLTL.ContainsCoordinate(0, 200));
        Assert.True(!compSPTLTL.ContainsCoordinate(0, 300));
        Assert.True(!compSPTLTL.ContainsCoordinate(400, 0));
        Assert.True(!compSPTLTL.ContainsCoordinate(400, 300));

    }

    [Fact]
    public void TunnelTest()
    {

        SpacePartition sp = CreateUnlinkedComplexPartition();

        // top left
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // top right
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // bot left
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // bot right
        Assert.True(!sp.partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        sp.partitions[SpacePartition.Quadrant.TOPRIGHT] = sp.partitions[SpacePartition.Quadrant.TOPLEFT].Tunnel(sp.partitions[SpacePartition.Quadrant.TOPRIGHT]);

        // top left
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // top right
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // bot left
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // bot right
        Assert.True(!sp.partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);


        // THIS SHOULD NOT DO ANYTHING SINCE WERE ALREADY CONNECTED
        sp.partitions[SpacePartition.Quadrant.TOPRIGHT] = sp.partitions[SpacePartition.Quadrant.TOPLEFT].Tunnel(sp.partitions[SpacePartition.Quadrant.TOPRIGHT]);

        // top left
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // top right
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // bot left
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // bot right
        Assert.True(!sp.partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);


        // THIS SHOULD NOT DO ANYTHING SINCE WERE ALREADY CONNECTED
        // EVEN THOUGH WE RUN IT THE OTHER WAY
        sp.partitions[SpacePartition.Quadrant.TOPLEFT] = sp.partitions[SpacePartition.Quadrant.TOPRIGHT].Tunnel(sp.partitions[SpacePartition.Quadrant.TOPLEFT]);


        // top left
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // top right
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // bot left
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // bot right
        Assert.True(!sp.partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);


    }

    [Fact]
    public void MergeTreesTest()
    {

        SpacePartition sp = CreateUnlinkedComplexPartition();

        // top left
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // top right
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // bot left
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // bot right
        Assert.True(!sp.partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        sp.MergeTrees();

        // top left
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.TOPLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // top right
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.TOPRIGHT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // bot left
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPLEFT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.TOPRIGHT].traversible);
        Assert.True(sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMLEFT].traversible);
        Assert.True(!sp.partitions[SpacePartition.Quadrant.BOTTOMLEFT].partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);

        // bot right
        Assert.True(!sp.partitions[SpacePartition.Quadrant.BOTTOMRIGHT].traversible);




    }

}

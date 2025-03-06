namespace SpacePartition.Tests;
using Xunit;

public class UnitTest1
{
    public SpacePartition CreatePartition()
    {
        SpacePartition sp = new SpacePartition(0, 0, 800, 600, null);
        SpacePartition topLeft = new SpacePartition(0, 0, 400, 300, sp);
        SpacePartition topRight = new SpacePartition(400, 0, 400, 300, sp);
        SpacePartition bottomLeft = new SpacePartition(0, 300, 400, 300, sp);
        SpacePartition bottomRight = new SpacePartition(400, 300, 400, 300, sp);

        sp.partitions.Add(SpacePartition.Quadrant.TOPLEFT, topLeft);
        sp.partitions.Add(SpacePartition.Quadrant.TOPRIGHT, topRight);
        sp.partitions.Add(SpacePartition.Quadrant.BOTTOMLEFT, bottomLeft);
        sp.partitions.Add(SpacePartition.Quadrant.BOTTOMRIGHT, bottomRight);

        return sp;
    }

    public SpacePartition CreateComplexPartition()
    {
        SpacePartition sp = new SpacePartition(0, 0, 800, 600, null);
        SpacePartition topLeft = new SpacePartition(0, 0, 400, 300, sp);
        SpacePartition topRight = new SpacePartition(400, 0, 400, 300, sp);
        SpacePartition bottomLeft = new SpacePartition(0, 300, 400, 300, sp);
        SpacePartition bottomRight = new SpacePartition(400, 300, 400, 300, sp);

        sp.partitions.Add(SpacePartition.Quadrant.TOPLEFT, topLeft);
        sp.partitions.Add(SpacePartition.Quadrant.TOPRIGHT, topRight);
        sp.partitions.Add(SpacePartition.Quadrant.BOTTOMLEFT, bottomLeft);
        sp.partitions.Add(SpacePartition.Quadrant.BOTTOMRIGHT, bottomRight);

        return sp;
    }

    [Fact]
    public void CreateSpacePartitionsTest()
    {
        SpacePartition sp = new SpacePartition(0, 0, 800, 600, null);
        SpacePartition topLeft = new SpacePartition(0, 0, 400, 300, sp);
        SpacePartition topRight = new SpacePartition(400, 0, 400, 300, sp);
        SpacePartition bottomLeft = new SpacePartition(0, 300, 400, 300, sp);
        SpacePartition bottomRight = new SpacePartition(400, 300, 400, 300, sp);

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
    }
}

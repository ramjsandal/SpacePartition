using Raylib_cs;
using SpacePartition;


public class Program
{

    public static void Main()
    {
        SpacePartition.SpacePartition root = new SpacePartition.SpacePartition(0, 0, 800, 600, null);
        root.Subdivide(0, 100, 100);
        root.AssignTraversability();
        List<SpacePartition.SpacePartition> partitions = new List<SpacePartition.SpacePartition>();
        root.GetAllLeaves(ref partitions);

        int screenWidth = 800;
        int screenHeight = 600;
        Raylib.InitWindow(screenWidth, screenHeight, "Wahoo");

        while (!Raylib.WindowShouldClose())
        {

            Raylib.ClearBackground(Color.SkyBlue);
            Raylib.BeginDrawing();

            foreach (SpacePartition.SpacePartition partition in partitions)
            {
                Raylib.DrawRectangle(partition.xOrigin, partition.yOrigin, partition.width, partition.height, partition.traversible ? Raylib_cs.Color.White : Raylib_cs.Color.Black);
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();

    }
}

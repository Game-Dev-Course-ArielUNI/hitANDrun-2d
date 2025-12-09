using System;
using System.Collections.Generic;

// ----------------------
// Helper struct (like Vector3Int)
// ----------------------
public struct Pos
{
    public int X;
    public int Y;

    public Pos(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"({X},{Y})";
}

// ----------------------
// GAME WORLD 
// ----------------------
public static class GameWorld
{
    // '#' = wall
    // '.' = walkable
    public static char[,] Map;

    public static bool IsWalkable(Pos p)
    {
        int h = Map.GetLength(0);
        int w = Map.GetLength(1);

        if (p.X < 0 || p.X >= w || p.Y < 0 || p.Y >= h)
            return false;

        return Map[p.Y, p.X] != '#';
    }

    public static List<Pos> GetNeighbors(Pos p)
    {
        List<Pos> neighbors = new();
        Pos[] dirs =
        {
            new Pos(1, 0),
            new Pos(-1, 0),
            new Pos(0, 1),
            new Pos(0, -1)
        };

        foreach (var d in dirs)
        {
            Pos n = new Pos(p.X + d.X, p.Y + d.Y);
            if (IsWalkable(n))
                neighbors.Add(n);
        }

        return neighbors;
    }
}

// ----------------------
// ESCAPER ALGORITHM 
// ----------------------
public static class Escaper
{
    public static Pos EscapeStep(Pos enemy, Pos player)
    {
        List<Pos> neighbors = GameWorld.GetNeighbors(enemy);

        Pos farthest = enemy;
        double maxDist = Distance(enemy, player);

        foreach (var n in neighbors)
        {
            double dist = Distance(n, player);
            if (dist > maxDist)
            {
                maxDist = dist;
                farthest = n;
            }
        }

        return farthest;
    }

    private static double Distance(Pos a, Pos b)
    {
        int dx = a.X - b.X;
        int dy = a.Y - b.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}

// ----------------------
// MAP UTILITIES
// ----------------------
public static class MapUtils
{
    public static void Load(string[] lines, out Pos player, out Pos enemy)
    {
        int h = lines.Length;
        int w = lines[0].Length;

        GameWorld.Map = new char[h, w];
        player = new Pos(-1, -1);
        enemy = new Pos(-1, -1);

        for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
            {
                char c = lines[y][x];
                GameWorld.Map[y, x] = c;

                if (c == 'P') player = new Pos(x, y);
                if (c == 'E') enemy = new Pos(x, y);
            }
    }

    public static void Print(Pos player, Pos enemy)
    {
        int h = GameWorld.Map.GetLength(0);
        int w = GameWorld.Map.GetLength(1);

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                if (player.X == x && player.Y == y) Console.Write('P');
                else if (enemy.X == x && enemy.Y == y) Console.Write('E');
                else Console.Write(GameWorld.Map[y, x]);
            }
            Console.WriteLine();
        }
    }
}

// ----------------------
// TESTS
// ----------------------
class Program
{
    static void Main()
    {
        Console.WriteLine("=== Escape Algorithm Tests ===\n");

        Test1();
        Test2();
        Test3();
        Test4();
        Test5();

        Console.WriteLine("\nDONE.");
        Console.ReadKey();
    }

    // Straight corridor — enemy should move away.
    static void Test1()
    {
        Console.WriteLine("TEST 1 — Straight corridor");

        string[] map =
        {
            "##########",
            "#P.......#",
            "#........#",
            "#......E.#",
            "##########"
        };

        MapUtils.Load(map, out Pos player, out Pos enemy);

        Console.WriteLine("Initial:");
        MapUtils.Print(player, enemy);

        enemy = Escaper.EscapeStep(enemy, player);

        Console.WriteLine("\nAfter Escape Step:");
        MapUtils.Print(player, enemy);
        Console.WriteLine($"Enemy moved to: {enemy}\n");
    }

    // Two paths — chooses farther.
    static void Test2()
    {
        Console.WriteLine("TEST 2 — Branch");

        string[] map =
        {
            "###########",
            "#P........#",
            "#.....#...#",
            "#.....#E..#",
            "#.....#...#",
            "###########",
        };

        MapUtils.Load(map, out Pos player, out Pos enemy);

        Console.WriteLine("Initial:");
        MapUtils.Print(player, enemy);

        enemy = Escaper.EscapeStep(enemy, player);

        Console.WriteLine("\nAfter Escape Step:");
        MapUtils.Print(player, enemy);
        Console.WriteLine($"Enemy moved to: {enemy}\n");
    }

    // Dead end — moves to best reachable cell.
    static void Test3()
    {
        Console.WriteLine("TEST 3 — Dead end");

        string[] map =
        {
            "###########",
            "#P........#",
            "#.#######.#",
            "#.E.....#.#",
            "#.#######.#",
            "###########",
        };

        MapUtils.Load(map, out Pos player, out Pos enemy);

        Console.WriteLine("Initial:");
        MapUtils.Print(player, enemy);

        enemy = Escaper.EscapeStep(enemy, player);

        Console.WriteLine("\nAfter Escape Step:");
        MapUtils.Print(player, enemy);
        Console.WriteLine($"Enemy moved to: {enemy}\n");
    }
    static void Test4()
    {
        Console.WriteLine("TEST 4 — Player behind wall");

        string[] map =
        {
        "###########",
        "#P####....#",
        "#....#....#",
        "#.E..#....#",
        "#....#....#",
        "###########",
    };

        MapUtils.Load(map, out Pos player, out Pos enemy);

        Console.WriteLine("Initial:");
        MapUtils.Print(player, enemy);

        enemy = Escaper.EscapeStep(enemy, player);

        Console.WriteLine("\nAfter Escape Step:");
        MapUtils.Print(player, enemy);
        Console.WriteLine($"Enemy moved to: {enemy}\n");
    }
    static void Test5()
    {
        Console.WriteLine("TEST 5 — Maze navigation");

        string[] map =
        {
        "#############",
        "#P....#.....#",
        "#.##..#..##.#",
        "#....##....E#",
        "#.##..#..##.#",
        "#.....#.....#",
        "#############",
    };

        MapUtils.Load(map, out Pos player, out Pos enemy);

        Console.WriteLine("Initial:");
        MapUtils.Print(player, enemy);

        enemy = Escaper.EscapeStep(enemy, player);

        Console.WriteLine("\nAfter Escape Step:");
        MapUtils.Print(player, enemy);
        Console.WriteLine($"Enemy moved to: {enemy}\n");
    }


}

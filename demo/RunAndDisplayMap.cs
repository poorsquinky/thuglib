using System;
using System.Collections;
using System.Collections.Generic;

namespace ThugLib
{
    public class RunAndDisplayMap
    {

        private static void DisplayMap(MapData md)
        {
            for (int i = 0; i < md.grid[0].Length; i++) 
            {
                for (int j = 0; j < md.grid.Length; j++) 
                {
                    Console.Write(md.palette[md.grid[j][i]].glyph);
                }
                Console.Write("\n");
            }
        }

        public static int Main(String[] args)
        {
            ClearMapGenerator gen = new ClearMapGenerator(new int[] {0, 0},
               MapCoordinate.GenerateRandom());

            MapData mapdata = new MapData(40, 40);
            ClearMapGenerator gen2 = new ClearMapGenerator(new int[] {2, 1},
               MapCoordinate.GenerateRandom());
            gen2.Run(mapdata.grid, new MapRectangle(10, 10, 10, 10), null);
            List<MapRoom> blockedList = new List<MapRoom>();
            MapRectangle fullArea = new MapRectangle(0, 0, 40, 40);
            blockedList.Add(new MapRoom(new MapRectangle(10, 10, 10, 10)));
            gen.Run(mapdata.grid, fullArea, blockedList);

            if (args.Length == 0)
            {
                Console.WriteLine("Specify -cave, -dungeon, or -office");
                return 0;
            }
            else if (args[0] == "-office")
            {
                gen.Run(mapdata.grid, fullArea, null);
                BSPBuildingMapGenerator bsp = new BSPBuildingMapGenerator(
                   new int[] {0, 1, 7}, MapCoordinate.GenerateRandom(),
                   4, 3, 2);
                MapRectangle borderedArea = new MapRectangle(1, 1, 38, 38);
                bsp.Run(mapdata.grid, borderedArea, null);

                mapdata.AddSpaceType(glyph: '#');
                mapdata.AddSpaceType(glyph: '.');
                mapdata.AddSpaceType(glyph: 'O');
                mapdata.AddSpaceType(glyph: '*');
                mapdata.AddSpaceType(glyph: '~');
                mapdata.AddSpaceType(glyph: 'X');
                mapdata.AddSpaceType(glyph: ' ');
                mapdata.AddSpaceType(glyph: '+');
            }
            else if (args[0] == "-dungeon")
            {
                DungeonRoomMapGenerator drmg = new DungeonRoomMapGenerator(new
                   int[] {5, 6, 7}, MapCoordinate.GenerateRandom(),
                   5, 12, 10, 3);
                List<MapRoom> allRooms = drmg.Run(mapdata.grid, fullArea,
                   blockedList);
                DungeonCorridorMapGenerator dcmg = new
                   DungeonCorridorMapGenerator(
                   new int[] {5, 6, 7}, MapCoordinate.GenerateRandom(), 2,
                   new int[] {0, 100000, 100000, 0, 0, 100000, 0, 0});
                dcmg.Run(mapdata.grid, fullArea, allRooms);

                mapdata.AddSpaceType(glyph: '#');
                mapdata.AddSpaceType(glyph: '#');
                mapdata.AddSpaceType(glyph: '#');
                mapdata.AddSpaceType(glyph: '*');
                mapdata.AddSpaceType(glyph: '~');
                mapdata.AddSpaceType(glyph: '#');
                mapdata.AddSpaceType(glyph: ' ');
                mapdata.AddSpaceType(glyph: '+');
            }
            else if (args[0] == "-cave")
            {
                CADecayMapGenerator cad = new CADecayMapGenerator(new
                   int[] {0, 1}, MapCoordinate.GenerateRandom(), 10, 20, 5, 10);
                // add this to not get random caves
                // cad.UseCoordinateBasedRandom();
                cad.Run(mapdata.grid, fullArea, blockedList);
                CAGrowthMapGenerator cag1 = new CAGrowthMapGenerator(new int[]
                   {3, 1}, MapCoordinate.GenerateRandom(), 50, 2, 20);
                cag1.Run(mapdata.grid, fullArea, blockedList);
                CAGrowthMapGenerator cag2 = new CAGrowthMapGenerator(new int[]
                   {4, 1, 2}, MapCoordinate.GenerateRandom(), 200, 8, 20);
                cag2.Run(mapdata.grid, fullArea, blockedList);
                mapdata.AddSpaceType(glyph: '#');
                mapdata.AddSpaceType(glyph: '.');
                mapdata.AddSpaceType(glyph: 'O');
                mapdata.AddSpaceType(glyph: '*');
                mapdata.AddSpaceType(glyph: '~');
                mapdata.AddSpaceType(glyph: 'X');
                mapdata.AddSpaceType(glyph: ' ');
                mapdata.AddSpaceType(glyph: '+');
            }
            else if (args[0] == "-line")
            {
                Path path = PathUtils.GetBresenhamPath(0, 1, 6, 4, null);
                Console.WriteLine("Path from 0, 1 to 6, 4");
                PathUtils.PrintPath(path, 0, 1);
                PathUtils.GetBresenhamPath(-3, 1, -4, 9, path);
                Console.WriteLine("Path from -3, 1 to -4, 9");
                PathUtils.PrintPath(path, -3, 1);

                gen = new ClearMapGenerator(new int[] {1, 1},
                   MapCoordinate.GenerateRandom());
                fullArea = new MapRectangle(0, 0, 40, 40);
                mapdata = new MapData(40, 40);
                gen.Run(mapdata.grid, fullArea, null);
                int length = PathUtils.CalculateBresenhamProductSquareToSquare(
                   0, 1, 6, 4, mapdata.grid, (x, y) => x + y, 0);
                Console.WriteLine("Length (0, 1) -> (6, 4) in steps = " + length);
                
                MapData distance = new MapData(40, 40);
                PathUtils.CalculateBresenhamProductsToRectangle(5, 5, mapdata.grid,
                   fullArea, (x, y) => x + y, 0, distance.grid);
                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine("3 " + i + " dist = " + distance.grid[3][i]);
                }
                return 0;
            }
            else if (args[0] == "-bfs")
            {
                DungeonRoomMapGenerator drmg = new DungeonRoomMapGenerator(new
                   int[] {5, 6, 7}, MapCoordinate.GenerateRandom(),
                   5, 12, 10, 3);
                List<MapRoom> allRooms = drmg.Run(mapdata.grid, fullArea,
                   blockedList);
                DungeonCorridorMapGenerator dcmg = new
                   DungeonCorridorMapGenerator(
                   new int[] {5, 6, 7}, MapCoordinate.GenerateRandom(), 2,
                   new int[] {0, 100000, 100000, 0, 0, 100000, 0, 0});
                dcmg.Run(mapdata.grid, fullArea, allRooms);

                int xStart = allRooms[0].bounds.xCenter;
                int yStart = allRooms[0].bounds.yCenter;
                int xEnd = allRooms[1].bounds.xCenter;
                int yEnd = allRooms[1].bounds.yCenter;
                Path path = PathUtils.BFSPath(xStart, yStart, xEnd, yEnd, null, 
                   (x, y) => (mapdata.grid[x][y] >= 6), null, (x, y, d) => 
                   ((((int)d) % 2) == 1 ? 140 : 100), fullArea);
                if (path != null)
                {
                    int[][] pathSquares = PathUtils.UnrollPath(path, xStart, yStart);
                    for (int i = 0; i < pathSquares.Length; i++)
                    {
                        mapdata.grid[pathSquares[i][0]][pathSquares[i][1]] = 8;
                    }
                }
                mapdata.grid[xStart][yStart] = 9;
                mapdata.grid[xEnd][yEnd] = 10;

                mapdata.AddSpaceType(glyph: '#');
                mapdata.AddSpaceType(glyph: '#');
                mapdata.AddSpaceType(glyph: '#');
                mapdata.AddSpaceType(glyph: '*');
                mapdata.AddSpaceType(glyph: '~');
                mapdata.AddSpaceType(glyph: '#');
                mapdata.AddSpaceType(glyph: ' ');
                mapdata.AddSpaceType(glyph: '+');
                mapdata.AddSpaceType(glyph: 'v');
                mapdata.AddSpaceType(glyph: 'S');
                mapdata.AddSpaceType(glyph: 'E');
            }
            else
            {
                Console.WriteLine("Specify -cave, -dungeon, or -office");
                return 0;
            }
            DisplayMap(mapdata);
            return 0;
        }
    }
}

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
                DungeonRoomMapGenerator drmg = new DungeonRoomMapGenerator(new
                   int[] {5, 6, 7}, MapCoordinate.GenerateRandom(),
                   5, 12, 20, 4);
                drmg.Run(mapdata.grid, fullArea, blockedList);
                mapdata.AddSpaceType(glyph: '#');
                mapdata.AddSpaceType(glyph: '.');
                mapdata.AddSpaceType(glyph: 'O');
                mapdata.AddSpaceType(glyph: '*');
                mapdata.AddSpaceType(glyph: '~');
                mapdata.AddSpaceType(glyph: 'X');
                mapdata.AddSpaceType(glyph: ' ');
                mapdata.AddSpaceType(glyph: '+');
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

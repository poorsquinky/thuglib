using System;
using System.Collections;
using System.Collections.Generic;

namespace ThugLib
{
    public class RunAndDisplayMap
    {
        private static int[][] AllocateMap(int w, int h)
        {
            int[][] map = new int[w][];
            for (int i = 0; i < w; i++)
            {
                map[i] = new int[h];
            }
            return map;
        }

        private static void DisplayMap(int[][] map, char[] palette)
        {
            for (int i = 0; i < map[0].Length; i++) 
            {
                for (int j = 0; j < map.Length; j++) 
                {
                    Console.Write(palette[map[j][i]]);
                }
                Console.Write("\n");
            }
        }

        public static int Main(String[] args)
        {
            ClearMapGenerator gen = new ClearMapGenerator(new int[] {0, 0},
               MapCoordinate.GenerateRandom());
            int[][] map = AllocateMap(80, 40);
            ClearMapGenerator gen2 = new ClearMapGenerator(new int[]{2, 1},
               MapCoordinate.GenerateRandom());
            gen2.Run(map, new MapRectangle(10, 10, 10, 10), null);
            List<MapRoom> blockedList = new List<MapRoom>();
            MapRectangle fullArea = new MapRectangle(0, 0, 80, 40);
            blockedList.Add(new MapRoom(new MapRectangle(10, 10, 10, 10)));
            gen.Run(map, fullArea, blockedList);

            DungeonRoomMapGenerator drmg = new DungeonRoomMapGenerator(new int[]
               {5, 6, 7}, MapCoordinate.GenerateRandom(), 5, 12, 10, 3);
            List<MapRoom> allRooms = drmg.Run(map, fullArea, blockedList);

            DungeonCorridorMapGenerator dcmg = new DungeonCorridorMapGenerator(
               new int[] {5, 6, 7}, MapCoordinate.GenerateRandom(), 2,
               new int[] {0, 100000, 100000, 0, 0, 100000, 0, 0});
            dcmg.Run(map, fullArea, allRooms);

#if ZERO
            CADecayMapGenerator cad = new CADecayMapGenerator(new int[] {0, 1},
               MapCoordinate.GenerateRandom(), 10, 20, 5, 10);
            // cad.UseCoordinateBasedRandom(); // add this to not get random caves
            cad.Run(map, fullArea, blockedList);
            CAGrowthMapGenerator cag1 = new CAGrowthMapGenerator(new int[]
               {3, 1}, MapCoordinate.GenerateRandom(), 50, 2, 20);
            cag1.Run(map, fullArea, blockedList);
            CAGrowthMapGenerator cag2 = new CAGrowthMapGenerator(new int[]
               {4, 1, 2}, MapCoordinate.GenerateRandom(), 200, 8, 20);
            cag2.Run(map, fullArea, blockedList);
            DungeonRoomMapGenerator drmg = new DungeonRoomMapGenerator(new int[]
               {5, 6, 7}, MapCoordinate.GenerateRandom(), 5, 12, 20, 4);
            drmg.Run(map, fullArea, blockedList);
            DisplayMap(map, new char[] {'#', '.', 'O', '*', '~', 'X', ' ', '+'});
#endif
            DisplayMap(map, new char[] {'#', '#', '#', '*', '~', '#', ' ', '+'});
            return 0;
        }
    }
}

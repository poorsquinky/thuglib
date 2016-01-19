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
            for (int i = 0; i < map.Length; i++) 
            {
                for (int j = 0; j < map[i].Length; j++) 
                {
                    Console.Write(palette[map[j][i]]);
                }
                Console.Write("\n");
            }
        }

        public static int Main(String[] args)
        {
            ClearMapGenerator gen = new ClearMapGenerator(new int[] {0, 0});
            int[][] map = AllocateMap(40, 40);
            gen.Run(map, new MapRectangle(0, 0, 40, 40));
            CADecayMapGenerator cad = new CADecayMapGenerator(new int[] {0, 1},
               10, 20, 5, 10);
            cad.Run(map, new MapRectangle(0, 0, 40, 40));
            DisplayMap(map, new char[] {'#', '.'});
            return 0;
        }
    }
}

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
            
        }

        public static int Main(String[] args)
        {
            ClearMapGenerator gen = new ClearMapGenerator(new int[] {0, 1});
            int[][] map = AllocateMap(30, 30);
            gen.Run(map, new MapRectangle(0, 0, 30, 30));
            DisplayMap(map, new char[] {'#', '.'});
            return 0;
        }
    }
}

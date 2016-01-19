using System;
using System.Collections;
using System.Collections.Generic;

namespace RoguelikeLib
{
    public class ClearMapGenerator : MapGenerator
    {
        public ClearMapGenerator(int[] pixelTypes) : base(pixelTypes)
        {
        }

        // pixel types: 
        //    [0] = outer wall
        //    [1] = inner fill
        // custom params:
        //    none
        public override List<MapRoom> Run(int[][] map, MapRectangle fillRegion)
        {
            for (int i = fillRegion.x; i < fillRegion.x + fillRegion.w; i++)
            {
                for (int j = fillRegion.y; j < fillRegion.y + fillRegion.h; j++)
                {
                    map[i][j] = pixelTypes[1];
                }
            }
            if (pixelTypes[1] != pixelTypes[0])
            {
                for (int i = fillRegion.x; i < fillRegion.x + fillRegion.w; i++)
                {
                    map[i][fillRegion.y] = pixelTypes[0];
                    map[i][fillRegion.y + fillRegion.h] = pixelTypes[0];
                }
                for (int j = fillRegion.y; j < fillRegion.y + fillRegion.h; j++)
                {
                    map[fillRegion.x][j] = pixelTypes[0];
                    map[fillRegion.x + fillRegion.w][j] = pixelTypes[0];
                }
            }
            return new List<MapRoom>(new MapRoom[0]);
        }
    }
}

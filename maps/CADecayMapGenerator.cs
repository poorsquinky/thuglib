using System;
using System.Collections;
using System.Collections.Generic;

namespace ThugLib
{
    public class CADecayMapGenerator : MapGenerator
    {
        private int seedCorridorAverageSpacing;

        private int seedCorridorAverageLength;

        private int numberOfDecays;

        private int decayPercentPerDecayedNeighbor;

        private Random random;

        // pixel types: 
        //    [0] = wall (filled)
        //    [1] = floor (empty)
        // custom params:
        //    seedCorridorAverageSpacing - average x and y distance between
        //       parallel seed corridors
        //    seedCorridorAverageLength - average length of seed corridors;
        //       length from 50% to 150% of that.  Should be somewhat less than
        //       smallest map dim.  Seed corridors are truncated at inviolable
        //       rectangles
        //    numberOfDecays - number of CA decay steps to run
        //    decayPercentPerDecayedNeighbor - chance of a filled square 
        //       decaying to empty = # of decayed neighbors * this percent
        public CADecayMapGenerator(int[] pixelTypes,
           int seedCorridorAverageSpacing,
           int seedCorridorAverageLength,
           int numberOfDecays,
           int decayPercentPerDecayedNeighbor) : base(pixelTypes)
        {
            this.seedCorridorAverageSpacing = seedCorridorAverageSpacing;
            this.seedCorridorAverageLength = seedCorridorAverageLength;
            this.numberOfDecays = numberOfDecays;
            this.decayPercentPerDecayedNeighbor = decayPercentPerDecayedNeighbor;
            this.random = new Random();
        }

        public override List<MapRoom> Run(int[][] map, MapRectangle fillRegion)
        {
            // clear to solid
            for (int i = fillRegion.x; i < fillRegion.x + fillRegion.w; i++)
            {
                for (int j = fillRegion.y; j < fillRegion.y + fillRegion.h; j++)
                {
                    map[i][j] = pixelTypes[0];
                }
            }

            // put in the horizontal seed corridors
            int n = fillRegion.h / seedCorridorAverageSpacing;
            for (int i = 0; i < n; i++)
            {
                int l = random.Next(seedCorridorAverageLength / 2,
                   3 * seedCorridorAverageLength / 2 + 1);
                int offset = 0;
                if (l >= fillRegion.w)
                {
                    l = fillRegion.w;
                }
                else
                {
                    offset = random.Next(0, fillRegion.w - l + 1);
                }
                int crossOffset = random.Next(fillRegion.y, fillRegion.y +
                   fillRegion.h);
                for (int j = offset; j < offset + l; j++)
                {
                    map[j + fillRegion.x][crossOffset] = pixelTypes[1];
                }
            }

            // put in the vertical seed corridors
            n = fillRegion.w / seedCorridorAverageSpacing;
            for (int i = 0; i < n; i++)
            {
                int l = random.Next(seedCorridorAverageLength / 2,
                   3 * seedCorridorAverageLength / 2 + 1);
                int offset = 0;
                if (l >= fillRegion.h)
                {
                    l = fillRegion.h;
                }
                else
                {
                    offset = random.Next(0, fillRegion.h - l + 1);
                }
                int crossOffset = random.Next(fillRegion.x, fillRegion.x +
                   fillRegion.w);
                for (int j = offset; j < offset + l; j++)
                {
                    map[crossOffset][j + fillRegion.y] = pixelTypes[1];
                }
            }

            for (int k = 0; k < numberOfDecays; k++)
            {
                for (int i = fillRegion.x; i < fillRegion.x + fillRegion.w; i++)
                {
                    for (int j = fillRegion.y; j < fillRegion.y + fillRegion.h;
                       j++)
                    {
                        if (map[i][j] == pixelTypes[0])
                        {
                            int nNeighbors = 0;
                            if (i > 0)
                            {
                               if (j > 0)
                               {
                                   if (map[i - 1][j - 1] == pixelTypes[1])
                                   {
                                       nNeighbors++;
                                   }
                               }
                               if (j < map[i].Length - 1)
                               {
                                   if (map[i - 1][j + 1] == pixelTypes[1])
                                   {
                                       nNeighbors++;
                                   }
                               }
                               if (map[i - 1][j] == pixelTypes[1])
                               {
                                   nNeighbors++;
                               }
                            }
                            if (i < map.Length - 1)
                            {
                               if (j > 0)
                               {
                                   if (map[i + 1][j - 1] == pixelTypes[1])
                                   {
                                       nNeighbors++;
                                   }
                               }
                               if (j < map[i].Length - 1)
                               {
                                   if (map[i + 1][j + 1] == pixelTypes[1])
                                   {
                                       nNeighbors++;
                                   }
                               }
                               if (map[i + 1][j] == pixelTypes[1])
                               {
                                   nNeighbors++;
                               }
                            }
                            if (j > 0)
                            {
                                if (map[i][j - 1] == pixelTypes[1])
                                {
                                    nNeighbors++;
                                }
                            }
                            if (j < map[i].Length - 1)
                            {
                                if (map[i][j + 1] == pixelTypes[1])
                                {
                                    nNeighbors++;
                                }
                            }
                            int flipChance = nNeighbors * 
                               decayPercentPerDecayedNeighbor;
                            if (random.Next(1, 101) < flipChance)
                            {
                                map[i][j] = -1;
                            }
                        }
                    }
                }
                for (int i = fillRegion.x; i < fillRegion.x + fillRegion.w; i++)
                {
                    for (int j = fillRegion.y; j < fillRegion.y + fillRegion.h;
                       j++)
                    {
                        if (map[i][j] == -1)
                        {
                            map[i][j] = pixelTypes[1];
                        }
                    }
                }
            }

            return new List<MapRoom>(new MapRoom[0]);
        }
    }
}

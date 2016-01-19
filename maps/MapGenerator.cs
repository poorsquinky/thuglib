using System;
using System.Collections;
using System.Collections.Generic;

namespace ThugLib
{
    public struct MapRectangle
    {
        public int x, y; /* upper left corner */
        public int w, h;
        public int x2
        {
           get
           {
              return x + w - 1;
           }
        }
        public int y2
        {
            get
            {
                return y + h - 1;
            }
        }
        public MapRectangle(int x, int y, int w, int h)
        {
            this.x = x; this.y = y; this.w = w; this.h = h;
        }
    };

    public struct MapRoomDoor
    {
        public int side; /* 0 1 2 3 = N E S W */
        public int offset; /* from W or N end */
    };

    public struct MapRoom
    {
        public MapRectangle bounds;
        public List<MapRoomDoor> doors;
        public MapRoom(MapRectangle bounds)
        {
            this.bounds = bounds;
            this.doors = new List<MapRoomDoor>();
        }
    };

    // represents the map's coordinate in the world; used to seed the RNG
    // if desired.
    public struct MapCoordinate
    {
        public long guid;

        public static long nextGuid = 0L;

        public static MapCoordinate GenerateRandom()
        {
            MapCoordinate newCoord = new MapCoordinate();
            newCoord.guid = nextGuid++;
            return newCoord;
        }
    };

    public abstract class MapGenerator
    {
        protected int[] pixelTypes;

        protected MapCoordinate coordinate;

        protected Random random;

        public void UseCoordinateBasedRandom()
        {
            random = new Random((Int32)coordinate.guid);
        }

        public MapGenerator(int[] pixelTypes, MapCoordinate coordinate)
        {
            this.pixelTypes = pixelTypes;
            this.coordinate = coordinate;
            this.random = new Random();
        }

        protected int NextRandom(int low, int highPlusOne)
        {
            return random.Next(low, highPlusOne);
        }

        public abstract List<MapRoom> Run(int[][] map, MapRectangle fillRegion,
           List<MapRoom> roomsToInclude);

        public bool[][] BuildProtectedMap(List<MapRoom> roomsToInclude,
           int w, int h) 
        {
            bool[][] mask = new bool[w][];
            for (int i = 0; i < w; i++)
            {
                mask[i] = new bool[h];
            }
            if (roomsToInclude != null)
            {
                for (int i = 0; i < roomsToInclude.Count; i++)
                {
                    for (int x = roomsToInclude[i].bounds.x; x <=
                       roomsToInclude[i].bounds.x2; x++)
                    {
                        for (int y = roomsToInclude[i].bounds.y; y <=
                           roomsToInclude[i].bounds.y2; y++)
                        {
                            mask[x][y] = true;
                        }
                    }
                }
            }
            return mask;
        }
    }
}

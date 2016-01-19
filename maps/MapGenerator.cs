using System;
using System.Collections;
using System.Collections.Generic;

namespace RoguelikeLib
{
    public struct MapRectangle
    {
        public int x, y; /* upper left corner */
        public int w, h;
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
        MapRectangle bounds;
        List<MapRoomDoor> doors;
    };

    public abstract class MapGenerator
    {
        protected int[] pixelTypes;

        public MapGenerator(int[] pixelTypes)
        {
            this.pixelTypes = pixelTypes;
        }

        public abstract List<MapRoom> Run(int[][] map, MapRectangle fillRegion);
    }
}

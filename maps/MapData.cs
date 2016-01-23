using System;
using System.Collections;
using System.Collections.Generic;


namespace ThugLib
{
    public class MapSpaceType
    {
        public char glyph         = '#';
        public bool passable      = false;
        public string description = "A wall.";
        public int r = 128;
        public int g = 128;
        public int b = 128;
        public int br = 0;
        public int bg = 0;
        public int bb = 0;

    }

    public class MapData
    {
        public int[][] grid;
        public List<MapSpaceType> palette = new List<MapSpaceType>();

        public MapData(int w, int h)
        {
            this.grid = new int[w][];
            for (int i = 0; i < w; i++)
            {
                this.grid[i] = new int[h];
            }
        }

        public MapData()
        {
            // XXX this placeholder map is going away soon
            this.grid     = new int[20][];
            this.grid[0]  = new int[20] {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};
            this.grid[1]  = new int[20] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1};
            this.grid[2]  = new int[20] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1};
            this.grid[3]  = new int[20] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1};
            this.grid[4]  = new int[20] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1};
            this.grid[5]  = new int[20] {1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1};
            this.grid[6]  = new int[20] {1,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1};
            this.grid[7]  = new int[20] {1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1};
            this.grid[8]  = new int[20] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1};
            this.grid[9]  = new int[20] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1};
            this.grid[10] = new int[20] {1,0,0,0,0,0,0,0,0,0,0,1,1,0,1,1,1,1,1,1};
            this.grid[11] = new int[20] {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,1};
            this.grid[12] = new int[20] {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,1};
            this.grid[13] = new int[20] {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,1};
            this.grid[14] = new int[20] {1,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,1,1};
            this.grid[15] = new int[20] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1};
            this.grid[16] = new int[20] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1};
            this.grid[17] = new int[20] {1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1};
            this.grid[18] = new int[20] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1};
            this.grid[19] = new int[20] {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};

            MapSpaceType open = new MapSpaceType();
            MapSpaceType wall = new MapSpaceType();

            open.glyph       = '.';
            open.passable    = true;
            open.description = "Open space.";

            this.palette.Add(open);
            this.palette.Add(wall);
        }

        public void AddSpaceType(
                char glyph,
                bool passable      = true,
                string description = "",
                int r              = 128,
                int g              = 128,
                int b              = 128,
                int br             = 0,
                int bg             = 0,
                int bb             = 0)
        {
            MapSpaceType st = new MapSpaceType();
            st.glyph = glyph;
            st.passable = passable;
            st.description = description;
            st.r = r;
            st.g = g;
            st.b = b;
            st.br = br;
            st.bg = bg;
            st.bb = bb;

            this.palette.Add(st);
        }

    }
}

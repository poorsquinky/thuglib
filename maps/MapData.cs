using System;
using System.Collections;
using System.Collections.Generic;


namespace ThugLib
{
    public class MapSpaceType
    {
        public char glyph         = '#';
        public bool passable      = false;
        public bool transparent   = false;
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

        public MapData(int w=100, int h=100)
        {
            this.grid = new int[w][];
            for (int i = 0; i < w; i++)
            {
                this.grid[i] = new int[h];
            }
        }

        public void AddSpaceType(
                char glyph,
                bool passable      = true,
                bool transparent   = true,
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
            st.transparent = transparent;
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

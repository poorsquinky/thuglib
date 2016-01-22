using System;
using System.Collections;
using System.Collections.Generic;


namespace ThugLib
{
    public class LevelSpaceType
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
        public List<LevelSpaceType> palette = new List<LevelSpaceType>();

        public MapData()
        {
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

            LevelSpaceType open = new LevelSpaceType();
            LevelSpaceType wall = new LevelSpaceType();

            open.glyph       = '.';
            open.passable    = true;
            open.description = "Open space.";

            this.palette.Add(open);
            this.palette.Add(wall);
        }

    }
}

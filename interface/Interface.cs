using System;
using System.Collections;
using System.Collections.Generic;


namespace ThugLib
{

    public class InterfaceMapElement
    {
        int x1,y1,x2,y2;
        Interface iface;

        public MapData map;

        public InterfaceMapElement(int x1, int y1, int x2, int y2, Interface iface) {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            this.iface = iface;
            this.map = new MapData();
        }

        public void Draw()
        {
            for (int y = this.y1; y <= this.y2; y++)
            {
                for (int x = this.x1; x <= this.x2; x++)
                {
                    if ( (x < this.map.grid.Length) && (y < this.map.grid[x].Length) ) {
                        LevelSpaceType ls = this.map.palette[this.map.grid[y][x]];
                        iface.DrawAt(x,y,ls.glyph.ToString(),ls.r,ls.g,ls.b,ls.br,ls.bg,ls.bb);
                    }
                }
            }
        }
    }

    public abstract class Interface
    {
        public Interface()
        {
        }

        public virtual void DrawAt(int x, int y, string glyph)
        {
            DrawAt(x,y,glyph,192,192,192,0,0,0);
        }

        public virtual void DrawAt(int x, int y, string glyph, int r, int g, int b)
        {
            DrawAt(x,y,glyph,r,g,b,0,0,0);
        }

        public virtual void DrawAt(int x, int y, string glyph, int r, int g, int b, int br, int bg, int bb)
        {
        }
    }

}


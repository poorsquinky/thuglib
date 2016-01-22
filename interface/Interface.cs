using System;
using System.Collections;
using System.Collections.Generic;


namespace ThugLib
{

    public class InterfaceMapElement
    {
        int x1,y1,x2,y2;
        Interface iface;

        public int[][] map;
        char[] palette = new char[] {'#', '.', 'O', '*', '~', 'X', ' ', '+'};

        private static int[][] AllocateMap(int w, int h)
        {
            int[][] map = new int[w][];
            for (int i = 0; i < w; i++)
            {
                map[i] = new int[h];
            }
            return map;
        }

        public InterfaceMapElement(int x1, int y1, int x2, int y2, Interface iface) {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            this.iface = iface;
            this.map = AllocateMap(40,40);
        }

        public void Draw()
        {
            for (int y = this.y1; y <= this.y2; y++)
            {
                for (int x = this.x1; x <= this.x2; x++)
                {
                    if ( (x < this.map.Length) && (y < this.map[x].Length) )
                        iface.DrawAt(x,y,this.palette[this.map[x][y]].ToString(),64,64,64,0,0,0);
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


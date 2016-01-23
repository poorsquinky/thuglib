using System;
using System.Collections;
using System.Collections.Generic;


namespace ThugLib
{

    public class UIMapElement
    {
        int x1,y1,x2,y2;
        UI ui;
        int offset_x = 35, offset_y = 6;

        public MapData map;

        public UIMapElement(int x1, int y1, int x2, int y2, UI ui) {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            this.ui = ui;
            this.map = new MapData();
        }

        public void Draw()
        {

            for (int y = 0; y <= this.y2 - this.y1; y++)
            {
                int screen_y = y + this.y1;
                int map_y    = y + this.offset_y - 9;
                for (int x = 0; x <= this.x2 - this.x1; x++)
                {
                    // XXX consider having offset_x point to center
                    int screen_x = x + this.x1;
                    int map_x    = x + this.offset_x - 40;

                    if (
                            (map_x >= 0) &&
                            (map_y >= 0) &&
                            (map_x < this.map.grid.Length) &&
                            (map_y < this.map.grid[map_x].Length) &&
                            (this.map.grid[map_x][map_y] < this.map.palette.Count) )
                    {
                        MapSpaceType ms = this.map.palette[this.map.grid[map_x][map_y]];
                        ui.DrawAt(screen_x,screen_y,ms.glyph.ToString(),ms.r,ms.g,ms.b,ms.br,ms.bg,ms.bb);
                    } else {
                        ui.DrawAt(screen_x,screen_y,"#",64,64,64,0,0,0);
                    }

                }
            }
        }
    }

    public abstract class UI
    {
        public UI()
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


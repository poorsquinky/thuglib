using System;

using System.Collections.Generic;

namespace ThugLib
{
    public class TerminalInterface : Interface
    {
        public struct TerminalCharacter
        {
            public string glyph;
            public int r,g,b,br,bg,bb;
        };
        public List<List<TerminalCharacter>> screenBuffer = new List<List<TerminalCharacter>>();

        public int term_w = 80;
        public int term_h = 24;

        public void InitializeBuffers()
        {
            for (int y = 0; y < term_h; y++)
            {
                List<TerminalCharacter> row = new List<TerminalCharacter>();
                for (int x = 0; x < term_w; x++)
                {
                    TerminalCharacter tc = new TerminalCharacter();
                    tc.glyph=" ";
                    tc.r=255;
                    tc.g=255;
                    tc.b=255;
                    tc.br=0;
                    tc.bg=0;
                    tc.bb=0;
                    row.Add(tc);
                }
                screenBuffer.Add(row);
            }
        }

        public InterfaceMapElement MapElement(int x, int y, int h)
        {
            return new InterfaceMapElement(
                    x1: x,
                    y1: y,
                    x2: term_w - 1,
                    y2: y + h,
                    iface: this
            );
        }

        public override void DrawAt(int x, int y, string glyph)
        {
            DrawAt(x,y,glyph,192,192,192,0,0,0);
        }

        public override void DrawAt(int x, int y, string glyph, int r, int g, int b)
        {
            DrawAt(x,y,glyph,r,g,b,0,0,0);
        }

        public override void DrawAt(int x, int y, string glyph, int r, int g, int b, int br, int bg, int bb)
        {
            foreach (char c in glyph.ToCharArray()) {
                if (x > term_w)
                    return;
                TerminalCharacter buf = screenBuffer[y][x];
                buf.glyph = c.ToString();
                buf.r=r;
                buf.g=g;
                buf.b=b;
                buf.br=br;
                buf.bg=bg;
                buf.bb=bb;
                screenBuffer[y][x] = buf;
                x += 1;
            }
        }

        public virtual void ClearScreen()
        {
            for (int y = 0; y < term_h; y++)
                Console.Write("\n");
        }

        public virtual void DrawScreen()
        {
            for (int y = 0; y < term_h; y++)
            {
                for (int x = 0; x < term_w; x++)
                {
                    Console.Write(screenBuffer[y][x].glyph);
                }
                Console.Write("\n");
            }
        }

        public TerminalInterface()
        {
            InitializeBuffers();
        }
    }
}

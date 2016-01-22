using System;

using System.Text;
using System.IO;

namespace ThugLib
{
    public class VT100Display : TerminalDisplay
    {

        public const string ESC = "\x01B";

        public const string TERM_RESET      = ESC + "c";
        public const string TERM_NOLINEWRAP = ESC + "[7l";
        public const string TERM_LINEWRAP   = ESC + "[7h";

        public string CursorPosition(int row, int col)
        {
            return ESC + "[" + row.ToString() + ";" + col.ToString() + "H";
        }

        public int map_x1 = 1;
        public int map_y1 = 4;
        public int map_x2 = 79;
        public int map_y2 = 21;

        // TODO: actually sort out a decent way to query the window size

        // color values cribbed from https://gist.github.com/MicahElliott/719710
        public struct ColorEntry
        {
            public int code,r,g,b;
        };
        public readonly static ColorEntry[] ColorTable = {
            new ColorEntry { code=00,  r=0x00, g=0x00, b=0x00 },
            new ColorEntry { code=01,  r=0x80, g=0x00, b=0x00 },
            new ColorEntry { code=02,  r=0x00, g=0x80, b=0x00 },
            new ColorEntry { code=03,  r=0x80, g=0x80, b=0x00 },
            new ColorEntry { code=04,  r=0x00, g=0x00, b=0x80 },
            new ColorEntry { code=05,  r=0x80, g=0x00, b=0x80 },
            new ColorEntry { code=06,  r=0x00, g=0x80, b=0x80 },
            new ColorEntry { code=07,  r=0xc0, g=0xc0, b=0xc0 },
            new ColorEntry { code=08,  r=0x80, g=0x80, b=0x80 },
            new ColorEntry { code=09,  r=0xff, g=0x00, b=0x00 },
            new ColorEntry { code=10,  r=0x00, g=0xff, b=0x00 },
            new ColorEntry { code=11,  r=0xff, g=0xff, b=0x00 },
            new ColorEntry { code=12,  r=0x00, g=0x00, b=0xff },
            new ColorEntry { code=13,  r=0xff, g=0x00, b=0xff },
            new ColorEntry { code=14,  r=0x00, g=0xff, b=0xff },
            new ColorEntry { code=15,  r=0xff, g=0xff, b=0xff },
            new ColorEntry { code=16,  r=0x00, g=0x00, b=0x00 },
            new ColorEntry { code=17,  r=0x00, g=0x00, b=0x5f },
            new ColorEntry { code=18,  r=0x00, g=0x00, b=0x87 },
            new ColorEntry { code=19,  r=0x00, g=0x00, b=0xaf },
            new ColorEntry { code=20,  r=0x00, g=0x00, b=0xd7 },
            new ColorEntry { code=21,  r=0x00, g=0x00, b=0xff },
            new ColorEntry { code=22,  r=0x00, g=0x5f, b=0x00 },
            new ColorEntry { code=23,  r=0x00, g=0x5f, b=0x5f },
            new ColorEntry { code=24,  r=0x00, g=0x5f, b=0x87 },
            new ColorEntry { code=25,  r=0x00, g=0x5f, b=0xaf },
            new ColorEntry { code=26,  r=0x00, g=0x5f, b=0xd7 },
            new ColorEntry { code=27,  r=0x00, g=0x5f, b=0xff },
            new ColorEntry { code=28,  r=0x00, g=0x87, b=0x00 },
            new ColorEntry { code=29,  r=0x00, g=0x87, b=0x5f },
            new ColorEntry { code=30,  r=0x00, g=0x87, b=0x87 },
            new ColorEntry { code=31,  r=0x00, g=0x87, b=0xaf },
            new ColorEntry { code=32,  r=0x00, g=0x87, b=0xd7 },
            new ColorEntry { code=33,  r=0x00, g=0x87, b=0xff },
            new ColorEntry { code=34,  r=0x00, g=0xaf, b=0x00 },
            new ColorEntry { code=35,  r=0x00, g=0xaf, b=0x5f },
            new ColorEntry { code=36,  r=0x00, g=0xaf, b=0x87 },
            new ColorEntry { code=37,  r=0x00, g=0xaf, b=0xaf },
            new ColorEntry { code=38,  r=0x00, g=0xaf, b=0xd7 },
            new ColorEntry { code=39,  r=0x00, g=0xaf, b=0xff },
            new ColorEntry { code=40,  r=0x00, g=0xd7, b=0x00 },
            new ColorEntry { code=41,  r=0x00, g=0xd7, b=0x5f },
            new ColorEntry { code=42,  r=0x00, g=0xd7, b=0x87 },
            new ColorEntry { code=43,  r=0x00, g=0xd7, b=0xaf },
            new ColorEntry { code=44,  r=0x00, g=0xd7, b=0xd7 },
            new ColorEntry { code=45,  r=0x00, g=0xd7, b=0xff },
            new ColorEntry { code=46,  r=0x00, g=0xff, b=0x00 },
            new ColorEntry { code=47,  r=0x00, g=0xff, b=0x5f },
            new ColorEntry { code=48,  r=0x00, g=0xff, b=0x87 },
            new ColorEntry { code=49,  r=0x00, g=0xff, b=0xaf },
            new ColorEntry { code=50,  r=0x00, g=0xff, b=0xd7 },
            new ColorEntry { code=51,  r=0x00, g=0xff, b=0xff },
            new ColorEntry { code=52,  r=0x5f, g=0x00, b=0x00 },
            new ColorEntry { code=53,  r=0x5f, g=0x00, b=0x5f },
            new ColorEntry { code=54,  r=0x5f, g=0x00, b=0x87 },
            new ColorEntry { code=55,  r=0x5f, g=0x00, b=0xaf },
            new ColorEntry { code=56,  r=0x5f, g=0x00, b=0xd7 },
            new ColorEntry { code=57,  r=0x5f, g=0x00, b=0xff },
            new ColorEntry { code=58,  r=0x5f, g=0x5f, b=0x00 },
            new ColorEntry { code=59,  r=0x5f, g=0x5f, b=0x5f },
            new ColorEntry { code=60,  r=0x5f, g=0x5f, b=0x87 },
            new ColorEntry { code=61,  r=0x5f, g=0x5f, b=0xaf },
            new ColorEntry { code=62,  r=0x5f, g=0x5f, b=0xd7 },
            new ColorEntry { code=63,  r=0x5f, g=0x5f, b=0xff },
            new ColorEntry { code=64,  r=0x5f, g=0x87, b=0x00 },
            new ColorEntry { code=65,  r=0x5f, g=0x87, b=0x5f },
            new ColorEntry { code=66,  r=0x5f, g=0x87, b=0x87 },
            new ColorEntry { code=67,  r=0x5f, g=0x87, b=0xaf },
            new ColorEntry { code=68,  r=0x5f, g=0x87, b=0xd7 },
            new ColorEntry { code=69,  r=0x5f, g=0x87, b=0xff },
            new ColorEntry { code=70,  r=0x5f, g=0xaf, b=0x00 },
            new ColorEntry { code=71,  r=0x5f, g=0xaf, b=0x5f },
            new ColorEntry { code=72,  r=0x5f, g=0xaf, b=0x87 },
            new ColorEntry { code=73,  r=0x5f, g=0xaf, b=0xaf },
            new ColorEntry { code=74,  r=0x5f, g=0xaf, b=0xd7 },
            new ColorEntry { code=75,  r=0x5f, g=0xaf, b=0xff },
            new ColorEntry { code=76,  r=0x5f, g=0xd7, b=0x00 },
            new ColorEntry { code=77,  r=0x5f, g=0xd7, b=0x5f },
            new ColorEntry { code=78,  r=0x5f, g=0xd7, b=0x87 },
            new ColorEntry { code=79,  r=0x5f, g=0xd7, b=0xaf },
            new ColorEntry { code=80,  r=0x5f, g=0xd7, b=0xd7 },
            new ColorEntry { code=81,  r=0x5f, g=0xd7, b=0xff },
            new ColorEntry { code=82,  r=0x5f, g=0xff, b=0x00 },
            new ColorEntry { code=83,  r=0x5f, g=0xff, b=0x5f },
            new ColorEntry { code=84,  r=0x5f, g=0xff, b=0x87 },
            new ColorEntry { code=85,  r=0x5f, g=0xff, b=0xaf },
            new ColorEntry { code=86,  r=0x5f, g=0xff, b=0xd7 },
            new ColorEntry { code=87,  r=0x5f, g=0xff, b=0xff },
            new ColorEntry { code=88,  r=0x87, g=0x00, b=0x00 },
            new ColorEntry { code=89,  r=0x87, g=0x00, b=0x5f },
            new ColorEntry { code=90,  r=0x87, g=0x00, b=0x87 },
            new ColorEntry { code=91,  r=0x87, g=0x00, b=0xaf },
            new ColorEntry { code=92,  r=0x87, g=0x00, b=0xd7 },
            new ColorEntry { code=93,  r=0x87, g=0x00, b=0xff },
            new ColorEntry { code=94,  r=0x87, g=0x5f, b=0x00 },
            new ColorEntry { code=95,  r=0x87, g=0x5f, b=0x5f },
            new ColorEntry { code=96,  r=0x87, g=0x5f, b=0x87 },
            new ColorEntry { code=97,  r=0x87, g=0x5f, b=0xaf },
            new ColorEntry { code=98,  r=0x87, g=0x5f, b=0xd7 },
            new ColorEntry { code=99,  r=0x87, g=0x5f, b=0xff },
            new ColorEntry { code=100, r=0x87, g=0x87, b=0x00 },
            new ColorEntry { code=101, r=0x87, g=0x87, b=0x5f },
            new ColorEntry { code=102, r=0x87, g=0x87, b=0x87 },
            new ColorEntry { code=103, r=0x87, g=0x87, b=0xaf },
            new ColorEntry { code=104, r=0x87, g=0x87, b=0xd7 },
            new ColorEntry { code=105, r=0x87, g=0x87, b=0xff },
            new ColorEntry { code=106, r=0x87, g=0xaf, b=0x00 },
            new ColorEntry { code=107, r=0x87, g=0xaf, b=0x5f },
            new ColorEntry { code=108, r=0x87, g=0xaf, b=0x87 },
            new ColorEntry { code=109, r=0x87, g=0xaf, b=0xaf },
            new ColorEntry { code=110, r=0x87, g=0xaf, b=0xd7 },
            new ColorEntry { code=111, r=0x87, g=0xaf, b=0xff },
            new ColorEntry { code=112, r=0x87, g=0xd7, b=0x00 },
            new ColorEntry { code=113, r=0x87, g=0xd7, b=0x5f },
            new ColorEntry { code=114, r=0x87, g=0xd7, b=0x87 },
            new ColorEntry { code=115, r=0x87, g=0xd7, b=0xaf },
            new ColorEntry { code=116, r=0x87, g=0xd7, b=0xd7 },
            new ColorEntry { code=117, r=0x87, g=0xd7, b=0xff },
            new ColorEntry { code=118, r=0x87, g=0xff, b=0x00 },
            new ColorEntry { code=119, r=0x87, g=0xff, b=0x5f },
            new ColorEntry { code=120, r=0x87, g=0xff, b=0x87 },
            new ColorEntry { code=121, r=0x87, g=0xff, b=0xaf },
            new ColorEntry { code=122, r=0x87, g=0xff, b=0xd7 },
            new ColorEntry { code=123, r=0x87, g=0xff, b=0xff },
            new ColorEntry { code=124, r=0xaf, g=0x00, b=0x00 },
            new ColorEntry { code=125, r=0xaf, g=0x00, b=0x5f },
            new ColorEntry { code=126, r=0xaf, g=0x00, b=0x87 },
            new ColorEntry { code=127, r=0xaf, g=0x00, b=0xaf },
            new ColorEntry { code=128, r=0xaf, g=0x00, b=0xd7 },
            new ColorEntry { code=129, r=0xaf, g=0x00, b=0xff },
            new ColorEntry { code=130, r=0xaf, g=0x5f, b=0x00 },
            new ColorEntry { code=131, r=0xaf, g=0x5f, b=0x5f },
            new ColorEntry { code=132, r=0xaf, g=0x5f, b=0x87 },
            new ColorEntry { code=133, r=0xaf, g=0x5f, b=0xaf },
            new ColorEntry { code=134, r=0xaf, g=0x5f, b=0xd7 },
            new ColorEntry { code=135, r=0xaf, g=0x5f, b=0xff },
            new ColorEntry { code=136, r=0xaf, g=0x87, b=0x00 },
            new ColorEntry { code=137, r=0xaf, g=0x87, b=0x5f },
            new ColorEntry { code=138, r=0xaf, g=0x87, b=0x87 },
            new ColorEntry { code=139, r=0xaf, g=0x87, b=0xaf },
            new ColorEntry { code=140, r=0xaf, g=0x87, b=0xd7 },
            new ColorEntry { code=141, r=0xaf, g=0x87, b=0xff },
            new ColorEntry { code=142, r=0xaf, g=0xaf, b=0x00 },
            new ColorEntry { code=143, r=0xaf, g=0xaf, b=0x5f },
            new ColorEntry { code=144, r=0xaf, g=0xaf, b=0x87 },
            new ColorEntry { code=145, r=0xaf, g=0xaf, b=0xaf },
            new ColorEntry { code=146, r=0xaf, g=0xaf, b=0xd7 },
            new ColorEntry { code=147, r=0xaf, g=0xaf, b=0xff },
            new ColorEntry { code=148, r=0xaf, g=0xd7, b=0x00 },
            new ColorEntry { code=149, r=0xaf, g=0xd7, b=0x5f },
            new ColorEntry { code=150, r=0xaf, g=0xd7, b=0x87 },
            new ColorEntry { code=151, r=0xaf, g=0xd7, b=0xaf },
            new ColorEntry { code=152, r=0xaf, g=0xd7, b=0xd7 },
            new ColorEntry { code=153, r=0xaf, g=0xd7, b=0xff },
            new ColorEntry { code=154, r=0xaf, g=0xff, b=0x00 },
            new ColorEntry { code=155, r=0xaf, g=0xff, b=0x5f },
            new ColorEntry { code=156, r=0xaf, g=0xff, b=0x87 },
            new ColorEntry { code=157, r=0xaf, g=0xff, b=0xaf },
            new ColorEntry { code=158, r=0xaf, g=0xff, b=0xd7 },
            new ColorEntry { code=159, r=0xaf, g=0xff, b=0xff },
            new ColorEntry { code=160, r=0xd7, g=0x00, b=0x00 },
            new ColorEntry { code=161, r=0xd7, g=0x00, b=0x5f },
            new ColorEntry { code=162, r=0xd7, g=0x00, b=0x87 },
            new ColorEntry { code=163, r=0xd7, g=0x00, b=0xaf },
            new ColorEntry { code=164, r=0xd7, g=0x00, b=0xd7 },
            new ColorEntry { code=165, r=0xd7, g=0x00, b=0xff },
            new ColorEntry { code=166, r=0xd7, g=0x5f, b=0x00 },
            new ColorEntry { code=167, r=0xd7, g=0x5f, b=0x5f },
            new ColorEntry { code=168, r=0xd7, g=0x5f, b=0x87 },
            new ColorEntry { code=169, r=0xd7, g=0x5f, b=0xaf },
            new ColorEntry { code=170, r=0xd7, g=0x5f, b=0xd7 },
            new ColorEntry { code=171, r=0xd7, g=0x5f, b=0xff },
            new ColorEntry { code=172, r=0xd7, g=0x87, b=0x00 },
            new ColorEntry { code=173, r=0xd7, g=0x87, b=0x5f },
            new ColorEntry { code=174, r=0xd7, g=0x87, b=0x87 },
            new ColorEntry { code=175, r=0xd7, g=0x87, b=0xaf },
            new ColorEntry { code=176, r=0xd7, g=0x87, b=0xd7 },
            new ColorEntry { code=177, r=0xd7, g=0x87, b=0xff },
            new ColorEntry { code=178, r=0xd7, g=0xaf, b=0x00 },
            new ColorEntry { code=179, r=0xd7, g=0xaf, b=0x5f },
            new ColorEntry { code=180, r=0xd7, g=0xaf, b=0x87 },
            new ColorEntry { code=181, r=0xd7, g=0xaf, b=0xaf },
            new ColorEntry { code=182, r=0xd7, g=0xaf, b=0xd7 },
            new ColorEntry { code=183, r=0xd7, g=0xaf, b=0xff },
            new ColorEntry { code=184, r=0xd7, g=0xd7, b=0x00 },
            new ColorEntry { code=185, r=0xd7, g=0xd7, b=0x5f },
            new ColorEntry { code=186, r=0xd7, g=0xd7, b=0x87 },
            new ColorEntry { code=187, r=0xd7, g=0xd7, b=0xaf },
            new ColorEntry { code=188, r=0xd7, g=0xd7, b=0xd7 },
            new ColorEntry { code=189, r=0xd7, g=0xd7, b=0xff },
            new ColorEntry { code=190, r=0xd7, g=0xff, b=0x00 },
            new ColorEntry { code=191, r=0xd7, g=0xff, b=0x5f },
            new ColorEntry { code=192, r=0xd7, g=0xff, b=0x87 },
            new ColorEntry { code=193, r=0xd7, g=0xff, b=0xaf },
            new ColorEntry { code=194, r=0xd7, g=0xff, b=0xd7 },
            new ColorEntry { code=195, r=0xd7, g=0xff, b=0xff },
            new ColorEntry { code=196, r=0xff, g=0x00, b=0x00 },
            new ColorEntry { code=197, r=0xff, g=0x00, b=0x5f },
            new ColorEntry { code=198, r=0xff, g=0x00, b=0x87 },
            new ColorEntry { code=199, r=0xff, g=0x00, b=0xaf },
            new ColorEntry { code=200, r=0xff, g=0x00, b=0xd7 },
            new ColorEntry { code=201, r=0xff, g=0x00, b=0xff },
            new ColorEntry { code=202, r=0xff, g=0x5f, b=0x00 },
            new ColorEntry { code=203, r=0xff, g=0x5f, b=0x5f },
            new ColorEntry { code=204, r=0xff, g=0x5f, b=0x87 },
            new ColorEntry { code=205, r=0xff, g=0x5f, b=0xaf },
            new ColorEntry { code=206, r=0xff, g=0x5f, b=0xd7 },
            new ColorEntry { code=207, r=0xff, g=0x5f, b=0xff },
            new ColorEntry { code=208, r=0xff, g=0x87, b=0x00 },
            new ColorEntry { code=209, r=0xff, g=0x87, b=0x5f },
            new ColorEntry { code=210, r=0xff, g=0x87, b=0x87 },
            new ColorEntry { code=211, r=0xff, g=0x87, b=0xaf },
            new ColorEntry { code=212, r=0xff, g=0x87, b=0xd7 },
            new ColorEntry { code=213, r=0xff, g=0x87, b=0xff },
            new ColorEntry { code=214, r=0xff, g=0xaf, b=0x00 },
            new ColorEntry { code=215, r=0xff, g=0xaf, b=0x5f },
            new ColorEntry { code=216, r=0xff, g=0xaf, b=0x87 },
            new ColorEntry { code=217, r=0xff, g=0xaf, b=0xaf },
            new ColorEntry { code=218, r=0xff, g=0xaf, b=0xd7 },
            new ColorEntry { code=219, r=0xff, g=0xaf, b=0xff },
            new ColorEntry { code=220, r=0xff, g=0xd7, b=0x00 },
            new ColorEntry { code=221, r=0xff, g=0xd7, b=0x5f },
            new ColorEntry { code=222, r=0xff, g=0xd7, b=0x87 },
            new ColorEntry { code=223, r=0xff, g=0xd7, b=0xaf },
            new ColorEntry { code=224, r=0xff, g=0xd7, b=0xd7 },
            new ColorEntry { code=225, r=0xff, g=0xd7, b=0xff },
            new ColorEntry { code=226, r=0xff, g=0xff, b=0x00 },
            new ColorEntry { code=227, r=0xff, g=0xff, b=0x5f },
            new ColorEntry { code=228, r=0xff, g=0xff, b=0x87 },
            new ColorEntry { code=229, r=0xff, g=0xff, b=0xaf },
            new ColorEntry { code=230, r=0xff, g=0xff, b=0xd7 },
            new ColorEntry { code=231, r=0xff, g=0xff, b=0xff },
            new ColorEntry { code=232, r=0x08, g=0x08, b=0x08 },
            new ColorEntry { code=233, r=0x12, g=0x12, b=0x12 },
            new ColorEntry { code=234, r=0x1c, g=0x1c, b=0x1c },
            new ColorEntry { code=235, r=0x26, g=0x26, b=0x26 },
            new ColorEntry { code=236, r=0x30, g=0x30, b=0x30 },
            new ColorEntry { code=237, r=0x3a, g=0x3a, b=0x3a },
            new ColorEntry { code=238, r=0x44, g=0x44, b=0x44 },
            new ColorEntry { code=239, r=0x4e, g=0x4e, b=0x4e },
            new ColorEntry { code=240, r=0x58, g=0x58, b=0x58 },
            new ColorEntry { code=241, r=0x62, g=0x62, b=0x62 },
            new ColorEntry { code=242, r=0x6c, g=0x6c, b=0x6c },
            new ColorEntry { code=243, r=0x76, g=0x76, b=0x76 },
            new ColorEntry { code=244, r=0x80, g=0x80, b=0x80 },
            new ColorEntry { code=245, r=0x8a, g=0x8a, b=0x8a },
            new ColorEntry { code=246, r=0x94, g=0x94, b=0x94 },
            new ColorEntry { code=247, r=0x9e, g=0x9e, b=0x9e },
            new ColorEntry { code=248, r=0xa8, g=0xa8, b=0xa8 },
            new ColorEntry { code=249, r=0xb2, g=0xb2, b=0xb2 },
            new ColorEntry { code=250, r=0xbc, g=0xbc, b=0xbc },
            new ColorEntry { code=251, r=0xc6, g=0xc6, b=0xc6 },
            new ColorEntry { code=252, r=0xd0, g=0xd0, b=0xd0 },
            new ColorEntry { code=253, r=0xda, g=0xda, b=0xda },
            new ColorEntry { code=254, r=0xe4, g=0xe4, b=0xe4 },
            new ColorEntry { code=255, r=0xee, g=0xee, b=0xee }
        };

        public int NearestColorIncrement(int c)
        {
            if (c < 47)
                return 0;
            if (c < 115)
                return 0x5f;
            if (c < 155)
                return 0x87;
            if (c < 195)
                return 0xaf;
            if (c < 235)
                return 0xd7;
            return 0xff;
        }

        // This is probably very slow and could use a cache or smarter general approach
        public int RGBtoXterm(int r, int g, int b)
        {
            foreach (ColorEntry c in ColorTable)
            {
                if ((r == c.r) && (g == c.g) && (b == c.b)) {
                    return c.code;
                }
            }

            // no exact match found? Try the next closest thing
            r = NearestColorIncrement(r);
            g = NearestColorIncrement(g);
            b = NearestColorIncrement(b);

            foreach (ColorEntry c in ColorTable)
            {
                if ((r == c.r) && (g == c.g) && (b == c.b)) {
                    return c.code;
                }
            }

            // failsafe
            return 255;
        }

        public int HSVtoXterm(double h, double s, double v)
        {
            int i;
            double f, p, q, t;
            if ( s <= 0.0 )
                return RGBtoXterm( (int)(v * 255),  (int)(v * 255),  (int)(v * 255));

            h /= 60;
            i = (int)Math.Floor(h);
            f = h - i;
            p = v * ( 1 - s );
            q = v * ( 1 - s * f );
            t = v * ( 1 - s * (1 - f) );

            if ( i == 0 )
                return RGBtoXterm( (int)(v * 255),  (int)(t * 255),  (int)(p * 255));
            if ( i == 1 )
                return RGBtoXterm( (int)(q * 255),  (int)(v * 255),  (int)(p * 255));
            if ( i == 2 )
                return RGBtoXterm( (int)(p * 255),  (int)(v * 255),  (int)(t * 255));
            if ( i == 3 )
                return RGBtoXterm( (int)(p * 255),  (int)(q * 255),  (int)(v * 255));
            if ( i == 4 )
                return RGBtoXterm( (int)(t * 255),  (int)(p * 255),  (int)(v * 255));

            return RGBtoXterm( (int)(v * 255),  (int)(p * 255),  (int)(q * 255));
        }


        public void ForegroundRGB(int r, int g, int b)
        {
            Console.Write(ESC + "[38;5;" + RGBtoXterm(r,g,b) + "m");
        }

        public void ForegroundHSV(double h, double s, double v)
        {
            Console.Write(ESC + "[38;5;" + HSVtoXterm(h,s,v) + "m");
        }

        public void ForegroundHSV(int h, double s, double v)
        {
            Console.Write(ESC + "[38;5;" + HSVtoXterm((double)h,s,v) + "m");
        }


        public void BackgroundRGB(int r, int g, int b)
        {
            Console.Write(ESC + "[48;5;" + RGBtoXterm(r,g,b) + "m");
        }

        public void BackgroundHSV(double h, double s, double v)
        {
            Console.Write(ESC + "[48;5;" + HSVtoXterm(h,s,v) + "m");
        }

        public void BackgroundHSV(int h, double s, double v)
        {
            Console.Write(ESC + "[48;5;" + HSVtoXterm((double)h,s,v) + "m");
        }

        public void ColorDemo()
        {
            for (double v = 0.1; v <= 1.0; v += 0.03) {
                for (int i = 0; i < 360; i += 5) {
                    BackgroundHSV(i, 1, v);
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
        }

        public override void ClearScreen()
        {
            Console.Write(ESC + "[2J");
        }

        public void UpdateMap()
        {
            ForegroundRGB(128,128,128);
            for (int row = map_y1; row <= map_y2; row++) {
                MoveCursor(row,map_x1);
                for (int col = map_x1; col <= map_x2; col++)
                    Console.Write("#");
            }
        }

        public void MoveCursor(int row, int col)
        {
            Console.Write(CursorPosition(row,col));
        }

        public override void DrawScreen()
        {
            ClearScreen();
            MoveCursor(1,1);
            for (int y = 0; y < term_h; y++)
            {
                for (int x = 0; x < term_w; x++)
                {
                    TerminalCharacter c = screenBuffer[y][x];
                    BackgroundRGB(c.br,c.bg,c.bb);
                    ForegroundRGB(c.r,c.g,c.b);
                    Console.Write(c.glyph);
                }
                Console.Write("\n");
            }
        }

        public VT100Display()
        {
        }
    }
}

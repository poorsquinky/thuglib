using System;
using System.Collections;
using System.Collections.Generic;

namespace ThugLib
{
    public class MathUtils
    {
        // based on the Wikipedia implementation; will reuse fillPath to
        // save allocations if possible.  Null fillPath is ok.
        public static void Swap(ref int a, ref int b)
        {
            int swap = a;
            a = b;
            b = swap;
        }
    }
}

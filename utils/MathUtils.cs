using System;
using System.Collections;
using System.Collections.Generic;

namespace ThugLib
{
    public class MathUtils
    {
        public static int RoundToInt(double d)
        {
            return (int)(d + 0.5);
        }

        public static void Swap(ref int a, ref int b)
        {
            int swap = a;
            a = b;
            b = swap;
        }

        public static void Swap(ref List<int> a, ref List<int> b)
        {
            List<int> swap = a;
            a = b;
            b = swap;
        }

        public static int Limit(int a, int low, int high)
        {
            if (a < low) a = low;
            if (a > high) a = high;
            return a;
        }
    }
}

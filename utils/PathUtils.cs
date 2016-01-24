using System;
using System.Collections;
using System.Collections.Generic;

namespace ThugLib
{
    public enum eStep : int
    {
        North,
        Northeast,
        East,
        Southeast,
        South,
        Southwest,
        West,
        Northwest
    };

    public class Path
    {
        public eStep[] Steps;

        public int NSteps; // may be < steps.Length

        public Path(eStep[] steps)
        {
            this.Steps = steps;
            this.NSteps = Steps.Length;
        }
    }

    public class PathUtils
    {
        public static int[] StepDX = new int[]
           {  0,  1,  1,  1,  0, -1, -1, -1 };
        public static int[] StepDY = new int[]
           {  1,  1,  0, -1, -1, -1,  0,  1 };

        // based on the Wikipedia implementation; will reuse fillPath to
        // save allocations if possible.  Null fillPath is ok.
        public static Path GetBresenhamPath(int x1, int y1, int x2, int y2,
           Path fillPath)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;

            // handle the degenerate case
            if (dx == 0 && dy == 0)
            {
                return new Path(new eStep[0]);
            }

            // move the problem to the first octant
            eStep diagonalStep, straightStep;
            if (dx < 0)
            {
                dx = -dx;
                if (dy < 0)
                {
                    dy = -dy;
                    if (dx >= dy)
                    {
                        diagonalStep = eStep.Southwest;
                        straightStep = eStep.West;
                    }
                    else
                    {
                        MathUtils.Swap(ref dx, ref dy);
                        diagonalStep = eStep.Southwest;
                        straightStep = eStep.South;
                    }
                }
                else
                {
                    if (dx >= dy)
                    {
                        diagonalStep = eStep.Northwest;
                        straightStep = eStep.West;
                    }
                    else
                    {
                        MathUtils.Swap(ref dx, ref dy);
                        diagonalStep = eStep.Northwest;
                        straightStep = eStep.North;
                    }
                }
            }
            else
            {
                if (dy < 0)
                {
                    dy = -dy;
                    if (dx >= dy)
                    {
                        diagonalStep = eStep.Southeast;
                        straightStep = eStep.East;
                    }
                    else
                    {
                        MathUtils.Swap(ref dx, ref dy);
                        diagonalStep = eStep.Southeast;
                        straightStep = eStep.South;
                    }
                }
                else
                {
                    if (dx >= dy)
                    {
                        diagonalStep = eStep.Northeast;
                        straightStep = eStep.East;
                    }
                    else
                    {
                        MathUtils.Swap(ref dx, ref dy);
                        diagonalStep = eStep.Northeast;
                        straightStep = eStep.North;
                    }
                }
            }

            // allocate results
            if (fillPath == null)
            {
                fillPath = new Path(new eStep[dx]);
            }
            else if (fillPath.Steps == null || fillPath.Steps.Length < dx)
            {
                fillPath.Steps = new eStep[dx];
                fillPath.NSteps = dx;
            }
            else
            {
                fillPath.NSteps = dx;
            }

            // run the first-octant Bresenham line algorithm
            int two_dy = dy + dy;
            int two_dx = dx + dx;
            int D = two_dy - dx;
            for (int x = 0; x < dx; x++)
            {
                if (D > 0)
                {
                    D -= two_dx;
                    fillPath.Steps[x] = diagonalStep;
                }
                else
                {
                    fillPath.Steps[x] = straightStep;
                }
                D += two_dy;
            }
            return fillPath;
        }

        public static void PrintPath(Path fillPath, int x1, int y1)
        {
            int x = x1, y = y1;
            Console.WriteLine("   " + x + ", " + y);
            for (int i = 0; i < fillPath.NSteps; i++) 
            {
                x += StepDX[(int)fillPath.Steps[i]];
                y += StepDY[(int)fillPath.Steps[i]];
                Console.WriteLine("   " + x + ", " + y);
            }
        }
    }
}

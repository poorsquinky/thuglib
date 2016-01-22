using System;
using System.Collections;
using System.Collections.Generic;


namespace ThugLib
{

    public class InterfaceMapDemo
    {
        static public void Main ()
        {
            //Interface d = new VT100Interface();
            VT100Interface d = new VT100Interface();

            // This is just a placeholder

            for (int x = 0; x < 80; x++)
            {
                d.DrawAt(x,3," ",0,0,0,0,0,64);
            }

            for (int x = 0; x < 80; x++)
            {
                d.DrawAt(x,22," ",0,0,0,0,0,64);
            }

            InterfaceMapElement map_element = d.MapElement(
                    x: 0,
                    y: 4,
                    h: 17
            );

            d.DrawAt(54,3," 5 targets in range    ",255,255,192,0,0,0);
            d.DrawAt(74,3,"[T] ",192,192,255,0,0,0);
            d.DrawAt(58,22," Press     for help ",255,255,192,0,0,0);
            d.DrawAt(65,22,"[?]",192,192,255,0,0,0);

            map_element.Draw();

            d.DrawScreen();
        }
    }
}


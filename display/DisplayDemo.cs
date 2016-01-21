using System;


namespace ThugLib
{
    public class DisplayDemo
    {
        static public void Main ()
        {
            //Display d = new VT100Display();
            VT100Display d = new VT100Display();

            // This is just a placeholder

            for (int x = 0; x < 80; x++)
            {
                d.DrawAt(x,3,"-",192,0,255);
            }

            for (int x = 0; x < 80; x++)
            {
                d.DrawAt(x,22,"-",192,0,255);
            }

            d.DrawAt(56,3,"5 targets in range    ",255,255,192,0,0,0);
            d.DrawAt(75,3,"[T]",192,192,255,0,0,0);
            d.DrawAt(60,22,"Press     for help",255,255,192,0,0,0);
            d.DrawAt(66,22,"[?]",192,192,255,0,0,0);

            d.DrawScreen();
        }
    }
}


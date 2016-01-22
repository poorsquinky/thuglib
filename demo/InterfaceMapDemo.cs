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

            ClearMapGenerator gen = new ClearMapGenerator(new int[] {0, 0},
               MapCoordinate.GenerateRandom());
            ClearMapGenerator gen2 = new ClearMapGenerator(new int[]{2, 1},
               MapCoordinate.GenerateRandom());
            gen2.Run(map_element.map, new MapRectangle(10, 10, 10, 10), null);
            List<MapRoom> blockedList = new List<MapRoom>();
            MapRectangle fullArea = new MapRectangle(0, 0, 40, 40);
            blockedList.Add(new MapRoom(new MapRectangle(10, 10, 10, 10)));
            gen.Run(map_element.map, fullArea, blockedList);
            CADecayMapGenerator cad = new CADecayMapGenerator(new int[] {0, 1},
               MapCoordinate.GenerateRandom(), 10, 20, 5, 10);
            // cad.UseCoordinateBasedRandom(); // add this to not get random caves
            cad.Run(map_element.map, fullArea, blockedList);
            CAGrowthMapGenerator cag1 = new CAGrowthMapGenerator(new int[]
               {3, 1}, MapCoordinate.GenerateRandom(), 50, 2, 20);
            cag1.Run(map_element.map, fullArea, blockedList);
            CAGrowthMapGenerator cag2 = new CAGrowthMapGenerator(new int[]
               {4, 1, 2}, MapCoordinate.GenerateRandom(), 200, 8, 20);
            cag2.Run(map_element.map, fullArea, blockedList);
            DungeonRoomMapGenerator drmg = new DungeonRoomMapGenerator(new int[]
               {5, 6, 7}, MapCoordinate.GenerateRandom(), 5, 12, 20, 4);
            drmg.Run(map_element.map, fullArea, blockedList);


            d.DrawAt(54,3," 5 targets in range    ",255,255,192,0,0,0);
            d.DrawAt(74,3,"[T] ",192,192,255,0,0,0);
            d.DrawAt(58,22," Press     for help ",255,255,192,0,0,0);
            d.DrawAt(65,22,"[?]",192,192,255,0,0,0);

            map_element.Draw();

            d.DrawScreen();
        }
    }
}


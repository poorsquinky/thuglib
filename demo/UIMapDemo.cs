using System;
using System.Collections;
using System.Collections.Generic;


namespace ThugLib
{

    public class UIMapDemo
    {
        static public void Main ()
        {
            //UI d = new VT100UI();
            VT100UI d = new VT100UI();

            // This is just a placeholder

            for (int x = 0; x < 80; x++)
            {
                d.DrawAt(x,3," ",0,0,0,0,0,64);
            }

            for (int x = 0; x < 80; x++)
            {
                d.DrawAt(x,22," ",0,0,0,0,0,64);
            }

            UIMapElement map_element = d.MapElement(
                    x: 0,
                    y: 4,
                    h: 17
            );

            ClearMapGenerator gen = new ClearMapGenerator(new int[] {0, 0},
               MapCoordinate.GenerateRandom());

            map_element.map = new MapData(80,40);
            ClearMapGenerator gen2 = new ClearMapGenerator(new int[]{2, 1},
               MapCoordinate.GenerateRandom());
            gen2.Run(map_element.map.grid, new MapRectangle(10, 10, 10, 10), null);
            List<MapRoom> blockedList = new List<MapRoom>();
            MapRectangle fullArea = new MapRectangle(0, 0, 80, 40);
            blockedList.Add(new MapRoom(new MapRectangle(10, 10, 10, 10)));
            gen.Run(map_element.map.grid, fullArea, blockedList);

            DungeonRoomMapGenerator drmg = new DungeonRoomMapGenerator(new int[]
               {5, 6, 7}, MapCoordinate.GenerateRandom(), 5, 12, 10, 3);
            List<MapRoom> allRooms = drmg.Run(map_element.map.grid, fullArea, blockedList);

            DungeonCorridorMapGenerator dcmg = new DungeonCorridorMapGenerator(
               new int[] {5, 6, 7}, MapCoordinate.GenerateRandom(), 2,
               new int[] {0, 100000, 100000, 0, 0, 100000, 0, 0});
            dcmg.Run(map_element.map.grid, fullArea, allRooms);

            map_element.map.AddSpaceType(glyph: '#', r: 128, g: 128, b: 128);
            map_element.map.AddSpaceType(glyph: '#', r: 128, g: 128, b: 128);
            map_element.map.AddSpaceType(glyph: '#', r: 128, g: 128, b: 128);
            map_element.map.AddSpaceType(glyph: '*');
            map_element.map.AddSpaceType(glyph: '~', r:   0, g:  32, b: 255);
            map_element.map.AddSpaceType(glyph: '#', r: 128, g: 128, b: 128);
            map_element.map.AddSpaceType(glyph: '.', r:  64, g:  64, b:  64);
            map_element.map.AddSpaceType(glyph: '+', r: 128, g:  64, b:   0);

            d.DrawAt(54,3," 5 targets in range    ",255,255,192,0,0,0);
            d.DrawAt(74,3,"[T] ",192,192,255,0,0,0);
            d.DrawAt(58,22," Press     for help ",255,255,192,0,0,0);
            d.DrawAt(65,22,"[?]",192,192,255,0,0,0);

            map_element.Draw();

            d.DrawScreen();
        }
    }
}


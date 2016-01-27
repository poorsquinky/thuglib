﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ThugLib;

public class TestLevelManagerScript : MonoBehaviour {

    public int levelWidth=40;
    public int levelHeight=40;

    public GameObject playerPrefab;
    public GameObject cameraPrefab;

    public List<GameObject> tilePrefabs;

    private List<List<Object>> tileGrid = new List<List<Object>>();
    private GameObject player;

    public MapData mapdata;

    // Use this for initialization
    void Start () {

        int player_x = 0;
        int player_y = 0;

        mapdata = new MapData(levelWidth,levelHeight);

        ClearMapGenerator gen = new ClearMapGenerator(new int[] {0, 0},
           MapCoordinate.GenerateRandom());

        ClearMapGenerator gen2 = new ClearMapGenerator(new int[] {2, 1},
           MapCoordinate.GenerateRandom());
        gen2.Run(mapdata.grid, new MapRectangle(10, 10, 10, 10), null);
        List<MapRoom> blockedList = new List<MapRoom>();
        MapRectangle fullArea = new MapRectangle(0, 0, 40, 40);
        blockedList.Add(new MapRoom(new MapRectangle(10, 10, 10, 10)));
        gen.Run(mapdata.grid, fullArea, blockedList);

        DungeonRoomMapGenerator drmg = new DungeonRoomMapGenerator(new
           int[] {5, 6, 7}, MapCoordinate.GenerateRandom(),
           5, 12, 10, 3);
        List<MapRoom> allRooms = drmg.Run(mapdata.grid, fullArea,
           blockedList);
        DungeonCorridorMapGenerator dcmg = new
           DungeonCorridorMapGenerator(
           new int[] {5, 6, 7}, MapCoordinate.GenerateRandom(), 2,
           new int[] {0, 100000, 100000, 0, 0, 100000, 0, 0});
        dcmg.Run(mapdata.grid, fullArea, allRooms);

        mapdata.AddSpaceType(glyph: '#', passable: false);
        mapdata.AddSpaceType(glyph: '#', passable: false);
        mapdata.AddSpaceType(glyph: '#', passable: false);
        mapdata.AddSpaceType(glyph: '*', passable: true);
        mapdata.AddSpaceType(glyph: '~', passable: true);
        mapdata.AddSpaceType(glyph: '#', passable: false);
        mapdata.AddSpaceType(glyph: ' ', passable: true);
        mapdata.AddSpaceType(glyph: '+', passable: true);

        bool[][] visibility_map = new bool[levelHeight][];
        for (int i = 0; i < levelWidth; i++)
        {
            visibility_map[i] = new bool[levelWidth];
            for (int j = 0; j < levelHeight; j++)
            {
                visibility_map[i][j] = false;
                bool keepLooping = true;
                for (int x = i-1; (keepLooping == true) && (x <= i+1); x++)
                {
                    for (int y = j-1; (keepLooping == true) && (y <= j+1); y++)
                    {
                        if ( (x >= 0) && (x < levelWidth) && (y >= 0) && (y < levelHeight) && (mapdata.palette[mapdata.grid[x][y]].passable) )
                        {
                            keepLooping = false;
                            visibility_map[i][j] = true;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < levelHeight; i++)
        {
            tileGrid.Add(new List<Object>());
            for (int j = 0; j < levelWidth; j++)
            {

                // XXX this is profoundly dumb -- we're just assigning the player coords to the last passable square.  But it's temporary.
                if (mapdata.palette[mapdata.grid[j][i]].passable)
                {
                    player_x = j;
                    player_y = i;
                }

                // special case for doors :(  -- they need a floor.
                if (mapdata.grid[j][i] == 7)
                {
                    var flr = Instantiate(this.tilePrefabs[6]) as GameObject;
                    flr.transform.position = new Vector3(j, i, 1);
                }

                var o = Instantiate(this.tilePrefabs[mapdata.grid[j][i]]) as GameObject;
                o.transform.position = new Vector3(j, i, 0);
                var sts = o.GetComponent<ShapeTerrainScript>();
                if (sts)
                {
                    int surroundNum = 0;
                    // 1: north
                    if ( (i < 39) && (visibility_map[j][i+1]) && (! mapdata.palette[mapdata.grid[j][i+1]].passable) )
                    {
                            surroundNum += 1;
                    }
                    // 2: east
                    if ( (j < 39) && (visibility_map[j+1][i]) && (! mapdata.palette[mapdata.grid[j+1][i]].passable) )
                    {
                        surroundNum += 2;
                    }
                    // 4: south
                    if ( (i > 0) && (visibility_map[j][i-1]) && (! mapdata.palette[mapdata.grid[j][i-1]].passable) )
                    {
                            surroundNum += 4;
                    }
                    // 8: west
                    if ( (j > 0) && (visibility_map[j-1][i]) && (! mapdata.palette[mapdata.grid[j-1][i]].passable) )
                    {
                            surroundNum += 8;
                    }
                    sts.SetSprite(surroundNum);
                    if (! visibility_map[j][i])
                        o.GetComponent<SpriteRenderer>().enabled = false;
                }
                tileGrid[i].Add(o);
            }
        }

        this.player = Instantiate(this.playerPrefab) as GameObject;
        Vector3 pos = new Vector3(player_x, player_y, 0);
        this.player.transform.position = pos;
        this.player.transform.SetParent(transform);

        GameObject camera = Instantiate( cameraPrefab );
        pos.z=-10;
        camera.transform.position = pos;
        camera.transform.SetParent(this.player.transform);

    }

    // Update is called once per frame
    void Update () {

    }
}

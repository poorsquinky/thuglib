using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private bool moving = false;
    private Vector3 movingFrom = new Vector3(0,0,0);
    private Vector3 movingTo   = new Vector3(0,0,0);

    private long keyDownHoldStart = 0L; // in 100-ns ticks
    private const long keyDownTimeToRepeatInMS = 200;
    private const long keyDownRepeatIntervalInMS = 100;
    private const long TICKS_PER_MS = 10000;
    private bool keyIsDown = false;
    private bool keyIsRepeating = false;
    private int nKeyRepeats = 0;
        
        private void StartKeyDown()
        {
            keyDownHoldStart = Environment.TickCount;
            keyIsDown = true;
            nKeyRepeats = 0;
        }

    // Use this for initialization
    void Start () {

    }

    void Update()
    {
        if (!moving)
        {
            // float h = Input.GetAxis("Horizontal");
            // float v = Input.GetAxis("Vertical");
            float h = 0, v = 0;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                v = 1;
                StartKeyDown();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                v = -1;
                StartKeyDown();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                h = -1;
                StartKeyDown();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                h = 1;
                StartKeyDown();
            }
            else if (keyIsDown)
            {
                // check for hold
                float possible_h = 0, possible_v = 0;
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    possible_h = -1;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    possible_h = 1;
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    possible_v = 1;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    possible_v = -1;
                }
                if (possible_h != 0 || possible_v != 0)
                {
                    long currentTick = Environment.TickCount;
                    if (!keyIsRepeating &&
                       currentTick - keyDownHoldStart > keyDownTimeToRepeatInMS)
                    {
                        keyIsRepeating = true;
                        nKeyRepeats = 1;
                        h = possible_h;
                        v = possible_v;
                    }
                    else if (keyIsRepeating &&
                       currentTick - keyDownHoldStart > keyDownTimeToRepeatInMS
                       + nKeyRepeats * keyDownRepeatIntervalInMS)
                    {
                        nKeyRepeats++;
                        h = possible_h;
                        v = possible_v;
                    }
                }
                else
                {
                    keyIsDown = false;
                    keyIsRepeating = false;
                    nKeyRepeats = 0;
                }
            }

            movingFrom = transform.position;
            movingTo   = movingFrom;

            int x = (int)movingFrom.x;
            int y = (int)movingFrom.y;

            TestLevelManagerScript lm = transform.parent.GetComponent<TestLevelManagerScript>();

            if ((h < 0.0f) && (x > 0) && (lm.mapdata.palette[lm.mapdata.grid[x-1][y]].passable))
            {
                movingTo.x -= 1;
            }
            else if ((h > 0.0f) && (x < lm.levelWidth) &&  (lm.mapdata.palette[lm.mapdata.grid[x+1][y]].passable))
            {
                movingTo.x += 1;
            }

            if ((v < 0.0f) && (y > 0) &&  (lm.mapdata.palette[lm.mapdata.grid[x][y-1]].passable))
            {
                movingTo.y -= 1;
            }
            else if ((v > 0.0f) && (y < lm.levelHeight) &&  (lm.mapdata.palette[lm.mapdata.grid[x][y+1]].passable))
            {
                movingTo.y += 1;
            }


            transform.position = movingTo;

        }
    }

    // Update is called once per frame
    void FixedUpdate () {

    }
}

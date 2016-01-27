using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private bool moving = false;
    private Vector3 movingFrom = new Vector3(0,0,0);
    private Vector3 movingTo   = new Vector3(0,0,0);

    // Use this for initialization
    void Start () {

    }

    void FixedUpdate()
    {
        if (!moving)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

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
    void Update () {

    }
}

using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private bool moving = false;
    private Vector3 movingFrom = new Vector3(0,0,0);
    private Vector3 movingTo   = new Vector3(0,0,0);

    private float smooth = 15;

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

            int x = (int)movingTo.x;
            int y = (int)movingTo.y;

            TestLevelManagerScript lm = transform.parent.GetComponent<TestLevelManagerScript>();

            if ((h < 0.0f) && (x > 0) && (lm.mapdata.palette[lm.mapdata.grid[x-1][y]].passable))
            {
                movingTo.x -= 1;
                x -= 1;
            }
            else if ((h > 0.0f) && (x < lm.levelWidth) &&  (lm.mapdata.palette[lm.mapdata.grid[x+1][y]].passable))
            {
                movingTo.x += 1;
                x += 1;
            }

            if ((v < 0.0f) && (y > 0) &&  (lm.mapdata.palette[lm.mapdata.grid[x][y-1]].passable))
            {
                movingTo.y -= 1;
            }
            else if ((v > 0.0f) && (y < lm.levelHeight) &&  (lm.mapdata.palette[lm.mapdata.grid[x][y+1]].passable))
            {
                movingTo.y += 1;
            }
            if (transform.position != movingTo)
                moving = true;
        }
        else
        {
            Vector3 newPos = transform.position;
            newPos.x = Mathf.Lerp(transform.position.x,movingTo.x,Time.deltaTime*this.smooth);
            newPos.y = Mathf.Lerp(transform.position.y,movingTo.y,Time.deltaTime*this.smooth);
            transform.position = newPos;
            float dist = Vector3.Distance(transform.position, movingTo);
            if (dist < 0.01)
            {
                transform.position = movingTo;
                moving = false;
            }

        }
    }

    // Update is called once per frame
    void Update () {

    }
}

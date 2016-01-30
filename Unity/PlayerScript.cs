using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private bool moving = false;
    private Vector3 movingFrom = new Vector3(0,0,0);
    private Vector3 movingTo   = new Vector3(0,0,0);

    private float smooth = 15;
    private double moveDelay = 0.1;
    private double currentMoveDelay = 0;

    // Use this for initialization
    void Start () {

    }

    void FixedUpdate()
    {
        if (!moving)
        {
            if (currentMoveDelay > 0)
            {
                currentMoveDelay -= Time.deltaTime;
                if (currentMoveDelay < 0)
                    currentMoveDelay = 0;
            }
            else
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

                movingFrom = transform.position;
                movingTo   = movingFrom;

                int x = (int)movingTo.x;
                int y = (int)movingTo.y;

                TestLevelManagerScript lm = transform.parent.GetComponent<TestLevelManagerScript>();

                if (h < 0.0f)
                    x -= 1;
                else if (h > 0.0f)
                    x += 1;

                if (v < 0.0f)
                    y -= 1;
                else if (v > 0.0f)
                    y += 1;

                if (
                        (x > 0) &&
                        (x < lm.levelWidth) &&
                        (y > 0) &&
                        (y < lm.levelHeight) &&
                        (lm.mapdata.palette[lm.mapdata.grid[x][y]].passable)
                   )
                {
                    movingTo.x = x;
                    movingTo.y = y;
                }

                if (transform.position != movingTo)
                    moving = true;
            }
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
                currentMoveDelay = moveDelay;
            }

        }
    }

    // Update is called once per frame
    void Update () {

    }
}

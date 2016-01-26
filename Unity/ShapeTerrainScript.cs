using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShapeTerrainScript : MonoBehaviour {

    public List<Sprite> spriteList;

    public void SetSprite(int spritenum)
    {
        if (spriteList.Count > spritenum)
        {
            GetComponent<SpriteRenderer>().sprite = spriteList[spritenum];
        }
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}

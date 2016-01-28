using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Transform target = null;
    private float smooth = 5;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        if (target)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x,target.position.x,Time.deltaTime*this.smooth), Mathf.Lerp(transform.position.y,target.position.y,Time.deltaTime*this.smooth), -100);
        }

    }
}

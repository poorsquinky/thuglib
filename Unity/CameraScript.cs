using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Transform target = null;
    private float smooth = 5;
    private Vector3 virtual_position = new Vector3(0,0,0);

    // Use this for initialization
    void Start () {
        virtual_position = transform.position;
    }

    // Update is called once per frame
    void Update () {

        if (target)
        {
            virtual_position = new Vector3(Mathf.Lerp(virtual_position.x,target.position.x,Time.deltaTime*this.smooth), Mathf.Lerp(virtual_position.y,target.position.y,Time.deltaTime*this.smooth), -100);
            Vector3 new_pos = transform.position;
            new_pos.x = Mathf.Round(virtual_position.x * 16) / 16;
            new_pos.y = Mathf.Round(virtual_position.y * 16) / 16;
            transform.position = new_pos;
        }

    }
}

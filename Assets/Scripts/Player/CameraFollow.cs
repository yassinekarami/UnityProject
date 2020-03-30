using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.offset = transform.position - this.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = this.offset + this.player.transform.position;
    }
}

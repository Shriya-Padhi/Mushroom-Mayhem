using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    public Vector3 Z_offset;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Z_offset = new Vector3(0, 0, -20);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + Z_offset;
    }
}

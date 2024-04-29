using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Camera;
    public Transform target;
    public Vector3 startpoint;
    public static Transform Playertransform;
    // Start is called before the first frame update
    void Start()
    {
        CameraController.Camera = this;
        //Player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
        target = PlayerScript.player.transform;
        target.position = startpoint;
        DontDestroyOnLoad(this);
    }
    

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = target.position.x;
        pos.y = target.position.y;
        transform.position = pos;
    }
}
